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
using DslDesign = global::Microsoft.VisualStudio.Modeling.Design;
using DslDiagrams = global::Microsoft.VisualStudio.Modeling.Diagrams;

namespace Castle.ActiveWriter
{
	/// <summary>
	/// Double-derived base class for DomainClass ClassShape
	/// </summary>
	[DslDesign::DisplayNameResource("Castle.ActiveWriter.ClassShape.DisplayName", typeof(global::Castle.ActiveWriter.ActiveWriterDomainModel), "Castle.ActiveWriter.GeneratedCode.DomainModelResx")]
	[DslDesign::DescriptionResource("Castle.ActiveWriter.ClassShape.Description", typeof(global::Castle.ActiveWriter.ActiveWriterDomainModel), "Castle.ActiveWriter.GeneratedCode.DomainModelResx")]
	[DslModeling::DomainModelOwner(typeof(global::Castle.ActiveWriter.ActiveWriterDomainModel))]
	[global::System.CLSCompliant(true)]
	[DslModeling::DomainObjectId("ca45d586-12d1-4f5d-99c7-83c1eb0e61eb")]
	public abstract partial class ClassShapeBase : DslDiagrams::CompartmentShape
	{
		#region DiagramElement boilerplate
		private static DslDiagrams::StyleSet classStyleSet;
		private static global::System.Collections.Generic.IList<DslDiagrams::ShapeField> shapeFields;
		private static global::System.Collections.Generic.IList<DslDiagrams::Decorator> decorators;
		
		/// <summary>
		/// Per-class style set for this shape.
		/// </summary>
		protected override DslDiagrams::StyleSet ClassStyleSet
		{
			get
			{
				if (classStyleSet == null)
				{
					classStyleSet = CreateClassStyleSet();
				}
				return classStyleSet;
			}
		}
		
		/// <summary>
		/// Per-class ShapeFields for this shape.
		/// </summary>
		public override global::System.Collections.Generic.IList<DslDiagrams::ShapeField> ShapeFields
		{
			get
			{
				if (shapeFields == null)
				{
					shapeFields = CreateShapeFields();
				}
				return shapeFields;
			}
		}
		
		/// <summary>
		/// Event fired when decorator initialization is complete for this shape type.
		/// </summary>
		public static event global::System.EventHandler DecoratorsInitialized;
		
		/// <summary>
		/// List containing decorators used by this type.
		/// </summary>
		public override global::System.Collections.Generic.IList<DslDiagrams::Decorator> Decorators
		{
			get 
			{
				if(decorators == null)
				{
					decorators = CreateDecorators();
					
					// fire this event to allow the diagram to initialize decorator mappings for this shape type.
					if(DecoratorsInitialized != null)
					{
						DecoratorsInitialized(this, global::System.EventArgs.Empty);
					}
				}
				
				return decorators; 
			}
		}
		
		/// <summary>
		/// Finds a decorator associated with ClassShape.
		/// </summary>
		public static DslDiagrams::Decorator FindClassShapeDecorator(string decoratorName)
		{	
			if(decorators == null) return null;
			return DslDiagrams::ShapeElement.FindDecorator(decorators, decoratorName);
		}
		
		
		/// <summary>
		/// Shape instance initialization.
		/// </summary>
		public override void OnInitialize()
		{
			base.OnInitialize();
			
			// Create host shapes for outer decorators.
			foreach(DslDiagrams::Decorator decorator in Decorators)
			{
				if(decorator.RequiresHost)
				{
					decorator.ConfigureHostShape(this);
				}
			}
			
		}
		#endregion
		#region Shape size
		
		/// <summary>
		/// Default size for this shape.
		/// </summary>
		public override DslDiagrams::SizeD DefaultSize
		{
			get
			{
				return new DslDiagrams::SizeD(1.5, 0.4);
			}
		}
		#endregion
		#region Shape styles
		/// <summary>
		/// Initializes style set resources for this shape type
		/// </summary>
		/// <param name="classStyleSet">The style set for this shape class</param>
		protected override void InitializeResources(DslDiagrams::StyleSet classStyleSet)
		{
			base.InitializeResources(classStyleSet);
			
			// Fill brush settings for this shape.
			DslDiagrams::BrushSettings backgroundBrush = new DslDiagrams::BrushSettings();
			backgroundBrush.Color = global::System.Drawing.Color.FromKnownColor(global::System.Drawing.KnownColor.LightSteelBlue);
			classStyleSet.OverrideBrush(DslDiagrams::DiagramBrushes.ShapeBackground, backgroundBrush);
		
		}
		
