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

namespace ICSharpCode.NRefactory.Parser.VB
{
	using System.Collections;

	public static class Tokens
	{
		// ----- terminal classes -----
		public const int EOF                  = 0;
		public const int EOL                  = 1;
		public const int Identifier           = 2;
		public const int LiteralString        = 3;
		public const int LiteralCharacter     = 4;
		public const int LiteralInteger       = 5;
		public const int LiteralDouble        = 6;
		public const int LiteralSingle        = 7;
		public const int LiteralDecimal       = 8;
		public const int LiteralDate          = 9;

		// ----- special character -----
		public const int Assign               = 10;
		public const int Colon                = 11;
		public const int Comma                = 12;
		public const int ConcatString         = 13;
		public const int Div                  = 14;
		public const int DivInteger           = 15;
		public const int Dot                  = 16;
		public const int ExclamationMark      = 17;
		public const int Minus                = 18;
		public const int Plus                 = 19;
		public const int Power                = 20;
		public const int QuestionMark         = 21;
		public const int Times                = 22;
		public const int OpenCurlyBrace       = 23;
		public const int CloseCurlyBrace      = 24;
		public const int OpenParenthesis      = 25;
		public const int CloseParenthesis     = 26;
		public const int GreaterThan          = 27;
		public const int LessThan             = 28;
		public const int NotEqual             = 29;
		public const int GreaterEqual         = 30;
		public const int LessEqual            = 31;
		public const int ShiftLeft            = 32;
		public const int ShiftRight           = 33;
		public const int PlusAssign           = 34;
		public const int PowerAssign          = 35;
		public const int MinusAssign          = 36;
		public const int TimesAssign          = 37;
		public const int DivAssign            = 38;
		public const int DivIntegerAssign     = 39;
		public const int ShiftLeftAssign      = 40;
		public const int ShiftRightAssign     = 41;
		public const int ConcatStringAssign   = 42;

