#define EnvAppFile "..\mRemoteV2\bin\Release\mRemoteNC.exe"
#define EnvSrcDir "..\mRemoteV2\bin\Release\"
#define EnvDepDir ".\Dependencies\"
#define EnvTxtDir "..\mRemoteV2\"
#define FileVerStr GetFileVersion(EnvAppFile)
#define StripBuild(VerStr) Copy(VerStr, 1, RPos(".", VerStr)-1)
#define AppVerStr StripBuild(FileVerStr)

#define MyAppName "mRemoteNC"
#define MyAppExeName "mRemoteNC.exe"
#define MyAppSetupName 'mRemoteNC'

#define MyAppVersion StripBuild(FileVerStr)
#define SetupScriptVersion '1'

#include "scripts\products.iss"
#include "scripts\products\winversion.iss"
#include "scripts\products\fileversion.iss"
#include "scripts\products\msi45.iss"
#include "scripts\products\dotnetfx40full.iss"

[Setup]
AppName=mRemoteNC
AppCopyright=Copyright © 2007-2009 Felix Deimel, 2010-2012 Riley McArdle, 2012-2013 Exaktus
AppId={{268C15E9-2FFC-43A9-8E67-1764CDCFA9C0}
DefaultDirName={pf}\mRemoteNC
SolidCompression=True
Compression=lzma2/ultra
InternalCompressLevel=ultra
AppVersion={#AppVerStr}
VersionInfoVersion={#FileVerStr}
VersionInfoTextVersion={#AppVerStr}
OutputDir=..\Release
OutputBaseFilename={#MyAppName}-Installer-{#AppVerStr}
LicenseFile={#EnvTxtDir}\COPYING.TXT
DisableWelcomePage=False
DisableReadyPage=False
DisableReadyMemo=False
DefaultGroupName={#MyAppName}
WizardImageFile=.\welcomefinish.bmp
WizardSmallImageFile=.\SmallIco.bmp
SetupIconFile=.\mRemoteNC.ico
PrivilegesRequired=admin
DirExistsWarning=no
CreateAppDir=true
AllowNoIcons=yes
UsePreviousGroup=yes
UsePreviousAppDir=yes
Uninstallable=true


[Files]
Source: {#EnvSrcDir}\*; DestDir: {app}; Flags: ignoreversion recursesubdirs createallsubdirs
Source: {#EnvDepDir}\*; DestDir: {app}; Flags: ignoreversion recursesubdirs createallsubdirs


[Icons]
Name: {commondesktop}\{#MyAppName}; Filename: {app}\{#MyAppExeName}; WorkingDir: {app}; Tasks: desktopicon
Name: {group}\{#MyAppName}; Filename: {app}\{#MyAppExeName}; WorkingDir: {app};

[Tasks]
Name: desktopicon; Description: {cm:CreateDesktopIcon}; GroupDescription: {cm:AdditionalIcons}

[Run]
Filename: {app}\{#MyAppExeName};  Flags: nowait postinstall skipifsilent runasoriginaluser; WorkingDir: {app}

[Languages]
Name: "en"; MessagesFile: "compiler:Default.isl"
Name: "french"; MessagesFile: "compiler:Languages\French.isl"
Name: "de"; MessagesFile: "compiler:Languages\German.isl"
Name: "polish"; MessagesFile: "compiler:Languages\Polish.isl"
Name: "russian"; MessagesFile: "compiler:Languages\Russian.isl"
Name: "spanish"; MessagesFile: "compiler:Languages\Spanish.isl"
Name: "ukrainian"; MessagesFile: "compiler:Languages\Ukrainian.isl"

[CustomMessages]
win2000sp3_title=Windows 2000 Service Pack 3
winxpsp2_title=Windows XP Service Pack 2
winxpsp3_title=Windows XP Service Pack 3



[Code]
function InitializeSetup(): Boolean;
begin
	initwinversion();
  msi45('4.5');
	dotnetfx40full(false);
	Result := true;
end;