		/// <summary>
		/// Indicates whether this shape displays a background gradient.
		/// </summary>
		public override bool HasBackgroundGradient
		{
			get
			{
				return true;
			}
		}
		
		/// <summary>
		/// Indicates the direction of the gradient.
		/// </summary>
		public override global::System.Drawing.Drawing2D.LinearGradientMode BackgroundGradientMode
		{
			get
			{
				return global::System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
			}
		}
		/// <summary>
		/// Specifies the geometry used by this shape
		/// </summary>
		public override DslDiagrams::ShapeGeometry ShapeGeometry
		{
			get
			{
				return DslDiagrams::ShapeGeometries.RoundedRectangle;
			}
		}
		#endregion
		#region Decorators
		/// <summary>
		/// Initialize the collection of shape fields associated with this shape type.
		/// </summary>
		protected override void InitializeShapeFields(global::System.Collections.Generic.IList<DslDiagrams::ShapeField> shapeFields)
		{
			base.InitializeShapeFields(shapeFields);
			DslDiagrams::TextField field1 = new DslDiagrams::TextField("Name");
			field1.DefaultText = global::Castle.ActiveWriter.ActiveWriterDomainModel.SingletonResourceManager.GetString("ClassShapeNameDefaultText");
			field1.DefaultFocusable = true;
			field1.DefaultAutoSize = true;
			field1.AnchoringBehavior.MinimumHeightInLines = 1;
			field1.AnchoringBehavior.MinimumWidthInCharacters = 1;
			field1.DefaultAccessibleState = global::System.Windows.Forms.AccessibleStates.Invisible;
			shapeFields.Add(field1);
			
			DslDiagrams::ChevronButtonField field2 = new DslDiagrams::ChevronButtonField("ExpandCollapse");
			field2.DefaultSelectable = false;
			field2.DefaultFocusable = false;
			shapeFields.Add(field2);
			
			DslDiagrams::ImageField field3 = new DslDiagrams::ImageField("Key");
			field3.DefaultImage = DslDiagrams::ImageHelper.GetImage(global::Castle.ActiveWriter.ActiveWriterDomainModel.SingletonResourceManager.GetObject("ClassShapeKeyDefaultImage"));
			shapeFields.Add(field3);
			
			DslDiagrams::ImageField field4 = new DslDiagrams::ImageField("Validation");
			field4.DefaultImage = DslDiagrams::ImageHelper.GetImage(global::Castle.ActiveWriter.ActiveWriterDomainModel.SingletonResourceManager.GetObject("ClassShapeValidationDefaultImage"));
			shapeFields.Add(field4);
			
		}
		