		// ----- keywords -----
		public const int AddHandler           = 43;
		public const int AddressOf            = 44;
		public const int Aggregate            = 45;
		public const int Alias                = 46;
		public const int And                  = 47;
		public const int AndAlso              = 48;
		public const int Ansi                 = 49;
		public const int As                   = 50;
		public const int Ascending            = 51;
		public const int Assembly             = 52;
		public const int Auto                 = 53;
		public const int Binary               = 54;
		public const int Boolean              = 55;
		public const int ByRef                = 56;
		public const int By                   = 57;
		public const int Byte                 = 58;
		public const int ByVal                = 59;
		public const int Call                 = 60;
		public const int Case                 = 61;
		public const int Catch                = 62;
		public const int CBool                = 63;
		public const int CByte                = 64;
		public const int CChar                = 65;
		public const int CDate                = 66;
		public const int CDbl                 = 67;
		public const int CDec                 = 68;
		public const int Char                 = 69;
		public const int CInt                 = 70;
		public const int Class                = 71;
		public const int CLng                 = 72;
		public const int CObj                 = 73;
		public const int Compare              = 74;
		public const int Const                = 75;
		public const int Continue             = 76;
		public const int CSByte               = 77;
		public const int CShort               = 78;
		public const int CSng                 = 79;
		public const int CStr                 = 80;
		public const int CType                = 81;
		public const int CUInt                = 82;
		public const int CULng                = 83;
		public const int CUShort              = 84;
		public const int Custom               = 85;
		public const int Date                 = 86;
		public const int Decimal              = 87;
		public const int Declare              = 88;
		public const int Default              = 89;
		public const int Delegate             = 90;
		public const int Descending           = 91;
		public const int Dim                  = 92;
		public const int DirectCast           = 93;
		public const int Distinct             = 94;
		public const int Do                   = 95;
		public const int Double               = 96;
		public const int Each                 = 97;
		public const int Else                 = 98;
		public const int ElseIf               = 99;
		public const int End                  = 100;
		public const int EndIf                = 101;
		public const int Enum                 = 102;
		new public const int Equals               = 103;
		public const int Erase                = 104;
		public const int Error                = 105;
		public const int Event                = 106;
		public const int Exit                 = 107;
		public const int Explicit             = 108;
		public const int False                = 109;
		public const int Finally              = 110;
		public const int For                  = 111;
		public const int Friend               = 112;
		public const int From                 = 113;
		public const int Function             = 114;
		public const int Get                  = 115;
		new public const int GetType              = 116;
		public const int Global               = 117;
		public const int GoSub                = 118;
		public const int GoTo                 = 119;
		public const int Group                = 120;
		public const int Handles              = 121;
		public const int If                   = 122;
		public const int Implements           = 123;
		public const int Imports              = 124;
		public const int In                   = 125;
		public const int Infer                = 126;
		public const int Inherits             = 127;
		public const int Integer              = 128;
		public const int Interface            = 129;
		public const int Into                 = 130;
		public const int Is                   = 131;
		public const int IsNot                = 132;
		public const int Join                 = 133;
		public const int Let                  = 134;
		public const int Lib                  = 135;
		public const int Like                 = 136;
		public const int Long                 = 137;
		public const int Loop                 = 138;
		public const int Me                   = 139;
		public const int Mod                  = 140;
		public const int Module               = 141;
		public const int MustInherit          = 142;
		public const int MustOverride         = 143;
		public const int MyBase               = 144;
		public const int MyClass              = 145;
		public const int Namespace            = 146;
		public const int Narrowing            = 147;
		public const int New                  = 148;
		public const int Next                 = 149;
		public const int Not                  = 150;
		public const int Nothing              = 151;
		public const int NotInheritable       = 152;
		public const int NotOverridable       = 153;
		public const int Object               = 154;
		public const int Of                   = 155;
		public const int Off                  = 156;
		public const int On                   = 157;
		public const int Operator             = 158;
		public const int Option               = 159;
		public const int Optional             = 160;
		public const int Or                   = 161;
		public const int Order                = 162;
		public const int OrElse               = 163;
		public const int Overloads            = 164;
		public const int Overridable          = 165;
		public const int Overrides            = 166;
		public const int ParamArray           = 167;
		public const int Partial              = 168;
		public const int Preserve             = 169;
		public const int Private              = 170;
		public const int Property             = 171;
		public const int Protected            = 172;
		public const int Public               = 173;
		public const int RaiseEvent           = 174;
		public const int ReadOnly             = 175;
		public const int ReDim                = 176;
		public const int Rem                  = 177;
		public const int RemoveHandler        = 178;
		public const int Resume               = 179;
		public const int Return               = 180;
		public const int SByte                = 181;
		public const int Select               = 182;
		public const int Set                  = 183;
		public const int Shadows              = 184;
		public const int Shared               = 185;
		public const int Short                = 186;
		public const int Single               = 187;
		public const int Skip                 = 188;
		public const int Static               = 189;
		public const int Step                 = 190;
		public const int Stop                 = 191;
		public const int Strict               = 192;
		public const int String               = 193;
		public const int Structure            = 194;
		public const int Sub                  = 195;
		public const int SyncLock             = 196;
		public const int Take                 = 197;
		public const int Text                 = 198;
		public const int Then                 = 199;
		public const int Throw                = 200;
		public const int To                   = 201;
		public const int True                 = 202;
		public const int Try                  = 203;
		public const int TryCast              = 204;
		public const int TypeOf               = 205;
		public const int UInteger             = 206;
		public const int ULong                = 207;
		public const int Unicode              = 208;
		public const int Until                = 209;
		public const int UShort               = 210;
		public const int Using                = 211;
		public const int Variant              = 212;
		public const int Wend                 = 213;
		public const int When                 = 214;
		public const int Where                = 215;
		public const int While                = 216;
		public const int Widening             = 217;
		public const int With                 = 218;
		public const int WithEvents           = 219;
		public const int WriteOnly            = 220;
		public const int Xor                  = 221;

		public const int MaxToken = 222;
		static BitArray NewSet(params int[] values)
		{
			BitArray bitArray = new BitArray(MaxToken);
			foreach (int val in values) {
			bitArray[val] = true;
			}
			return bitArray;
		}
		public static BitArray Null = NewSet(Nothing);
		public static BitArray BlockSucc = NewSet(Case, Catch, Else, ElseIf, End, Finally, Loop, Next);
		public static BitArray IdentifierTokens = NewSet(Text, Binary, Compare, Assembly, Ansi, Auto, Preserve, Unicode, Until, Off, Explicit, Infer, From, Join, Equals, Distinct, Where, Take, Skip, Order, By, Ascending, Descending, Group, Into, Aggregate);

