﻿
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
//
// This file contains generated default command definitions.
// Additional command definitions should be added to CustomCmd.ctc.
//

///////////////////////////////////////////////////////////////////////////////
// CTC command IDs - MUST be kept in sync with Constants.cs

#define menuidContext		0x10000
#define grpidContextMain	0x20000
#define menuidExplorer		0x10001
#define cmdidViewExplorer	0x0001

///////////////////////////////////////////////////////////////////////////////
// CTC macros

#define OI_NOID		guidOfficeIcon:msotcidNoIcon
#define DIS_DEF		DEFAULTDISABLED | DEFAULTINVISIBLE | DYNAMICVISIBILITY
#define VIS_DEF		COMMANDWELLONLY	

///////////////////////////////////////////////////////////////////////////////
// Menu definitions

#define GENERATED_MENUS \
	guidCmdSet:menuidContext, guidCmdSet:menuidContext,	0x0000,	CONTEXT|ALWAYSCREATE, "ActiveWriter Designer Context Menu", "ActiveWriter Context"; \
	guidCmdSet:menuidExplorer, guidCmdSet:menuidExplorer, 0x0000, CONTEXT|ALWAYSCREATE, "ActiveWriter Explorer Context Menu", "ActiveWriter"; \

///////////////////////////////////////////////////////////////////////////////
// Group definitions

#define GENERATED_GROUPS \
	guidCmdSet:grpidContextMain, guidCmdSet:grpidContextMain,	0x0010; \


///////////////////////////////////////////////////////////////////////////////
// Command definitions

#define GENERATED_BUTTONS \
	guidCmdSet:cmdidViewExplorer, guidSHLMainMenu:IDG_VS_WNDO_OTRWNDWS1, 0x0100,	OI_NOID, BUTTON, DIS_DEF, "ActiveWriter";


///////////////////////////////////////////////////////////////////////////////
// Command placement definitions

#define GENERATED_CMDPLACEMENT \
	guidVSStd97:cmdidDelete, guidCmdSet:grpidContextMain, 0x0010; \
	guidSHLMainMenu:IDG_VS_CTXT_SOLUTION_PROPERTIES, guidCmdSet:menuidContext, 0x0500; \
	guidCmdSet:grpidContextMain, guidCmdSet:menuidContext, 0x0010; \
	guidCommonModelingMenu:grpidCompartmentShapeMenuGroup, guidCmdSet:menuidContext, 0x0008; \
	guidCommonModelingMenu:grpidSwimlaneShapeMenuGroup, guidCmdSet:menuidContext, 0x0008; \
	guidCommonModelingMenu:grpidValidateCommands, guidCmdSet:menuidContext, 0x0020; \
	guidCommonModelingMenu:grpidLayoutMenuGroup, guidCmdSet:menuidContext, 0x0030; \
	guidCommonModelingMenu:grpidExplorerMenuGroup, guidCmdSet:menuidExplorer, 0x0010; \
	guidSHLMainMenu:IDG_VS_CTXT_SOLUTION_PROPERTIES, guidCmdSet:menuidExplorer, 0x0020; \
	guidCommonModelingMenu:grpidValidateCommands, guidCmdSet:menuidExplorer, 0x0030; \
    

///////////////////////////////////////////////////////////////////////////////
// Command visibility definitions

#define GENERATED_VISIBILITY \
	guidCmdSet:cmdidViewExplorer, guidEditor; \

///////////////////////////////////////////////////////////////////////////////
// CTC guids - MUST be kept in sync with GeneratedCmd.cs

#define guidPkg			{ 0x9f4f10d0, 0x81ad, 0x4308, { 0xa3, 0x98, 0xba, 0x77, 0x41, 0xf3, 0xf7, 0xe2 } }
#define guidEditor		{ 0xafa967d1, 0x99df, 0x4330, { 0xa1, 0xd6, 0xa9, 0x63, 0x43, 0xc2, 0x47, 0x86 } }



#define guidCmdSet		{ 0x9928a048, 0x76e7, 0x4f47, { 0x80, 0x55, 0x2b, 0x7d, 0xa1, 0x1b, 0x6f, 0x58 } }