		/// <summary>
		/// Initialize the collection of decorators associated with this shape type.  This method also
		/// creates shape fields for outer decorators, because these are not part of the shape fields collection
		/// associated with the shape, so they must be created here rather than in InitializeShapeFields.
		/// </summary>
		protected override void InitializeDecorators(global::System.Collections.Generic.IList<DslDiagrams::ShapeField> shapeFields, global::System.Collections.Generic.IList<DslDiagrams::Decorator> decorators)
		{
			base.InitializeDecorators(shapeFields, decorators);
			
			DslDiagrams::ShapeField field1 = DslDiagrams::ShapeElement.FindShapeField(shapeFields, "Name");
			DslDiagrams::Decorator decorator1 = new DslDiagrams::ShapeDecorator(field1, DslDiagrams::ShapeDecoratorPosition.InnerTopCenter, DslDiagrams::PointD.Empty);
			decorators.Add(decorator1);
				
			DslDiagrams::ShapeField field2 = DslDiagrams::ShapeElement.FindShapeField(shapeFields, "ExpandCollapse");
			DslDiagrams::Decorator decorator2 = new DslDiagrams::ExpandCollapseDecorator(Store, (DslDiagrams::ToggleButtonField)field2, DslDiagrams::ShapeDecoratorPosition.InnerTopRight, DslDiagrams::PointD.Empty);
			decorators.Add(decorator2);
				
			DslDiagrams::ShapeField field3 = DslDiagrams::ShapeElement.FindShapeField(shapeFields, "Key");
			DslDiagrams::Decorator decorator3 = new DslDiagrams::ShapeDecorator(field3, DslDiagrams::ShapeDecoratorPosition.InnerTopLeft, DslDiagrams::PointD.Empty);
			decorators.Add(decorator3);
				
			DslDiagrams::ShapeField field4 = DslDiagrams::ShapeElement.FindShapeField(shapeFields, "Validation");
			DslDiagrams::Decorator decorator4 = new DslDiagrams::ShapeDecorator(field4, DslDiagrams::ShapeDecoratorPosition.InnerTopLeft, new DslDiagrams::PointD(0, 0.14));
			decorators.Add(decorator4);
				
		}
		
		/// <summary>
		/// Ensure outer decorators are placed appropriately.  This is called during view fixup,
		/// after the shape has been associated with the model element.
		/// </summary>
		public override void OnBoundsFixup(DslDiagrams::BoundsFixupState fixupState, int iteration, bool createdDuringViewFixup)
		{
			base.OnBoundsFixup(fixupState, iteration, createdDuringViewFixup);
			
			if(iteration == 0)
			{
				foreach(DslDiagrams::Decorator decorator in Decorators)
				{
					if(decorator.RequiresHost)
					{
						decorator.RepositionHostShape(decorator.GetHostShape(this));
					}
				}
			}
		}
		#endregion
		#region CompartmentShape code
		/// <summary>
		/// Returns a value indicating whether compartment header should be visible if there is only one of them.
		/// </summary>
		public override bool IsSingleCompartmentHeaderVisible
		{
			get { return true; }
		}
		
		private static DslDiagrams::CompartmentDescription[] compartmentDescriptions;
		
		/// <summary>
		/// Gets an array of CompartmentDescription for all compartments shown on this shape
		/// (including compartments defined on base shapes).
		/// </summary>
		/// <returns></returns>
		public override DslDiagrams::CompartmentDescription[] GetCompartmentDescriptions()
		{
			if(compartmentDescriptions == null)
			{
				// Initialize the array of compartment descriptions if we haven't done so already. 
				// First we get any compartment descriptions in base shapes, and add on any compartments
				// that are defined on this shape. 
				DslDiagrams::CompartmentDescription[] baseCompartmentDescriptions = base.GetCompartmentDescriptions();
				
				int localCompartmentsOffset = 0;
				if(baseCompartmentDescriptions!=null)
				{
					localCompartmentsOffset = baseCompartmentDescriptions.Length;
				}
				compartmentDescriptions = new DslDiagrams::ElementListCompartmentDescription[1+localCompartmentsOffset];
				
				if(baseCompartmentDescriptions!=null)
				{
					baseCompartmentDescriptions.CopyTo(compartmentDescriptions, 0);	
				}
				{
					string title = global::Castle.ActiveWriter.ActiveWriterDomainModel.SingletonResourceManager.GetString("ClassShapePropertiesTitle");
					DslDiagrams::ElementListCompartmentDescription descriptor = new DslDiagrams::ElementListCompartmentDescription("Properties", title, 
						global::System.Drawing.Color.FromKnownColor(global::System.Drawing.KnownColor.LightGray), false, 
						global::System.Drawing.Color.FromKnownColor(global::System.Drawing.KnownColor.White), false,
						null, null,
						false);
					compartmentDescriptions[localCompartmentsOffset+0] = descriptor;
				}
			}
			
			return ClassShape.compartmentDescriptions;
		}
		
		private static global::System.Collections.Generic.Dictionary<global::System.Type, DslDiagrams::CompartmentMapping[]> compartmentMappings;
		
