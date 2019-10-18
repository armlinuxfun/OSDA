; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "OSDA"
#define MyAppVersion "3.4.0"
#define MyVersionInfoVersion "3.4.0.5"
#define MyAppExeName "OSDA.exe"
#define MyAppCopyright "Copyright (C) 2019 Cao Liwen"
#define MyAppPublisher "Cao Liwen"

; x64(64bit) and x86(32 bit)
#define x_64

; Visual Studio 2017 and Visual Studio 2019
;#define VS2017

[Setup]
AppId={{CD6994CA-F69D-4B27-A1EC-1E34684AE73A}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppVerName={#MyAppName} {#MyAppVersion}
VersionInfoVersion={#MyVersionInfoVersion}
AppPublisher={#MyAppPublisher}
AppCopyright={#MyAppCopyright}
DefaultDirName={commonpf}\{#MyAppName}
DefaultGroupName=OSDA
DisableProgramGroupPage=auto
OutputDir=.\
SetupIconFile=..\icon\favicon.ico
#ifdef x_64
  ArchitecturesInstallIn64BitMode=x64
  OutputBaseFilename=OSDA_Windows_Framework_V{#MyAppVersion}_x64
#else
  OutputBaseFilename=OSDA_Windows_Framework_V{#MyAppVersion}_x86
#endif
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "chinesesimplified"; MessagesFile: "compiler:Languages\ChineseSimplified.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "..\LICENSE"; DestDir: "{app}"; Flags: ignoreversion

; Visual Studio 2017
#ifdef VS2017
#ifdef x_64
  Source: "..\msvc\VS2017.Framework\bin\x64\Release\OSDA.exe"; DestDir: "{app}"; Flags: ignoreversion
  Source: "..\msvc\VS2017.Framework\bin\x64\Release\OSDA.exe.config"; DestDir: "{app}"; Flags: ignoreversion
#else
  Source: "..\msvc\VS2017.Framework\bin\x86\Release\OSDA.exe"; DestDir: "{app}"; Flags: ignoreversion
  Source: "..\msvc\VS2017.Framework\bin\x86\Release\OSDA.exe.config"; DestDir: "{app}"; Flags: ignoreversion
#endif

; Visual Studio 2019
#else
#ifdef x_64
  Source: "..\msvc\VS2019.Framework\bin\x64\Release\OSDA.exe"; DestDir: "{app}"; Flags: ignoreversion
  Source: "..\msvc\VS2019.Framework\bin\x64\Release\OSDA.exe.config"; DestDir: "{app}"; Flags: ignoreversion
#else
  Source: "..\msvc\VS2019.Framework\bin\x86\Release\OSDA.exe"; DestDir: "{app}"; Flags: ignoreversion
  Source: "..\msvc\VS2019.Framework\bin\x86\Release\OSDA.exe.config"; DestDir: "{app}"; Flags: ignoreversion
#endif
#endif

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent