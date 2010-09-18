#region License
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
namespace ICSharpCode.NRefactory.Visitors {
	using System.Collections.Generic;
	using System.Diagnostics;
	using Ast;

	/// <summary>
	/// The AbstractAstTransformer will iterate through the whole AST,
	/// just like the AbstractAstVisitor. However, the AbstractAstTransformer allows
	/// you to modify the AST at the same time: It does not use 'foreach' internally,
	/// so you can add members to collections of parents of the current node (but
	/// you cannot insert or delete items as that will make the index used invalid).
	/// You can use the methods ReplaceCurrentNode and RemoveCurrentNode to replace
	/// or remove the current node, totally independent from the type of the parent node.
	/// </summary>
	public abstract class AbstractAstTransformer : IAstVisitor {
		
		private Stack<INode> nodeStack = new Stack<INode>();
		
		public void ReplaceCurrentNode(INode newNode) {
			nodeStack.Pop();
			nodeStack.Push(newNode);
		}
		
		public void RemoveCurrentNode() {
			nodeStack.Pop();
			nodeStack.Push(null);
		}
		
		public virtual object VisitAddHandlerStatement(AddHandlerStatement addHandlerStatement, object data) {
			Debug.Assert((addHandlerStatement != null));
			Debug.Assert((addHandlerStatement.EventExpression != null));
			Debug.Assert((addHandlerStatement.HandlerExpression != null));
			nodeStack.Push(addHandlerStatement.EventExpression);
			addHandlerStatement.EventExpression.AcceptVisitor(this, data);
			addHandlerStatement.EventExpression = ((Expression)(nodeStack.Pop()));
			nodeStack.Push(addHandlerStatement.HandlerExpression);
			addHandlerStatement.HandlerExpression.AcceptVisitor(this, data);
			addHandlerStatement.HandlerExpression = ((Expression)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitAddressOfExpression(AddressOfExpression addressOfExpression, object data) {
			Debug.Assert((addressOfExpression != null));
			Debug.Assert((addressOfExpression.Expression != null));
			nodeStack.Push(addressOfExpression.Expression);
			addressOfExpression.Expression.AcceptVisitor(this, data);
			addressOfExpression.Expression = ((Expression)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitAnonymousMethodExpression(AnonymousMethodExpression anonymousMethodExpression, object data) {
			Debug.Assert((anonymousMethodExpression != null));
			Debug.Assert((anonymousMethodExpression.Parameters != null));
			Debug.Assert((anonymousMethodExpression.Body != null));
			for (int i = 0; i < anonymousMethodExpression.Parameters.Count; i++) {
				ParameterDeclarationExpression o = anonymousMethodExpression.Parameters[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (ParameterDeclarationExpression)nodeStack.Pop();
				if (o == null)
					anonymousMethodExpression.Parameters.RemoveAt(i--);
				else
					anonymousMethodExpression.Parameters[i] = o;
			}
			nodeStack.Push(anonymousMethodExpression.Body);
			anonymousMethodExpression.Body.AcceptVisitor(this, data);
			anonymousMethodExpression.Body = ((BlockStatement)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitArrayCreateExpression(ArrayCreateExpression arrayCreateExpression, object data) {
			Debug.Assert((arrayCreateExpression != null));
			Debug.Assert((arrayCreateExpression.CreateType != null));
			Debug.Assert((arrayCreateExpression.Arguments != null));
			Debug.Assert((arrayCreateExpression.ArrayInitializer != null));
			nodeStack.Push(arrayCreateExpression.CreateType);
			arrayCreateExpression.CreateType.AcceptVisitor(this, data);
			arrayCreateExpression.CreateType = ((TypeReference)(nodeStack.Pop()));
			for (int i = 0; i < arrayCreateExpression.Arguments.Count; i++) {
				Expression o = arrayCreateExpression.Arguments[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (Expression)nodeStack.Pop();
				if (o == null)
					arrayCreateExpression.Arguments.RemoveAt(i--);
				else
					arrayCreateExpression.Arguments[i] = o;
			}
			nodeStack.Push(arrayCreateExpression.ArrayInitializer);
			arrayCreateExpression.ArrayInitializer.AcceptVisitor(this, data);
			arrayCreateExpression.ArrayInitializer = ((CollectionInitializerExpression)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitAssignmentExpression(AssignmentExpression assignmentExpression, object data) {
			Debug.Assert((assignmentExpression != null));
			Debug.Assert((assignmentExpression.Left != null));
			Debug.Assert((assignmentExpression.Right != null));
			nodeStack.Push(assignmentExpression.Left);
			assignmentExpression.Left.AcceptVisitor(this, data);
			assignmentExpression.Left = ((Expression)(nodeStack.Pop()));
			nodeStack.Push(assignmentExpression.Right);
			assignmentExpression.Right.AcceptVisitor(this, data);
			assignmentExpression.Right = ((Expression)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitAttribute(ICSharpCode.NRefactory.Ast.Attribute attribute, object data) {
			Debug.Assert((attribute != null));
			Debug.Assert((attribute.PositionalArguments != null));
			Debug.Assert((attribute.NamedArguments != null));
			for (int i = 0; i < attribute.PositionalArguments.Count; i++) {
				Expression o = attribute.PositionalArguments[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (Expression)nodeStack.Pop();
				if (o == null)
					attribute.PositionalArguments.RemoveAt(i--);
				else
					attribute.PositionalArguments[i] = o;
			}
			for (int i = 0; i < attribute.NamedArguments.Count; i++) {
				NamedArgumentExpression o = attribute.NamedArguments[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (NamedArgumentExpression)nodeStack.Pop();
				if (o == null)
					attribute.NamedArguments.RemoveAt(i--);
				else
					attribute.NamedArguments[i] = o;
			}
			return null;
		}
		
		public virtual object VisitAttributeSection(AttributeSection attributeSection, object data) {
			Debug.Assert((attributeSection != null));
			Debug.Assert((attributeSection.Attributes != null));
			for (int i = 0; i < attributeSection.Attributes.Count; i++) {
				ICSharpCode.NRefactory.Ast.Attribute o = attributeSection.Attributes[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (ICSharpCode.NRefactory.Ast.Attribute)nodeStack.Pop();
				if (o == null)
					attributeSection.Attributes.RemoveAt(i--);
				else
					attributeSection.Attributes[i] = o;
			}
			return null;
		}
		
		public virtual object VisitBaseReferenceExpression(BaseReferenceExpression baseReferenceExpression, object data) {
			Debug.Assert((baseReferenceExpression != null));
			return null;
		}
		
		public virtual object VisitBinaryOperatorExpression(BinaryOperatorExpression binaryOperatorExpression, object data) {
			Debug.Assert((binaryOperatorExpression != null));
			Debug.Assert((binaryOperatorExpression.Left != null));
			Debug.Assert((binaryOperatorExpression.Right != null));
			nodeStack.Push(binaryOperatorExpression.Left);
			binaryOperatorExpression.Left.AcceptVisitor(this, data);
			binaryOperatorExpression.Left = ((Expression)(nodeStack.Pop()));
			nodeStack.Push(binaryOperatorExpression.Right);
			binaryOperatorExpression.Right.AcceptVisitor(this, data);
			binaryOperatorExpression.Right = ((Expression)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitBlockStatement(BlockStatement blockStatement, object data) {
			Debug.Assert((blockStatement != null));
			for (int i = 0; i < blockStatement.Children.Count; i++) {
				INode o = blockStatement.Children[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = nodeStack.Pop();
				if (o == null)
					blockStatement.Children.RemoveAt(i--);
				else
					blockStatement.Children[i] = o;
			}
			return null;
		}
		
		public virtual object VisitBreakStatement(BreakStatement breakStatement, object data) {
			Debug.Assert((breakStatement != null));
			return null;
		}
		
		public virtual object VisitCaseLabel(CaseLabel caseLabel, object data) {
			Debug.Assert((caseLabel != null));
			Debug.Assert((caseLabel.Label != null));
			Debug.Assert((caseLabel.ToExpression != null));
			nodeStack.Push(caseLabel.Label);
			caseLabel.Label.AcceptVisitor(this, data);
			caseLabel.Label = ((Expression)(nodeStack.Pop()));
			nodeStack.Push(caseLabel.ToExpression);
			caseLabel.ToExpression.AcceptVisitor(this, data);
			caseLabel.ToExpression = ((Expression)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitCastExpression(CastExpression castExpression, object data) {
			Debug.Assert((castExpression != null));
			Debug.Assert((castExpression.CastTo != null));
			Debug.Assert((castExpression.Expression != null));
			nodeStack.Push(castExpression.CastTo);
			castExpression.CastTo.AcceptVisitor(this, data);
			castExpression.CastTo = ((TypeReference)(nodeStack.Pop()));
			nodeStack.Push(castExpression.Expression);
			castExpression.Expression.AcceptVisitor(this, data);
			castExpression.Expression = ((Expression)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitCatchClause(CatchClause catchClause, object data) {
			Debug.Assert((catchClause != null));
			Debug.Assert((catchClause.TypeReference != null));
			Debug.Assert((catchClause.StatementBlock != null));
			Debug.Assert((catchClause.Condition != null));
			nodeStack.Push(catchClause.TypeReference);
			catchClause.TypeReference.AcceptVisitor(this, data);
			catchClause.TypeReference = ((TypeReference)(nodeStack.Pop()));
			nodeStack.Push(catchClause.StatementBlock);
			catchClause.StatementBlock.AcceptVisitor(this, data);
			catchClause.StatementBlock = ((Statement)(nodeStack.Pop()));
			nodeStack.Push(catchClause.Condition);
			catchClause.Condition.AcceptVisitor(this, data);
			catchClause.Condition = ((Expression)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitCheckedExpression(CheckedExpression checkedExpression, object data) {
			Debug.Assert((checkedExpression != null));
			Debug.Assert((checkedExpression.Expression != null));
			nodeStack.Push(checkedExpression.Expression);
			checkedExpression.Expression.AcceptVisitor(this, data);
			checkedExpression.Expression = ((Expression)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitCheckedStatement(CheckedStatement checkedStatement, object data) {
			Debug.Assert((checkedStatement != null));
			Debug.Assert((checkedStatement.Block != null));
			nodeStack.Push(checkedStatement.Block);
			checkedStatement.Block.AcceptVisitor(this, data);
			checkedStatement.Block = ((Statement)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitClassReferenceExpression(ClassReferenceExpression classReferenceExpression, object data) {
			Debug.Assert((classReferenceExpression != null));
			return null;
		}
		
		public virtual object VisitCollectionInitializerExpression(CollectionInitializerExpression collectionInitializerExpression, object data) {
			Debug.Assert((collectionInitializerExpression != null));
			Debug.Assert((collectionInitializerExpression.CreateExpressions != null));
			for (int i = 0; i < collectionInitializerExpression.CreateExpressions.Count; i++) {
				Expression o = collectionInitializerExpression.CreateExpressions[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (Expression)nodeStack.Pop();
				if (o == null)
					collectionInitializerExpression.CreateExpressions.RemoveAt(i--);
				else
					collectionInitializerExpression.CreateExpressions[i] = o;
			}
			return null;
		}
		
		public virtual object VisitCompilationUnit(CompilationUnit compilationUnit, object data) {
			Debug.Assert((compilationUnit != null));
			for (int i = 0; i < compilationUnit.Children.Count; i++) {
				INode o = compilationUnit.Children[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = nodeStack.Pop();
				if (o == null)
					compilationUnit.Children.RemoveAt(i--);
				else
					compilationUnit.Children[i] = o;
			}
			return null;
		}
		
		public virtual object VisitConditionalExpression(ConditionalExpression conditionalExpression, object data) {
			Debug.Assert((conditionalExpression != null));
			Debug.Assert((conditionalExpression.Condition != null));
			Debug.Assert((conditionalExpression.TrueExpression != null));
			Debug.Assert((conditionalExpression.FalseExpression != null));
			nodeStack.Push(conditionalExpression.Condition);
			conditionalExpression.Condition.AcceptVisitor(this, data);
			conditionalExpression.Condition = ((Expression)(nodeStack.Pop()));
			nodeStack.Push(conditionalExpression.TrueExpression);
			conditionalExpression.TrueExpression.AcceptVisitor(this, data);
			conditionalExpression.TrueExpression = ((Expression)(nodeStack.Pop()));
			nodeStack.Push(conditionalExpression.FalseExpression);
			conditionalExpression.FalseExpression.AcceptVisitor(this, data);
			conditionalExpression.FalseExpression = ((Expression)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitConstructorDeclaration(ConstructorDeclaration constructorDeclaration, object data) {
			Debug.Assert((constructorDeclaration != null));
			Debug.Assert((constructorDeclaration.Attributes != null));
			Debug.Assert((constructorDeclaration.Parameters != null));
			Debug.Assert((constructorDeclaration.ConstructorInitializer != null));
			Debug.Assert((constructorDeclaration.Body != null));
			for (int i = 0; i < constructorDeclaration.Attributes.Count; i++) {
				AttributeSection o = constructorDeclaration.Attributes[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (AttributeSection)nodeStack.Pop();
				if (o == null)
					constructorDeclaration.Attributes.RemoveAt(i--);
				else
					constructorDeclaration.Attributes[i] = o;
			}
			for (int i = 0; i < constructorDeclaration.Parameters.Count; i++) {
				ParameterDeclarationExpression o = constructorDeclaration.Parameters[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (ParameterDeclarationExpression)nodeStack.Pop();
				if (o == null)
					constructorDeclaration.Parameters.RemoveAt(i--);
				else
					constructorDeclaration.Parameters[i] = o;
			}
			nodeStack.Push(constructorDeclaration.ConstructorInitializer);
			constructorDeclaration.ConstructorInitializer.AcceptVisitor(this, data);
			constructorDeclaration.ConstructorInitializer = ((ConstructorInitializer)(nodeStack.Pop()));
			nodeStack.Push(constructorDeclaration.Body);
			constructorDeclaration.Body.AcceptVisitor(this, data);
			constructorDeclaration.Body = ((BlockStatement)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitConstructorInitializer(ConstructorInitializer constructorInitializer, object data) {
			Debug.Assert((constructorInitializer != null));
			Debug.Assert((constructorInitializer.Arguments != null));
			for (int i = 0; i < constructorInitializer.Arguments.Count; i++) {
				Expression o = constructorInitializer.Arguments[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (Expression)nodeStack.Pop();
				if (o == null)
					constructorInitializer.Arguments.RemoveAt(i--);
				else
					constructorInitializer.Arguments[i] = o;
			}
			return null;
		}
		
		public virtual object VisitContinueStatement(ContinueStatement continueStatement, object data) {
			Debug.Assert((continueStatement != null));
			return null;
		}
		
		public virtual object VisitDeclareDeclaration(DeclareDeclaration declareDeclaration, object data) {
			Debug.Assert((declareDeclaration != null));
			Debug.Assert((declareDeclaration.Attributes != null));
			Debug.Assert((declareDeclaration.Parameters != null));
			Debug.Assert((declareDeclaration.TypeReference != null));
			for (int i = 0; i < declareDeclaration.Attributes.Count; i++) {
				AttributeSection o = declareDeclaration.Attributes[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (AttributeSection)nodeStack.Pop();
				if (o == null)
					declareDeclaration.Attributes.RemoveAt(i--);
				else
					declareDeclaration.Attributes[i] = o;
			}
			for (int i = 0; i < declareDeclaration.Parameters.Count; i++) {
				ParameterDeclarationExpression o = declareDeclaration.Parameters[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (ParameterDeclarationExpression)nodeStack.Pop();
				if (o == null)
					declareDeclaration.Parameters.RemoveAt(i--);
				else
					declareDeclaration.Parameters[i] = o;
			}
			nodeStack.Push(declareDeclaration.TypeReference);
			declareDeclaration.TypeReference.AcceptVisitor(this, data);
			declareDeclaration.TypeReference = ((TypeReference)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitDefaultValueExpression(DefaultValueExpression defaultValueExpression, object data) {
			Debug.Assert((defaultValueExpression != null));
			Debug.Assert((defaultValueExpression.TypeReference != null));
			nodeStack.Push(defaultValueExpression.TypeReference);
			defaultValueExpression.TypeReference.AcceptVisitor(this, data);
			defaultValueExpression.TypeReference = ((TypeReference)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitDelegateDeclaration(DelegateDeclaration delegateDeclaration, object data) {
			Debug.Assert((delegateDeclaration != null));
			Debug.Assert((delegateDeclaration.Attributes != null));
			Debug.Assert((delegateDeclaration.ReturnType != null));
			Debug.Assert((delegateDeclaration.Parameters != null));
			Debug.Assert((delegateDeclaration.Templates != null));
			for (int i = 0; i < delegateDeclaration.Attributes.Count; i++) {
				AttributeSection o = delegateDeclaration.Attributes[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (AttributeSection)nodeStack.Pop();
				if (o == null)
					delegateDeclaration.Attributes.RemoveAt(i--);
				else
					delegateDeclaration.Attributes[i] = o;
			}
			nodeStack.Push(delegateDeclaration.ReturnType);
			delegateDeclaration.ReturnType.AcceptVisitor(this, data);
			delegateDeclaration.ReturnType = ((TypeReference)(nodeStack.Pop()));
			for (int i = 0; i < delegateDeclaration.Parameters.Count; i++) {
				ParameterDeclarationExpression o = delegateDeclaration.Parameters[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (ParameterDeclarationExpression)nodeStack.Pop();
				if (o == null)
					delegateDeclaration.Parameters.RemoveAt(i--);
				else
					delegateDeclaration.Parameters[i] = o;
			}
			for (int i = 0; i < delegateDeclaration.Templates.Count; i++) {
				TemplateDefinition o = delegateDeclaration.Templates[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (TemplateDefinition)nodeStack.Pop();
				if (o == null)
					delegateDeclaration.Templates.RemoveAt(i--);
				else
					delegateDeclaration.Templates[i] = o;
			}
			return null;
		}
		
		public virtual object VisitDestructorDeclaration(DestructorDeclaration destructorDeclaration, object data) {
			Debug.Assert((destructorDeclaration != null));
			Debug.Assert((destructorDeclaration.Attributes != null));
			Debug.Assert((destructorDeclaration.Body != null));
			for (int i = 0; i < destructorDeclaration.Attributes.Count; i++) {
				AttributeSection o = destructorDeclaration.Attributes[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (AttributeSection)nodeStack.Pop();
				if (o == null)
					destructorDeclaration.Attributes.RemoveAt(i--);
				else
					destructorDeclaration.Attributes[i] = o;
			}
			nodeStack.Push(destructorDeclaration.Body);
			destructorDeclaration.Body.AcceptVisitor(this, data);
			destructorDeclaration.Body = ((BlockStatement)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitDirectionExpression(DirectionExpression directionExpression, object data) {
			Debug.Assert((directionExpression != null));
			Debug.Assert((directionExpression.Expression != null));
			nodeStack.Push(directionExpression.Expression);
			directionExpression.Expression.AcceptVisitor(this, data);
			directionExpression.Expression = ((Expression)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitDoLoopStatement(DoLoopStatement doLoopStatement, object data) {
			Debug.Assert((doLoopStatement != null));
			Debug.Assert((doLoopStatement.Condition != null));
			Debug.Assert((doLoopStatement.EmbeddedStatement != null));
			nodeStack.Push(doLoopStatement.Condition);
			doLoopStatement.Condition.AcceptVisitor(this, data);
			doLoopStatement.Condition = ((Expression)(nodeStack.Pop()));
			nodeStack.Push(doLoopStatement.EmbeddedStatement);
			doLoopStatement.EmbeddedStatement.AcceptVisitor(this, data);
			doLoopStatement.EmbeddedStatement = ((Statement)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitElseIfSection(ElseIfSection elseIfSection, object data) {
			Debug.Assert((elseIfSection != null));
			Debug.Assert((elseIfSection.Condition != null));
			Debug.Assert((elseIfSection.EmbeddedStatement != null));
			nodeStack.Push(elseIfSection.Condition);
			elseIfSection.Condition.AcceptVisitor(this, data);
			elseIfSection.Condition = ((Expression)(nodeStack.Pop()));
			nodeStack.Push(elseIfSection.EmbeddedStatement);
			elseIfSection.EmbeddedStatement.AcceptVisitor(this, data);
			elseIfSection.EmbeddedStatement = ((Statement)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitEmptyStatement(EmptyStatement emptyStatement, object data) {
			Debug.Assert((emptyStatement != null));
			return null;
		}
		
		public virtual object VisitEndStatement(EndStatement endStatement, object data) {
			Debug.Assert((endStatement != null));
			return null;
		}
		
		public virtual object VisitEraseStatement(EraseStatement eraseStatement, object data) {
			Debug.Assert((eraseStatement != null));
			Debug.Assert((eraseStatement.Expressions != null));
			for (int i = 0; i < eraseStatement.Expressions.Count; i++) {
				Expression o = eraseStatement.Expressions[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (Expression)nodeStack.Pop();
				if (o == null)
					eraseStatement.Expressions.RemoveAt(i--);
				else
					eraseStatement.Expressions[i] = o;
			}
			return null;
		}
		
		public virtual object VisitErrorStatement(ErrorStatement errorStatement, object data) {
			Debug.Assert((errorStatement != null));
			Debug.Assert((errorStatement.Expression != null));
			nodeStack.Push(errorStatement.Expression);
			errorStatement.Expression.AcceptVisitor(this, data);
			errorStatement.Expression = ((Expression)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitEventAddRegion(EventAddRegion eventAddRegion, object data) {
			Debug.Assert((eventAddRegion != null));
			Debug.Assert((eventAddRegion.Attributes != null));
			Debug.Assert((eventAddRegion.Block != null));
			Debug.Assert((eventAddRegion.Parameters != null));
			for (int i = 0; i < eventAddRegion.Attributes.Count; i++) {
				AttributeSection o = eventAddRegion.Attributes[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (AttributeSection)nodeStack.Pop();
				if (o == null)
					eventAddRegion.Attributes.RemoveAt(i--);
				else
					eventAddRegion.Attributes[i] = o;
			}
			nodeStack.Push(eventAddRegion.Block);
			eventAddRegion.Block.AcceptVisitor(this, data);
			eventAddRegion.Block = ((BlockStatement)(nodeStack.Pop()));
			for (int i = 0; i < eventAddRegion.Parameters.Count; i++) {
				ParameterDeclarationExpression o = eventAddRegion.Parameters[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (ParameterDeclarationExpression)nodeStack.Pop();
				if (o == null)
					eventAddRegion.Parameters.RemoveAt(i--);
				else
					eventAddRegion.Parameters[i] = o;
			}
			return null;
		}
		
		public virtual object VisitEventDeclaration(EventDeclaration eventDeclaration, object data) {
			Debug.Assert((eventDeclaration != null));
			Debug.Assert((eventDeclaration.Attributes != null));
			Debug.Assert((eventDeclaration.Parameters != null));
			Debug.Assert((eventDeclaration.InterfaceImplementations != null));
			Debug.Assert((eventDeclaration.TypeReference != null));
			Debug.Assert((eventDeclaration.AddRegion != null));
			Debug.Assert((eventDeclaration.RemoveRegion != null));
			Debug.Assert((eventDeclaration.RaiseRegion != null));
			Debug.Assert((eventDeclaration.Initializer != null));
			for (int i = 0; i < eventDeclaration.Attributes.Count; i++) {
				AttributeSection o = eventDeclaration.Attributes[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (AttributeSection)nodeStack.Pop();
				if (o == null)
					eventDeclaration.Attributes.RemoveAt(i--);
				else
					eventDeclaration.Attributes[i] = o;
			}
			for (int i = 0; i < eventDeclaration.Parameters.Count; i++) {
				ParameterDeclarationExpression o = eventDeclaration.Parameters[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (ParameterDeclarationExpression)nodeStack.Pop();
				if (o == null)
					eventDeclaration.Parameters.RemoveAt(i--);
				else
					eventDeclaration.Parameters[i] = o;
			}
			for (int i = 0; i < eventDeclaration.InterfaceImplementations.Count; i++) {
				InterfaceImplementation o = eventDeclaration.InterfaceImplementations[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (InterfaceImplementation)nodeStack.Pop();
				if (o == null)
					eventDeclaration.InterfaceImplementations.RemoveAt(i--);
				else
					eventDeclaration.InterfaceImplementations[i] = o;
			}
			nodeStack.Push(eventDeclaration.TypeReference);
			eventDeclaration.TypeReference.AcceptVisitor(this, data);
			eventDeclaration.TypeReference = ((TypeReference)(nodeStack.Pop()));
			nodeStack.Push(eventDeclaration.AddRegion);
			eventDeclaration.AddRegion.AcceptVisitor(this, data);
			eventDeclaration.AddRegion = ((EventAddRegion)(nodeStack.Pop()));
			nodeStack.Push(eventDeclaration.RemoveRegion);
			eventDeclaration.RemoveRegion.AcceptVisitor(this, data);
			eventDeclaration.RemoveRegion = ((EventRemoveRegion)(nodeStack.Pop()));
			nodeStack.Push(eventDeclaration.RaiseRegion);
			eventDeclaration.RaiseRegion.AcceptVisitor(this, data);
			eventDeclaration.RaiseRegion = ((EventRaiseRegion)(nodeStack.Pop()));
			nodeStack.Push(eventDeclaration.Initializer);
			eventDeclaration.Initializer.AcceptVisitor(this, data);
			eventDeclaration.Initializer = ((Expression)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitEventRaiseRegion(EventRaiseRegion eventRaiseRegion, object data) {
			Debug.Assert((eventRaiseRegion != null));
			Debug.Assert((eventRaiseRegion.Attributes != null));
			Debug.Assert((eventRaiseRegion.Block != null));
			Debug.Assert((eventRaiseRegion.Parameters != null));
			for (int i = 0; i < eventRaiseRegion.Attributes.Count; i++) {
				AttributeSection o = eventRaiseRegion.Attributes[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (AttributeSection)nodeStack.Pop();
				if (o == null)
					eventRaiseRegion.Attributes.RemoveAt(i--);
				else
					eventRaiseRegion.Attributes[i] = o;
			}
			nodeStack.Push(eventRaiseRegion.Block);
			eventRaiseRegion.Block.AcceptVisitor(this, data);
			eventRaiseRegion.Block = ((BlockStatement)(nodeStack.Pop()));
			for (int i = 0; i < eventRaiseRegion.Parameters.Count; i++) {
				ParameterDeclarationExpression o = eventRaiseRegion.Parameters[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (ParameterDeclarationExpression)nodeStack.Pop();
				if (o == null)
					eventRaiseRegion.Parameters.RemoveAt(i--);
				else
					eventRaiseRegion.Parameters[i] = o;
			}
			return null;
		}
		
		public virtual object VisitEventRemoveRegion(EventRemoveRegion eventRemoveRegion, object data) {
			Debug.Assert((eventRemoveRegion != null));
			Debug.Assert((eventRemoveRegion.Attributes != null));
			Debug.Assert((eventRemoveRegion.Block != null));
			Debug.Assert((eventRemoveRegion.Parameters != null));
			for (int i = 0; i < eventRemoveRegion.Attributes.Count; i++) {
				AttributeSection o = eventRemoveRegion.Attributes[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (AttributeSection)nodeStack.Pop();
				if (o == null)
					eventRemoveRegion.Attributes.RemoveAt(i--);
				else
					eventRemoveRegion.Attributes[i] = o;
			}
			nodeStack.Push(eventRemoveRegion.Block);
			eventRemoveRegion.Block.AcceptVisitor(this, data);
			eventRemoveRegion.Block = ((BlockStatement)(nodeStack.Pop()));
			for (int i = 0; i < eventRemoveRegion.Parameters.Count; i++) {
				ParameterDeclarationExpression o = eventRemoveRegion.Parameters[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (ParameterDeclarationExpression)nodeStack.Pop();
				if (o == null)
					eventRemoveRegion.Parameters.RemoveAt(i--);
				else
					eventRemoveRegion.Parameters[i] = o;
			}
			return null;
		}
		
		public virtual object VisitExitStatement(ExitStatement exitStatement, object data) {
			Debug.Assert((exitStatement != null));
			return null;
		}
		
		public virtual object VisitExpressionRangeVariable(ExpressionRangeVariable expressionRangeVariable, object data) {
			Debug.Assert((expressionRangeVariable != null));
			Debug.Assert((expressionRangeVariable.Expression != null));
			Debug.Assert((expressionRangeVariable.Type != null));
			nodeStack.Push(expressionRangeVariable.Expression);
			expressionRangeVariable.Expression.AcceptVisitor(this, data);
			expressionRangeVariable.Expression = ((Expression)(nodeStack.Pop()));
			nodeStack.Push(expressionRangeVariable.Type);
			expressionRangeVariable.Type.AcceptVisitor(this, data);
			expressionRangeVariable.Type = ((TypeReference)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitExpressionStatement(ExpressionStatement expressionStatement, object data) {
			Debug.Assert((expressionStatement != null));
			Debug.Assert((expressionStatement.Expression != null));
			nodeStack.Push(expressionStatement.Expression);
			expressionStatement.Expression.AcceptVisitor(this, data);
			expressionStatement.Expression = ((Expression)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitExternAliasDirective(ExternAliasDirective externAliasDirective, object data) {
			Debug.Assert((externAliasDirective != null));
			return null;
		}
		
		public virtual object VisitFieldDeclaration(FieldDeclaration fieldDeclaration, object data) {
			Debug.Assert((fieldDeclaration != null));
			Debug.Assert((fieldDeclaration.Attributes != null));
			Debug.Assert((fieldDeclaration.TypeReference != null));
			Debug.Assert((fieldDeclaration.Fields != null));
			for (int i = 0; i < fieldDeclaration.Attributes.Count; i++) {
				AttributeSection o = fieldDeclaration.Attributes[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (AttributeSection)nodeStack.Pop();
				if (o == null)
					fieldDeclaration.Attributes.RemoveAt(i--);
				else
					fieldDeclaration.Attributes[i] = o;
			}
			nodeStack.Push(fieldDeclaration.TypeReference);
			fieldDeclaration.TypeReference.AcceptVisitor(this, data);
			fieldDeclaration.TypeReference = ((TypeReference)(nodeStack.Pop()));
			for (int i = 0; i < fieldDeclaration.Fields.Count; i++) {
				VariableDeclaration o = fieldDeclaration.Fields[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (VariableDeclaration)nodeStack.Pop();
				if (o == null)
					fieldDeclaration.Fields.RemoveAt(i--);
				else
					fieldDeclaration.Fields[i] = o;
			}
			return null;
		}
		
		public virtual object VisitFixedStatement(FixedStatement fixedStatement, object data) {
			Debug.Assert((fixedStatement != null));
			Debug.Assert((fixedStatement.PointerDeclaration != null));
			Debug.Assert((fixedStatement.EmbeddedStatement != null));
			nodeStack.Push(fixedStatement.PointerDeclaration);
			fixedStatement.PointerDeclaration.AcceptVisitor(this, data);
			fixedStatement.PointerDeclaration = ((Statement)(nodeStack.Pop()));
			nodeStack.Push(fixedStatement.EmbeddedStatement);
			fixedStatement.EmbeddedStatement.AcceptVisitor(this, data);
			fixedStatement.EmbeddedStatement = ((Statement)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitForeachStatement(ForeachStatement foreachStatement, object data) {
			Debug.Assert((foreachStatement != null));
			Debug.Assert((foreachStatement.TypeReference != null));
			Debug.Assert((foreachStatement.Expression != null));
			Debug.Assert((foreachStatement.NextExpression != null));
			Debug.Assert((foreachStatement.EmbeddedStatement != null));
			nodeStack.Push(foreachStatement.TypeReference);
			foreachStatement.TypeReference.AcceptVisitor(this, data);
			foreachStatement.TypeReference = ((TypeReference)(nodeStack.Pop()));
			nodeStack.Push(foreachStatement.Expression);
			foreachStatement.Expression.AcceptVisitor(this, data);
			foreachStatement.Expression = ((Expression)(nodeStack.Pop()));
			nodeStack.Push(foreachStatement.NextExpression);
			foreachStatement.NextExpression.AcceptVisitor(this, data);
			foreachStatement.NextExpression = ((Expression)(nodeStack.Pop()));
			nodeStack.Push(foreachStatement.EmbeddedStatement);
			foreachStatement.EmbeddedStatement.AcceptVisitor(this, data);
			foreachStatement.EmbeddedStatement = ((Statement)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitForNextStatement(ForNextStatement forNextStatement, object data) {
			Debug.Assert((forNextStatement != null));
			Debug.Assert((forNextStatement.Start != null));
			Debug.Assert((forNextStatement.End != null));
			Debug.Assert((forNextStatement.Step != null));
			Debug.Assert((forNextStatement.NextExpressions != null));
			Debug.Assert((forNextStatement.TypeReference != null));
			Debug.Assert((forNextStatement.LoopVariableExpression != null));
			Debug.Assert((forNextStatement.EmbeddedStatement != null));
			nodeStack.Push(forNextStatement.Start);
			forNextStatement.Start.AcceptVisitor(this, data);
			forNextStatement.Start = ((Expression)(nodeStack.Pop()));
			nodeStack.Push(forNextStatement.End);
			forNextStatement.End.AcceptVisitor(this, data);
			forNextStatement.End = ((Expression)(nodeStack.Pop()));
			nodeStack.Push(forNextStatement.Step);
			forNextStatement.Step.AcceptVisitor(this, data);
			forNextStatement.Step = ((Expression)(nodeStack.Pop()));
			for (int i = 0; i < forNextStatement.NextExpressions.Count; i++) {
				Expression o = forNextStatement.NextExpressions[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (Expression)nodeStack.Pop();
				if (o == null)
					forNextStatement.NextExpressions.RemoveAt(i--);
				else
					forNextStatement.NextExpressions[i] = o;
			}
			nodeStack.Push(forNextStatement.TypeReference);
			forNextStatement.TypeReference.AcceptVisitor(this, data);
			forNextStatement.TypeReference = ((TypeReference)(nodeStack.Pop()));
			nodeStack.Push(forNextStatement.LoopVariableExpression);
			forNextStatement.LoopVariableExpression.AcceptVisitor(this, data);
			forNextStatement.LoopVariableExpression = ((Expression)(nodeStack.Pop()));
			nodeStack.Push(forNextStatement.EmbeddedStatement);
			forNextStatement.EmbeddedStatement.AcceptVisitor(this, data);
			forNextStatement.EmbeddedStatement = ((Statement)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitForStatement(ForStatement forStatement, object data) {
			Debug.Assert((forStatement != null));
			Debug.Assert((forStatement.Initializers != null));
			Debug.Assert((forStatement.Condition != null));
			Debug.Assert((forStatement.Iterator != null));
			Debug.Assert((forStatement.EmbeddedStatement != null));
			for (int i = 0; i < forStatement.Initializers.Count; i++) {
				Statement o = forStatement.Initializers[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (Statement)nodeStack.Pop();
				if (o == null)
					forStatement.Initializers.RemoveAt(i--);
				else
					forStatement.Initializers[i] = o;
			}
			nodeStack.Push(forStatement.Condition);
			forStatement.Condition.AcceptVisitor(this, data);
			forStatement.Condition = ((Expression)(nodeStack.Pop()));
			for (int i = 0; i < forStatement.Iterator.Count; i++) {
				Statement o = forStatement.Iterator[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (Statement)nodeStack.Pop();
				if (o == null)
					forStatement.Iterator.RemoveAt(i--);
				else
					forStatement.Iterator[i] = o;
			}
			nodeStack.Push(forStatement.EmbeddedStatement);
			forStatement.EmbeddedStatement.AcceptVisitor(this, data);
			forStatement.EmbeddedStatement = ((Statement)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitGotoCaseStatement(GotoCaseStatement gotoCaseStatement, object data) {
			Debug.Assert((gotoCaseStatement != null));
			Debug.Assert((gotoCaseStatement.Expression != null));
			nodeStack.Push(gotoCaseStatement.Expression);
			gotoCaseStatement.Expression.AcceptVisitor(this, data);
			gotoCaseStatement.Expression = ((Expression)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitGotoStatement(GotoStatement gotoStatement, object data) {
			Debug.Assert((gotoStatement != null));
			return null;
		}
		
		public virtual object VisitIdentifierExpression(IdentifierExpression identifierExpression, object data) {
			Debug.Assert((identifierExpression != null));
			Debug.Assert((identifierExpression.TypeArguments != null));
			for (int i = 0; i < identifierExpression.TypeArguments.Count; i++) {
				TypeReference o = identifierExpression.TypeArguments[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (TypeReference)nodeStack.Pop();
				if (o == null)
					identifierExpression.TypeArguments.RemoveAt(i--);
				else
					identifierExpression.TypeArguments[i] = o;
			}
			return null;
		}
		
		public virtual object VisitIfElseStatement(IfElseStatement ifElseStatement, object data) {
			Debug.Assert((ifElseStatement != null));
			Debug.Assert((ifElseStatement.Condition != null));
			Debug.Assert((ifElseStatement.TrueStatement != null));
			Debug.Assert((ifElseStatement.FalseStatement != null));
			Debug.Assert((ifElseStatement.ElseIfSections != null));
			nodeStack.Push(ifElseStatement.Condition);
			ifElseStatement.Condition.AcceptVisitor(this, data);
			ifElseStatement.Condition = ((Expression)(nodeStack.Pop()));
			for (int i = 0; i < ifElseStatement.TrueStatement.Count; i++) {
				Statement o = ifElseStatement.TrueStatement[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (Statement)nodeStack.Pop();
				if (o == null)
					ifElseStatement.TrueStatement.RemoveAt(i--);
				else
					ifElseStatement.TrueStatement[i] = o;
			}
			for (int i = 0; i < ifElseStatement.FalseStatement.Count; i++) {
				Statement o = ifElseStatement.FalseStatement[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (Statement)nodeStack.Pop();
				if (o == null)
					ifElseStatement.FalseStatement.RemoveAt(i--);
				else
					ifElseStatement.FalseStatement[i] = o;
			}
			for (int i = 0; i < ifElseStatement.ElseIfSections.Count; i++) {
				ElseIfSection o = ifElseStatement.ElseIfSections[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (ElseIfSection)nodeStack.Pop();
				if (o == null)
					ifElseStatement.ElseIfSections.RemoveAt(i--);
				else
					ifElseStatement.ElseIfSections[i] = o;
			}
			return null;
		}
		
		public virtual object VisitIndexerDeclaration(IndexerDeclaration indexerDeclaration, object data) {
			Debug.Assert((indexerDeclaration != null));
			Debug.Assert((indexerDeclaration.Attributes != null));
			Debug.Assert((indexerDeclaration.Parameters != null));
			Debug.Assert((indexerDeclaration.InterfaceImplementations != null));
			Debug.Assert((indexerDeclaration.TypeReference != null));
			Debug.Assert((indexerDeclaration.GetRegion != null));
			Debug.Assert((indexerDeclaration.SetRegion != null));
			for (int i = 0; i < indexerDeclaration.Attributes.Count; i++) {
				AttributeSection o = indexerDeclaration.Attributes[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (AttributeSection)nodeStack.Pop();
				if (o == null)
					indexerDeclaration.Attributes.RemoveAt(i--);
				else
					indexerDeclaration.Attributes[i] = o;
			}
			for (int i = 0; i < indexerDeclaration.Parameters.Count; i++) {
				ParameterDeclarationExpression o = indexerDeclaration.Parameters[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (ParameterDeclarationExpression)nodeStack.Pop();
				if (o == null)
					indexerDeclaration.Parameters.RemoveAt(i--);
				else
					indexerDeclaration.Parameters[i] = o;
			}
			for (int i = 0; i < indexerDeclaration.InterfaceImplementations.Count; i++) {
				InterfaceImplementation o = indexerDeclaration.InterfaceImplementations[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (InterfaceImplementation)nodeStack.Pop();
				if (o == null)
					indexerDeclaration.InterfaceImplementations.RemoveAt(i--);
				else
					indexerDeclaration.InterfaceImplementations[i] = o;
			}
			nodeStack.Push(indexerDeclaration.TypeReference);
			indexerDeclaration.TypeReference.AcceptVisitor(this, data);
			indexerDeclaration.TypeReference = ((TypeReference)(nodeStack.Pop()));
			nodeStack.Push(indexerDeclaration.GetRegion);
			indexerDeclaration.GetRegion.AcceptVisitor(this, data);
			indexerDeclaration.GetRegion = ((PropertyGetRegion)(nodeStack.Pop()));
			nodeStack.Push(indexerDeclaration.SetRegion);
			indexerDeclaration.SetRegion.AcceptVisitor(this, data);
			indexerDeclaration.SetRegion = ((PropertySetRegion)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitIndexerExpression(IndexerExpression indexerExpression, object data) {
			Debug.Assert((indexerExpression != null));
			Debug.Assert((indexerExpression.TargetObject != null));
			Debug.Assert((indexerExpression.Indexes != null));
			nodeStack.Push(indexerExpression.TargetObject);
			indexerExpression.TargetObject.AcceptVisitor(this, data);
			indexerExpression.TargetObject = ((Expression)(nodeStack.Pop()));
			for (int i = 0; i < indexerExpression.Indexes.Count; i++) {
				Expression o = indexerExpression.Indexes[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (Expression)nodeStack.Pop();
				if (o == null)
					indexerExpression.Indexes.RemoveAt(i--);
				else
					indexerExpression.Indexes[i] = o;
			}
			return null;
		}
		
		public virtual object VisitInnerClassTypeReference(InnerClassTypeReference innerClassTypeReference, object data) {
			Debug.Assert((innerClassTypeReference != null));
			Debug.Assert((innerClassTypeReference.GenericTypes != null));
			Debug.Assert((innerClassTypeReference.BaseType != null));
			for (int i = 0; i < innerClassTypeReference.GenericTypes.Count; i++) {
				TypeReference o = innerClassTypeReference.GenericTypes[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (TypeReference)nodeStack.Pop();
				if (o == null)
					innerClassTypeReference.GenericTypes.RemoveAt(i--);
				else
					innerClassTypeReference.GenericTypes[i] = o;
			}
			nodeStack.Push(innerClassTypeReference.BaseType);
			innerClassTypeReference.BaseType.AcceptVisitor(this, data);
			innerClassTypeReference.BaseType = ((TypeReference)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitInterfaceImplementation(InterfaceImplementation interfaceImplementation, object data) {
			Debug.Assert((interfaceImplementation != null));
			Debug.Assert((interfaceImplementation.InterfaceType != null));
			nodeStack.Push(interfaceImplementation.InterfaceType);
			interfaceImplementation.InterfaceType.AcceptVisitor(this, data);
			interfaceImplementation.InterfaceType = ((TypeReference)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitInvocationExpression(InvocationExpression invocationExpression, object data) {
			Debug.Assert((invocationExpression != null));
			Debug.Assert((invocationExpression.TargetObject != null));
			Debug.Assert((invocationExpression.Arguments != null));
			nodeStack.Push(invocationExpression.TargetObject);
			invocationExpression.TargetObject.AcceptVisitor(this, data);
			invocationExpression.TargetObject = ((Expression)(nodeStack.Pop()));
			for (int i = 0; i < invocationExpression.Arguments.Count; i++) {
				Expression o = invocationExpression.Arguments[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (Expression)nodeStack.Pop();
				if (o == null)
					invocationExpression.Arguments.RemoveAt(i--);
				else
					invocationExpression.Arguments[i] = o;
			}
			return null;
		}
		
		public virtual object VisitLabelStatement(LabelStatement labelStatement, object data) {
			Debug.Assert((labelStatement != null));
			return null;
		}
		
		public virtual object VisitLambdaExpression(LambdaExpression lambdaExpression, object data) {
			Debug.Assert((lambdaExpression != null));
			Debug.Assert((lambdaExpression.Parameters != null));
			Debug.Assert((lambdaExpression.StatementBody != null));
			Debug.Assert((lambdaExpression.ExpressionBody != null));
			for (int i = 0; i < lambdaExpression.Parameters.Count; i++) {
				ParameterDeclarationExpression o = lambdaExpression.Parameters[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (ParameterDeclarationExpression)nodeStack.Pop();
				if (o == null)
					lambdaExpression.Parameters.RemoveAt(i--);
				else
					lambdaExpression.Parameters[i] = o;
			}
			nodeStack.Push(lambdaExpression.StatementBody);
			lambdaExpression.StatementBody.AcceptVisitor(this, data);
			lambdaExpression.StatementBody = ((BlockStatement)(nodeStack.Pop()));
			nodeStack.Push(lambdaExpression.ExpressionBody);
			lambdaExpression.ExpressionBody.AcceptVisitor(this, data);
			lambdaExpression.ExpressionBody = ((Expression)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitLocalVariableDeclaration(LocalVariableDeclaration localVariableDeclaration, object data) {
			Debug.Assert((localVariableDeclaration != null));
			Debug.Assert((localVariableDeclaration.TypeReference != null));
			Debug.Assert((localVariableDeclaration.Variables != null));
			nodeStack.Push(localVariableDeclaration.TypeReference);
			localVariableDeclaration.TypeReference.AcceptVisitor(this, data);
			localVariableDeclaration.TypeReference = ((TypeReference)(nodeStack.Pop()));
			for (int i = 0; i < localVariableDeclaration.Variables.Count; i++) {
				VariableDeclaration o = localVariableDeclaration.Variables[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (VariableDeclaration)nodeStack.Pop();
				if (o == null)
					localVariableDeclaration.Variables.RemoveAt(i--);
				else
					localVariableDeclaration.Variables[i] = o;
			}
			return null;
		}
		
		public virtual object VisitLockStatement(LockStatement lockStatement, object data) {
			Debug.Assert((lockStatement != null));
			Debug.Assert((lockStatement.LockExpression != null));
			Debug.Assert((lockStatement.EmbeddedStatement != null));
			nodeStack.Push(lockStatement.LockExpression);
			lockStatement.LockExpression.AcceptVisitor(this, data);
			lockStatement.LockExpression = ((Expression)(nodeStack.Pop()));
			nodeStack.Push(lockStatement.EmbeddedStatement);
			lockStatement.EmbeddedStatement.AcceptVisitor(this, data);
			lockStatement.EmbeddedStatement = ((Statement)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitMemberReferenceExpression(MemberReferenceExpression memberReferenceExpression, object data) {
			Debug.Assert((memberReferenceExpression != null));
			Debug.Assert((memberReferenceExpression.TargetObject != null));
			Debug.Assert((memberReferenceExpression.TypeArguments != null));
			nodeStack.Push(memberReferenceExpression.TargetObject);
			memberReferenceExpression.TargetObject.AcceptVisitor(this, data);
			memberReferenceExpression.TargetObject = ((Expression)(nodeStack.Pop()));
			for (int i = 0; i < memberReferenceExpression.TypeArguments.Count; i++) {
				TypeReference o = memberReferenceExpression.TypeArguments[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (TypeReference)nodeStack.Pop();
				if (o == null)
					memberReferenceExpression.TypeArguments.RemoveAt(i--);
				else
					memberReferenceExpression.TypeArguments[i] = o;
			}
			return null;
		}
		
		public virtual object VisitMethodDeclaration(MethodDeclaration methodDeclaration, object data) {
			Debug.Assert((methodDeclaration != null));
			Debug.Assert((methodDeclaration.Attributes != null));
			Debug.Assert((methodDeclaration.Parameters != null));
			Debug.Assert((methodDeclaration.InterfaceImplementations != null));
			Debug.Assert((methodDeclaration.TypeReference != null));
			Debug.Assert((methodDeclaration.Body != null));
			Debug.Assert((methodDeclaration.Templates != null));
			for (int i = 0; i < methodDeclaration.Attributes.Count; i++) {
				AttributeSection o = methodDeclaration.Attributes[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (AttributeSection)nodeStack.Pop();
				if (o == null)
					methodDeclaration.Attributes.RemoveAt(i--);
				else
					methodDeclaration.Attributes[i] = o;
			}
			for (int i = 0; i < methodDeclaration.Parameters.Count; i++) {
				ParameterDeclarationExpression o = methodDeclaration.Parameters[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (ParameterDeclarationExpression)nodeStack.Pop();
				if (o == null)
					methodDeclaration.Parameters.RemoveAt(i--);
				else
					methodDeclaration.Parameters[i] = o;
			}
			for (int i = 0; i < methodDeclaration.InterfaceImplementations.Count; i++) {
				InterfaceImplementation o = methodDeclaration.InterfaceImplementations[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (InterfaceImplementation)nodeStack.Pop();
				if (o == null)
					methodDeclaration.InterfaceImplementations.RemoveAt(i--);
				else
					methodDeclaration.InterfaceImplementations[i] = o;
			}
			nodeStack.Push(methodDeclaration.TypeReference);
			methodDeclaration.TypeReference.AcceptVisitor(this, data);
			methodDeclaration.TypeReference = ((TypeReference)(nodeStack.Pop()));
			nodeStack.Push(methodDeclaration.Body);
			methodDeclaration.Body.AcceptVisitor(this, data);
			methodDeclaration.Body = ((BlockStatement)(nodeStack.Pop()));
			for (int i = 0; i < methodDeclaration.Templates.Count; i++) {
				TemplateDefinition o = methodDeclaration.Templates[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (TemplateDefinition)nodeStack.Pop();
				if (o == null)
					methodDeclaration.Templates.RemoveAt(i--);
				else
					methodDeclaration.Templates[i] = o;
			}
			return null;
		}
		
		public virtual object VisitNamedArgumentExpression(NamedArgumentExpression namedArgumentExpression, object data) {
			Debug.Assert((namedArgumentExpression != null));
			Debug.Assert((namedArgumentExpression.Expression != null));
			nodeStack.Push(namedArgumentExpression.Expression);
			namedArgumentExpression.Expression.AcceptVisitor(this, data);
			namedArgumentExpression.Expression = ((Expression)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitNamespaceDeclaration(NamespaceDeclaration namespaceDeclaration, object data) {
			Debug.Assert((namespaceDeclaration != null));
			for (int i = 0; i < namespaceDeclaration.Children.Count; i++) {
				INode o = namespaceDeclaration.Children[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = nodeStack.Pop();
				if (o == null)
					namespaceDeclaration.Children.RemoveAt(i--);
				else
					namespaceDeclaration.Children[i] = o;
			}
			return null;
		}
		
		public virtual object VisitObjectCreateExpression(ObjectCreateExpression objectCreateExpression, object data) {
			Debug.Assert((objectCreateExpression != null));
			Debug.Assert((objectCreateExpression.CreateType != null));
			Debug.Assert((objectCreateExpression.Parameters != null));
			Debug.Assert((objectCreateExpression.ObjectInitializer != null));
			nodeStack.Push(objectCreateExpression.CreateType);
			objectCreateExpression.CreateType.AcceptVisitor(this, data);
			objectCreateExpression.CreateType = ((TypeReference)(nodeStack.Pop()));
			for (int i = 0; i < objectCreateExpression.Parameters.Count; i++) {
				Expression o = objectCreateExpression.Parameters[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (Expression)nodeStack.Pop();
				if (o == null)
					objectCreateExpression.Parameters.RemoveAt(i--);
				else
					objectCreateExpression.Parameters[i] = o;
			}
			nodeStack.Push(objectCreateExpression.ObjectInitializer);
			objectCreateExpression.ObjectInitializer.AcceptVisitor(this, data);
			objectCreateExpression.ObjectInitializer = ((CollectionInitializerExpression)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitOnErrorStatement(OnErrorStatement onErrorStatement, object data) {
			Debug.Assert((onErrorStatement != null));
			Debug.Assert((onErrorStatement.EmbeddedStatement != null));
			nodeStack.Push(onErrorStatement.EmbeddedStatement);
			onErrorStatement.EmbeddedStatement.AcceptVisitor(this, data);
			onErrorStatement.EmbeddedStatement = ((Statement)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitOperatorDeclaration(OperatorDeclaration operatorDeclaration, object data) {
			Debug.Assert((operatorDeclaration != null));
			Debug.Assert((operatorDeclaration.Attributes != null));
			Debug.Assert((operatorDeclaration.Parameters != null));
			Debug.Assert((operatorDeclaration.InterfaceImplementations != null));
			Debug.Assert((operatorDeclaration.TypeReference != null));
			Debug.Assert((operatorDeclaration.Body != null));
			Debug.Assert((operatorDeclaration.Templates != null));
			Debug.Assert((operatorDeclaration.ReturnTypeAttributes != null));
			for (int i = 0; i < operatorDeclaration.Attributes.Count; i++) {
				AttributeSection o = operatorDeclaration.Attributes[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (AttributeSection)nodeStack.Pop();
				if (o == null)
					operatorDeclaration.Attributes.RemoveAt(i--);
				else
					operatorDeclaration.Attributes[i] = o;
			}
			for (int i = 0; i < operatorDeclaration.Parameters.Count; i++) {
				ParameterDeclarationExpression o = operatorDeclaration.Parameters[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (ParameterDeclarationExpression)nodeStack.Pop();
				if (o == null)
					operatorDeclaration.Parameters.RemoveAt(i--);
				else
					operatorDeclaration.Parameters[i] = o;
			}
			for (int i = 0; i < operatorDeclaration.InterfaceImplementations.Count; i++) {
				InterfaceImplementation o = operatorDeclaration.InterfaceImplementations[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (InterfaceImplementation)nodeStack.Pop();
				if (o == null)
					operatorDeclaration.InterfaceImplementations.RemoveAt(i--);
				else
					operatorDeclaration.InterfaceImplementations[i] = o;
			}
			nodeStack.Push(operatorDeclaration.TypeReference);
			operatorDeclaration.TypeReference.AcceptVisitor(this, data);
			operatorDeclaration.TypeReference = ((TypeReference)(nodeStack.Pop()));
			nodeStack.Push(operatorDeclaration.Body);
			operatorDeclaration.Body.AcceptVisitor(this, data);
			operatorDeclaration.Body = ((BlockStatement)(nodeStack.Pop()));
			for (int i = 0; i < operatorDeclaration.Templates.Count; i++) {
				TemplateDefinition o = operatorDeclaration.Templates[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (TemplateDefinition)nodeStack.Pop();
				if (o == null)
					operatorDeclaration.Templates.RemoveAt(i--);
				else
					operatorDeclaration.Templates[i] = o;
			}
			for (int i = 0; i < operatorDeclaration.ReturnTypeAttributes.Count; i++) {
				AttributeSection o = operatorDeclaration.ReturnTypeAttributes[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (AttributeSection)nodeStack.Pop();
				if (o == null)
					operatorDeclaration.ReturnTypeAttributes.RemoveAt(i--);
				else
					operatorDeclaration.ReturnTypeAttributes[i] = o;
			}
			return null;
		}
		
		public virtual object VisitOptionDeclaration(OptionDeclaration optionDeclaration, object data) {
			Debug.Assert((optionDeclaration != null));
			return null;
		}
		
		public virtual object VisitParameterDeclarationExpression(ParameterDeclarationExpression parameterDeclarationExpression, object data) {
			Debug.Assert((parameterDeclarationExpression != null));
			Debug.Assert((parameterDeclarationExpression.Attributes != null));
			Debug.Assert((parameterDeclarationExpression.TypeReference != null));
			Debug.Assert((parameterDeclarationExpression.DefaultValue != null));
			for (int i = 0; i < parameterDeclarationExpression.Attributes.Count; i++) {
				AttributeSection o = parameterDeclarationExpression.Attributes[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (AttributeSection)nodeStack.Pop();
				if (o == null)
					parameterDeclarationExpression.Attributes.RemoveAt(i--);
				else
					parameterDeclarationExpression.Attributes[i] = o;
			}
			nodeStack.Push(parameterDeclarationExpression.TypeReference);
			parameterDeclarationExpression.TypeReference.AcceptVisitor(this, data);
			parameterDeclarationExpression.TypeReference = ((TypeReference)(nodeStack.Pop()));
			nodeStack.Push(parameterDeclarationExpression.DefaultValue);
			parameterDeclarationExpression.DefaultValue.AcceptVisitor(this, data);
			parameterDeclarationExpression.DefaultValue = ((Expression)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitParenthesizedExpression(ParenthesizedExpression parenthesizedExpression, object data) {
			Debug.Assert((parenthesizedExpression != null));
			Debug.Assert((parenthesizedExpression.Expression != null));
			nodeStack.Push(parenthesizedExpression.Expression);
			parenthesizedExpression.Expression.AcceptVisitor(this, data);
			parenthesizedExpression.Expression = ((Expression)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitPointerReferenceExpression(PointerReferenceExpression pointerReferenceExpression, object data) {
			Debug.Assert((pointerReferenceExpression != null));
			Debug.Assert((pointerReferenceExpression.TargetObject != null));
			Debug.Assert((pointerReferenceExpression.TypeArguments != null));
			nodeStack.Push(pointerReferenceExpression.TargetObject);
			pointerReferenceExpression.TargetObject.AcceptVisitor(this, data);
			pointerReferenceExpression.TargetObject = ((Expression)(nodeStack.Pop()));
			for (int i = 0; i < pointerReferenceExpression.TypeArguments.Count; i++) {
				TypeReference o = pointerReferenceExpression.TypeArguments[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (TypeReference)nodeStack.Pop();
				if (o == null)
					pointerReferenceExpression.TypeArguments.RemoveAt(i--);
				else
					pointerReferenceExpression.TypeArguments[i] = o;
			}
			return null;
		}
		
		public virtual object VisitPrimitiveExpression(PrimitiveExpression primitiveExpression, object data) {
			Debug.Assert((primitiveExpression != null));
			return null;
		}
		
		public virtual object VisitPropertyDeclaration(PropertyDeclaration propertyDeclaration, object data) {
			Debug.Assert((propertyDeclaration != null));
			Debug.Assert((propertyDeclaration.Attributes != null));
			Debug.Assert((propertyDeclaration.Parameters != null));
			Debug.Assert((propertyDeclaration.InterfaceImplementations != null));
			Debug.Assert((propertyDeclaration.TypeReference != null));
			Debug.Assert((propertyDeclaration.GetRegion != null));
			Debug.Assert((propertyDeclaration.SetRegion != null));
			for (int i = 0; i < propertyDeclaration.Attributes.Count; i++) {
				AttributeSection o = propertyDeclaration.Attributes[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (AttributeSection)nodeStack.Pop();
				if (o == null)
					propertyDeclaration.Attributes.RemoveAt(i--);
				else
					propertyDeclaration.Attributes[i] = o;
			}
			for (int i = 0; i < propertyDeclaration.Parameters.Count; i++) {
				ParameterDeclarationExpression o = propertyDeclaration.Parameters[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (ParameterDeclarationExpression)nodeStack.Pop();
				if (o == null)
					propertyDeclaration.Parameters.RemoveAt(i--);
				else
					propertyDeclaration.Parameters[i] = o;
			}
			for (int i = 0; i < propertyDeclaration.InterfaceImplementations.Count; i++) {
				InterfaceImplementation o = propertyDeclaration.InterfaceImplementations[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (InterfaceImplementation)nodeStack.Pop();
				if (o == null)
					propertyDeclaration.InterfaceImplementations.RemoveAt(i--);
				else
					propertyDeclaration.InterfaceImplementations[i] = o;
			}
			nodeStack.Push(propertyDeclaration.TypeReference);
			propertyDeclaration.TypeReference.AcceptVisitor(this, data);
			propertyDeclaration.TypeReference = ((TypeReference)(nodeStack.Pop()));
			nodeStack.Push(propertyDeclaration.GetRegion);
			propertyDeclaration.GetRegion.AcceptVisitor(this, data);
			propertyDeclaration.GetRegion = ((PropertyGetRegion)(nodeStack.Pop()));
			nodeStack.Push(propertyDeclaration.SetRegion);
			propertyDeclaration.SetRegion.AcceptVisitor(this, data);
			propertyDeclaration.SetRegion = ((PropertySetRegion)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitPropertyGetRegion(PropertyGetRegion propertyGetRegion, object data) {
			Debug.Assert((propertyGetRegion != null));
			Debug.Assert((propertyGetRegion.Attributes != null));
			Debug.Assert((propertyGetRegion.Block != null));
			for (int i = 0; i < propertyGetRegion.Attributes.Count; i++) {
				AttributeSection o = propertyGetRegion.Attributes[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (AttributeSection)nodeStack.Pop();
				if (o == null)
					propertyGetRegion.Attributes.RemoveAt(i--);
				else
					propertyGetRegion.Attributes[i] = o;
			}
			nodeStack.Push(propertyGetRegion.Block);
			propertyGetRegion.Block.AcceptVisitor(this, data);
			propertyGetRegion.Block = ((BlockStatement)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitPropertySetRegion(PropertySetRegion propertySetRegion, object data) {
			Debug.Assert((propertySetRegion != null));
			Debug.Assert((propertySetRegion.Attributes != null));
			Debug.Assert((propertySetRegion.Block != null));
			Debug.Assert((propertySetRegion.Parameters != null));
			for (int i = 0; i < propertySetRegion.Attributes.Count; i++) {
				AttributeSection o = propertySetRegion.Attributes[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (AttributeSection)nodeStack.Pop();
				if (o == null)
					propertySetRegion.Attributes.RemoveAt(i--);
				else
					propertySetRegion.Attributes[i] = o;
			}
			nodeStack.Push(propertySetRegion.Block);
			propertySetRegion.Block.AcceptVisitor(this, data);
			propertySetRegion.Block = ((BlockStatement)(nodeStack.Pop()));
			for (int i = 0; i < propertySetRegion.Parameters.Count; i++) {
				ParameterDeclarationExpression o = propertySetRegion.Parameters[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (ParameterDeclarationExpression)nodeStack.Pop();
				if (o == null)
					propertySetRegion.Parameters.RemoveAt(i--);
				else
					propertySetRegion.Parameters[i] = o;
			}
			return null;
		}
		
		public virtual object VisitQueryExpression(QueryExpression queryExpression, object data) {
			Debug.Assert((queryExpression != null));
			Debug.Assert((queryExpression.FromClause != null));
			Debug.Assert((queryExpression.MiddleClauses != null));
			Debug.Assert((queryExpression.SelectOrGroupClause != null));
			nodeStack.Push(queryExpression.FromClause);
			queryExpression.FromClause.AcceptVisitor(this, data);
			queryExpression.FromClause = ((QueryExpressionFromClause)(nodeStack.Pop()));
			for (int i = 0; i < queryExpression.MiddleClauses.Count; i++) {
				QueryExpressionClause o = queryExpression.MiddleClauses[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (QueryExpressionClause)nodeStack.Pop();
				if (o == null)
					queryExpression.MiddleClauses.RemoveAt(i--);
				else
					queryExpression.MiddleClauses[i] = o;
			}
			nodeStack.Push(queryExpression.SelectOrGroupClause);
			queryExpression.SelectOrGroupClause.AcceptVisitor(this, data);
			queryExpression.SelectOrGroupClause = ((QueryExpressionClause)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitQueryExpressionAggregateClause(QueryExpressionAggregateClause queryExpressionAggregateClause, object data) {
			Debug.Assert((queryExpressionAggregateClause != null));
			Debug.Assert((queryExpressionAggregateClause.FromClause != null));
			Debug.Assert((queryExpressionAggregateClause.MiddleClauses != null));
			Debug.Assert((queryExpressionAggregateClause.IntoVariables != null));
			nodeStack.Push(queryExpressionAggregateClause.FromClause);
			queryExpressionAggregateClause.FromClause.AcceptVisitor(this, data);
			queryExpressionAggregateClause.FromClause = ((QueryExpressionFromClause)(nodeStack.Pop()));
			for (int i = 0; i < queryExpressionAggregateClause.MiddleClauses.Count; i++) {
				QueryExpressionClause o = queryExpressionAggregateClause.MiddleClauses[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (QueryExpressionClause)nodeStack.Pop();
				if (o == null)
					queryExpressionAggregateClause.MiddleClauses.RemoveAt(i--);
				else
					queryExpressionAggregateClause.MiddleClauses[i] = o;
			}
			for (int i = 0; i < queryExpressionAggregateClause.IntoVariables.Count; i++) {
				ExpressionRangeVariable o = queryExpressionAggregateClause.IntoVariables[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (ExpressionRangeVariable)nodeStack.Pop();
				if (o == null)
					queryExpressionAggregateClause.IntoVariables.RemoveAt(i--);
				else
					queryExpressionAggregateClause.IntoVariables[i] = o;
			}
			return null;
		}
		
		public virtual object VisitQueryExpressionDistinctClause(QueryExpressionDistinctClause queryExpressionDistinctClause, object data) {
			Debug.Assert((queryExpressionDistinctClause != null));
			return null;
		}
		
		public virtual object VisitQueryExpressionFromClause(QueryExpressionFromClause queryExpressionFromClause, object data) {
			Debug.Assert((queryExpressionFromClause != null));
			Debug.Assert((queryExpressionFromClause.Type != null));
			Debug.Assert((queryExpressionFromClause.InExpression != null));
			nodeStack.Push(queryExpressionFromClause.Type);
			queryExpressionFromClause.Type.AcceptVisitor(this, data);
			queryExpressionFromClause.Type = ((TypeReference)(nodeStack.Pop()));
			nodeStack.Push(queryExpressionFromClause.InExpression);
			queryExpressionFromClause.InExpression.AcceptVisitor(this, data);
			queryExpressionFromClause.InExpression = ((Expression)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitQueryExpressionGroupClause(QueryExpressionGroupClause queryExpressionGroupClause, object data) {
			Debug.Assert((queryExpressionGroupClause != null));
			Debug.Assert((queryExpressionGroupClause.Projection != null));
			Debug.Assert((queryExpressionGroupClause.GroupBy != null));
			nodeStack.Push(queryExpressionGroupClause.Projection);
			queryExpressionGroupClause.Projection.AcceptVisitor(this, data);
			queryExpressionGroupClause.Projection = ((Expression)(nodeStack.Pop()));
			nodeStack.Push(queryExpressionGroupClause.GroupBy);
			queryExpressionGroupClause.GroupBy.AcceptVisitor(this, data);
			queryExpressionGroupClause.GroupBy = ((Expression)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitQueryExpressionGroupJoinVBClause(QueryExpressionGroupJoinVBClause queryExpressionGroupJoinVBClause, object data) {
			Debug.Assert((queryExpressionGroupJoinVBClause != null));
			Debug.Assert((queryExpressionGroupJoinVBClause.JoinClause != null));
			Debug.Assert((queryExpressionGroupJoinVBClause.IntoVariables != null));
			nodeStack.Push(queryExpressionGroupJoinVBClause.JoinClause);
			queryExpressionGroupJoinVBClause.JoinClause.AcceptVisitor(this, data);
			queryExpressionGroupJoinVBClause.JoinClause = ((QueryExpressionJoinVBClause)(nodeStack.Pop()));
			for (int i = 0; i < queryExpressionGroupJoinVBClause.IntoVariables.Count; i++) {
				ExpressionRangeVariable o = queryExpressionGroupJoinVBClause.IntoVariables[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (ExpressionRangeVariable)nodeStack.Pop();
				if (o == null)
					queryExpressionGroupJoinVBClause.IntoVariables.RemoveAt(i--);
				else
					queryExpressionGroupJoinVBClause.IntoVariables[i] = o;
			}
			return null;
		}
		
		public virtual object VisitQueryExpressionGroupVBClause(QueryExpressionGroupVBClause queryExpressionGroupVBClause, object data) {
			Debug.Assert((queryExpressionGroupVBClause != null));
			Debug.Assert((queryExpressionGroupVBClause.GroupVariables != null));
			Debug.Assert((queryExpressionGroupVBClause.ByVariables != null));
			Debug.Assert((queryExpressionGroupVBClause.IntoVariables != null));
			for (int i = 0; i < queryExpressionGroupVBClause.GroupVariables.Count; i++) {
				ExpressionRangeVariable o = queryExpressionGroupVBClause.GroupVariables[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (ExpressionRangeVariable)nodeStack.Pop();
				if (o == null)
					queryExpressionGroupVBClause.GroupVariables.RemoveAt(i--);
				else
					queryExpressionGroupVBClause.GroupVariables[i] = o;
			}
			for (int i = 0; i < queryExpressionGroupVBClause.ByVariables.Count; i++) {
				ExpressionRangeVariable o = queryExpressionGroupVBClause.ByVariables[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (ExpressionRangeVariable)nodeStack.Pop();
				if (o == null)
					queryExpressionGroupVBClause.ByVariables.RemoveAt(i--);
				else
					queryExpressionGroupVBClause.ByVariables[i] = o;
			}
			for (int i = 0; i < queryExpressionGroupVBClause.IntoVariables.Count; i++) {
				ExpressionRangeVariable o = queryExpressionGroupVBClause.IntoVariables[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (ExpressionRangeVariable)nodeStack.Pop();
				if (o == null)
					queryExpressionGroupVBClause.IntoVariables.RemoveAt(i--);
				else
					queryExpressionGroupVBClause.IntoVariables[i] = o;
			}
			return null;
		}
		
		public virtual object VisitQueryExpressionJoinClause(QueryExpressionJoinClause queryExpressionJoinClause, object data) {
			Debug.Assert((queryExpressionJoinClause != null));
			Debug.Assert((queryExpressionJoinClause.Type != null));
			Debug.Assert((queryExpressionJoinClause.InExpression != null));
			Debug.Assert((queryExpressionJoinClause.OnExpression != null));
			Debug.Assert((queryExpressionJoinClause.EqualsExpression != null));
			nodeStack.Push(queryExpressionJoinClause.Type);
			queryExpressionJoinClause.Type.AcceptVisitor(this, data);
			queryExpressionJoinClause.Type = ((TypeReference)(nodeStack.Pop()));
			nodeStack.Push(queryExpressionJoinClause.InExpression);
			queryExpressionJoinClause.InExpression.AcceptVisitor(this, data);
			queryExpressionJoinClause.InExpression = ((Expression)(nodeStack.Pop()));
			nodeStack.Push(queryExpressionJoinClause.OnExpression);
			queryExpressionJoinClause.OnExpression.AcceptVisitor(this, data);
			queryExpressionJoinClause.OnExpression = ((Expression)(nodeStack.Pop()));
			nodeStack.Push(queryExpressionJoinClause.EqualsExpression);
			queryExpressionJoinClause.EqualsExpression.AcceptVisitor(this, data);
			queryExpressionJoinClause.EqualsExpression = ((Expression)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitQueryExpressionJoinConditionVB(QueryExpressionJoinConditionVB queryExpressionJoinConditionVB, object data) {
			Debug.Assert((queryExpressionJoinConditionVB != null));
			Debug.Assert((queryExpressionJoinConditionVB.LeftSide != null));
			Debug.Assert((queryExpressionJoinConditionVB.RightSide != null));
			nodeStack.Push(queryExpressionJoinConditionVB.LeftSide);
			queryExpressionJoinConditionVB.LeftSide.AcceptVisitor(this, data);
			queryExpressionJoinConditionVB.LeftSide = ((Expression)(nodeStack.Pop()));
			nodeStack.Push(queryExpressionJoinConditionVB.RightSide);
			queryExpressionJoinConditionVB.RightSide.AcceptVisitor(this, data);
			queryExpressionJoinConditionVB.RightSide = ((Expression)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitQueryExpressionJoinVBClause(QueryExpressionJoinVBClause queryExpressionJoinVBClause, object data) {
			Debug.Assert((queryExpressionJoinVBClause != null));
			Debug.Assert((queryExpressionJoinVBClause.JoinVariable != null));
			Debug.Assert((queryExpressionJoinVBClause.SubJoin != null));
			Debug.Assert((queryExpressionJoinVBClause.Conditions != null));
			nodeStack.Push(queryExpressionJoinVBClause.JoinVariable);
			queryExpressionJoinVBClause.JoinVariable.AcceptVisitor(this, data);
			queryExpressionJoinVBClause.JoinVariable = ((QueryExpressionFromClause)(nodeStack.Pop()));
			nodeStack.Push(queryExpressionJoinVBClause.SubJoin);
			queryExpressionJoinVBClause.SubJoin.AcceptVisitor(this, data);
			queryExpressionJoinVBClause.SubJoin = ((QueryExpressionJoinVBClause)(nodeStack.Pop()));
			for (int i = 0; i < queryExpressionJoinVBClause.Conditions.Count; i++) {
				QueryExpressionJoinConditionVB o = queryExpressionJoinVBClause.Conditions[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (QueryExpressionJoinConditionVB)nodeStack.Pop();
				if (o == null)
					queryExpressionJoinVBClause.Conditions.RemoveAt(i--);
				else
					queryExpressionJoinVBClause.Conditions[i] = o;
			}
			return null;
		}
		
		public virtual object VisitQueryExpressionLetClause(QueryExpressionLetClause queryExpressionLetClause, object data) {
			Debug.Assert((queryExpressionLetClause != null));
			Debug.Assert((queryExpressionLetClause.Expression != null));
			nodeStack.Push(queryExpressionLetClause.Expression);
			queryExpressionLetClause.Expression.AcceptVisitor(this, data);
			queryExpressionLetClause.Expression = ((Expression)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitQueryExpressionLetVBClause(QueryExpressionLetVBClause queryExpressionLetVBClause, object data) {
			Debug.Assert((queryExpressionLetVBClause != null));
			Debug.Assert((queryExpressionLetVBClause.Variables != null));
			for (int i = 0; i < queryExpressionLetVBClause.Variables.Count; i++) {
				ExpressionRangeVariable o = queryExpressionLetVBClause.Variables[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (ExpressionRangeVariable)nodeStack.Pop();
				if (o == null)
					queryExpressionLetVBClause.Variables.RemoveAt(i--);
				else
					queryExpressionLetVBClause.Variables[i] = o;
			}
			return null;
		}
		
		public virtual object VisitQueryExpressionOrderClause(QueryExpressionOrderClause queryExpressionOrderClause, object data) {
			Debug.Assert((queryExpressionOrderClause != null));
			Debug.Assert((queryExpressionOrderClause.Orderings != null));
			for (int i = 0; i < queryExpressionOrderClause.Orderings.Count; i++) {
				QueryExpressionOrdering o = queryExpressionOrderClause.Orderings[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (QueryExpressionOrdering)nodeStack.Pop();
				if (o == null)
					queryExpressionOrderClause.Orderings.RemoveAt(i--);
				else
					queryExpressionOrderClause.Orderings[i] = o;
			}
			return null;
		}
		
		public virtual object VisitQueryExpressionOrdering(QueryExpressionOrdering queryExpressionOrdering, object data) {
			Debug.Assert((queryExpressionOrdering != null));
			Debug.Assert((queryExpressionOrdering.Criteria != null));
			nodeStack.Push(queryExpressionOrdering.Criteria);
			queryExpressionOrdering.Criteria.AcceptVisitor(this, data);
			queryExpressionOrdering.Criteria = ((Expression)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitQueryExpressionPartitionVBClause(QueryExpressionPartitionVBClause queryExpressionPartitionVBClause, object data) {
			Debug.Assert((queryExpressionPartitionVBClause != null));
			Debug.Assert((queryExpressionPartitionVBClause.Expression != null));
			nodeStack.Push(queryExpressionPartitionVBClause.Expression);
			queryExpressionPartitionVBClause.Expression.AcceptVisitor(this, data);
			queryExpressionPartitionVBClause.Expression = ((Expression)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitQueryExpressionSelectClause(QueryExpressionSelectClause queryExpressionSelectClause, object data) {
			Debug.Assert((queryExpressionSelectClause != null));
			Debug.Assert((queryExpressionSelectClause.Projection != null));
			nodeStack.Push(queryExpressionSelectClause.Projection);
			queryExpressionSelectClause.Projection.AcceptVisitor(this, data);
			queryExpressionSelectClause.Projection = ((Expression)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitQueryExpressionSelectVBClause(QueryExpressionSelectVBClause queryExpressionSelectVBClause, object data) {
			Debug.Assert((queryExpressionSelectVBClause != null));
			Debug.Assert((queryExpressionSelectVBClause.Variables != null));
			for (int i = 0; i < queryExpressionSelectVBClause.Variables.Count; i++) {
				ExpressionRangeVariable o = queryExpressionSelectVBClause.Variables[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (ExpressionRangeVariable)nodeStack.Pop();
				if (o == null)
					queryExpressionSelectVBClause.Variables.RemoveAt(i--);
				else
					queryExpressionSelectVBClause.Variables[i] = o;
			}
			return null;
		}
		
		public virtual object VisitQueryExpressionWhereClause(QueryExpressionWhereClause queryExpressionWhereClause, object data) {
			Debug.Assert((queryExpressionWhereClause != null));
			Debug.Assert((queryExpressionWhereClause.Condition != null));
			nodeStack.Push(queryExpressionWhereClause.Condition);
			queryExpressionWhereClause.Condition.AcceptVisitor(this, data);
			queryExpressionWhereClause.Condition = ((Expression)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitRaiseEventStatement(RaiseEventStatement raiseEventStatement, object data) {
			Debug.Assert((raiseEventStatement != null));
			Debug.Assert((raiseEventStatement.Arguments != null));
			for (int i = 0; i < raiseEventStatement.Arguments.Count; i++) {
				Expression o = raiseEventStatement.Arguments[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (Expression)nodeStack.Pop();
				if (o == null)
					raiseEventStatement.Arguments.RemoveAt(i--);
				else
					raiseEventStatement.Arguments[i] = o;
			}
			return null;
		}
		
		public virtual object VisitReDimStatement(ReDimStatement reDimStatement, object data) {
			Debug.Assert((reDimStatement != null));
			Debug.Assert((reDimStatement.ReDimClauses != null));
			for (int i = 0; i < reDimStatement.ReDimClauses.Count; i++) {
				InvocationExpression o = reDimStatement.ReDimClauses[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (InvocationExpression)nodeStack.Pop();
				if (o == null)
					reDimStatement.ReDimClauses.RemoveAt(i--);
				else
					reDimStatement.ReDimClauses[i] = o;
			}
			return null;
		}
		
		public virtual object VisitRemoveHandlerStatement(RemoveHandlerStatement removeHandlerStatement, object data) {
			Debug.Assert((removeHandlerStatement != null));
			Debug.Assert((removeHandlerStatement.EventExpression != null));
			Debug.Assert((removeHandlerStatement.HandlerExpression != null));
			nodeStack.Push(removeHandlerStatement.EventExpression);
			removeHandlerStatement.EventExpression.AcceptVisitor(this, data);
			removeHandlerStatement.EventExpression = ((Expression)(nodeStack.Pop()));
			nodeStack.Push(removeHandlerStatement.HandlerExpression);
			removeHandlerStatement.HandlerExpression.AcceptVisitor(this, data);
			removeHandlerStatement.HandlerExpression = ((Expression)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitResumeStatement(ResumeStatement resumeStatement, object data) {
			Debug.Assert((resumeStatement != null));
			return null;
		}
		
		public virtual object VisitReturnStatement(ReturnStatement returnStatement, object data) {
			Debug.Assert((returnStatement != null));
			Debug.Assert((returnStatement.Expression != null));
			nodeStack.Push(returnStatement.Expression);
			returnStatement.Expression.AcceptVisitor(this, data);
			returnStatement.Expression = ((Expression)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitSizeOfExpression(SizeOfExpression sizeOfExpression, object data) {
			Debug.Assert((sizeOfExpression != null));
			Debug.Assert((sizeOfExpression.TypeReference != null));
			nodeStack.Push(sizeOfExpression.TypeReference);
			sizeOfExpression.TypeReference.AcceptVisitor(this, data);
			sizeOfExpression.TypeReference = ((TypeReference)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitStackAllocExpression(StackAllocExpression stackAllocExpression, object data) {
			Debug.Assert((stackAllocExpression != null));
			Debug.Assert((stackAllocExpression.TypeReference != null));
			Debug.Assert((stackAllocExpression.Expression != null));
			nodeStack.Push(stackAllocExpression.TypeReference);
			stackAllocExpression.TypeReference.AcceptVisitor(this, data);
			stackAllocExpression.TypeReference = ((TypeReference)(nodeStack.Pop()));
			nodeStack.Push(stackAllocExpression.Expression);
			stackAllocExpression.Expression.AcceptVisitor(this, data);
			stackAllocExpression.Expression = ((Expression)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitStopStatement(StopStatement stopStatement, object data) {
			Debug.Assert((stopStatement != null));
			return null;
		}
		
		public virtual object VisitSwitchSection(SwitchSection switchSection, object data) {
			Debug.Assert((switchSection != null));
			Debug.Assert((switchSection.SwitchLabels != null));
			for (int i = 0; i < switchSection.SwitchLabels.Count; i++) {
				CaseLabel o = switchSection.SwitchLabels[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (CaseLabel)nodeStack.Pop();
				if (o == null)
					switchSection.SwitchLabels.RemoveAt(i--);
				else
					switchSection.SwitchLabels[i] = o;
			}
			for (int i = 0; i < switchSection.Children.Count; i++) {
				INode o = switchSection.Children[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = nodeStack.Pop();
				if (o == null)
					switchSection.Children.RemoveAt(i--);
				else
					switchSection.Children[i] = o;
			}
			return null;
		}
		
		public virtual object VisitSwitchStatement(SwitchStatement switchStatement, object data) {
			Debug.Assert((switchStatement != null));
			Debug.Assert((switchStatement.SwitchExpression != null));
			Debug.Assert((switchStatement.SwitchSections != null));
			nodeStack.Push(switchStatement.SwitchExpression);
			switchStatement.SwitchExpression.AcceptVisitor(this, data);
			switchStatement.SwitchExpression = ((Expression)(nodeStack.Pop()));
			for (int i = 0; i < switchStatement.SwitchSections.Count; i++) {
				SwitchSection o = switchStatement.SwitchSections[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (SwitchSection)nodeStack.Pop();
				if (o == null)
					switchStatement.SwitchSections.RemoveAt(i--);
				else
					switchStatement.SwitchSections[i] = o;
			}
			return null;
		}
		
		public virtual object VisitTemplateDefinition(TemplateDefinition templateDefinition, object data) {
			Debug.Assert((templateDefinition != null));
			Debug.Assert((templateDefinition.Attributes != null));
			Debug.Assert((templateDefinition.Bases != null));
			for (int i = 0; i < templateDefinition.Attributes.Count; i++) {
				AttributeSection o = templateDefinition.Attributes[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (AttributeSection)nodeStack.Pop();
				if (o == null)
					templateDefinition.Attributes.RemoveAt(i--);
				else
					templateDefinition.Attributes[i] = o;
			}
			for (int i = 0; i < templateDefinition.Bases.Count; i++) {
				TypeReference o = templateDefinition.Bases[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (TypeReference)nodeStack.Pop();
				if (o == null)
					templateDefinition.Bases.RemoveAt(i--);
				else
					templateDefinition.Bases[i] = o;
			}
			return null;
		}
		
		public virtual object VisitThisReferenceExpression(ThisReferenceExpression thisReferenceExpression, object data) {
			Debug.Assert((thisReferenceExpression != null));
			return null;
		}
		
		public virtual object VisitThrowStatement(ThrowStatement throwStatement, object data) {
			Debug.Assert((throwStatement != null));
			Debug.Assert((throwStatement.Expression != null));
			nodeStack.Push(throwStatement.Expression);
			throwStatement.Expression.AcceptVisitor(this, data);
			throwStatement.Expression = ((Expression)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitTryCatchStatement(TryCatchStatement tryCatchStatement, object data) {
			Debug.Assert((tryCatchStatement != null));
			Debug.Assert((tryCatchStatement.StatementBlock != null));
			Debug.Assert((tryCatchStatement.CatchClauses != null));
			Debug.Assert((tryCatchStatement.FinallyBlock != null));
			nodeStack.Push(tryCatchStatement.StatementBlock);
			tryCatchStatement.StatementBlock.AcceptVisitor(this, data);
			tryCatchStatement.StatementBlock = ((Statement)(nodeStack.Pop()));
			for (int i = 0; i < tryCatchStatement.CatchClauses.Count; i++) {
				CatchClause o = tryCatchStatement.CatchClauses[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (CatchClause)nodeStack.Pop();
				if (o == null)
					tryCatchStatement.CatchClauses.RemoveAt(i--);
				else
					tryCatchStatement.CatchClauses[i] = o;
			}
			nodeStack.Push(tryCatchStatement.FinallyBlock);
			tryCatchStatement.FinallyBlock.AcceptVisitor(this, data);
			tryCatchStatement.FinallyBlock = ((Statement)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitTypeDeclaration(TypeDeclaration typeDeclaration, object data) {
			Debug.Assert((typeDeclaration != null));
			Debug.Assert((typeDeclaration.Attributes != null));
			Debug.Assert((typeDeclaration.BaseTypes != null));
			Debug.Assert((typeDeclaration.Templates != null));
			for (int i = 0; i < typeDeclaration.Attributes.Count; i++) {
				AttributeSection o = typeDeclaration.Attributes[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (AttributeSection)nodeStack.Pop();
				if (o == null)
					typeDeclaration.Attributes.RemoveAt(i--);
				else
					typeDeclaration.Attributes[i] = o;
			}
			for (int i = 0; i < typeDeclaration.BaseTypes.Count; i++) {
				TypeReference o = typeDeclaration.BaseTypes[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (TypeReference)nodeStack.Pop();
				if (o == null)
					typeDeclaration.BaseTypes.RemoveAt(i--);
				else
					typeDeclaration.BaseTypes[i] = o;
			}
			for (int i = 0; i < typeDeclaration.Templates.Count; i++) {
				TemplateDefinition o = typeDeclaration.Templates[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (TemplateDefinition)nodeStack.Pop();
				if (o == null)
					typeDeclaration.Templates.RemoveAt(i--);
				else
					typeDeclaration.Templates[i] = o;
			}
			for (int i = 0; i < typeDeclaration.Children.Count; i++) {
				INode o = typeDeclaration.Children[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = nodeStack.Pop();
				if (o == null)
					typeDeclaration.Children.RemoveAt(i--);
				else
					typeDeclaration.Children[i] = o;
			}
			return null;
		}
		
		public virtual object VisitTypeOfExpression(TypeOfExpression typeOfExpression, object data) {
			Debug.Assert((typeOfExpression != null));
			Debug.Assert((typeOfExpression.TypeReference != null));
			nodeStack.Push(typeOfExpression.TypeReference);
			typeOfExpression.TypeReference.AcceptVisitor(this, data);
			typeOfExpression.TypeReference = ((TypeReference)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitTypeOfIsExpression(TypeOfIsExpression typeOfIsExpression, object data) {
			Debug.Assert((typeOfIsExpression != null));
			Debug.Assert((typeOfIsExpression.Expression != null));
			Debug.Assert((typeOfIsExpression.TypeReference != null));
			nodeStack.Push(typeOfIsExpression.Expression);
			typeOfIsExpression.Expression.AcceptVisitor(this, data);
			typeOfIsExpression.Expression = ((Expression)(nodeStack.Pop()));
			nodeStack.Push(typeOfIsExpression.TypeReference);
			typeOfIsExpression.TypeReference.AcceptVisitor(this, data);
			typeOfIsExpression.TypeReference = ((TypeReference)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitTypeReference(TypeReference typeReference, object data) {
			Debug.Assert((typeReference != null));
			Debug.Assert((typeReference.GenericTypes != null));
			for (int i = 0; i < typeReference.GenericTypes.Count; i++) {
				TypeReference o = typeReference.GenericTypes[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (TypeReference)nodeStack.Pop();
				if (o == null)
					typeReference.GenericTypes.RemoveAt(i--);
				else
					typeReference.GenericTypes[i] = o;
			}
			return null;
		}
		
		public virtual object VisitTypeReferenceExpression(TypeReferenceExpression typeReferenceExpression, object data) {
			Debug.Assert((typeReferenceExpression != null));
			Debug.Assert((typeReferenceExpression.TypeReference != null));
			nodeStack.Push(typeReferenceExpression.TypeReference);
			typeReferenceExpression.TypeReference.AcceptVisitor(this, data);
			typeReferenceExpression.TypeReference = ((TypeReference)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitUnaryOperatorExpression(UnaryOperatorExpression unaryOperatorExpression, object data) {
			Debug.Assert((unaryOperatorExpression != null));
			Debug.Assert((unaryOperatorExpression.Expression != null));
			nodeStack.Push(unaryOperatorExpression.Expression);
			unaryOperatorExpression.Expression.AcceptVisitor(this, data);
			unaryOperatorExpression.Expression = ((Expression)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitUncheckedExpression(UncheckedExpression uncheckedExpression, object data) {
			Debug.Assert((uncheckedExpression != null));
			Debug.Assert((uncheckedExpression.Expression != null));
			nodeStack.Push(uncheckedExpression.Expression);
			uncheckedExpression.Expression.AcceptVisitor(this, data);
			uncheckedExpression.Expression = ((Expression)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitUncheckedStatement(UncheckedStatement uncheckedStatement, object data) {
			Debug.Assert((uncheckedStatement != null));
			Debug.Assert((uncheckedStatement.Block != null));
			nodeStack.Push(uncheckedStatement.Block);
			uncheckedStatement.Block.AcceptVisitor(this, data);
			uncheckedStatement.Block = ((Statement)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitUnsafeStatement(UnsafeStatement unsafeStatement, object data) {
			Debug.Assert((unsafeStatement != null));
			Debug.Assert((unsafeStatement.Block != null));
			nodeStack.Push(unsafeStatement.Block);
			unsafeStatement.Block.AcceptVisitor(this, data);
			unsafeStatement.Block = ((Statement)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitUsing(Using @using, object data) {
			Debug.Assert((@using != null));
			Debug.Assert((@using.Alias != null));
			nodeStack.Push(@using.Alias);
			@using.Alias.AcceptVisitor(this, data);
			@using.Alias = ((TypeReference)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitUsingDeclaration(UsingDeclaration usingDeclaration, object data) {
			Debug.Assert((usingDeclaration != null));
			Debug.Assert((usingDeclaration.Usings != null));
			for (int i = 0; i < usingDeclaration.Usings.Count; i++) {
				Using o = usingDeclaration.Usings[i];
				Debug.Assert(o != null);
				nodeStack.Push(o);
				o.AcceptVisitor(this, data);
				o = (Using)nodeStack.Pop();
				if (o == null)
					usingDeclaration.Usings.RemoveAt(i--);
				else
					usingDeclaration.Usings[i] = o;
			}
			return null;
		}
		
		public virtual object VisitUsingStatement(UsingStatement usingStatement, object data) {
			Debug.Assert((usingStatement != null));
			Debug.Assert((usingStatement.ResourceAcquisition != null));
			Debug.Assert((usingStatement.EmbeddedStatement != null));
			nodeStack.Push(usingStatement.ResourceAcquisition);
			usingStatement.ResourceAcquisition.AcceptVisitor(this, data);
			usingStatement.ResourceAcquisition = ((Statement)(nodeStack.Pop()));
			nodeStack.Push(usingStatement.EmbeddedStatement);
			usingStatement.EmbeddedStatement.AcceptVisitor(this, data);
			usingStatement.EmbeddedStatement = ((Statement)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitVariableDeclaration(VariableDeclaration variableDeclaration, object data) {
			Debug.Assert((variableDeclaration != null));
			Debug.Assert((variableDeclaration.Initializer != null));
			Debug.Assert((variableDeclaration.TypeReference != null));
			Debug.Assert((variableDeclaration.FixedArrayInitialization != null));
			nodeStack.Push(variableDeclaration.Initializer);
			variableDeclaration.Initializer.AcceptVisitor(this, data);
			variableDeclaration.Initializer = ((Expression)(nodeStack.Pop()));
			nodeStack.Push(variableDeclaration.TypeReference);
			variableDeclaration.TypeReference.AcceptVisitor(this, data);
			variableDeclaration.TypeReference = ((TypeReference)(nodeStack.Pop()));
			nodeStack.Push(variableDeclaration.FixedArrayInitialization);
			variableDeclaration.FixedArrayInitialization.AcceptVisitor(this, data);
			variableDeclaration.FixedArrayInitialization = ((Expression)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitWithStatement(WithStatement withStatement, object data) {
			Debug.Assert((withStatement != null));
			Debug.Assert((withStatement.Expression != null));
			Debug.Assert((withStatement.Body != null));
			nodeStack.Push(withStatement.Expression);
			withStatement.Expression.AcceptVisitor(this, data);
			withStatement.Expression = ((Expression)(nodeStack.Pop()));
			nodeStack.Push(withStatement.Body);
			withStatement.Body.AcceptVisitor(this, data);
			withStatement.Body = ((BlockStatement)(nodeStack.Pop()));
			return null;
		}
		
		public virtual object VisitYieldStatement(YieldStatement yieldStatement, object data) {
			Debug.Assert((yieldStatement != null));
			Debug.Assert((yieldStatement.Statement != null));
			nodeStack.Push(yieldStatement.Statement);
			yieldStatement.Statement.AcceptVisitor(this, data);
			yieldStatement.Statement = ((Statement)(nodeStack.Pop()));
			return null;
		}
	}
}