		/// <summary>
		/// Gets an array of CompartmentMappings for all compartments displayed on this shape
		/// (including compartment maps defined on base shapes). 
		/// </summary>
		/// <param name="melType">The type of the DomainClass that this shape is mapped to</param>
		/// <returns></returns>
		protected override DslDiagrams::CompartmentMapping[] GetCompartmentMappings(global::System.Type melType)
		{
			if(melType==null) throw new global::System.ArgumentNullException("melType");
			
			if(compartmentMappings==null)
			{
				// Initialize the table of compartment mappings if we haven't done so already. 
				// The table contains an array of CompartmentMapping for every Type that this
				// shape can be mapped to. 
				compartmentMappings = new global::System.Collections.Generic.Dictionary<global::System.Type, DslDiagrams::CompartmentMapping[]>();
				{
					// First we get the mappings defined for the base shape, and add on any mappings defined for this
					// shape. 
					DslDiagrams::CompartmentMapping[] baseMappings = base.GetCompartmentMappings(typeof(global::Castle.ActiveWriter.ModelClass));
					int localCompartmentMappingsOffset = 0;
					if(baseMappings!=null)
					{
						localCompartmentMappingsOffset = baseMappings.Length;
					}
					DslDiagrams::CompartmentMapping[] mappings = new DslDiagrams::CompartmentMapping[1+localCompartmentMappingsOffset];
					
					if(baseMappings!=null)
					{
						baseMappings.CopyTo(mappings, 0);
					}
					mappings[localCompartmentMappingsOffset+0] = new DslDiagrams::ElementListCompartmentMapping(
																				"Properties", 
																				global::Castle.ActiveWriter.NamedElement.NameDomainPropertyId, 
																				global::Castle.ActiveWriter.ModelProperty.DomainClassId, 
																				GetElementsFromModelClassForProperties,
																				null,
																				null,
																				null);
					compartmentMappings.Add(typeof(global::Castle.ActiveWriter.ModelClass), mappings);
				}
			}
			
			// See if we can find the mapping being requested directly in the table. 
			DslDiagrams::CompartmentMapping[] returnValue;
			if(compartmentMappings.TryGetValue(melType, out returnValue))
			{
				return returnValue;
			}
			
			// If not, loop through the types in the table, and find the 'most derived' base
			// class of melType. 
			global::System.Type selectedMappedType = null;
			foreach(global::System.Type mappedType in compartmentMappings.Keys)
			{
				if(mappedType.IsAssignableFrom(melType) && (selectedMappedType==null || selectedMappedType.IsAssignableFrom(mappedType)))
				{
					selectedMappedType = mappedType;
				}
			}
			if(selectedMappedType!=null)
			{
				return compartmentMappings[selectedMappedType];
			}
			return new DslDiagrams::CompartmentMapping[] {};
		}
		
			#region DomainPath traversal methods to get the list of elements to display in a compartment.
			internal static global::System.Collections.IList GetElementsFromModelClassForProperties(DslModeling::ModelElement element)
			{
				global::Castle.ActiveWriter.ModelClass root = (global::Castle.ActiveWriter.ModelClass)element;
					// Segments 0 and 1
					DslModeling::LinkedElementCollection<global::Castle.ActiveWriter.ModelProperty> result = root.Properties;
				return result;
			}
			#endregion
		
		#endregion
		#region Constructors, domain class Id
	
		/// <summary>
		/// ClassShape domain class Id.
		/// </summary>
		public static readonly new global::System.Guid DomainClassId = new global::System.Guid(0xca45d586, 0x12d1, 0x4f5d, 0x99, 0xc7, 0x83, 0xc1, 0xeb, 0x0e, 0x61, 0xeb);
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="partition">Partition where new element is to be created.</param>
		/// <param name="propertyAssignments">List of domain property id/value pairs to set once the element is created.</param>
		protected ClassShapeBase(DslModeling::Partition partition, DslModeling::PropertyAssignment[] propertyAssignments)
			: base(partition, propertyAssignments)
		{
		}
		#endregion
	}
	/// <summary>
	/// DomainClass ClassShape
	/// </summary>
	[global::System.CLSCompliant(true)]
			
