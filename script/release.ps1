$7Zip = ${env:ProgramFiles}+"\7-Zip\7z.exe" # Path to 7z.exe

if (-not (Test-Path -Path "..\.release")) {
	# Create the release folder if it doesn't exist
	New-Item -Path "..\.release" -ItemType Directory -ErrorAction Stop | Out-Null
} else {
	if (Test-Path -LiteralPath "..\src\GHOSTbackup\bin\Release\GHOSTbackup.exe") {
		# Copy compiled binaries and other files inside the release folder
		Write-Host "Calculating SHA256 checksum."
		Get-FileHash -LiteralPath "..\src\GHOSTbackup\bin\Release\GHOSTbackup.exe" -Algorithm SHA256 | Out-File -FilePath "..\.release\checksum.sha256" -Encoding utf8 -Width 80
		Write-Host "Copying compiled binaries and files."
		Copy-Item -LiteralPath "..\src\GHOSTbackup\bin\Release\GHOSTbackup.exe" -Destination "..\.release\GHOST Buster.exe" -Force
		Copy-Item -LiteralPath "..\src\GHOSTbackup\bin\Release\INIFileParser.dll" -Destination "..\.release\INIFileParser.dll" -Force
		Copy-Item -Path "..\src\Licenses" -Destination "..\.release\" -Recurse -Force
		Copy-Item -LiteralPath "..\CHANGELOG.txt" -Destination "..\.release\CHANGELOG.txt" -Force

		# Create release archive
		Write-Host "Creating release archive."
		Remove-Item "..\.release\*.7z" -ErrorAction SilentlyContinue # Remove old release archive to avoid adding it in the new archive
		Start-Process -FilePath ${7zip} -WorkingDirectory "..\.release" -ArgumentList "a -t7z GHOSTbackup_release.7z .\*" -NoNewWindow
	} else {
		Write-Warning "GHOST Buster binary not found. Compile it first!"
	}
}

Pause
