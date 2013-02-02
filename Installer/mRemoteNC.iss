#define EnvAppFile "..\mRemoteV2\bin\Release\mRemoteNC.exe"
#define EnvSrcDir "..\mRemoteV2\bin\Release\"
#define EnvDepDir ".\Dependencies\"
#define EnvTxtDir "..\mRemoteV2\"
#define FileVerStr GetFileVersion(EnvAppFile)
#define StripBuild(VerStr) Copy(VerStr, 1, RPos(".", VerStr)-1)
#define AppVerStr StripBuild(FileVerStr)
#define MyAppName "mRemoteNC"
#define MyAppExeName "mRemoteNC.exe"

[Setup]
AppName=mRemoteNC
AppCopyright=Copyright © 2007-2009 Felix Deimel, 2010-2012 Riley McArdle, 2012 Exaktus
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
DisableWelcomePage=True
DisableReadyPage=True
DisableReadyMemo=True
DefaultGroupName={#MyAppName}
WizardImageFile=.\welcomefinish.bmp
WizardSmallImageFile=.\SmallIco.bmp
SetupIconFile=.\mRemoteNC.ico

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
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "french"; MessagesFile: "compiler:Languages\French.isl"
Name: "german"; MessagesFile: "compiler:Languages\German.isl"
Name: "polish"; MessagesFile: "compiler:Languages\Polish.isl"
Name: "russian"; MessagesFile: "compiler:Languages\Russian.isl"
Name: "spanish"; MessagesFile: "compiler:Languages\Spanish.isl"
Name: "ukrainian"; MessagesFile: "compiler:Languages\Ukrainian.isl"