		static string[] tokenList = new string[] {
			// ----- terminal classes -----
			"<EOF>",
			"<EOL>",
			"<Identifier>",
			"<LiteralString>",
			"<LiteralCharacter>",
			"<LiteralInteger>",
			"<LiteralDouble>",
			"<LiteralSingle>",
			"<LiteralDecimal>",
			"<LiteralDate>",
			// ----- special character -----
			"=",
			":",
			",",
			"&",
			"/",
			"\\",
			".",
			"!",
			"-",
			"+",
			"^",
			"?",
			"*",
			"{",
			"}",
			"(",
			")",
			">",
			"<",
			"<>",
			">=",
			"<=",
			"<<",
			">>",
			"+=",
			"^=",
			"-=",
			"*=",
			"/=",
			"\\=",
			"<<=",
			">>=",
			"&=",
			// ----- keywords -----
			"AddHandler",
			"AddressOf",
			"Aggregate",
			"Alias",
			"And",
			"AndAlso",
			"Ansi",
			"As",
			"Ascending",
			"Assembly",
			"Auto",
			"Binary",
			"Boolean",
			"ByRef",
			"By",
			"Byte",
			"ByVal",
			"Call",
			"Case",
			"Catch",
			"CBool",
			"CByte",
			"CChar",
			"CDate",
			"CDbl",
			"CDec",
			"Char",
			"CInt",
			"Class",
			"CLng",
			"CObj",
			"Compare",
			"Const",
			"Continue",
			"CSByte",
			"CShort",
			"CSng",
			"CStr",
			"CType",
			"CUInt",
			"CULng",
			"CUShort",
			"Custom",
			"Date",
			"Decimal",
			"Declare",
			"Default",
			"Delegate",
			"Descending",
			"Dim",
			"DirectCast",
			"Distinct",
			"Do",
			"Double",
			"Each",
			"Else",
			"ElseIf",
			"End",
			"EndIf",
			"Enum",
			"Equals",
			"Erase",
			"Error",
			"Event",
			"Exit",
			"Explicit",
			"False",
			"Finally",
			"For",
			"Friend",
			"From",
			"Function",
			"Get",
			"GetType",
			"Global",
			"GoSub",
			"GoTo",
			"Group",
			"Handles",
			"If",
			"Implements",
			"Imports",
			"In",
			"Infer",
			"Inherits",
			"Integer",
			"Interface",
			"Into",
			"Is",
			"IsNot",
			"Join",
			"Let",
			"Lib",
			"Like",
			"Long",
			"Loop",
			"Me",
			"Mod",
			"Module",
			"MustInherit",
			"MustOverride",
			"MyBase",
			"MyClass",
			"Namespace",
			"Narrowing",
			"New",
			"Next",
			"Not",
			"Nothing",
			"NotInheritable",
			"NotOverridable",
			"Object",
			"Of",
			"Off",
			"On",
			"Operator",
			"Option",
			"Optional",
			"Or",
			"Order",
			"OrElse",
			"Overloads",
			"Overridable",
			"Overrides",
			"ParamArray",
			"Partial",
			"Preserve",
			"Private",
			"Property",
			"Protected",
			"Public",
			"RaiseEvent",
			"ReadOnly",
			"ReDim",
			"Rem",
			"RemoveHandler",
			"Resume",
			"Return",
			"SByte",
			"Select",
			"Set",
			"Shadows",
			"Shared",
			"Short",
			"Single",
			"Skip",
			"Static",
			"Step",
			"Stop",
			"Strict",
			"String",
			"Structure",
			"Sub",
			"SyncLock",
			"Take",
			"Text",
			"Then",
			"Throw",
			"To",
			"True",
			"Try",
			"TryCast",
			"TypeOf",
			"UInteger",
			"ULong",
			"Unicode",
			"Until",
			"UShort",
			"Using",
			"Variant",
			"Wend",
			"When",
			"Where",
			"While",
			"Widening",
			"With",
			"WithEvents",
			"WriteOnly",
			"Xor",
		};
		public static string GetTokenString(int token)
		{
			if (token >= 0 && token < tokenList.Length) {
				return tokenList[token];
			}
			throw new System.NotSupportedException("Unknown token:" + token);
		}
	}
}
