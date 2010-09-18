﻿#region License
//  Copyright 2004-2010 Castle Project - http:www.castleproject.org/
//  
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//  
//      http:www.apache.org/licenses/LICENSE-2.0
//  
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
// 
#endregion

using DslModeling = global::Microsoft.VisualStudio.Modeling;
using DslValidation = global::Microsoft.VisualStudio.Modeling.Validation;
using DslDiagrams = global::Microsoft.VisualStudio.Modeling.Diagrams;

namespace Castle.ActiveWriter
{
	using System.Linq;

	partial class ActiveWriterDomainModel
	{
		///<Summary>
		/// Provide an implementation of the partial method to set up the serialization behavior for this model.
		///</Summary>
		///<remarks>
		/// This partial method is called from the constructor of the domain class.
		///</remarks>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance","CA1822:MarkMembersAsStatic", Justification="Alternative implementations might need to reference instance variables, so not marked as static.")]
		partial void InitializeSerialization(DslModeling::Store store)
		{
			// Register the serializers and moniker resolver for this model
			ActiveWriterSerializationHelper.Instance.InitializeSerialization(store);	
		}
	}
	
	
	/// <summary>
	/// Helper class for serializing and deserializing ActiveWriter models.
	/// </summary>
	public abstract partial class ActiveWriterSerializationHelperBase
	{
		#region Constructor
		/// <summary>
		/// Constructor
		/// </summary>
		protected ActiveWriterSerializationHelperBase() { }
		#endregion
		
		#region Methods
		
		/// <summary>
		/// Ensure that moniker resolvers and domain element serializers are installed properly on the given store, 
		/// so that deserialization can be carried out correctly.
		/// </summary>
		/// <param name="store">Store on which moniker resolvers will be set up.</param>
		internal protected virtual void InitializeSerialization(DslModeling::Store store)
		{
			#region Check Parameters
			global::System.Diagnostics.Debug.Assert(store != null);
			if (store == null)
				throw new global::System.ArgumentNullException("store");
			#endregion
	
			DslModeling::DomainXmlSerializerDirectory directory = GetDirectory(store);
	
			// Register the moniker resolver for this model, unless one is already registered
			DslModeling::IMonikerResolver monikerResolver = store.FindMonikerResolver(ActiveWriterDomainModel.DomainModelId);
			if (monikerResolver == null)
			{
				monikerResolver = new ActiveWriterSerializationBehaviorMonikerResolver(store, directory);
				store.AddMonikerResolver(ActiveWriterDomainModel.DomainModelId, monikerResolver);
			}
			
			// Add serialization behaviors
			directory.AddBehavior(ActiveWriterSerializationBehavior.Instance);
		}
	
		/// <Summary>
		/// Called by the serialization helper to allow any necessary setup to be done on each load / save.
		/// </Summary>
		/// <param name="partition">The partition being serialized.</param>
		/// <param name="serializationContext">The current serialization context instance.</param>
		/// <param name="isLoading">Flag to indicate whether the file is being loaded or saved.</param>
		/// <Remarks>The base implementation does nothing</Remarks>
		protected virtual void InitializeSerializationContext(DslModeling::Partition partition, DslModeling::SerializationContext serializationContext, bool isLoading)
		{
		}
	
		/// <summary>
		/// Return the directory of serializers to use
		/// </summary>
		protected virtual DslModeling::DomainXmlSerializerDirectory GetDirectory(DslModeling::Store store)
		{
			// Just return the default serialization directory from the store
			return store.SerializerDirectory;
		}
			
		/// <summary>
		/// This method returns the moniker resolvers for each of the domain models in the store
		/// </summary>
		/// <param name="store">Store on which the moniker resolvers are set up.</param>
		internal protected virtual global::System.Collections.Generic.IDictionary<global::System.Guid, DslModeling::IMonikerResolver> GetMonikerResolvers(DslModeling::Store store)
		{
			#region Check Parameters
			global::System.Diagnostics.Debug.Assert(store != null);
			if (store == null)
				throw new global::System.ArgumentNullException("store");
			#endregion
			
			global::System.Collections.Generic.Dictionary<global::System.Guid, DslModeling::IMonikerResolver> result = new global::System.Collections.Generic.Dictionary<global::System.Guid, DslModeling::IMonikerResolver>();
			foreach (DslModeling::DomainModelInfo modelInfo in store.DomainDataDirectory.DomainModels)
			{
				if (modelInfo.MonikerResolver != null)
				{
					result.Add(modelInfo.Id, modelInfo.MonikerResolver);
				}
			}
			
			return result;
		}
	
		/// <summary>
		/// Write extension element data inside the current XML element
		/// </summary>
		/// <param name="serializationContext">The current serialization context instance.</param>
		/// <param name="element">The element whose attributes have just been written.</param>
		/// <param name="writer">XmlWriter to write serialized data to.</param>
		/// <remarks>The default implemenation is to write out all non-embedded extension elements,
		/// regardless of whether they relate to the current element or not.
		/// The additional data should be written as a series of one or more
		/// XML elements.</remarks>
		internal protected virtual void WriteExtensions(DslModeling::SerializationContext serializationContext, DslModeling::ModelElement element, global::System.Xml.XmlWriter writer)
		{
			if (serializationContext == null)
			{
				throw new global::System.ArgumentNullException("serializationContext");
			}
			if (element == null)
			{
				throw new global::System.ArgumentNullException("element");
			}
			if (writer == null)
			{
				throw new global::System.ArgumentNullException("writer");
			}
	
			// Build a list of extension elements to serialize
			global::System.Collections.ObjectModel.ReadOnlyCollection<DslModeling::ModelElement> allExtensionElements = element.Partition.ElementDirectory.FindElements(DslModeling::ExtensionElement.DomainClassId, true);
			global::System.Collections.Generic.IEnumerable<DslModeling::ExtensionElement> nonEmbeddedExtensionsElements = allExtensionElements.Where(e => DslModeling::DomainClassInfo.FindEmbeddingElementLink(e) == null).OfType<DslModeling::ExtensionElement>();
	
			DslModeling::SerializationUtilities.WriteExtensions(serializationContext, writer, nonEmbeddedExtensionsElements);
		}
	
		/// <summary>
		/// Read any extension data written inside this XML element
		/// </summary>
		/// <param name="serializationContext">The current serialization context instance.</param>
		/// <param name="element">In-memory ModelElement instance that is currently being read.</param>
		/// <param name="reader">Reader for the file being read. The reader is positioned after the attributes of the specified element.</param>
		/// <remarks>The method reads any extension element data, regardless of whether it relates the current
		/// element or not. There may be no additional data for the specified element.</remarks>
		internal protected virtual void ReadExtensions(DslModeling::SerializationContext serializationContext, DslModeling::ModelElement element, global::System.Xml.XmlReader reader)
		{
			if (serializationContext == null)
			{
				throw new global::System.ArgumentNullException("serializationContext");
			}
			if (element == null)
			{
				throw new global::System.ArgumentNullException("element");
			}
			if (reader == null)
			{
				throw new global::System.ArgumentNullException("reader");
			}
	
			if (string.CompareOrdinal(reader.LocalName, DslModeling::SerializationUtilities.ExtensionsXmlElementName) == 0)
			{
				DslModeling::SerializationUtilities.ReadExtensions(serializationContext, reader, element.Partition);
			}
		}
		
		/// <summary>
		/// Writes the specified attribute to the file.
		/// </summary>
		/// <param name="serializationContext">The current serialization context instance.</param>
		/// <param name="element">The element whose attributes have just been written.</param>
		/// <param name="writer">XmlWriter to write serialized data to.</param>
		/// <param name="attributeName">Name of the attribute to be written</param>
		/// <param name="attributeValue">Value of the attribute to be written</param>
		/// <remarks>This is an extension point to allow customisation e.g. to encode the data
		/// being written to the file.</remarks>
		internal virtual void WriteAttributeString(DslModeling::SerializationContext serializationContext, DslModeling::ModelElement element, global::System.Xml.XmlWriter writer, string attributeName, string attributeValue)
		{
			writer.WriteAttributeString(attributeName, attributeValue);
		}
	
		/// <summary>
		/// Writes the specified element to the file.
		/// </summary>
		/// <param name="serializationContext">The current serialization context instance.</param>
		/// <param name="element">The element whose attributes have just been written.</param>
		/// <param name="writer">XmlWriter to write serialized data to.</param>
		/// <param name="elementName">Name of the element to be written.</param>
		/// <param name="elementValue">Value of the element to be written.</param>
		/// <remarks>This is an extension point to allow customisation e.g. to encode the data
		/// being written to the file.</remarks>
		internal virtual void WriteElementString(DslModeling::SerializationContext serializationContext, DslModeling::ModelElement element, global::System.Xml.XmlWriter writer, string elementName, string elementValue)
		{
			writer.WriteElementString(elementName, elementValue);
		}
	
