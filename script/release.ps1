# If you want to create a release archive yourself edit 7zip variable first

$7zip = ${env:USERPROFILE} + '\scoop\apps\7zip\current\7z.exe'
$Resources = @( Get-ChildItem '..\src\GHOSTbackup\GHOSTbackup.Localization\*.resources' )

# Create release folder
if (!(Test-Path -Path '..\.release')) {
	New-Item -Path '..\.release' -ItemType Directory -ErrorAction Stop | Out-Null
}

if (Test-Path -LiteralPath '..\src\GHOSTbackup\bin\Release\GHOSTbackup.exe') {

	# Copy build output inside the release folder
	(Get-FileHash -LiteralPath '..\src\GHOSTbackup\bin\Release\GHOSTbackup.exe' -Algorithm SHA256).Hash | Out-File '..\.release\checksum.sha256' -Encoding utf8
	Copy-Item -LiteralPath '..\src\GHOSTbackup\bin\Release\GHOSTbackup.exe' -Destination '..\.release\GHOST Buster.exe' -Force
	Copy-Item -LiteralPath '..\src\GHOSTbackup\bin\Release\INIFileParser.dll' -Destination '..\.release\INIFileParser.dll' -Force
	Copy-Item -LiteralPath '..\src\GHOSTbackup\bin\Release\Microsoft.Toolkit.Uwp.Notifications.dll' -Destination '..\.release\Microsoft.Toolkit.Uwp.Notifications.dll' -Force
	Copy-Item -Path '..\src\Licenses' -Destination '..\.release\' -Recurse -Force
	Copy-Item -LiteralPath '..\CHANGELOG.txt' -Destination '..\.release\CHANGELOG.txt' -Force

	# Copy languages
	if (!(Test-Path -Path '..\.release\Languages')) {
		New-Item -Path '..\.release\Languages' -ItemType Directory -ErrorAction Stop | Out-Null
	}
	foreach (${Resource} in ${Resources}) {
		Copy-Item -LiteralPath ${Resource} -Destination '..\.release\Languages\' -Force
	}

	# Create release archive
	Remove-Item '..\.release\*.7z' -ErrorAction SilentlyContinue # Remove old release archive to avoid adding it in the new archive
	Start-Process -FilePath ${7zip} -WorkingDirectory '..\.release' -ArgumentList 'a -t7z GHOSTbackup_release.7z .\*' -NoNewWindow

} else {
	Write-Warning 'GHOST Buster binaries not found. Compile it first!'
}

Pause
