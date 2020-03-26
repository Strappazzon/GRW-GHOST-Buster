Start-Process -FilePath ${env:windir}"\Microsoft.NET\Framework\v4.0.30319\msbuild.exe" -ArgumentList "GHOSTbackup.sln /t:Build /p:Configuration=Release" -WorkingDirectory "..\src\" -NoNewWindow
