[CmdletBinding()]
Param(
  [Alias('p')]
  [Parameter(Mandatory = $false, Position = 0)]
  [String]$Path = "${env:ProgramFiles(x86)}\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.8.1 Tools"
)

Set-StrictMode -Version 1
$ErrorActionPreference = 'Stop'

[String]$SDKDirectory = $Path;

If (Test-Path -Path "${SDKDirectory}") {
  If (!([Environment]::GetEnvironmentVariable('TargetFrameworkSDKToolsDirectory', [EnvironmentVariableTarget]::User))) {
    [Environment]::SetEnvironmentVariable(
      'TargetFrameworkSDKToolsDirectory',
      "${SDKDirectory}",
      [EnvironmentVariableTarget]::User
    )
    Write-Output '"TargetFrameworkSDKToolsDirectory" environment variable set.'
  } Else {
    Write-Output 'TargetFrameworkSDKToolsDirectory is already set.'
  }
} Else {
  Write-Output 'Windows 11 SDK is required.'
  Write-Output 'Download it from: https://developer.microsoft.com/en-us/windows/downloads/sdk-archive/#windows-11'
}
