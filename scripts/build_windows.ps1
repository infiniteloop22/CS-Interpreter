# Ensure the script is run with administrative privileges
if (-Not ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole] "Administrator")) {
    Write-Host "Please run this script as an Administrator."
    exit
}

# Install .NET SDK
Write-Host "Installing .NET SDK..."
Invoke-WebRequest -Uri "https://dotnet.microsoft.com/download/dotnet/scripts/v1/dotnet-install.ps1" -OutFile "$env:TEMP\dotnet-install.ps1"
& "$env:TEMP\dotnet-install.ps1" -Channel 8.0 -InstallDir "$env:ProgramFiles\dotnet"
[System.Environment]::SetEnvironmentVariable("Path", "$env:Path;$env:ProgramFiles\dotnet", [System.EnvironmentVariableTarget]::Machine)
Remove-Item "$env:TEMP\dotnet-install.ps1"

# Verify installation
Write-Host "Verifying .NET installation..."
dotnet --version

Write-Host "Environment setup complete!"
