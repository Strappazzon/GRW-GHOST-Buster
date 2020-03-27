Remove-Item -LiteralPath "..\src\GHOSTbackup\bin\Release" -Recurse -ErrorAction SilentlyContinue
Remove-Item -LiteralPath "..\src\GHOSTbackup\obj\Release" -Recurse -ErrorAction SilentlyContinue
Start-Process -FilePath ${env:windir}"\Microsoft.NET\Framework\v4.0.30319\msbuild.exe" -ArgumentList ${PSScriptRoot}"\..\src\GHOSTbackup.sln /t:Build /p:Configuration=Release" -NoNewWindow
Pause