		/// <summary>
		/// Reads and returns the value of an attribute.
		/// </summary>
		/// <param name="serializationContext">The current serialization context instance.</param>
		/// <param name="element">The element whose attributes have just been written.</param>
		/// <param name="reader">XmlReader to read the serialized data from.</param>
		/// <param name="attributeName">The name of the attribute to be read.</param>
		/// <returns>The value of the attribute.</returns>
		/// <remarks>This is an extension point to allow customisation e.g. to decode the data
		/// being written to the file.</remarks>
		internal virtual string ReadAttribute(DslModeling::SerializationContext serializationContext, DslModeling::ModelElement element, global::System.Xml.XmlReader reader, string attributeName)
		{
			return reader.GetAttribute(attributeName);
		}
	
		/// <summary>
		/// Reads and returns the value of an element.
		/// </summary>
		/// <param name="serializationContext">The current serialization context instance.</param>
		/// <param name="element">The element whose attributes have just been written.</param>
		/// <param name="reader">XmlReader to read the serialized data from.</param>
		/// <returns>The value of the element.</returns>
		/// <remarks>This is an extension point to allow customisation e.g. to decode the data
		/// being written to the file.</remarks>
		internal virtual string ReadElementContentAsString(DslModeling::SerializationContext serializationContext, DslModeling::ModelElement element, global::System.Xml.XmlReader reader)
		{
			return reader.ReadElementContentAsString();
		}
	
		/// <summary>
		/// Creates and returns the settings used when reading a file.
		/// </summary>
		/// <param name="serializationContext">The current serialization context instance.</param>
		/// <param name="isDiagram">Indicates whether a diagram or model file is currently being serialized.</param>
		internal virtual global::System.Xml.XmlReaderSettings CreateXmlReaderSettings(DslModeling::SerializationContext serializationContext, bool isDiagram)
		{
			return new global::System.Xml.XmlReaderSettings();
		}
	
		/// <summary>
		/// Creates and returns the settings used when writing a file.
		/// </summary>
		/// <param name="serializationContext">The current serialization context instance.</param>
		/// <param name="isDiagram">Indicates whether a diagram or model file is currently being serialized.</param>
		/// <param name="encoding">The encoding to use when writing the file.</param>
		internal virtual global::System.Xml.XmlWriterSettings CreateXmlWriterSettings(DslModeling::SerializationContext serializationContext, bool isDiagram, global::System.Text.Encoding encoding)
		{
			global::System.Xml.XmlWriterSettings settings = new global::System.Xml.XmlWriterSettings();
			settings.Indent = true;
			settings.Encoding = encoding;
	
			return settings;
		}
		
		#endregion
	}
	
	/// <summary>
	/// Helper class for serializing and deserializing ActiveWriter models.
	/// </summary>
	public sealed partial class ActiveWriterSerializationHelper : ActiveWriterSerializationHelperBase
	{
		#region Constructor
		/// <summary>
		/// Private constructor to prevent direct instantiation.
		/// </summary>
		private ActiveWriterSerializationHelper() : base () { }
		#endregion
		
		#region Singleton Instance
		/// <summary>
		/// Singleton instance.
		/// </summary>
		private static ActiveWriterSerializationHelper instance;
		/// <summary>
		/// Singleton instance.
		/// </summary>
		[global::System.Diagnostics.DebuggerBrowsable(global::System.Diagnostics.DebuggerBrowsableState.Never)] // Will trigger creation otherwise.
		public static ActiveWriterSerializationHelper Instance
		{
			[global::System.Diagnostics.DebuggerStepThrough]
			get
			{
				if (ActiveWriterSerializationHelper.instance == null)
					ActiveWriterSerializationHelper.instance = new ActiveWriterSerializationHelper();
				return ActiveWriterSerializationHelper.instance;
			}
		}
		#endregion
	}
	
}


namespace Castle.ActiveWriter
{
	using System.Linq;

	partial class ActiveWriterSerializationHelperBase
	{
	
		/// <summary>
		/// Loads a Model instance into the default partition of the given store, and ignore serialization result.
		/// </summary>
		/// <param name="store">The new Model instance will be created into the default partition of this store.</param>
		/// <param name="fileName">Name of the file from which the Model instance will be deserialized.</param>
		/// <param name="schemaResolver">
		/// An ISchemaResolver that allows the serializer to do schema validation on the root element (and everything inside it).
		/// If null is passed, schema validation will not be performed.
		/// </param>
		/// <param name="validationController">
		/// A ValidationController that will be used to do load-time validation (validations with validation category "Load"). If null
		/// is passed, load-time validation will not be performed.
		/// </param>
		/// <param name="serializerLocator">
		/// An ISerializerLocator that will be used to locate any additional domain model types required to load the model. Can be null.
		/// </param>
		/// <returns>The loaded Model instance.</returns>
		public virtual Model LoadModel(DslModeling::Store store, string fileName, DslModeling::ISchemaResolver schemaResolver, DslValidation::ValidationController validationController, DslModeling::ISerializerLocator serializerLocator)
		{
			#region Check Parameters
			if (store == null) 
				throw new global::System.ArgumentNullException("store");
			#endregion
			
			return LoadModel(new DslModeling::SerializationResult(), store.DefaultPartition, fileName, schemaResolver, validationController, serializerLocator);
		}
		
		/// <summary>
		/// Loads a Model instance into the default partition of the given store.
		/// </summary>
		/// <param name="serializationResult">Stores serialization result from the load operation.</param>
		/// <param name="store">The new Model instance will be created into the default partition of this store.</param>
		/// <param name="fileName">Name of the file from which the Model instance will be deserialized.</param>
		/// <param name="schemaResolver">
		/// An ISchemaResolver that allows the serializer to do schema validation on the root element (and everything inside it).
		/// If null is passed, schema validation will not be performed.
		/// </param>
		/// <param name="validationController">
		/// A ValidationController that will be used to do load-time validation (validations with validation category "Load"). If null
		/// is passed, load-time validation will not be performed.
		/// </param>
		/// <param name="serializerLocator">
		/// An ISerializerLocator that will be used to locate any additional domain model types required to load the model. Can be null.
		/// </param>
		/// <returns>The loaded Model instance.</returns>
		public virtual Model LoadModel(DslModeling::SerializationResult serializationResult, DslModeling::Store store, string fileName, DslModeling::ISchemaResolver schemaResolver, DslValidation::ValidationController validationController, DslModeling::ISerializerLocator serializerLocator)
		{
			#region Check Parameters
			if (store == null) 
				throw new global::System.ArgumentNullException("store");
			#endregion
			
			return LoadModel(serializationResult, store.DefaultPartition, fileName, schemaResolver, validationController, serializerLocator);
		}
	
