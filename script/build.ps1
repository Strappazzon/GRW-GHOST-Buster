$ResGen = "C:\Program Files (x86)\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.6.1 Tools\ResGen.exe" # resgen.exe path
$ResourcesDir = @(Get-ChildItem ${PSScriptRoot}"\..\src\GHOSTbackup\GHOSTbackup.Localization\*.resx")

Remove-Item -LiteralPath "..\src\GHOSTbackup\bin\Release" -Recurse -ErrorAction SilentlyContinue
Remove-Item -LiteralPath "..\src\GHOSTbackup\obj\Release" -Recurse -ErrorAction SilentlyContinue
Start-Process -FilePath ${env:windir}"\Microsoft.NET\Framework\v4.0.30319\msbuild.exe" -ArgumentList ${PSScriptRoot}"\..\src\GHOSTbackup.sln /t:Build /p:Configuration=Release" -NoNewWindow
foreach (${Resource} in ${ResourcesDir}) { Start-Process -FilePath ${ResGen} -ArgumentList ${Resource} -NoNewWindow }

Pause
