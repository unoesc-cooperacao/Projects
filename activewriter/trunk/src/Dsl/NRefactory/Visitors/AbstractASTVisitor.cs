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
	using System.Diagnostics;
	using Ast;

	public abstract class AbstractAstVisitor : IAstVisitor {
		
		public virtual object VisitAddHandlerStatement(AddHandlerStatement addHandlerStatement, object data) {
			Debug.Assert((addHandlerStatement != null));
			Debug.Assert((addHandlerStatement.EventExpression != null));
			Debug.Assert((addHandlerStatement.HandlerExpression != null));
			addHandlerStatement.EventExpression.AcceptVisitor(this, data);
			return addHandlerStatement.HandlerExpression.AcceptVisitor(this, data);
		}
		
		public virtual object VisitAddressOfExpression(AddressOfExpression addressOfExpression, object data) {
			Debug.Assert((addressOfExpression != null));
			Debug.Assert((addressOfExpression.Expression != null));
			return addressOfExpression.Expression.AcceptVisitor(this, data);
		}
		
		public virtual object VisitAnonymousMethodExpression(AnonymousMethodExpression anonymousMethodExpression, object data) {
			Debug.Assert((anonymousMethodExpression != null));
			Debug.Assert((anonymousMethodExpression.Parameters != null));
			Debug.Assert((anonymousMethodExpression.Body != null));
			foreach (ParameterDeclarationExpression o in anonymousMethodExpression.Parameters) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			return anonymousMethodExpression.Body.AcceptVisitor(this, data);
		}
		
		public virtual object VisitArrayCreateExpression(ArrayCreateExpression arrayCreateExpression, object data) {
			Debug.Assert((arrayCreateExpression != null));
			Debug.Assert((arrayCreateExpression.CreateType != null));
			Debug.Assert((arrayCreateExpression.Arguments != null));
			Debug.Assert((arrayCreateExpression.ArrayInitializer != null));
			arrayCreateExpression.CreateType.AcceptVisitor(this, data);
			foreach (Expression o in arrayCreateExpression.Arguments) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			return arrayCreateExpression.ArrayInitializer.AcceptVisitor(this, data);
		}
		
		public virtual object VisitAssignmentExpression(AssignmentExpression assignmentExpression, object data) {
			Debug.Assert((assignmentExpression != null));
			Debug.Assert((assignmentExpression.Left != null));
			Debug.Assert((assignmentExpression.Right != null));
			assignmentExpression.Left.AcceptVisitor(this, data);
			return assignmentExpression.Right.AcceptVisitor(this, data);
		}
		
		public virtual object VisitAttribute(ICSharpCode.NRefactory.Ast.Attribute attribute, object data) {
			Debug.Assert((attribute != null));
			Debug.Assert((attribute.PositionalArguments != null));
			Debug.Assert((attribute.NamedArguments != null));
			foreach (Expression o in attribute.PositionalArguments) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			foreach (NamedArgumentExpression o in attribute.NamedArguments) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			return null;
		}
		
		public virtual object VisitAttributeSection(AttributeSection attributeSection, object data) {
			Debug.Assert((attributeSection != null));
			Debug.Assert((attributeSection.Attributes != null));
			foreach (ICSharpCode.NRefactory.Ast.Attribute o in attributeSection.Attributes) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
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
			binaryOperatorExpression.Left.AcceptVisitor(this, data);
			return binaryOperatorExpression.Right.AcceptVisitor(this, data);
		}
		
		public virtual object VisitBlockStatement(BlockStatement blockStatement, object data) {
			Debug.Assert((blockStatement != null));
			return blockStatement.AcceptChildren(this, data);
		}
		
		public virtual object VisitBreakStatement(BreakStatement breakStatement, object data) {
			Debug.Assert((breakStatement != null));
			return null;
		}
		
		public virtual object VisitCaseLabel(CaseLabel caseLabel, object data) {
			Debug.Assert((caseLabel != null));
			Debug.Assert((caseLabel.Label != null));
			Debug.Assert((caseLabel.ToExpression != null));
			caseLabel.Label.AcceptVisitor(this, data);
			return caseLabel.ToExpression.AcceptVisitor(this, data);
		}
		
		public virtual object VisitCastExpression(CastExpression castExpression, object data) {
			Debug.Assert((castExpression != null));
			Debug.Assert((castExpression.CastTo != null));
			Debug.Assert((castExpression.Expression != null));
			castExpression.CastTo.AcceptVisitor(this, data);
			return castExpression.Expression.AcceptVisitor(this, data);
		}
		
		public virtual object VisitCatchClause(CatchClause catchClause, object data) {
			Debug.Assert((catchClause != null));
			Debug.Assert((catchClause.TypeReference != null));
			Debug.Assert((catchClause.StatementBlock != null));
			Debug.Assert((catchClause.Condition != null));
			catchClause.TypeReference.AcceptVisitor(this, data);
			catchClause.StatementBlock.AcceptVisitor(this, data);
			return catchClause.Condition.AcceptVisitor(this, data);
		}
		
		public virtual object VisitCheckedExpression(CheckedExpression checkedExpression, object data) {
			Debug.Assert((checkedExpression != null));
			Debug.Assert((checkedExpression.Expression != null));
			return checkedExpression.Expression.AcceptVisitor(this, data);
		}
		
		public virtual object VisitCheckedStatement(CheckedStatement checkedStatement, object data) {
			Debug.Assert((checkedStatement != null));
			Debug.Assert((checkedStatement.Block != null));
			return checkedStatement.Block.AcceptVisitor(this, data);
		}
		
		public virtual object VisitClassReferenceExpression(ClassReferenceExpression classReferenceExpression, object data) {
			Debug.Assert((classReferenceExpression != null));
			return null;
		}
		
		public virtual object VisitCollectionInitializerExpression(CollectionInitializerExpression collectionInitializerExpression, object data) {
			Debug.Assert((collectionInitializerExpression != null));
			Debug.Assert((collectionInitializerExpression.CreateExpressions != null));
			foreach (Expression o in collectionInitializerExpression.CreateExpressions) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			return null;
		}
		
		public virtual object VisitCompilationUnit(CompilationUnit compilationUnit, object data) {
			Debug.Assert((compilationUnit != null));
			return compilationUnit.AcceptChildren(this, data);
		}
		
		public virtual object VisitConditionalExpression(ConditionalExpression conditionalExpression, object data) {
			Debug.Assert((conditionalExpression != null));
			Debug.Assert((conditionalExpression.Condition != null));
			Debug.Assert((conditionalExpression.TrueExpression != null));
			Debug.Assert((conditionalExpression.FalseExpression != null));
			conditionalExpression.Condition.AcceptVisitor(this, data);
			conditionalExpression.TrueExpression.AcceptVisitor(this, data);
			return conditionalExpression.FalseExpression.AcceptVisitor(this, data);
		}
		
		public virtual object VisitConstructorDeclaration(ConstructorDeclaration constructorDeclaration, object data) {
			Debug.Assert((constructorDeclaration != null));
			Debug.Assert((constructorDeclaration.Attributes != null));
			Debug.Assert((constructorDeclaration.Parameters != null));
			Debug.Assert((constructorDeclaration.ConstructorInitializer != null));
			Debug.Assert((constructorDeclaration.Body != null));
			foreach (AttributeSection o in constructorDeclaration.Attributes) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			foreach (ParameterDeclarationExpression o in constructorDeclaration.Parameters) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			constructorDeclaration.ConstructorInitializer.AcceptVisitor(this, data);
			return constructorDeclaration.Body.AcceptVisitor(this, data);
		}
		
		public virtual object VisitConstructorInitializer(ConstructorInitializer constructorInitializer, object data) {
			Debug.Assert((constructorInitializer != null));
			Debug.Assert((constructorInitializer.Arguments != null));
			foreach (Expression o in constructorInitializer.Arguments) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
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
			foreach (AttributeSection o in declareDeclaration.Attributes) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			foreach (ParameterDeclarationExpression o in declareDeclaration.Parameters) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			return declareDeclaration.TypeReference.AcceptVisitor(this, data);
		}
		
		public virtual object VisitDefaultValueExpression(DefaultValueExpression defaultValueExpression, object data) {
			Debug.Assert((defaultValueExpression != null));
			Debug.Assert((defaultValueExpression.TypeReference != null));
			return defaultValueExpression.TypeReference.AcceptVisitor(this, data);
		}
		
		public virtual object VisitDelegateDeclaration(DelegateDeclaration delegateDeclaration, object data) {
			Debug.Assert((delegateDeclaration != null));
			Debug.Assert((delegateDeclaration.Attributes != null));
			Debug.Assert((delegateDeclaration.ReturnType != null));
			Debug.Assert((delegateDeclaration.Parameters != null));
			Debug.Assert((delegateDeclaration.Templates != null));
			foreach (AttributeSection o in delegateDeclaration.Attributes) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			delegateDeclaration.ReturnType.AcceptVisitor(this, data);
			foreach (ParameterDeclarationExpression o in delegateDeclaration.Parameters) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			foreach (TemplateDefinition o in delegateDeclaration.Templates) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			return null;
		}
		
		public virtual object VisitDestructorDeclaration(DestructorDeclaration destructorDeclaration, object data) {
			Debug.Assert((destructorDeclaration != null));
			Debug.Assert((destructorDeclaration.Attributes != null));
			Debug.Assert((destructorDeclaration.Body != null));
			foreach (AttributeSection o in destructorDeclaration.Attributes) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			return destructorDeclaration.Body.AcceptVisitor(this, data);
		}
		
		public virtual object VisitDirectionExpression(DirectionExpression directionExpression, object data) {
			Debug.Assert((directionExpression != null));
			Debug.Assert((directionExpression.Expression != null));
			return directionExpression.Expression.AcceptVisitor(this, data);
		}
		
		public virtual object VisitDoLoopStatement(DoLoopStatement doLoopStatement, object data) {
			Debug.Assert((doLoopStatement != null));
			Debug.Assert((doLoopStatement.Condition != null));
			Debug.Assert((doLoopStatement.EmbeddedStatement != null));
			doLoopStatement.Condition.AcceptVisitor(this, data);
			return doLoopStatement.EmbeddedStatement.AcceptVisitor(this, data);
		}
		
		public virtual object VisitElseIfSection(ElseIfSection elseIfSection, object data) {
			Debug.Assert((elseIfSection != null));
			Debug.Assert((elseIfSection.Condition != null));
			Debug.Assert((elseIfSection.EmbeddedStatement != null));
			elseIfSection.Condition.AcceptVisitor(this, data);
			return elseIfSection.EmbeddedStatement.AcceptVisitor(this, data);
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
			foreach (Expression o in eraseStatement.Expressions) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			return null;
		}
		
		public virtual object VisitErrorStatement(ErrorStatement errorStatement, object data) {
			Debug.Assert((errorStatement != null));
			Debug.Assert((errorStatement.Expression != null));
			return errorStatement.Expression.AcceptVisitor(this, data);
		}
		
		public virtual object VisitEventAddRegion(EventAddRegion eventAddRegion, object data) {
			Debug.Assert((eventAddRegion != null));
			Debug.Assert((eventAddRegion.Attributes != null));
			Debug.Assert((eventAddRegion.Block != null));
			Debug.Assert((eventAddRegion.Parameters != null));
			foreach (AttributeSection o in eventAddRegion.Attributes) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			eventAddRegion.Block.AcceptVisitor(this, data);
			foreach (ParameterDeclarationExpression o in eventAddRegion.Parameters) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
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
			foreach (AttributeSection o in eventDeclaration.Attributes) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			foreach (ParameterDeclarationExpression o in eventDeclaration.Parameters) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			foreach (InterfaceImplementation o in eventDeclaration.InterfaceImplementations) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			eventDeclaration.TypeReference.AcceptVisitor(this, data);
			eventDeclaration.AddRegion.AcceptVisitor(this, data);
			eventDeclaration.RemoveRegion.AcceptVisitor(this, data);
			eventDeclaration.RaiseRegion.AcceptVisitor(this, data);
			return eventDeclaration.Initializer.AcceptVisitor(this, data);
		}
		
		public virtual object VisitEventRaiseRegion(EventRaiseRegion eventRaiseRegion, object data) {
			Debug.Assert((eventRaiseRegion != null));
			Debug.Assert((eventRaiseRegion.Attributes != null));
			Debug.Assert((eventRaiseRegion.Block != null));
			Debug.Assert((eventRaiseRegion.Parameters != null));
			foreach (AttributeSection o in eventRaiseRegion.Attributes) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			eventRaiseRegion.Block.AcceptVisitor(this, data);
			foreach (ParameterDeclarationExpression o in eventRaiseRegion.Parameters) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			return null;
		}
		
		public virtual object VisitEventRemoveRegion(EventRemoveRegion eventRemoveRegion, object data) {
			Debug.Assert((eventRemoveRegion != null));
			Debug.Assert((eventRemoveRegion.Attributes != null));
			Debug.Assert((eventRemoveRegion.Block != null));
			Debug.Assert((eventRemoveRegion.Parameters != null));
			foreach (AttributeSection o in eventRemoveRegion.Attributes) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			eventRemoveRegion.Block.AcceptVisitor(this, data);
			foreach (ParameterDeclarationExpression o in eventRemoveRegion.Parameters) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
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
			expressionRangeVariable.Expression.AcceptVisitor(this, data);
			return expressionRangeVariable.Type.AcceptVisitor(this, data);
		}
		
		public virtual object VisitExpressionStatement(ExpressionStatement expressionStatement, object data) {
			Debug.Assert((expressionStatement != null));
			Debug.Assert((expressionStatement.Expression != null));
			return expressionStatement.Expression.AcceptVisitor(this, data);
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
			foreach (AttributeSection o in fieldDeclaration.Attributes) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			fieldDeclaration.TypeReference.AcceptVisitor(this, data);
			foreach (VariableDeclaration o in fieldDeclaration.Fields) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			return null;
		}
		
		public virtual object VisitFixedStatement(FixedStatement fixedStatement, object data) {
			Debug.Assert((fixedStatement != null));
			Debug.Assert((fixedStatement.PointerDeclaration != null));
			Debug.Assert((fixedStatement.EmbeddedStatement != null));
			fixedStatement.PointerDeclaration.AcceptVisitor(this, data);
			return fixedStatement.EmbeddedStatement.AcceptVisitor(this, data);
		}
		
		public virtual object VisitForeachStatement(ForeachStatement foreachStatement, object data) {
			Debug.Assert((foreachStatement != null));
			Debug.Assert((foreachStatement.TypeReference != null));
			Debug.Assert((foreachStatement.Expression != null));
			Debug.Assert((foreachStatement.NextExpression != null));
			Debug.Assert((foreachStatement.EmbeddedStatement != null));
			foreachStatement.TypeReference.AcceptVisitor(this, data);
			foreachStatement.Expression.AcceptVisitor(this, data);
			foreachStatement.NextExpression.AcceptVisitor(this, data);
			return foreachStatement.EmbeddedStatement.AcceptVisitor(this, data);
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
			forNextStatement.Start.AcceptVisitor(this, data);
			forNextStatement.End.AcceptVisitor(this, data);
			forNextStatement.Step.AcceptVisitor(this, data);
			foreach (Expression o in forNextStatement.NextExpressions) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			forNextStatement.TypeReference.AcceptVisitor(this, data);
			forNextStatement.LoopVariableExpression.AcceptVisitor(this, data);
			return forNextStatement.EmbeddedStatement.AcceptVisitor(this, data);
		}
		
		public virtual object VisitForStatement(ForStatement forStatement, object data) {
			Debug.Assert((forStatement != null));
			Debug.Assert((forStatement.Initializers != null));
			Debug.Assert((forStatement.Condition != null));
			Debug.Assert((forStatement.Iterator != null));
			Debug.Assert((forStatement.EmbeddedStatement != null));
			foreach (Statement o in forStatement.Initializers) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			forStatement.Condition.AcceptVisitor(this, data);
			foreach (Statement o in forStatement.Iterator) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			return forStatement.EmbeddedStatement.AcceptVisitor(this, data);
		}
		
		public virtual object VisitGotoCaseStatement(GotoCaseStatement gotoCaseStatement, object data) {
			Debug.Assert((gotoCaseStatement != null));
			Debug.Assert((gotoCaseStatement.Expression != null));
			return gotoCaseStatement.Expression.AcceptVisitor(this, data);
		}
		
		public virtual object VisitGotoStatement(GotoStatement gotoStatement, object data) {
			Debug.Assert((gotoStatement != null));
			return null;
		}
		
		public virtual object VisitIdentifierExpression(IdentifierExpression identifierExpression, object data) {
			Debug.Assert((identifierExpression != null));
			Debug.Assert((identifierExpression.TypeArguments != null));
			foreach (TypeReference o in identifierExpression.TypeArguments) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			return null;
		}
		
		public virtual object VisitIfElseStatement(IfElseStatement ifElseStatement, object data) {
			Debug.Assert((ifElseStatement != null));
			Debug.Assert((ifElseStatement.Condition != null));
			Debug.Assert((ifElseStatement.TrueStatement != null));
			Debug.Assert((ifElseStatement.FalseStatement != null));
			Debug.Assert((ifElseStatement.ElseIfSections != null));
			ifElseStatement.Condition.AcceptVisitor(this, data);
			foreach (Statement o in ifElseStatement.TrueStatement) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			foreach (Statement o in ifElseStatement.FalseStatement) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			foreach (ElseIfSection o in ifElseStatement.ElseIfSections) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
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
			foreach (AttributeSection o in indexerDeclaration.Attributes) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			foreach (ParameterDeclarationExpression o in indexerDeclaration.Parameters) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			foreach (InterfaceImplementation o in indexerDeclaration.InterfaceImplementations) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			indexerDeclaration.TypeReference.AcceptVisitor(this, data);
			indexerDeclaration.GetRegion.AcceptVisitor(this, data);
			return indexerDeclaration.SetRegion.AcceptVisitor(this, data);
		}
		
		public virtual object VisitIndexerExpression(IndexerExpression indexerExpression, object data) {
			Debug.Assert((indexerExpression != null));
			Debug.Assert((indexerExpression.TargetObject != null));
			Debug.Assert((indexerExpression.Indexes != null));
			indexerExpression.TargetObject.AcceptVisitor(this, data);
			foreach (Expression o in indexerExpression.Indexes) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			return null;
		}
		
		public virtual object VisitInnerClassTypeReference(InnerClassTypeReference innerClassTypeReference, object data) {
			Debug.Assert((innerClassTypeReference != null));
			Debug.Assert((innerClassTypeReference.GenericTypes != null));
			Debug.Assert((innerClassTypeReference.BaseType != null));
			foreach (TypeReference o in innerClassTypeReference.GenericTypes) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			return innerClassTypeReference.BaseType.AcceptVisitor(this, data);
		}
		
		public virtual object VisitInterfaceImplementation(InterfaceImplementation interfaceImplementation, object data) {
			Debug.Assert((interfaceImplementation != null));
			Debug.Assert((interfaceImplementation.InterfaceType != null));
			return interfaceImplementation.InterfaceType.AcceptVisitor(this, data);
		}
		
		public virtual object VisitInvocationExpression(InvocationExpression invocationExpression, object data) {
			Debug.Assert((invocationExpression != null));
			Debug.Assert((invocationExpression.TargetObject != null));
			Debug.Assert((invocationExpression.Arguments != null));
			invocationExpression.TargetObject.AcceptVisitor(this, data);
			foreach (Expression o in invocationExpression.Arguments) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
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
			foreach (ParameterDeclarationExpression o in lambdaExpression.Parameters) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			lambdaExpression.StatementBody.AcceptVisitor(this, data);
			return lambdaExpression.ExpressionBody.AcceptVisitor(this, data);
		}
		
		public virtual object VisitLocalVariableDeclaration(LocalVariableDeclaration localVariableDeclaration, object data) {
			Debug.Assert((localVariableDeclaration != null));
			Debug.Assert((localVariableDeclaration.TypeReference != null));
			Debug.Assert((localVariableDeclaration.Variables != null));
			localVariableDeclaration.TypeReference.AcceptVisitor(this, data);
			foreach (VariableDeclaration o in localVariableDeclaration.Variables) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			return null;
		}
		
		public virtual object VisitLockStatement(LockStatement lockStatement, object data) {
			Debug.Assert((lockStatement != null));
			Debug.Assert((lockStatement.LockExpression != null));
			Debug.Assert((lockStatement.EmbeddedStatement != null));
			lockStatement.LockExpression.AcceptVisitor(this, data);
			return lockStatement.EmbeddedStatement.AcceptVisitor(this, data);
		}
		
		public virtual object VisitMemberReferenceExpression(MemberReferenceExpression memberReferenceExpression, object data) {
			Debug.Assert((memberReferenceExpression != null));
			Debug.Assert((memberReferenceExpression.TargetObject != null));
			Debug.Assert((memberReferenceExpression.TypeArguments != null));
			memberReferenceExpression.TargetObject.AcceptVisitor(this, data);
			foreach (TypeReference o in memberReferenceExpression.TypeArguments) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
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
			foreach (AttributeSection o in methodDeclaration.Attributes) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			foreach (ParameterDeclarationExpression o in methodDeclaration.Parameters) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			foreach (InterfaceImplementation o in methodDeclaration.InterfaceImplementations) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			methodDeclaration.TypeReference.AcceptVisitor(this, data);
			methodDeclaration.Body.AcceptVisitor(this, data);
			foreach (TemplateDefinition o in methodDeclaration.Templates) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			return null;
		}
		
		public virtual object VisitNamedArgumentExpression(NamedArgumentExpression namedArgumentExpression, object data) {
			Debug.Assert((namedArgumentExpression != null));
			Debug.Assert((namedArgumentExpression.Expression != null));
			return namedArgumentExpression.Expression.AcceptVisitor(this, data);
		}
		
		public virtual object VisitNamespaceDeclaration(NamespaceDeclaration namespaceDeclaration, object data) {
			Debug.Assert((namespaceDeclaration != null));
			return namespaceDeclaration.AcceptChildren(this, data);
		}
		
		public virtual object VisitObjectCreateExpression(ObjectCreateExpression objectCreateExpression, object data) {
			Debug.Assert((objectCreateExpression != null));
			Debug.Assert((objectCreateExpression.CreateType != null));
			Debug.Assert((objectCreateExpression.Parameters != null));
			Debug.Assert((objectCreateExpression.ObjectInitializer != null));
			objectCreateExpression.CreateType.AcceptVisitor(this, data);
			foreach (Expression o in objectCreateExpression.Parameters) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			return objectCreateExpression.ObjectInitializer.AcceptVisitor(this, data);
		}
		
		public virtual object VisitOnErrorStatement(OnErrorStatement onErrorStatement, object data) {
			Debug.Assert((onErrorStatement != null));
			Debug.Assert((onErrorStatement.EmbeddedStatement != null));
			return onErrorStatement.EmbeddedStatement.AcceptVisitor(this, data);
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
			foreach (AttributeSection o in operatorDeclaration.Attributes) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			foreach (ParameterDeclarationExpression o in operatorDeclaration.Parameters) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			foreach (InterfaceImplementation o in operatorDeclaration.InterfaceImplementations) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			operatorDeclaration.TypeReference.AcceptVisitor(this, data);
			operatorDeclaration.Body.AcceptVisitor(this, data);
			foreach (TemplateDefinition o in operatorDeclaration.Templates) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			foreach (AttributeSection o in operatorDeclaration.ReturnTypeAttributes) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
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
			foreach (AttributeSection o in parameterDeclarationExpression.Attributes) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			parameterDeclarationExpression.TypeReference.AcceptVisitor(this, data);
			return parameterDeclarationExpression.DefaultValue.AcceptVisitor(this, data);
		}
		
		public virtual object VisitParenthesizedExpression(ParenthesizedExpression parenthesizedExpression, object data) {
			Debug.Assert((parenthesizedExpression != null));
			Debug.Assert((parenthesizedExpression.Expression != null));
			return parenthesizedExpression.Expression.AcceptVisitor(this, data);
		}
		
		public virtual object VisitPointerReferenceExpression(PointerReferenceExpression pointerReferenceExpression, object data) {
			Debug.Assert((pointerReferenceExpression != null));
			Debug.Assert((pointerReferenceExpression.TargetObject != null));
			Debug.Assert((pointerReferenceExpression.TypeArguments != null));
			pointerReferenceExpression.TargetObject.AcceptVisitor(this, data);
			foreach (TypeReference o in pointerReferenceExpression.TypeArguments) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
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
			foreach (AttributeSection o in propertyDeclaration.Attributes) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			foreach (ParameterDeclarationExpression o in propertyDeclaration.Parameters) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			foreach (InterfaceImplementation o in propertyDeclaration.InterfaceImplementations) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			propertyDeclaration.TypeReference.AcceptVisitor(this, data);
			propertyDeclaration.GetRegion.AcceptVisitor(this, data);
			return propertyDeclaration.SetRegion.AcceptVisitor(this, data);
		}
		
		public virtual object VisitPropertyGetRegion(PropertyGetRegion propertyGetRegion, object data) {
			Debug.Assert((propertyGetRegion != null));
			Debug.Assert((propertyGetRegion.Attributes != null));
			Debug.Assert((propertyGetRegion.Block != null));
			foreach (AttributeSection o in propertyGetRegion.Attributes) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			return propertyGetRegion.Block.AcceptVisitor(this, data);
		}
		
		public virtual object VisitPropertySetRegion(PropertySetRegion propertySetRegion, object data) {
			Debug.Assert((propertySetRegion != null));
			Debug.Assert((propertySetRegion.Attributes != null));
			Debug.Assert((propertySetRegion.Block != null));
			Debug.Assert((propertySetRegion.Parameters != null));
			foreach (AttributeSection o in propertySetRegion.Attributes) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			propertySetRegion.Block.AcceptVisitor(this, data);
			foreach (ParameterDeclarationExpression o in propertySetRegion.Parameters) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			return null;
		}
		
		public virtual object VisitQueryExpression(QueryExpression queryExpression, object data) {
			Debug.Assert((queryExpression != null));
			Debug.Assert((queryExpression.FromClause != null));
			Debug.Assert((queryExpression.MiddleClauses != null));
			Debug.Assert((queryExpression.SelectOrGroupClause != null));
			queryExpression.FromClause.AcceptVisitor(this, data);
			foreach (QueryExpressionClause o in queryExpression.MiddleClauses) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			return queryExpression.SelectOrGroupClause.AcceptVisitor(this, data);
		}
		
		public virtual object VisitQueryExpressionAggregateClause(QueryExpressionAggregateClause queryExpressionAggregateClause, object data) {
			Debug.Assert((queryExpressionAggregateClause != null));
			Debug.Assert((queryExpressionAggregateClause.FromClause != null));
			Debug.Assert((queryExpressionAggregateClause.MiddleClauses != null));
			Debug.Assert((queryExpressionAggregateClause.IntoVariables != null));
			queryExpressionAggregateClause.FromClause.AcceptVisitor(this, data);
			foreach (QueryExpressionClause o in queryExpressionAggregateClause.MiddleClauses) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			foreach (ExpressionRangeVariable o in queryExpressionAggregateClause.IntoVariables) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
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
			queryExpressionFromClause.Type.AcceptVisitor(this, data);
			return queryExpressionFromClause.InExpression.AcceptVisitor(this, data);
		}
		
		public virtual object VisitQueryExpressionGroupClause(QueryExpressionGroupClause queryExpressionGroupClause, object data) {
			Debug.Assert((queryExpressionGroupClause != null));
			Debug.Assert((queryExpressionGroupClause.Projection != null));
			Debug.Assert((queryExpressionGroupClause.GroupBy != null));
			queryExpressionGroupClause.Projection.AcceptVisitor(this, data);
			return queryExpressionGroupClause.GroupBy.AcceptVisitor(this, data);
		}
		
		public virtual object VisitQueryExpressionGroupJoinVBClause(QueryExpressionGroupJoinVBClause queryExpressionGroupJoinVBClause, object data) {
			Debug.Assert((queryExpressionGroupJoinVBClause != null));
			Debug.Assert((queryExpressionGroupJoinVBClause.JoinClause != null));
			Debug.Assert((queryExpressionGroupJoinVBClause.IntoVariables != null));
			queryExpressionGroupJoinVBClause.JoinClause.AcceptVisitor(this, data);
			foreach (ExpressionRangeVariable o in queryExpressionGroupJoinVBClause.IntoVariables) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			return null;
		}
		
		public virtual object VisitQueryExpressionGroupVBClause(QueryExpressionGroupVBClause queryExpressionGroupVBClause, object data) {
			Debug.Assert((queryExpressionGroupVBClause != null));
			Debug.Assert((queryExpressionGroupVBClause.GroupVariables != null));
			Debug.Assert((queryExpressionGroupVBClause.ByVariables != null));
			Debug.Assert((queryExpressionGroupVBClause.IntoVariables != null));
			foreach (ExpressionRangeVariable o in queryExpressionGroupVBClause.GroupVariables) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			foreach (ExpressionRangeVariable o in queryExpressionGroupVBClause.ByVariables) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			foreach (ExpressionRangeVariable o in queryExpressionGroupVBClause.IntoVariables) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			return null;
		}
		
		public virtual object VisitQueryExpressionJoinClause(QueryExpressionJoinClause queryExpressionJoinClause, object data) {
			Debug.Assert((queryExpressionJoinClause != null));
			Debug.Assert((queryExpressionJoinClause.Type != null));
			Debug.Assert((queryExpressionJoinClause.InExpression != null));
			Debug.Assert((queryExpressionJoinClause.OnExpression != null));
			Debug.Assert((queryExpressionJoinClause.EqualsExpression != null));
			queryExpressionJoinClause.Type.AcceptVisitor(this, data);
			queryExpressionJoinClause.InExpression.AcceptVisitor(this, data);
			queryExpressionJoinClause.OnExpression.AcceptVisitor(this, data);
			return queryExpressionJoinClause.EqualsExpression.AcceptVisitor(this, data);
		}
		
		public virtual object VisitQueryExpressionJoinConditionVB(QueryExpressionJoinConditionVB queryExpressionJoinConditionVB, object data) {
			Debug.Assert((queryExpressionJoinConditionVB != null));
			Debug.Assert((queryExpressionJoinConditionVB.LeftSide != null));
			Debug.Assert((queryExpressionJoinConditionVB.RightSide != null));
			queryExpressionJoinConditionVB.LeftSide.AcceptVisitor(this, data);
			return queryExpressionJoinConditionVB.RightSide.AcceptVisitor(this, data);
		}
		
		public virtual object VisitQueryExpressionJoinVBClause(QueryExpressionJoinVBClause queryExpressionJoinVBClause, object data) {
			Debug.Assert((queryExpressionJoinVBClause != null));
			Debug.Assert((queryExpressionJoinVBClause.JoinVariable != null));
			Debug.Assert((queryExpressionJoinVBClause.SubJoin != null));
			Debug.Assert((queryExpressionJoinVBClause.Conditions != null));
			queryExpressionJoinVBClause.JoinVariable.AcceptVisitor(this, data);
			queryExpressionJoinVBClause.SubJoin.AcceptVisitor(this, data);
			foreach (QueryExpressionJoinConditionVB o in queryExpressionJoinVBClause.Conditions) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			return null;
		}
		
		public virtual object VisitQueryExpressionLetClause(QueryExpressionLetClause queryExpressionLetClause, object data) {
			Debug.Assert((queryExpressionLetClause != null));
			Debug.Assert((queryExpressionLetClause.Expression != null));
			return queryExpressionLetClause.Expression.AcceptVisitor(this, data);
		}
		
		public virtual object VisitQueryExpressionLetVBClause(QueryExpressionLetVBClause queryExpressionLetVBClause, object data) {
			Debug.Assert((queryExpressionLetVBClause != null));
			Debug.Assert((queryExpressionLetVBClause.Variables != null));
			foreach (ExpressionRangeVariable o in queryExpressionLetVBClause.Variables) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			return null;
		}
		
		public virtual object VisitQueryExpressionOrderClause(QueryExpressionOrderClause queryExpressionOrderClause, object data) {
			Debug.Assert((queryExpressionOrderClause != null));
			Debug.Assert((queryExpressionOrderClause.Orderings != null));
			foreach (QueryExpressionOrdering o in queryExpressionOrderClause.Orderings) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			return null;
		}
		
		public virtual object VisitQueryExpressionOrdering(QueryExpressionOrdering queryExpressionOrdering, object data) {
			Debug.Assert((queryExpressionOrdering != null));
			Debug.Assert((queryExpressionOrdering.Criteria != null));
			return queryExpressionOrdering.Criteria.AcceptVisitor(this, data);
		}
		
		public virtual object VisitQueryExpressionPartitionVBClause(QueryExpressionPartitionVBClause queryExpressionPartitionVBClause, object data) {
			Debug.Assert((queryExpressionPartitionVBClause != null));
			Debug.Assert((queryExpressionPartitionVBClause.Expression != null));
			return queryExpressionPartitionVBClause.Expression.AcceptVisitor(this, data);
		}
		
		public virtual object VisitQueryExpressionSelectClause(QueryExpressionSelectClause queryExpressionSelectClause, object data) {
			Debug.Assert((queryExpressionSelectClause != null));
			Debug.Assert((queryExpressionSelectClause.Projection != null));
			return queryExpressionSelectClause.Projection.AcceptVisitor(this, data);
		}
		
		public virtual object VisitQueryExpressionSelectVBClause(QueryExpressionSelectVBClause queryExpressionSelectVBClause, object data) {
			Debug.Assert((queryExpressionSelectVBClause != null));
			Debug.Assert((queryExpressionSelectVBClause.Variables != null));
			foreach (ExpressionRangeVariable o in queryExpressionSelectVBClause.Variables) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			return null;
		}
		
		public virtual object VisitQueryExpressionWhereClause(QueryExpressionWhereClause queryExpressionWhereClause, object data) {
			Debug.Assert((queryExpressionWhereClause != null));
			Debug.Assert((queryExpressionWhereClause.Condition != null));
			return queryExpressionWhereClause.Condition.AcceptVisitor(this, data);
		}
		
		public virtual object VisitRaiseEventStatement(RaiseEventStatement raiseEventStatement, object data) {
			Debug.Assert((raiseEventStatement != null));
			Debug.Assert((raiseEventStatement.Arguments != null));
			foreach (Expression o in raiseEventStatement.Arguments) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			return null;
		}
		
		public virtual object VisitReDimStatement(ReDimStatement reDimStatement, object data) {
			Debug.Assert((reDimStatement != null));
			Debug.Assert((reDimStatement.ReDimClauses != null));
			foreach (InvocationExpression o in reDimStatement.ReDimClauses) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			return null;
		}
		
		public virtual object VisitRemoveHandlerStatement(RemoveHandlerStatement removeHandlerStatement, object data) {
			Debug.Assert((removeHandlerStatement != null));
			Debug.Assert((removeHandlerStatement.EventExpression != null));
			Debug.Assert((removeHandlerStatement.HandlerExpression != null));
			removeHandlerStatement.EventExpression.AcceptVisitor(this, data);
			return removeHandlerStatement.HandlerExpression.AcceptVisitor(this, data);
		}
		
		public virtual object VisitResumeStatement(ResumeStatement resumeStatement, object data) {
			Debug.Assert((resumeStatement != null));
			return null;
		}
		
		public virtual object VisitReturnStatement(ReturnStatement returnStatement, object data) {
			Debug.Assert((returnStatement != null));
			Debug.Assert((returnStatement.Expression != null));
			return returnStatement.Expression.AcceptVisitor(this, data);
		}
		
		public virtual object VisitSizeOfExpression(SizeOfExpression sizeOfExpression, object data) {
			Debug.Assert((sizeOfExpression != null));
			Debug.Assert((sizeOfExpression.TypeReference != null));
			return sizeOfExpression.TypeReference.AcceptVisitor(this, data);
		}
		
		public virtual object VisitStackAllocExpression(StackAllocExpression stackAllocExpression, object data) {
			Debug.Assert((stackAllocExpression != null));
			Debug.Assert((stackAllocExpression.TypeReference != null));
			Debug.Assert((stackAllocExpression.Expression != null));
			stackAllocExpression.TypeReference.AcceptVisitor(this, data);
			return stackAllocExpression.Expression.AcceptVisitor(this, data);
		}
		
		public virtual object VisitStopStatement(StopStatement stopStatement, object data) {
			Debug.Assert((stopStatement != null));
			return null;
		}
		
		public virtual object VisitSwitchSection(SwitchSection switchSection, object data) {
			Debug.Assert((switchSection != null));
			Debug.Assert((switchSection.SwitchLabels != null));
			foreach (CaseLabel o in switchSection.SwitchLabels) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			return switchSection.AcceptChildren(this, data);
		}
		
		public virtual object VisitSwitchStatement(SwitchStatement switchStatement, object data) {
			Debug.Assert((switchStatement != null));
			Debug.Assert((switchStatement.SwitchExpression != null));
			Debug.Assert((switchStatement.SwitchSections != null));
			switchStatement.SwitchExpression.AcceptVisitor(this, data);
			foreach (SwitchSection o in switchStatement.SwitchSections) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			return null;
		}
		
		public virtual object VisitTemplateDefinition(TemplateDefinition templateDefinition, object data) {
			Debug.Assert((templateDefinition != null));
			Debug.Assert((templateDefinition.Attributes != null));
			Debug.Assert((templateDefinition.Bases != null));
			foreach (AttributeSection o in templateDefinition.Attributes) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			foreach (TypeReference o in templateDefinition.Bases) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
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
			return throwStatement.Expression.AcceptVisitor(this, data);
		}
		
		public virtual object VisitTryCatchStatement(TryCatchStatement tryCatchStatement, object data) {
			Debug.Assert((tryCatchStatement != null));
			Debug.Assert((tryCatchStatement.StatementBlock != null));
			Debug.Assert((tryCatchStatement.CatchClauses != null));
			Debug.Assert((tryCatchStatement.FinallyBlock != null));
			tryCatchStatement.StatementBlock.AcceptVisitor(this, data);
			foreach (CatchClause o in tryCatchStatement.CatchClauses) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			return tryCatchStatement.FinallyBlock.AcceptVisitor(this, data);
		}
		
		public virtual object VisitTypeDeclaration(TypeDeclaration typeDeclaration, object data) {
			Debug.Assert((typeDeclaration != null));
			Debug.Assert((typeDeclaration.Attributes != null));
			Debug.Assert((typeDeclaration.BaseTypes != null));
			Debug.Assert((typeDeclaration.Templates != null));
			foreach (AttributeSection o in typeDeclaration.Attributes) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			foreach (TypeReference o in typeDeclaration.BaseTypes) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			foreach (TemplateDefinition o in typeDeclaration.Templates) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			return typeDeclaration.AcceptChildren(this, data);
		}
		
		public virtual object VisitTypeOfExpression(TypeOfExpression typeOfExpression, object data) {
			Debug.Assert((typeOfExpression != null));
			Debug.Assert((typeOfExpression.TypeReference != null));
			return typeOfExpression.TypeReference.AcceptVisitor(this, data);
		}
		
		public virtual object VisitTypeOfIsExpression(TypeOfIsExpression typeOfIsExpression, object data) {
			Debug.Assert((typeOfIsExpression != null));
			Debug.Assert((typeOfIsExpression.Expression != null));
			Debug.Assert((typeOfIsExpression.TypeReference != null));
			typeOfIsExpression.Expression.AcceptVisitor(this, data);
			return typeOfIsExpression.TypeReference.AcceptVisitor(this, data);
		}
		
		public virtual object VisitTypeReference(TypeReference typeReference, object data) {
			Debug.Assert((typeReference != null));
			Debug.Assert((typeReference.GenericTypes != null));
			foreach (TypeReference o in typeReference.GenericTypes) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			return null;
		}
		
		public virtual object VisitTypeReferenceExpression(TypeReferenceExpression typeReferenceExpression, object data) {
			Debug.Assert((typeReferenceExpression != null));
			Debug.Assert((typeReferenceExpression.TypeReference != null));
			return typeReferenceExpression.TypeReference.AcceptVisitor(this, data);
		}
		
		public virtual object VisitUnaryOperatorExpression(UnaryOperatorExpression unaryOperatorExpression, object data) {
			Debug.Assert((unaryOperatorExpression != null));
			Debug.Assert((unaryOperatorExpression.Expression != null));
			return unaryOperatorExpression.Expression.AcceptVisitor(this, data);
		}
		
		public virtual object VisitUncheckedExpression(UncheckedExpression uncheckedExpression, object data) {
			Debug.Assert((uncheckedExpression != null));
			Debug.Assert((uncheckedExpression.Expression != null));
			return uncheckedExpression.Expression.AcceptVisitor(this, data);
		}
		
		public virtual object VisitUncheckedStatement(UncheckedStatement uncheckedStatement, object data) {
			Debug.Assert((uncheckedStatement != null));
			Debug.Assert((uncheckedStatement.Block != null));
			return uncheckedStatement.Block.AcceptVisitor(this, data);
		}
		
		public virtual object VisitUnsafeStatement(UnsafeStatement unsafeStatement, object data) {
			Debug.Assert((unsafeStatement != null));
			Debug.Assert((unsafeStatement.Block != null));
			return unsafeStatement.Block.AcceptVisitor(this, data);
		}
		
		public virtual object VisitUsing(Using @using, object data) {
			Debug.Assert((@using != null));
			Debug.Assert((@using.Alias != null));
			return @using.Alias.AcceptVisitor(this, data);
		}
		
		public virtual object VisitUsingDeclaration(UsingDeclaration usingDeclaration, object data) {
			Debug.Assert((usingDeclaration != null));
			Debug.Assert((usingDeclaration.Usings != null));
			foreach (Using o in usingDeclaration.Usings) {
				Debug.Assert(o != null);
				o.AcceptVisitor(this, data);
			}
			return null;
		}
		
		public virtual object VisitUsingStatement(UsingStatement usingStatement, object data) {
			Debug.Assert((usingStatement != null));
			Debug.Assert((usingStatement.ResourceAcquisition != null));
			Debug.Assert((usingStatement.EmbeddedStatement != null));
			usingStatement.ResourceAcquisition.AcceptVisitor(this, data);
			return usingStatement.EmbeddedStatement.AcceptVisitor(this, data);
		}
		
		public virtual object VisitVariableDeclaration(VariableDeclaration variableDeclaration, object data) {
			Debug.Assert((variableDeclaration != null));
			Debug.Assert((variableDeclaration.Initializer != null));
			Debug.Assert((variableDeclaration.TypeReference != null));
			Debug.Assert((variableDeclaration.FixedArrayInitialization != null));
			variableDeclaration.Initializer.AcceptVisitor(this, data);
			variableDeclaration.TypeReference.AcceptVisitor(this, data);
			return variableDeclaration.FixedArrayInitialization.AcceptVisitor(this, data);
		}
		
		public virtual object VisitWithStatement(WithStatement withStatement, object data) {
			Debug.Assert((withStatement != null));
			Debug.Assert((withStatement.Expression != null));
			Debug.Assert((withStatement.Body != null));
			withStatement.Expression.AcceptVisitor(this, data);
			return withStatement.Body.AcceptVisitor(this, data);
		}
		
		public virtual object VisitYieldStatement(YieldStatement yieldStatement, object data) {
			Debug.Assert((yieldStatement != null));
			Debug.Assert((yieldStatement.Statement != null));
			return yieldStatement.Statement.AcceptVisitor(this, data);
		}
	}
}