	public partial class ClassShape : ClassShapeBase
	{
		#region Constructors
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="store">Store where new element is to be created.</param>
		/// <param name="propertyAssignments">List of domain property id/value pairs to set once the element is created.</param>
		public ClassShape(DslModeling::Store store, params DslModeling::PropertyAssignment[] propertyAssignments)
			: this(store != null ? store.DefaultPartitionForClass(DomainClassId) : null, propertyAssignments)
		{
		}
		
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="partition">Partition where new element is to be created.</param>
		/// <param name="propertyAssignments">List of domain property id/value pairs to set once the element is created.</param>
		public ClassShape(DslModeling::Partition partition, params DslModeling::PropertyAssignment[] propertyAssignments)
			: base(partition, propertyAssignments)
		{
		}
		#endregion
	}
}
namespace Castle.ActiveWriter
{
	/// <summary>
	/// Double-derived base class for DomainClass NestedClassShape
	/// </summary>
	[DslDesign::DisplayNameResource("Castle.ActiveWriter.NestedClassShape.DisplayName", typeof(global::Castle.ActiveWriter.ActiveWriterDomainModel), "Castle.ActiveWriter.GeneratedCode.DomainModelResx")]
	[DslDesign::DescriptionResource("Castle.ActiveWriter.NestedClassShape.Description", typeof(global::Castle.ActiveWriter.ActiveWriterDomainModel), "Castle.ActiveWriter.GeneratedCode.DomainModelResx")]
	[DslModeling::DomainModelOwner(typeof(global::Castle.ActiveWriter.ActiveWriterDomainModel))]
	[global::System.CLSCompliant(true)]
	[DslModeling::DomainObjectId("db37440f-1f67-41af-869d-fb873cfa72a1")]
	public abstract partial class NestedClassShapeBase : DslDiagrams::CompartmentShape
	{
		#region DiagramElement boilerplate
		private static DslDiagrams::StyleSet classStyleSet;
		private static global::System.Collections.Generic.IList<DslDiagrams::ShapeField> shapeFields;
		private static global::System.Collections.Generic.IList<DslDiagrams::Decorator> decorators;
		
		/// <summary>
		/// Per-class style set for this shape.
		/// </summary>
		protected override DslDiagrams::StyleSet ClassStyleSet
		{
			get
			{
				if (classStyleSet == null)
				{
					classStyleSet = CreateClassStyleSet();
				}
				return classStyleSet;
			}
		}
		
		/// <summary>
		/// Per-class ShapeFields for this shape.
		/// </summary>
		public override global::System.Collections.Generic.IList<DslDiagrams::ShapeField> ShapeFields
		{
			get
			{
				if (shapeFields == null)
				{
					shapeFields = CreateShapeFields();
				}
				return shapeFields;
			}
		}
		
		/// <summary>
		/// Event fired when decorator initialization is complete for this shape type.
		/// </summary>
		public static event global::System.EventHandler DecoratorsInitialized;
		
		/// <summary>
		/// List containing decorators used by this type.
		/// </summary>
		public override global::System.Collections.Generic.IList<DslDiagrams::Decorator> Decorators
		{
			get 
			{
				if(decorators == null)
				{
					decorators = CreateDecorators();
					
					// fire this event to allow the diagram to initialize decorator mappings for this shape type.
					if(DecoratorsInitialized != null)
					{
						DecoratorsInitialized(this, global::System.EventArgs.Empty);
					}
				}
				
				return decorators; 
			}
		}
		
		/// <summary>
		/// Finds a decorator associated with NestedClassShape.
		/// </summary>
		public static DslDiagrams::Decorator FindNestedClassShapeDecorator(string decoratorName)
		{	
			if(decorators == null) return null;
			return DslDiagrams::ShapeElement.FindDecorator(decorators, decoratorName);
		}
		
		
		/// <summary>
		/// Shape instance initialization.
		/// </summary>
		public override void OnInitialize()
		{
			base.OnInitialize();
			
			// Create host shapes for outer decorators.
			foreach(DslDiagrams::Decorator decorator in Decorators)
			{
				if(decorator.RequiresHost)
				{
					decorator.ConfigureHostShape(this);
				}
			}
			
		}
		#endregion
		#region Shape size
		