		/// <summary>
		/// Loads a Model instance.
		/// </summary>
		/// <param name="serializationResult">Stores serialization result from the load operation.</param>
		/// <param name="partition">Partition in which the new Model instance will be created.</param>
		/// <param name="fileName">Name of the file from which the Model instance will be deserialized.</param>
		/// <param name="schemaResolver">
		/// An ISchemaResolver that allows the serializer to do schema validation on the root element (and everything inside it).
		/// If null is passed, schema validation will not be performed.
		/// </param>
		/// <param name="validationController">
		/// A ValidationController that will be used to do load-time validation (validations with validation category "Load"). If null
		/// is passed, load-time validation will not be performed.
		/// </param>
		/// <param name="serializerLocator">
		/// An ISerializerLocator that will be used to locate any additional domain model types required to load the model. Can be null.
		/// </param>
		/// <returns>The loaded Model instance.</returns>
		[global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability","CA1506:AvoidExcessiveClassCoupling", Justification="Generated code")]
		public virtual Model LoadModel(DslModeling::SerializationResult serializationResult, DslModeling::Partition partition, string fileName, DslModeling::ISchemaResolver schemaResolver, DslValidation::ValidationController validationController, DslModeling::ISerializerLocator serializerLocator)
		{
			#region Check Parameters
			if (serializationResult == null)
				throw new global::System.ArgumentNullException("serializationResult");
			if (partition == null)
				throw new global::System.ArgumentNullException("partition");
			if (string.IsNullOrEmpty(fileName))
				throw new global::System.ArgumentNullException("fileName");
			#endregion
		
			// Ensure there is a transaction for this model to Load in.
			if (!partition.Store.TransactionActive)
			{
				throw new global::System.InvalidOperationException(ActiveWriterDomainModel.SingletonResourceManager.GetString("MissingTransaction"));
			}
			
			Model modelRoot = null;
			DslModeling::DomainXmlSerializerDirectory directory = GetDirectory(partition.Store);
			DslModeling::DomainClassXmlSerializer modelRootSerializer = directory.GetSerializer(Model.DomainClassId);
			global::System.Diagnostics.Debug.Assert(modelRootSerializer != null, "Cannot find serializer for Model!");
			if (modelRootSerializer != null)
			{
				using (global::System.IO.FileStream fileStream = global::System.IO.File.OpenRead(fileName))
				{
					DslModeling::SerializationContext serializationContext = new DslModeling::SerializationContext(directory, fileStream.Name, serializationResult);
					InitializeSerializationContext(partition, serializationContext, true);
					DslModeling::TransactionContext transactionContext = new DslModeling::TransactionContext();
					transactionContext.Add(DslModeling::SerializationContext.TransactionContextKey, serializationContext);
					using (DslModeling::Transaction t = partition.Store.TransactionManager.BeginTransaction("Load Model from " + fileName, true, transactionContext))
					{
						// Ensure there is some content in the file.  Blank (or almost blank, to account for encoding header bytes, etc.)
						// files will cause a new root element to be created and returned. 
						if (fileStream.Length > 5)
						{
							try
							{
								global::System.Xml.XmlReaderSettings settings = ActiveWriterSerializationHelper.Instance.CreateXmlReaderSettings(serializationContext, false);
								using (global::System.Xml.XmlReader reader = global::System.Xml.XmlReader.Create(fileStream, settings))
								{
									// Attempt to read the encoding.
									reader.Read(); // Move to the first node - will be the XmlDeclaration if there is one.
									global::System.Text.Encoding encoding;
									if (TryGetEncoding(reader, out encoding))
									{
										serializationResult.Encoding = encoding;
									}
	
									// Load any additional domain models that are required
									DslModeling::SerializationUtilities.ResolveDomainModels(reader, serializerLocator, partition.Store);
								
									reader.MoveToContent();
	
									
									modelRoot = modelRootSerializer.TryCreateInstance(serializationContext, reader, partition) as Model;
									if (modelRoot != null && !serializationResult.Failed)
									{
										ReadRootElement(serializationContext, modelRoot, reader, schemaResolver);
									}
								}
	
							}
							catch (global::System.Xml.XmlException xEx)
							{
								DslModeling::SerializationUtilities.AddMessage(
									serializationContext,
									DslModeling::SerializationMessageKind.Error,
									xEx
								);
							}
						}
				
						if(modelRoot == null && !serializationResult.Failed)
						{
							// create model root if it doesn't exist.
							modelRoot = CreateModelHelper(partition);
						}
						if (t.IsActive)
							t.Commit();
					} // End Inner Tx
	
					// Do load-time validation if a ValidationController is provided.
					if (!serializationResult.Failed && validationController != null)
					{
						using (new SerializationValidationObserver(serializationResult, validationController))
						{
							validationController.Validate(partition, DslValidation::ValidationCategories.Load);
						}
					}
	
				}
			}
			return modelRoot;
		}
	
		/// <summary>
		/// Attempts to return the encoding used by the reader.
		/// </summary>
		/// <remarks>
		/// The reader will be positioned at the start of the document when calling this method.
		/// </remarks>
		protected virtual bool TryGetEncoding(global::System.Xml.XmlReader reader, out global::System.Text.Encoding encoding)
		{
			global::System.Diagnostics.Debug.Assert(reader.NodeType == System.Xml.XmlNodeType.XmlDeclaration, "reader should be at the XmlDeclaration node when calling this method");
	
			encoding = null;
			// Attempt to read and parse the "encoding" attribute from the XML declaration node
			if (reader.NodeType == global::System.Xml.XmlNodeType.XmlDeclaration)
			{
				string encodingName = reader.GetAttribute("encoding");
				if (!string.IsNullOrWhiteSpace(encodingName))
				{
					encoding = global::System.Text.Encoding.GetEncoding(encodingName);
					return true;
				}
			}
			return false;
		}
	
		/// <summary>
		/// Saves the given model root to the given file, with default encoding (UTF-8), and optional properties with default value will not
		/// be written out.
		/// </summary>
		/// <param name="serializationResult">Stores serialization result from the save operation.</param>
		/// <param name="modelRoot">Model instance to be saved.</param>
		/// <param name="fileName">Name of the file in which the Model instance will be saved.</param>
		public virtual void SaveModel(DslModeling::SerializationResult serializationResult, Model modelRoot, string fileName)
		{
			SaveModel(serializationResult, modelRoot, fileName, global::System.Text.Encoding.UTF8, false);
		}
		
		/// <summary>
		/// Saves the given model to the given file, with default encoding (UTF-8).
		/// </summary>
		/// <param name="serializationResult">Stores serialization result from the save operation.</param>
		/// <param name="modelRoot">Model instance to be saved.</param>
		/// <param name="fileName">Name of the file in which the Model instance will be saved.</param>
		/// <param name="writeOptionalPropertiesWithDefaultValue">Whether optional properties with default value will be saved.</param>
		public virtual void SaveModel(DslModeling::SerializationResult serializationResult, Model modelRoot, string fileName, bool writeOptionalPropertiesWithDefaultValue)
		{
			SaveModel(serializationResult, modelRoot, fileName, global::System.Text.Encoding.UTF8, writeOptionalPropertiesWithDefaultValue);
		}
	
		/// <summary>
		/// Saves the given model root to the given file, with specified encoding.
		/// </summary>
		/// <param name="serializationResult">Stores serialization result from the save operation.</param>
		/// <param name="modelRoot">Model instance to be saved.</param>
		/// <param name="fileName">Name of the file in which the Model instance will be saved.</param>
		/// <param name="encoding">Encoding to use when saving the Model instance.</param>
		/// <param name="writeOptionalPropertiesWithDefaultValue">Whether optional properties with default value will be saved.</param>
		[global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public virtual void SaveModel(DslModeling::SerializationResult serializationResult, Model modelRoot, string fileName, global::System.Text.Encoding encoding, bool writeOptionalPropertiesWithDefaultValue)
		{
			#region Check Parameters
			if (serializationResult == null)
				throw new global::System.ArgumentNullException("serializationResult");
			if (modelRoot == null)
				throw new global::System.ArgumentNullException("modelRoot");
			if (string.IsNullOrEmpty(fileName))
				throw new global::System.ArgumentNullException("fileName");
			#endregion
	
			if (serializationResult.Failed)
				return;
	
			using (global::System.IO.MemoryStream newFileContent = InternalSaveModel(serializationResult, modelRoot, fileName, encoding, writeOptionalPropertiesWithDefaultValue))
			{
				if (!serializationResult.Failed && newFileContent != null)
				{	// Only write the content if there's no error encountered during serialization.
					using (global::System.IO.FileStream fileStream = new global::System.IO.FileStream(fileName, global::System.IO.FileMode.Create, global::System.IO.FileAccess.Write, global::System.IO.FileShare.None))
					{
						using (global::System.IO.BinaryWriter writer = new global::System.IO.BinaryWriter(fileStream, encoding))
						{
							writer.Write(newFileContent.ToArray());
						}
					}
				}
			}
		}
	
		/// <summary>
		/// Saves the given model root as a in-memory stream.
		/// This is a helper used by SaveModel() and SaveModelAndDiagram(). When saving the model and the diagram together, we want to make sure that 
		/// both can be saved without error before writing the content to disk, so we serialize the model into a in-memory stream first.
		/// </summary>
		/// <param name="serializationResult">Stores serialization result from the save operation.</param>
		/// <param name="modelRoot">Model instance to be saved.</param>
		/// <param name="fileName">Name of the file in which the Model instance will be saved.</param>
		/// <param name="encoding">Encoding to use when saving the Model instance.</param>
		/// <param name="writeOptionalPropertiesWithDefaultValue">Whether optional properties with default value will be saved.</param>
		/// <returns>In-memory stream containing the serialized Model instance.</returns>
		[global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		private global::System.IO.MemoryStream InternalSaveModel(DslModeling::SerializationResult serializationResult, Model modelRoot, string fileName, global::System.Text.Encoding encoding, bool writeOptionalPropertiesWithDefaultValue)
		{
			#region Check Parameters
			global::System.Diagnostics.Debug.Assert(serializationResult != null);
			global::System.Diagnostics.Debug.Assert(modelRoot != null);
			global::System.Diagnostics.Debug.Assert(!serializationResult.Failed);
			#endregion
	
			serializationResult.Encoding = encoding;
	
			DslModeling::DomainXmlSerializerDirectory directory = GetDirectory(modelRoot.Store);
	
			
			global::System.IO.MemoryStream newFileContent = new global::System.IO.MemoryStream();
			
			DslModeling::SerializationContext serializationContext = new DslModeling::SerializationContext(directory, fileName, serializationResult);
			InitializeSerializationContext(modelRoot.Partition, serializationContext, false);
			// MonikerResolver shouldn't be required in Save operation, so not calling SetupMonikerResolver() here.
			serializationContext.WriteOptionalPropertiesWithDefaultValue = writeOptionalPropertiesWithDefaultValue;
			global::System.Xml.XmlWriterSettings settings = ActiveWriterSerializationHelper.Instance.CreateXmlWriterSettings(serializationContext, false, encoding);
			using (global::System.Xml.XmlWriter writer = global::System.Xml.XmlWriter.Create(newFileContent, settings))
			{
				WriteRootElement(serializationContext, modelRoot, writer);
			}
				
			return newFileContent;
		}
		/// <summary>
		/// Saves the given model root as a in-memory stream.
		/// This is a helper used by SaveModel() and SaveModelAndDiagram(). When saving the model and the diagram together, we want to make sure that 
		/// both can be saved without error before writing the content to disk, so we serialize the model into a in-memory stream first.
		/// </summary>
		/// <param name="serializationResult">Stores serialization result from the save operation.</param>
		/// <param name="diagram">ActiveRecordMapping to be saved.</param>
		/// <param name="diagramFileName">Name of the file in which the diagram will be saved.</param>
		/// <param name="encoding">Encoding to use when saving the diagram.</param>
		/// <param name="writeOptionalPropertiesWithDefaultValue">Whether optional properties with default value will be saved.</param>
		/// <returns>In-memory stream containing the serialized ActiveRecordMapping instance.</returns>
		[global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		private global::System.IO.MemoryStream InternalSaveDiagram(DslModeling::SerializationResult serializationResult, ActiveRecordMapping diagram, string diagramFileName, global::System.Text.Encoding encoding, bool writeOptionalPropertiesWithDefaultValue)
		{
			#region Check Parameters
			global::System.Diagnostics.Debug.Assert(serializationResult != null);
			global::System.Diagnostics.Debug.Assert(diagram != null);
			global::System.Diagnostics.Debug.Assert(!serializationResult.Failed);
			#endregion
	
			DslModeling::DomainXmlSerializerDirectory directory = GetDirectory(diagram.Store);
	
			
			global::System.IO.MemoryStream newFileContent = new global::System.IO.MemoryStream();
			
			DslModeling::SerializationContext serializationContext = new DslModeling::SerializationContext(directory, diagramFileName, serializationResult);
			InitializeSerializationContext(diagram.Partition, serializationContext, false);
			// MonikerResolver shouldn't be required in Save operation, so not calling SetupMonikerResolver() here.
			serializationContext.WriteOptionalPropertiesWithDefaultValue = writeOptionalPropertiesWithDefaultValue;
			global::System.Xml.XmlWriterSettings settings = ActiveWriterSerializationHelper.Instance.CreateXmlWriterSettings(serializationContext, true, encoding);
			using (global::System.Xml.XmlWriter writer = global::System.Xml.XmlWriter.Create(newFileContent, settings))
			{
				WriteRootElement(serializationContext, diagram, writer);
			}
	
			return newFileContent;
		}
	
		/// <summary>
		/// Helper method to create and initialize a new Model.
		/// </summary>
		internal protected virtual Model CreateModelHelper(DslModeling::Partition modelPartition)
		{
			Model model = new Model(modelPartition);
			return model;
		}
		
		/// <summary>
		/// Loads a Model instance and its associated diagram file into the default partition of the given store, and ignore serialization result.
		/// </summary>
		/// <param name="store">The new Model instance will be created into the default partition of this store.</param>
		/// <param name="modelFileName">Name of the file from which the Model instance will be deserialized.</param>
		/// <param name="diagramFileName">Name of the file from which the ActiveRecordMapping instance will be deserialized.</param>
		/// <param name="schemaResolver">
		/// An ISchemaResolver that allows the serializer to do schema validation on the root element (and everything inside it).
		/// If null is passed, schema validation will not be performed.
		/// </param>
		/// <param name="validationController">
		/// A ValidationController that will be used to do load-time validation (validations with validation category "Load"). If null
		/// is passed, load-time validation will not be performed.
		/// </param>
		/// <param name="serializerLocator">
		/// An ISerializerLocator that will be used to locate any additional domain model types required to load the model. Can be null.
		/// </param>
		/// <returns>The loaded Model instance.</returns>
		public virtual Model LoadModelAndDiagram(DslModeling::Store store, string modelFileName, string diagramFileName, DslModeling::ISchemaResolver schemaResolver, DslValidation::ValidationController validationController, DslModeling::ISerializerLocator serializerLocator)
		{
			return LoadModelAndDiagram(new DslModeling::SerializationResult(), store, modelFileName, diagramFileName, schemaResolver, validationController, serializerLocator);
		}
		
		/// <summary>
		/// Loads a Model instance and its associated diagram file into the default partition of the given store.
		/// </summary>
		/// <param name="serializationResult">Stores serialization result from the load operation.</param>
		/// <param name="store">The new Model instance will be created into the default partition of this store.</param>
		/// <param name="modelFileName">Name of the file from which the Model instance will be deserialized.</param>
		/// <param name="diagramFileName">Name of the file from which the ActiveRecordMapping instance will be deserialized.</param>
		/// <param name="schemaResolver">
		/// An ISchemaResolver that allows the serializer to do schema validation on the root element (and everything inside it).
		/// If null is passed, schema validation will not be performed.
		/// </param>
		/// <param name="validationController">
		/// A ValidationController that will be used to do load-time validation (validations with validation category "Load"). If null
		/// is passed, load-time validation will not be performed.
		/// </param>
		/// <param name="serializerLocator">
		/// An ISerializerLocator that will be used to locate any additional domain model types required to load the model. Can be null.
		/// </param>
		/// <returns>The loaded Model instance.</returns>
		public virtual Model LoadModelAndDiagram(DslModeling::SerializationResult serializationResult, DslModeling::Store store, string modelFileName, string diagramFileName, DslModeling::ISchemaResolver schemaResolver, DslValidation::ValidationController validationController, DslModeling::ISerializerLocator serializerLocator)
		{
			#region Check Parameters
			if (store == null)
				throw new global::System.ArgumentNullException("store");
			#endregion
			
			DslModeling::Partition diagramPartition = new DslModeling::Partition(store);
			return LoadModelAndDiagram(serializationResult, store.DefaultPartition, modelFileName, diagramPartition, diagramFileName, schemaResolver, validationController, serializerLocator);
		}
			
		/// <summary>
		/// Loads a Model instance and its associated diagram file.
		/// </summary>
		/// <param name="serializationResult">Stores serialization result from the load operation.</param>
		/// <param name="modelPartition">Partition in which the new Model instance will be created.</param>
		/// <param name="modelFileName">Name of the file from which the Model instance will be deserialized.</param>
		/// <param name="diagramPartition">Partition in which the new ActiveRecordMapping instance will be created.</param>
		/// <param name="diagramFileName">Name of the file from which the ActiveRecordMapping instance will be deserialized.</param>
		/// <param name="schemaResolver">
		/// An ISchemaResolver that allows the serializer to do schema validation on the root element (and everything inside it).
		/// If null is passed, schema validation will not be performed.
		/// </param>
		/// <param name="validationController">
		/// A ValidationController that will be used to do load-time validation (validations with validation category "Load"). If null
		/// is passed, load-time validation will not be performed.
		/// </param>
		/// <param name="serializerLocator">
		/// An ISerializerLocator that will be used to locate any additional domain model types required to load the model. Can be null.
		/// </param>
		/// <returns>The loaded Model instance.</returns>
		[global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Generated code.")]
		public virtual Model LoadModelAndDiagram(DslModeling::SerializationResult serializationResult, DslModeling::Partition modelPartition, string modelFileName, DslModeling::Partition diagramPartition, string diagramFileName, DslModeling::ISchemaResolver schemaResolver, DslValidation::ValidationController validationController, DslModeling::ISerializerLocator serializerLocator)
		{
			#region Check Parameters
			if (serializationResult == null)
				throw new global::System.ArgumentNullException("serializationResult");
			if (modelPartition == null)		
				throw new global::System.ArgumentNullException("modelPartition");
			if (diagramPartition == null)
				throw new global::System.ArgumentNullException("diagramPartition");
			if (string.IsNullOrEmpty(diagramFileName))
				throw new global::System.ArgumentNullException("diagramFileName");
			#endregion
	
			Model modelRoot;
	
			// Ensure there is an outer transaction spanning both model and diagram load, so moniker resolution works properly.
			if (!diagramPartition.Store.TransactionActive)
			{
				throw new global::System.InvalidOperationException(ActiveWriterDomainModel.SingletonResourceManager.GetString("MissingTransaction"));
			}
	
			modelRoot = LoadModel(serializationResult, modelPartition, modelFileName, schemaResolver, validationController, serializerLocator);
	
			if (serializationResult.Failed)
			{
				// don't try to deserialize diagram data if model load failed.
				return modelRoot;
			}
	
			ActiveRecordMapping diagram = null;
			DslModeling::DomainXmlSerializerDirectory directory = GetDirectory(diagramPartition.Store);
			DslModeling::DomainClassXmlSerializer diagramSerializer = directory.GetSerializer(ActiveRecordMapping.DomainClassId);
			global::System.Diagnostics.Debug.Assert(diagramSerializer != null, "Cannot find serializer for ActiveRecordMapping");
			if (diagramSerializer != null)
			{
				if(!global::System.IO.File.Exists(diagramFileName))
				{
					// missing diagram file indicates we should create a new diagram.
					diagram = CreateDiagramHelper(diagramPartition, modelRoot);
				}
				else
				{
					using (global::System.IO.FileStream fileStream = global::System.IO.File.OpenRead(diagramFileName))
					{
						DslModeling::SerializationContext serializationContext = new DslModeling::SerializationContext(directory, fileStream.Name, serializationResult);
						InitializeSerializationContext(diagramPartition, serializationContext, true);
						DslModeling::TransactionContext transactionContext = new DslModeling::TransactionContext();
						transactionContext.Add(DslModeling::SerializationContext.TransactionContextKey, serializationContext);
						
						using (DslModeling::Transaction t = diagramPartition.Store.TransactionManager.BeginTransaction("LoadDiagram", true, transactionContext))
						{
							// Ensure there is some content in the file. Blank (or almost blank, to account for encoding header bytes, etc.)
							// files will cause a new diagram to be created and returned 
							if (fileStream.Length > 5)
							{
								global::System.Xml.XmlReaderSettings settings = ActiveWriterSerializationHelper.Instance.CreateXmlReaderSettings(serializationContext, false);
								try
								{
									using (global::System.Xml.XmlReader reader = global::System.Xml.XmlReader.Create(fileStream, settings))
									{
										reader.MoveToContent();
										diagram = diagramSerializer.TryCreateInstance(serializationContext, reader, diagramPartition) as ActiveRecordMapping;
										if (diagram != null)
										{
											ReadRootElement(serializationContext, diagram, reader, schemaResolver);
										}
									}
								}
								catch (global::System.Xml.XmlException xEx)
								{
									DslModeling::SerializationUtilities.AddMessage(
										serializationContext,
										DslModeling::SerializationMessageKind.Error,
										xEx
									);
								}
								if (serializationResult.Failed)
								{	
									// Serialization error encountered, rollback the transaction.
									diagram = null;
									t.Rollback();
								}
							}
							
							if(diagram == null && !serializationResult.Failed)
							{
								// Create diagram if it doesn't exist
								diagram = CreateDiagramHelper(diagramPartition, modelRoot);
							}
							
							if (t.IsActive)
								t.Commit();
						} // End inner Tx
	
						// Do load-time validation if a ValidationController is provided.
						if (!serializationResult.Failed && validationController != null)
						{
							using (new SerializationValidationObserver(serializationResult, validationController))
							{
								validationController.Validate(diagramPartition, DslValidation::ValidationCategories.Load);
							}
						}
					}
				}
	
				if (diagram != null)
				{
					if (!serializationResult.Failed)
					{	// Succeeded.
						diagram.ModelElement = modelRoot;
						diagram.PostDeserialization(true);
						CheckForOrphanedShapes(diagram, serializationResult);
					}
					else
					{	// Failed.
						diagram.PostDeserialization(false);
					}
				}
			}
			return modelRoot;
		}
	
		/// <summary>
		/// Helper method to create and initialize a new ActiveRecordMapping.
		/// </summary>
		[global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId="modelRoot", Justification = "Signature enforced by caller.")]
		internal protected virtual ActiveRecordMapping CreateDiagramHelper(DslModeling::Partition diagramPartition, DslModeling::ModelElement modelRoot)
		{
			ActiveRecordMapping diagram = new ActiveRecordMapping(diagramPartition);
			return diagram;
		}
		
	
		/// <summary>
		/// Saves the given diagram to the given file, with default encoding (UTF-8), and optional properties with default value will not
		/// be written out.
		/// </summary>
		/// <param name="serializationResult">Stores serialization result from the save operation.</param>
		/// <param name="modelRoot">Model instance to be saved.</param>
		/// <param name="modelFileName">Name of the file in which the CanonicalSampleRoot instance will be saved.</param>
		/// <param name="diagram">ActiveRecordMapping to be saved.</param>
		/// <param name="diagramFileName">Name of the file in which the diagram will be saved.</param>
		public virtual void SaveModelAndDiagram(DslModeling::SerializationResult serializationResult, Model modelRoot, string modelFileName, ActiveRecordMapping diagram, string diagramFileName)
		{
			SaveModelAndDiagram(serializationResult, modelRoot, modelFileName, diagram, diagramFileName, global::System.Text.Encoding.UTF8, false);
		}
		
		/// <summary>
		/// Saves the given diagram to the given file, with default encoding (UTF-8).
		/// </summary>
		/// <param name="serializationResult">Stores serialization result from the save operation.</param>
		/// <param name="modelRoot">Model instance to be saved.</param>
		/// <param name="modelFileName">Name of the file in which the CanonicalSampleRoot instance will be saved.</param>
		/// <param name="diagram">ActiveRecordMapping to be saved.</param>
		/// <param name="diagramFileName">Name of the file in which the diagram will be saved.</param>
		/// <param name="writeOptionalPropertiesWithDefaultValue">Whether optional properties with default value will be saved.</param>
		public virtual void SaveModelAndDiagram(DslModeling::SerializationResult serializationResult, Model modelRoot, string modelFileName, ActiveRecordMapping diagram, string diagramFileName, bool writeOptionalPropertiesWithDefaultValue)
		{
			SaveModelAndDiagram(serializationResult, modelRoot, modelFileName, diagram, diagramFileName, global::System.Text.Encoding.UTF8, writeOptionalPropertiesWithDefaultValue);
		}
	
		/// <summary>
		/// Saves the given Model and ActiveRecordMapping to the given files, with specified encoding.
		/// </summary>
		/// <param name="serializationResult">Stores serialization result from the save operation.</param>
		/// <param name="modelRoot">Model instance to be saved.</param>
		/// <param name="modelFileName">Name of the file in which the CanonicalSampleRoot instance will be saved.</param>
		/// <param name="diagram">ActiveRecordMapping to be saved.</param>
		/// <param name="diagramFileName">Name of the file in which the diagram will be saved.</param>
		/// <param name="encoding">Encoding to use when saving the diagram.</param>
		/// <param name="writeOptionalPropertiesWithDefaultValue">Whether optional properties with default value will be saved.</param>
		[global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public virtual void SaveModelAndDiagram(DslModeling::SerializationResult serializationResult, Model modelRoot, string modelFileName, ActiveRecordMapping diagram, string diagramFileName, global::System.Text.Encoding encoding, bool writeOptionalPropertiesWithDefaultValue)
		{
			#region Check Parameters
			if (serializationResult == null)
				throw new global::System.ArgumentNullException("serializationResult");
			if (string.IsNullOrEmpty(modelFileName))
				throw new global::System.ArgumentNullException("modelFileName");
			if (diagram == null)
				throw new global::System.ArgumentNullException("diagram");
			if (string.IsNullOrEmpty(diagramFileName))
				throw new global::System.ArgumentNullException("diagramFileName");
			#endregion
	
			if (serializationResult.Failed)
				return;
	
			// Save the model file first
			using (global::System.IO.MemoryStream modelFileContent = InternalSaveModel(serializationResult, modelRoot, modelFileName, encoding, writeOptionalPropertiesWithDefaultValue))
			{
				if (serializationResult.Failed)
					return;
	
				using (global::System.IO.MemoryStream diagramFileContent = InternalSaveDiagram(serializationResult, diagram, diagramFileName, encoding, writeOptionalPropertiesWithDefaultValue))
				{
					if (!serializationResult.Failed)
					{
						// Only write the contents if there's no error encountered during serialization.
						if (modelFileContent != null)
						{
							using (global::System.IO.FileStream fileStream = new global::System.IO.FileStream(modelFileName, global::System.IO.FileMode.Create, global::System.IO.FileAccess.Write, global::System.IO.FileShare.None))
							{
								using (global::System.IO.BinaryWriter writer = new global::System.IO.BinaryWriter(fileStream, encoding))
								{
									writer.Write(modelFileContent.ToArray());
								}
							}
						}
						if (diagramFileContent != null)
						{
							using (global::System.IO.FileStream fileStream = new global::System.IO.FileStream(diagramFileName, global::System.IO.FileMode.Create, global::System.IO.FileAccess.Write, global::System.IO.FileShare.None))
							{
								using (global::System.IO.BinaryWriter writer = new global::System.IO.BinaryWriter(fileStream, encoding))
								{
									writer.Write(diagramFileContent.ToArray());
								}
							}
						}
					}
				}
			}
		}
	
		/// <summary>
		/// Saves the given diagram to the given file, with default encoding (UTF-8), and optional properties with default value will not
		/// be written out.
		/// </summary>
		/// <param name="serializationResult">Stores serialization result from the save operation.</param>
		/// <param name="diagram">ActiveRecordMapping to be saved.</param>
		/// <param name="diagramFileName">Name of the file in which the diagram will be saved.</param>
		public virtual void SaveDiagram(DslModeling::SerializationResult serializationResult, ActiveRecordMapping diagram, string diagramFileName)
		{
			SaveDiagram(serializationResult, diagram, diagramFileName, global::System.Text.Encoding.UTF8, false);
		}
		
		/// <summary>
		/// Saves the given diagram to the given file, with default encoding (UTF-8).
		/// </summary>
		/// <param name="serializationResult">Stores serialization result from the save operation.</param>
		/// <param name="diagram">ActiveRecordMapping to be saved.</param>
		/// <param name="diagramFileName">Name of the file in which the diagram will be saved.</param>
		/// <param name="writeOptionalPropertiesWithDefaultValue">Whether optional properties with default value will be saved.</param>
		public virtual void SaveDiagram(DslModeling::SerializationResult serializationResult, ActiveRecordMapping diagram, string diagramFileName, bool writeOptionalPropertiesWithDefaultValue)
		{
			SaveDiagram(serializationResult, diagram, diagramFileName, global::System.Text.Encoding.UTF8, writeOptionalPropertiesWithDefaultValue);
		}
	
		/// <summary>
		/// Saves the given ActiveRecordMapping to the given file, with specified encoding.
		/// </summary>
		/// <param name="serializationResult">Stores serialization result from the save operation.</param>
		/// <param name="diagram">ActiveRecordMapping to be saved.</param>
		/// <param name="diagramFileName">Name of the file in which the diagram will be saved.</param>
		/// <param name="encoding">Encoding to use when saving the diagram.</param>
		/// <param name="writeOptionalPropertiesWithDefaultValue">Whether optional properties with default value will be saved.</param>
		[global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public virtual void SaveDiagram(DslModeling::SerializationResult serializationResult, ActiveRecordMapping diagram, string diagramFileName, global::System.Text.Encoding encoding, bool writeOptionalPropertiesWithDefaultValue)
		{
			#region Check Parameters
			if (serializationResult == null)
				throw new global::System.ArgumentNullException("serializationResult");
			if (diagram == null)
				throw new global::System.ArgumentNullException("diagram");
			if (string.IsNullOrEmpty(diagramFileName))
				throw new global::System.ArgumentNullException("diagramFileName");
			#endregion
	
			if (serializationResult.Failed)
				return;
	
			using (global::System.IO.MemoryStream diagramFileContent = InternalSaveDiagram(serializationResult, diagram, diagramFileName, encoding, writeOptionalPropertiesWithDefaultValue))
			{
				if (!serializationResult.Failed)
				{
					// Only write the contents if there's no error encountered during serialization.
					if (diagramFileContent != null)
					{
						using (global::System.IO.FileStream fileStream = new global::System.IO.FileStream(diagramFileName, global::System.IO.FileMode.Create, global::System.IO.FileAccess.Write, global::System.IO.FileShare.None))
						{
							using (global::System.IO.BinaryWriter writer = new global::System.IO.BinaryWriter(fileStream, encoding))
							{
								writer.Write(diagramFileContent.ToArray());
							}
						}
					}
				}
			}
		}
	
		/// <summary>
		/// Read an element from the root of XML.
		/// </summary>
		/// <param name="serializationContext">Serialization context.</param>
		/// <param name="rootElement">In-memory element instance that will get the deserialized data.</param>
		/// <param name="reader">XmlReader to read serialized data from.</param>
		/// <param name="schemaResolver">An ISchemaResolver that allows the serializer to do schema validation on the root element (and everything inside it).</param>
		protected virtual void ReadRootElement(DslModeling::SerializationContext serializationContext, DslModeling::ModelElement rootElement, global::System.Xml.XmlReader reader, DslModeling::ISchemaResolver schemaResolver)
		{
			#region Check Parameters
			global::System.Diagnostics.Debug.Assert(serializationContext != null);
			if (serializationContext == null)
				throw new global::System.ArgumentNullException("serializationContext");
			global::System.Diagnostics.Debug.Assert(rootElement != null);
			if (rootElement == null)
				throw new global::System.ArgumentNullException("rootElement");
			global::System.Diagnostics.Debug.Assert(reader != null);
			if (reader == null)
				throw new global::System.ArgumentNullException("reader");
			#endregion
	
			DslModeling::DomainXmlSerializerDirectory directory = GetDirectory(rootElement.Store);
	
			DslModeling::DomainClassXmlSerializer rootSerializer = directory.GetSerializer(rootElement.GetDomainClass().Id);
			global::System.Diagnostics.Debug.Assert(rootSerializer != null, "Cannot find serializer for " + rootElement.GetDomainClass().Name + "!");
	
			// Version check.
			CheckVersion(serializationContext, reader);
	
			if (!serializationContext.Result.Failed)
			{	
				// Use a validating reader if possible
				using (reader = TryCreateValidatingReader(schemaResolver, reader, serializationContext))
				{
					rootSerializer.Read(serializationContext, rootElement, reader);
				}
			}
	
		}
		
		/// <summary>
		/// Write an element as the root of XML.
		/// </summary>
		/// <param name="serializationContext">Serialization context.</param>
		/// <param name="rootElement">Root element instance that will be serialized.</param>
		/// <param name="writer">XmlWriter to write serialized data to.</param>
		public virtual void WriteRootElement(DslModeling::SerializationContext serializationContext, DslModeling::ModelElement rootElement, global::System.Xml.XmlWriter writer)
		{
			#region Check Parameters
			global::System.Diagnostics.Debug.Assert(serializationContext != null);
			if (serializationContext == null)
				throw new global::System.ArgumentNullException("serializationContext");
			global::System.Diagnostics.Debug.Assert(rootElement != null);
			if (rootElement == null)
				throw new global::System.ArgumentNullException("rootElement");
			global::System.Diagnostics.Debug.Assert(writer != null);
			if (writer == null)
				throw new global::System.ArgumentNullException("writer");
			#endregion
	
			DslModeling::DomainXmlSerializerDirectory directory = GetDirectory(rootElement.Store);
	
			DslModeling::DomainClassXmlSerializer rootSerializer = directory.GetSerializer(rootElement.GetDomainClass().Id);
			global::System.Diagnostics.Debug.Assert(rootSerializer != null, "Cannot find serializer for " + rootElement.GetDomainClass().Name + "!");
	
			// Set up root element settings
			DslModeling::RootElementSettings rootElementSettings = new DslModeling::RootElementSettings();
			if (!(rootElement is DslDiagrams::Diagram))
			{
				// Only model has schema, diagram has no schema.
				rootElementSettings.SchemaTargetNamespace = "http://schemas.microsoft.com/dsltools/ActiveWriter";
			}
			rootElementSettings.Version = new global::System.Version("10.0.0.1");
	
			// Carry out the normal serialization.
			rootSerializer.Write(serializationContext, rootElement, writer, rootElementSettings);
		}
	
		/// <summary>
		/// Checks the version of the file being read.
		/// </summary>
		/// <param name="serializationContext">Serialization context.</param>
		/// <param name="reader">Reader for the file being read. The reader is positioned at the open tag of the root element being read.</param>
		protected virtual void CheckVersion(DslModeling::SerializationContext serializationContext, global::System.Xml.XmlReader reader)
		{
			#region Check Parameters
			global::System.Diagnostics.Debug.Assert(serializationContext != null);
			if (serializationContext == null)
				throw new global::System.ArgumentNullException("serializationContext");
			global::System.Diagnostics.Debug.Assert(reader != null);
			if (reader == null)
				throw new global::System.ArgumentNullException("reader");
			#endregion
	
			global::System.Version expectedVersion = new global::System.Version("10.0.0.1");
			string dslVersionStr = reader.GetAttribute("dslVersion");
			if (dslVersionStr != null)
			{
				try
				{
					global::System.Version actualVersion = new global::System.Version(dslVersionStr);
					if (actualVersion != expectedVersion)
					{
						ActiveWriterSerializationBehaviorSerializationMessages.VersionMismatch(serializationContext, reader, expectedVersion, actualVersion);
					}
				}
				catch (global::System.ArgumentException)
				{
					ActiveWriterSerializationBehaviorSerializationMessages.InvalidPropertyValue(serializationContext, reader, "dslVersion", typeof(global::System.Version), dslVersionStr);
				}
				catch (global::System.FormatException)
				{
					ActiveWriterSerializationBehaviorSerializationMessages.InvalidPropertyValue(serializationContext, reader, "dslVersion", typeof(global::System.Version), dslVersionStr);
				}
				catch (global::System.OverflowException)
				{
					ActiveWriterSerializationBehaviorSerializationMessages.InvalidPropertyValue(serializationContext, reader, "dslVersion", typeof(global::System.Version), dslVersionStr);
				}
			}
		}
	
	
		/// <summary>
		/// Attempts to return a validating XML reader
		/// </summary>
		/// <returns>If all of the schema for registered serializers and all of the schema referenced in the file can be resolved,
		/// then a validating reader is returned. Otherwise, the supplied reader is returned.
		/// The serialization context will contain warning messages for all schema that could not be resolved.</returns>
		protected virtual global::System.Xml.XmlReader TryCreateValidatingReader(DslModeling::ISchemaResolver schemaResolver, global::System.Xml.XmlReader reader, DslModeling::SerializationContext serializationContext)
		{
			System.Diagnostics.Debug.Assert(serializationContext != null, "Serialization context should not be null");
	
			// Can't do anything without a schema resolver
			if (schemaResolver == null)
			{
				return reader;
			}
			
			global::System.Xml.Schema.XmlSchemaSet schemaSet = new global::System.Xml.Schema.XmlSchemaSet(reader.NameTable);
	
			// Combine the list of namespaces for models in the store with those referred to in the current node
			System.Collections.Generic.IEnumerable<string> namespaces = serializationContext.Directory.Namespaces.Select(n => n.TargetNamespace);
			namespaces = namespaces.Concat(DslModeling::SerializationUtilities.GetNamespacesFromCurrentNode(reader));		
	
			bool success = true;
			foreach (string ns in namespaces.Distinct())
			{
				// Try to resolve all namespaces so warnings are shown for all missing namespaces
				success = ResolveSchema(ns, schemaSet, schemaResolver, reader, serializationContext) & success;
			}
	
			if (success && schemaSet.Count > 0)
			{
				global::System.Xml.XmlReaderSettings readerSettings = reader.Settings.Clone();
				readerSettings.ConformanceLevel = global::System.Xml.ConformanceLevel.Auto;
				readerSettings.ValidationType = global::System.Xml.ValidationType.Schema;
				readerSettings.Schemas = schemaSet;
				ActiveWriterSerializationBehaviorSchemaValidationCallback validationCallback = new ActiveWriterSerializationBehaviorSchemaValidationCallback(serializationContext);
				readerSettings.ValidationEventHandler += new global::System.Xml.Schema.ValidationEventHandler(validationCallback.Handler);
	
	
				// Wrap the given reader as a validating reader and carry out the normal deserialization.
				global::System.Xml.XmlReader validatingReader = global::System.Xml.XmlReader.Create(reader, readerSettings);
	
				validationCallback.Reader = validatingReader;
				return validatingReader;
			}
	
			return reader;
		}
	
		/// <summary>
		/// Attempts to resolve the supplied schema namespace
		/// </summary>
		/// <remarks>If the schema can be resolved it is added to the supplied schema set. Otherwise, a 
		/// warning will be written to serializationContext.
		/// </remarks>
		/// <returns>A flag indicating whether the schema was resolved or not</returns>
		protected static bool ResolveSchema(string targetNamespace, global::System.Xml.Schema.XmlSchemaSet schemaSet, DslModeling::ISchemaResolver schemaResolver, global::System.Xml.XmlReader reader, DslModeling::SerializationContext serializationContext)
		{
			global::System.Collections.Generic.IList<string> schemas = schemaResolver.ResolveSchema(targetNamespace);
			if (schemas != null && schemas.Count > 0)
			{
				if (schemas.Count > 1)
				{
					ActiveWriterSerializationBehaviorSerializationMessages.AmbiguousSchema(serializationContext, reader, targetNamespace, schemas[0]);
				}
				schemaSet.Add(targetNamespace, schemas[0]);
				return true;
			}
	
			ActiveWriterSerializationBehaviorSerializationMessages.NoSchema(serializationContext, reader, targetNamespace);
			return false;
		}
	
	
		/// <summary>
		/// A utility class to handle schema validation warning/error
		/// </summary>
		private sealed class ActiveWriterSerializationBehaviorSchemaValidationCallback
		{
			#region Member Variables
			/// <summary>
			/// SerializationContext to store schema validation warning/error.
			/// </summary>
			DslModeling::SerializationContext serializationContext;
			/// <summary>
			/// Reader that generates the schema warning/error.
			/// </summary>
			global::System.Xml.XmlReader reader;
			#endregion
	
			#region Constructor
			/// <summary>
			/// Constructor
			/// </summary>
			/// <param name="serializationContext">SerializationContext to be used to store schema validation warning/error.</param>
			internal ActiveWriterSerializationBehaviorSchemaValidationCallback(DslModeling::SerializationContext serializationContext)
			{
				global::System.Diagnostics.Debug.Assert(serializationContext != null);
				this.serializationContext = serializationContext;
			}
			#endregion
	
			#region Accessor
			/// <summary>
			/// Sets the reader that generates the schema validation warning/error.
			/// </summary>
			internal global::System.Xml.XmlReader Reader
			{
				[global::System.Diagnostics.DebuggerStepThrough]
				set { reader = value; }
			}
			#endregion
	
			#region Callback method
			/// <summary>
			/// Callback to handler schema validation warning/error.
			/// </summary>
			internal void Handler(object sender, global::System.Xml.Schema.ValidationEventArgs e)
			{
				global::System.Diagnostics.Debug.Assert(serializationContext != null);
				if (serializationContext != null)
					ActiveWriterSerializationBehaviorSerializationMessages.SchemaValidationError(serializationContext, reader, e.Message);
			}
			#endregion
		}
	
		/// <summary>
		/// Return the model in XML format
		/// </summary>
		/// <param name="modelRoot">Root instance to be saved.</param>
		/// <param name="encoding">Encoding to use when saving the root instance.</param>
		/// <returns>Model in XML form</returns>
		public virtual string GetSerializedModelString(global::Castle.ActiveWriter.Model modelRoot, global::System.Text.Encoding encoding)
		{
			string result = string.Empty;
			if (modelRoot == null)
			{
				return result;
			}
	
			DslModeling::SerializationResult serializationResult = new DslModeling::SerializationResult();
			using (global::System.IO.MemoryStream modelFileContent = InternalSaveModel(serializationResult, modelRoot, string.Empty, encoding, false))
			{
				if (!serializationResult.Failed && modelFileContent != null)
				{
					char[] chars = encoding.GetChars(modelFileContent.GetBuffer());
	
					// search the open angle bracket and trim off the Byte Of Mark.
					result = new string( chars);
					int indexPos = result.IndexOf('<');
					if (indexPos > 0)
					{
						// strip off the leading Byte Of Mark.
						result = result.Substring(indexPos);
					}
	
					// trim off trailing 0s.
					result = result.TrimEnd( '\0');
				}
			}
			return result;
		}
		
	
		#region Private/Helper Methods/Properties
			
		#region Class SerializationValidationObserver
		/// <summary>
		/// An utility class to collect validation messages during serialization, and store them in serialization result.
		/// </summary>
		protected sealed class SerializationValidationObserver : DslValidation::ValidationMessageObserver, global::System.IDisposable
		{
			#region Member Variables
			/// <summary>
			/// SerializationResult to store the messages.
			/// </summary>
			private DslModeling::SerializationResult serializationResult;
			/// <summary>
			/// ValidationController to get messages from.
			/// </summary>
			private DslValidation::ValidationController validationController;
			#endregion
	
			#region Constructor
			/// <summary>
			/// Constructor
			/// </summary>
			internal SerializationValidationObserver(DslModeling::SerializationResult serializationResult, DslValidation::ValidationController validationController)
			{
				#region Check Parameters
				global::System.Diagnostics.Debug.Assert(serializationResult != null);
				global::System.Diagnostics.Debug.Assert(validationController != null);
				#endregion
	
				this.serializationResult = serializationResult;
				this.validationController = validationController;
	
				// Subscribe to validation messages.
				this.validationController.AddObserver(this);
			}
	
			/// <summary>
			/// Destructor
			/// </summary>
			~SerializationValidationObserver()
			{
				Dispose(false);
			}
			#endregion
	
			#region Base Overrides
			/// <summary>
			/// Called with validation messages are added.
			/// </summary>
			protected override void OnValidationMessageAdded(DslValidation::ValidationMessage addedMessage)
			{
				#region Check Parameters
				global::System.Diagnostics.Debug.Assert(addedMessage != null);
				#endregion
	
				if (addedMessage != null && serializationResult != null)
				{	// Record the validation message as a serialization message.
					DslModeling::SerializationUtilities.AddValidationMessage(serializationResult, addedMessage);
				}
				base.OnValidationMessageAdded(addedMessage);
			}
			#endregion
	
			#region IDisposable Members
			/// <summary>
			/// IDisposable.Dispose().
			/// </summary>
			public void Dispose()
			{
				Dispose(true);
				global::System.GC.SuppressFinalize(this);
			}
	
			/// <summary>
			/// Unregister the observer on dispose.
			/// </summary>
			private void Dispose(bool disposing)
			{
				global::System.Diagnostics.Debug.Assert(disposing, "SerializationValidationObserver finalized without being disposed!");
				if (disposing && validationController != null)
				{
					validationController.RemoveObserver(this);
					validationController = null;
				}
				serializationResult = null;
			}
			#endregion
		}
		#endregion
		
		/// <summary>
		/// Go through the diagram to find all shapes that are not connected to a model element. Some of them may be by design, the rest are out-of-sync shapes.
		/// We want to make sure that all out-of-sync shapes are given a chance to fix themselves up, or post proper warning/error messages.
		/// </summary>
		/// <param name="diagram">The diagram of which the shapes are being checked.</param>
		/// <param name="serializationResult">SerializationResult to store warning/error in case an orphaned shape choose to do so.</param>
		[global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		protected virtual void CheckForOrphanedShapes(DslDiagrams::Diagram diagram, DslModeling::SerializationResult serializationResult)
		{
			global::System.Collections.Generic.List<DslDiagrams::ShapeElement> orphanedShapes = new global::System.Collections.Generic.List<DslDiagrams::ShapeElement>();
			diagram.IterateShapes(new OrphanedShapeIterator(orphanedShapes, diagram));
			foreach (DslDiagrams::ShapeElement orphanedShape in orphanedShapes)
			{
				if (serializationResult.Failed)
					break;
	
				orphanedShape.OnOrphaned(serializationResult);
			}
		}
		
		#region Class OrphanedShapeIterator
		/// <summary>
		/// An iterator to collect all the orphaned shapes.
		/// </summary>
		private class OrphanedShapeIterator : DslDiagrams::IShapeIterator
		{
			#region Member Variables
			/// <summary>
			/// Stores all iterated orphaned shapes.
			/// </summary>
			private global::System.Collections.Generic.List<DslDiagrams::ShapeElement> orphanedShapes;
			
			/// <summary>
			/// Diagram that contains the shapes to be iterated.
			/// </summary>
			private DslDiagrams::Diagram diagram;
			#endregion
	
			#region Constructor
			/// <summary>
			/// Constructor.
			/// </summary>
			/// <param name="orphanedShapes">Storage for all iterated orphaned shapes.</param>
			/// <param name="diagram">Diagram that contains the shapes to be iterated.</param>
			internal OrphanedShapeIterator(global::System.Collections.Generic.List<DslDiagrams::ShapeElement> orphanedShapes, DslDiagrams::Diagram diagram)
			{
				#region Check Parameters
				global::System.Diagnostics.Debug.Assert(orphanedShapes != null);
				global::System.Diagnostics.Debug.Assert(diagram != null);
				#endregion
	
				this.orphanedShapes = orphanedShapes;
				this.diagram = diagram;
			}
			#endregion
	
			#region IShapeIterator Members
			/// <summary>
			/// Called when a shape is encountered during iteration. The shape will be stored if it's orphaned.
			/// A shape is considered "orphaned" if:
			/// 1) It has a PresentationViewsSubject link out of it, but the Subject cannot be resolved.
			/// 2) It is a connector, and the Subject is null (regardless of the existence of PresentationViewsSubject relationship).
			/// </summary>
			/// <param name="shape">Shape encountered.</param>
			public void OnShape(DslDiagrams::ShapeElement shape)
			{
				if (diagram.IsOrphaned(shape))
					orphanedShapes.Add(shape);	// Orphaned.
			}
			#endregion
		}
		#endregion
		#endregion
	}
	
}

namespace Castle.ActiveWriter
{
	[DslValidation::ValidationState(DslValidation::ValidationState.Enabled)]
	public partial class Model
	{
		/// <summary>
		/// Check to make sure all elements in the model will have unambiguous monikers when serialized.
		/// </summary>
		[global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated code.")]
		[DslValidation::ValidationMethod(DslValidation::ValidationCategories.Load | DslValidation::ValidationCategories.Save | DslValidation::ValidationCategories.Menu)]
		private void ValidateMonikerAmbiguity(DslValidation::ValidationContext context)
		{
			// Don't run this rule when deserializing - any ambiguous monikers will 
			// already have stopped the file from loading.
			if (Store.InSerializationTransaction)
			{
				return;
			}
		
			global::System.Collections.Generic.IDictionary<global::System.Guid, DslModeling::IMonikerResolver> monikerResolvers = ActiveWriterSerializationHelper.Instance.GetMonikerResolvers(Store);
			foreach (DslModeling::ModelElement element in Store.ElementDirectory.AllElements)
			{
				global::System.Guid domainModelId = element.GetDomainClass().DomainModel.Id;
				DslModeling::IMonikerResolver monikerResolver = null;
				if (monikerResolvers.TryGetValue(domainModelId, out monikerResolver) && monikerResolver != null)
				{
					DslModeling::SimpleMonikerResolver simpleMonikerResolver = monikerResolver as DslModeling::SimpleMonikerResolver;
					if (simpleMonikerResolver != null)
					{
						try
						{
							simpleMonikerResolver.ProcessAddedElement(element);
						}
						catch (DslModeling::AmbiguousMonikerException amEx)
						{	// Ambiguous moniker detected.
							context.LogError(
								string.Format(
									global::System.Globalization.CultureInfo.CurrentCulture,
									ActiveWriterDomainModel.SingletonResourceManager.GetString("AmbiguousMoniker"),
									amEx.Moniker,
									DslModeling::SerializationUtilities.GetElementName(element),
									DslModeling::SerializationUtilities.GetElementName(amEx.Element)
								),
								"AmbiguousMoniker", 
								this,
								amEx.Element
							);
						}
					}
				}
			}
			// Clean up the created moniker resolvers after the check.
			foreach (DslModeling::IMonikerResolver monikerResolver in monikerResolvers.Values)
			{
				DslModeling::SimpleMonikerResolver simpleMonikerResolver = monikerResolver as DslModeling::SimpleMonikerResolver;
				if (simpleMonikerResolver != null)
					simpleMonikerResolver.Reset();
			}
			monikerResolvers.Clear();
		}
	}
}
