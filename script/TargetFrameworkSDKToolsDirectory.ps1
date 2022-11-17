if (!([Environment]::GetEnvironmentVariable('TargetFrameworkSDKToolsDirectory', 'User'))) {
	Write-Host 'Creating TargetFrameworkSDKToolsDirectory environment variable...'
	[Environment]::SetEnvironmentVariable('TargetFrameworkSDKToolsDirectory', 'C:\Program Files (x86)\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.8.1 Tools', 'User')
} else {
	Write-Host 'TargetFrameworkSDKToolsDirectory is already set.'
}

Pause
