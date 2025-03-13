[CmdletBinding()]
Param(
  [Alias('c')]
  [Parameter(Mandatory = $false, Position = 0)]
  [ValidateSet('Debug', 'Release', IgnoreCase = $true)]
  [String]$Configuration = 'Debug',

  [Alias('v')]
  [Parameter(Mandatory = $false, Position = 1)]
  [ValidateSet('quiet', 'minimal', 'normal', 'detailed', 'diagnostic', IgnoreCase = $true)]
  [String]$Verbosity = 'normal'
)

Set-StrictMode -Version 1
$ErrorActionPreference = 'Stop'

[String]$MSBuild = "${env:ProgramFiles}\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe"
[String[]]$ResxLoc = @(Get-ChildItem "${PSScriptRoot}\..\src\GHOSTbackup\GHOSTbackup.Localization\*.resx")

# Cleanup
Remove-Item -LiteralPath "${PSScriptRoot}\..\src\GHOSTbackup\bin\${Configuration}" -Recurse -ErrorAction SilentlyContinue
Remove-Item -LiteralPath "${PSScriptRoot}\..\src\GHOSTbackup\obj\${Configuration}" -Recurse -ErrorAction SilentlyContinue

# Restore NuGet packages
Start-Process -FilePath "${MSBuild}" -ArgumentList "-t:restore -v:${Verbosity} -nologo" -WorkingDirectory "${PSScriptRoot}\..\src" -NoNewWindow -Wait

# Build Solution
Start-Process -FilePath "${MSBuild}" -ArgumentList "${PSScriptRoot}\..\src\GHOSTbackup.sln -m -t:Build -p:Configuration=${Configuration} -v:${Verbosity} -nologo" -NoNewWindow -Wait

# Build localization
ForEach ($Resx in $ResxLoc) {
  Start-Process -FilePath "${env:TargetFrameworkSDKToolsDirectory}\ResGen.exe" -ArgumentList "${Resx}" -NoNewWindow -Wait
}

# Copy localization
New-Item -Path "${PSScriptRoot}\..\src\GHOSTbackup\bin\${Configuration}\Languages" -ItemType Directory | Out-Null
ForEach ($Res in (($ResxLoc).Replace('.resx', '.resources'))) {
  Copy-Item -LiteralPath "${Res}" -Destination "${PSScriptRoot}\..\src\GHOSTbackup\bin\${Configuration}\Languages\"
}