		/// <summary>
		/// Default size for this shape.
		/// </summary>
		public override DslDiagrams::SizeD DefaultSize
		{
			get
			{
				return new DslDiagrams::SizeD(1.5, 0.4);
			}
		}
		#endregion
		#region Shape styles
		/// <summary>
		/// Initializes style set resources for this shape type
		/// </summary>
		/// <param name="classStyleSet">The style set for this shape class</param>
		protected override void InitializeResources(DslDiagrams::StyleSet classStyleSet)
		{
			base.InitializeResources(classStyleSet);
			
			// Fill brush settings for this shape.
			DslDiagrams::BrushSettings backgroundBrush = new DslDiagrams::BrushSettings();
			backgroundBrush.Color = global::System.Drawing.Color.FromKnownColor(global::System.Drawing.KnownColor.Khaki);
			classStyleSet.OverrideBrush(DslDiagrams::DiagramBrushes.ShapeBackground, backgroundBrush);
		
		}
		
		/// <summary>
		/// Indicates whether this shape displays a background gradient.
		/// </summary>
		public override bool HasBackgroundGradient
		{
			get
			{
				return true;
			}
		}
		
		/// <summary>
		/// Indicates the direction of the gradient.
		/// </summary>
		public override global::System.Drawing.Drawing2D.LinearGradientMode BackgroundGradientMode
		{
			get
			{
				return global::System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
			}
		}
		/// <summary>
		/// Specifies the geometry used by this shape
		/// </summary>
		public override DslDiagrams::ShapeGeometry ShapeGeometry
		{
			get
			{
				return DslDiagrams::ShapeGeometries.RoundedRectangle;
			}
		}
		#endregion
		#region Decorators
		/// <summary>
		/// Initialize the collection of shape fields associated with this shape type.
		/// </summary>
		protected override void InitializeShapeFields(global::System.Collections.Generic.IList<DslDiagrams::ShapeField> shapeFields)
		{
			base.InitializeShapeFields(shapeFields);
			DslDiagrams::TextField field1 = new DslDiagrams::TextField("Name");
			field1.DefaultText = global::Castle.ActiveWriter.ActiveWriterDomainModel.SingletonResourceManager.GetString("NestedClassShapeNameDefaultText");
			field1.DefaultFocusable = true;
			field1.DefaultAutoSize = true;
			field1.AnchoringBehavior.MinimumHeightInLines = 1;
			field1.AnchoringBehavior.MinimumWidthInCharacters = 1;
			field1.DefaultAccessibleState = global::System.Windows.Forms.AccessibleStates.Invisible;
			shapeFields.Add(field1);
			
			DslDiagrams::ChevronButtonField field2 = new DslDiagrams::ChevronButtonField("ExpandCollapse");
			field2.DefaultSelectable = false;
			field2.DefaultFocusable = false;
			shapeFields.Add(field2);
			
		}
		
		/// <summary>
		/// Initialize the collection of decorators associated with this shape type.  This method also
		/// creates shape fields for outer decorators, because these are not part of the shape fields collection
		/// associated with the shape, so they must be created here rather than in InitializeShapeFields.
		/// </summary>
		protected override void InitializeDecorators(global::System.Collections.Generic.IList<DslDiagrams::ShapeField> shapeFields, global::System.Collections.Generic.IList<DslDiagrams::Decorator> decorators)
		{
			base.InitializeDecorators(shapeFields, decorators);
			
			DslDiagrams::ShapeField field1 = DslDiagrams::ShapeElement.FindShapeField(shapeFields, "Name");
			DslDiagrams::Decorator decorator1 = new DslDiagrams::ShapeDecorator(field1, DslDiagrams::ShapeDecoratorPosition.InnerTopCenter, DslDiagrams::PointD.Empty);
			decorators.Add(decorator1);
				
			DslDiagrams::ShapeField field2 = DslDiagrams::ShapeElement.FindShapeField(shapeFields, "ExpandCollapse");
			DslDiagrams::Decorator decorator2 = new DslDiagrams::ExpandCollapseDecorator(Store, (DslDiagrams::ToggleButtonField)field2, DslDiagrams::ShapeDecoratorPosition.InnerTopRight, DslDiagrams::PointD.Empty);
			decorators.Add(decorator2);
				
		}
		
		/// <summary>
		/// Ensure outer decorators are placed appropriately.  This is called during view fixup,
		/// after the shape has been associated with the model element.
		/// </summary>
		public override void OnBoundsFixup(DslDiagrams::BoundsFixupState fixupState, int iteration, bool createdDuringViewFixup)
		{
			base.OnBoundsFixup(fixupState, iteration, createdDuringViewFixup);
			
			if(iteration == 0)
			{
				foreach(DslDiagrams::Decorator decorator in Decorators)
				{
					if(decorator.RequiresHost)
					{
						decorator.RepositionHostShape(decorator.GetHostShape(this));
					}
				}
			}
		}
		#endregion
		#region CompartmentShape code
		/// <summary>
		/// Returns a value indicating whether compartment header should be visible if there is only one of them.
		/// </summary>
		public override bool IsSingleCompartmentHeaderVisible
		{
			get { return true; }
		}
		
		private static DslDiagrams::CompartmentDescription[] compartmentDescriptions;
		
		/// <summary>
		/// Gets an array of CompartmentDescription for all compartments shown on this shape
		/// (including compartments defined on base shapes).
		/// </summary>
		/// <returns></returns>
		public override DslDiagrams::CompartmentDescription[] GetCompartmentDescriptions()
		{
			if(compartmentDescriptions == null)
			{
				// Initialize the array of compartment descriptions if we haven't done so already. 
				// First we get any compartment descriptions in base shapes, and add on any compartments
				// that are defined on this shape. 
				DslDiagrams::CompartmentDescription[] baseCompartmentDescriptions = base.GetCompartmentDescriptions();
				
				int localCompartmentsOffset = 0;
				if(baseCompartmentDescriptions!=null)
				{
					localCompartmentsOffset = baseCompartmentDescriptions.Length;
				}
				compartmentDescriptions = new DslDiagrams::ElementListCompartmentDescription[1+localCompartmentsOffset];
				
				if(baseCompartmentDescriptions!=null)
				{
					baseCompartmentDescriptions.CopyTo(compartmentDescriptions, 0);	
				}
				{
					string title = global::Castle.ActiveWriter.ActiveWriterDomainModel.SingletonResourceManager.GetString("NestedClassShapePropertiesTitle");
					DslDiagrams::ElementListCompartmentDescription descriptor = new DslDiagrams::ElementListCompartmentDescription("Properties", title, 
						global::System.Drawing.Color.FromKnownColor(global::System.Drawing.KnownColor.LightGray), false, 
						global::System.Drawing.Color.FromKnownColor(global::System.Drawing.KnownColor.White), false,
						null, null,
						false);
					compartmentDescriptions[localCompartmentsOffset+0] = descriptor;
				}
			}
			
			return NestedClassShape.compartmentDescriptions;
		}
		
		private static global::System.Collections.Generic.Dictionary<global::System.Type, DslDiagrams::CompartmentMapping[]> compartmentMappings;
		
