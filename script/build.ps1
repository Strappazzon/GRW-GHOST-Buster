$ResGen = 'C:\Program Files (x86)\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.8.1 Tools\ResGen.exe'
$ResourcesDir = @(Get-ChildItem ${PSScriptRoot}"\..\src\GHOSTbackup\GHOSTbackup.Localization\*.resx")

# Cleanup
Remove-Item -LiteralPath '..\src\GHOSTbackup\bin\Release' -Recurse -ErrorAction SilentlyContinue
Remove-Item -LiteralPath '..\src\GHOSTbackup\obj\Release' -Recurse -ErrorAction SilentlyContinue

# Build Solution
Start-Process -FilePath ${env:ProgramFiles}"\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" -ArgumentList ${PSScriptRoot}"\..\src\GHOSTbackup.sln /t:Build /p:Configuration=Release" -NoNewWindow

# Build localization
foreach (${Resource} in ${ResourcesDir}) {
	Start-Process -FilePath ${ResGen} -ArgumentList ${Resource} -NoNewWindow
}
