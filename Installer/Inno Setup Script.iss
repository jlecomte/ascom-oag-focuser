#define MyAppPublisher "Dark Sky Geek"
#define MyAppName "OAG Focuser ASCOM Driver"
#define MyAppVersion "1.3.0"
#define MyAppURL "https://github.com/jlecomte/ascom-oag-focuser"

[Setup]
AppId={{2760CB5C-EDA1-41F8-BCD1-CE6E1A8B673B}
AppName={#MyAppPublisher}'s {#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}/issues
AppUpdatesURL={#MyAppURL}/releases
DefaultDirName={autopf}\{#MyAppPublisher}\{#MyAppName}
DisableProgramGroupPage=yes
Compression=lzma
SolidCompression=yes
UninstallFilesDir="{app}\Uninstall"
SourceDir="C:\Users\Julien\source\repos\ascom-oag-focuser"
OutputDir="Installer"
OutputBaseFilename={#MyAppPublisher} {#MyAppName} Setup-{#MyAppVersion}
WizardImageFile="Installer\WizardImageFile.bmp"

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Files]
Source: "ASCOM_Driver\bin\Release\ASCOM.DarkSkyGeek.OAGFocusDriver.dll"; DestDir: "{app}"
Source: "Focuser_App\bin\x64\Release\ASCOM.DarkSkyGeek.FocuserApp.exe"; DestDir: "{app}"

[Run]
Filename: "{dotnet4032}\regasm.exe"; Parameters: "/codebase ""{app}\ASCOM.DarkSkyGeek.OAGFocusDriver.dll"""; Flags: runhidden 32bit
Filename: "{dotnet4064}\regasm.exe"; Parameters: "/codebase ""{app}\ASCOM.DarkSkyGeek.OAGFocusDriver.dll"""; Flags: runhidden 64bit; Check: IsWin64

[UninstallRun]
Filename: "{dotnet4032}\regasm.exe"; Parameters: "-u ""{app}\ASCOM.DarkSkyGeek.OAGFocusDriver.dll"""; Flags: runhidden 32bit
; This helps to give a clean uninstall
Filename: "{dotnet4064}\regasm.exe"; Parameters: "/codebase ""{app}\ASCOM.DarkSkyGeek.OAGFocusDriver.dll"""; Flags: runhidden 64bit; Check: IsWin64
Filename: "{dotnet4064}\regasm.exe"; Parameters: "-u ""{app}\ASCOM.DarkSkyGeek.OAGFocusDriver.dll"""; Flags: runhidden 64bit; Check: IsWin64

[Code]
const REQUIRED_PLATFORM_VERSION = 6.2; // Set this to the minimum required ASCOM Platform version for this application

//
// Function to return the ASCOM Platform's version number as a double.
//
function PlatformVersion(): Double;
var
   PlatVerString : String;
begin
   Result := 0.0; // Initialize the return value in case we can't read the registry
   try
      if RegQueryStringValue(HKEY_LOCAL_MACHINE_32, 'Software\ASCOM','PlatformVersion', PlatVerString) then
      begin // Successfully read the value from the registry
         Result := StrToFloat(PlatVerString); // Create a double from the X.Y Platform version string
      end;
   except
      ShowExceptionMessage;
      Result:= -1.0; // Indicate in the return value that an exception was generated
   end;
end;

//
// Before the installer UI appears, verify that the required ASCOM Platform version is installed.
//
function InitializeSetup(): Boolean;
var
   PlatformVersionNumber : double;
 begin
   Result := FALSE; // Assume failure
   PlatformVersionNumber := PlatformVersion(); // Get the installed Platform version as a double
   If PlatformVersionNumber >= REQUIRED_PLATFORM_VERSION then	// Check whether we have the minimum required Platform or newer
      Result := TRUE
   else
      if PlatformVersionNumber = 0.0 then
         MsgBox('No ASCOM Platform is installed. Please install Platform ' + Format('%3.1f', [REQUIRED_PLATFORM_VERSION]) + ' or later from https://www.ascom-standards.org', mbCriticalError, MB_OK)
      else
         MsgBox('ASCOM Platform ' + Format('%3.1f', [REQUIRED_PLATFORM_VERSION]) + ' or later is required, but Platform '+ Format('%3.1f', [PlatformVersionNumber]) + ' is installed. Please install the latest Platform before continuing; you will find it at https://www.ascom-standards.org', mbCriticalError, MB_OK);
end;

// Code to enable the installer to uninstall previous versions of itself when a new version is installed
procedure CurStepChanged(CurStep: TSetupStep);
var
  ResultCode: Integer;
  UninstallExe: String;
  UninstallRegistry: String;
begin
  if (CurStep = ssInstall) then // Install step has started
	begin
      // Create the correct registry location name, which is based on the AppId
      UninstallRegistry := ExpandConstant('Software\Microsoft\Windows\CurrentVersion\Uninstall\{#SetupSetting("AppId")}' + '_is1');
      // Check whether an extry exists
      if RegQueryStringValue(HKLM, UninstallRegistry, 'UninstallString', UninstallExe) then
        begin // Entry exists and previous version is installed so run its uninstaller quietly after informing the user
          MsgBox('Setup will now remove the previous version.', mbInformation, MB_OK);
          Exec(RemoveQuotes(UninstallExe), ' /SILENT', '', SW_SHOWNORMAL, ewWaitUntilTerminated, ResultCode);
          sleep(1000); // Give enough time for the install screen to be repainted before continuing
        end
  end;
end;
