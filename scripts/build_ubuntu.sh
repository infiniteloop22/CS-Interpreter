#!/bin/bash

# Ensure the script is run with root privileges
if [ "$EUID" -ne 0 ]; then
  echo "Please run this script as root."
  exit
fi

# Install .NET SDK
echo "Installing .NET SDK..."
wget https://packages.microsoft.com/config/ubuntu/24.04/packages-microsoft-prod.deb -O /tmp/packages-microsoft-prod.deb
dpkg -i /tmp/packages-microsoft-prod.deb
rm /tmp/packages-microsoft-prod.deb
apt-get update
apt-get install -y dotnet-sdk-8.0

# Verify installation
echo "Verifying .NET installation..."
dotnet --version

echo "Environment setup complete!"