		/// <summary>
		/// Gets an array of CompartmentMappings for all compartments displayed on this shape
		/// (including compartment maps defined on base shapes). 
		/// </summary>
		/// <param name="melType">The type of the DomainClass that this shape is mapped to</param>
		/// <returns></returns>
		protected override DslDiagrams::CompartmentMapping[] GetCompartmentMappings(global::System.Type melType)
		{
			if(melType==null) throw new global::System.ArgumentNullException("melType");
			
			if(compartmentMappings==null)
			{
				// Initialize the table of compartment mappings if we haven't done so already. 
				// The table contains an array of CompartmentMapping for every Type that this
				// shape can be mapped to. 
				compartmentMappings = new global::System.Collections.Generic.Dictionary<global::System.Type, DslDiagrams::CompartmentMapping[]>();
				{
					// First we get the mappings defined for the base shape, and add on any mappings defined for this
					// shape. 
					DslDiagrams::CompartmentMapping[] baseMappings = base.GetCompartmentMappings(typeof(global::Castle.ActiveWriter.NestedClass));
					int localCompartmentMappingsOffset = 0;
					if(baseMappings!=null)
					{
						localCompartmentMappingsOffset = baseMappings.Length;
					}
					DslDiagrams::CompartmentMapping[] mappings = new DslDiagrams::CompartmentMapping[1+localCompartmentMappingsOffset];
					
					if(baseMappings!=null)
					{
						baseMappings.CopyTo(mappings, 0);
					}
					mappings[localCompartmentMappingsOffset+0] = new DslDiagrams::ElementListCompartmentMapping(
																				"Properties", 
																				global::Castle.ActiveWriter.NamedElement.NameDomainPropertyId, 
																				global::Castle.ActiveWriter.ModelProperty.DomainClassId, 
																				GetElementsFromNestedClassForProperties,
																				null,
																				null,
																				null);
					compartmentMappings.Add(typeof(global::Castle.ActiveWriter.NestedClass), mappings);
				}
			}
			
			// See if we can find the mapping being requested directly in the table. 
			DslDiagrams::CompartmentMapping[] returnValue;
			if(compartmentMappings.TryGetValue(melType, out returnValue))
			{
				return returnValue;
			}
			
			// If not, loop through the types in the table, and find the 'most derived' base
			// class of melType. 
			global::System.Type selectedMappedType = null;
			foreach(global::System.Type mappedType in compartmentMappings.Keys)
			{
				if(mappedType.IsAssignableFrom(melType) && (selectedMappedType==null || selectedMappedType.IsAssignableFrom(mappedType)))
				{
					selectedMappedType = mappedType;
				}
			}
			if(selectedMappedType!=null)
			{
				return compartmentMappings[selectedMappedType];
			}
			return new DslDiagrams::CompartmentMapping[] {};
		}
		
			#region DomainPath traversal methods to get the list of elements to display in a compartment.
			internal static global::System.Collections.IList GetElementsFromNestedClassForProperties(DslModeling::ModelElement element)
			{
				global::Castle.ActiveWriter.NestedClass root = (global::Castle.ActiveWriter.NestedClass)element;
					// Segments 0 and 1
					DslModeling::LinkedElementCollection<global::Castle.ActiveWriter.ModelProperty> result = root.Properties;
				return result;
			}
			#endregion
		
		#endregion
		#region Constructors, domain class Id
	
		/// <summary>
		/// NestedClassShape domain class Id.
		/// </summary>
		public static readonly new global::System.Guid DomainClassId = new global::System.Guid(0xdb37440f, 0x1f67, 0x41af, 0x86, 0x9d, 0xfb, 0x87, 0x3c, 0xfa, 0x72, 0xa1);
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="partition">Partition where new element is to be created.</param>
		/// <param name="propertyAssignments">List of domain property id/value pairs to set once the element is created.</param>
		protected NestedClassShapeBase(DslModeling::Partition partition, DslModeling::PropertyAssignment[] propertyAssignments)
			: base(partition, propertyAssignments)
		{
		}
		#endregion
	}
	/// <summary>
	/// DomainClass NestedClassShape
	/// </summary>
	[global::System.CLSCompliant(true)]
			
	public partial class NestedClassShape : NestedClassShapeBase
	{
		#region Constructors
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="store">Store where new element is to be created.</param>
		/// <param name="propertyAssignments">List of domain property id/value pairs to set once the element is created.</param>
		public NestedClassShape(DslModeling::Store store, params DslModeling::PropertyAssignment[] propertyAssignments)
			: this(store != null ? store.DefaultPartitionForClass(DomainClassId) : null, propertyAssignments)
		{
		}
		
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="partition">Partition where new element is to be created.</param>
		/// <param name="propertyAssignments">List of domain property id/value pairs to set once the element is created.</param>
		public NestedClassShape(DslModeling::Partition partition, params DslModeling::PropertyAssignment[] propertyAssignments)
			: base(partition, propertyAssignments)
		{
		}
		#endregion
	}
}

