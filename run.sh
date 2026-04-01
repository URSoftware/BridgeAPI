#!/bin/bash

# BridgeAPI Startup Script for macOS/Linux
# This script starts the BridgeAPI server on a specified port

echo ""
echo "========================================"
echo "   BridgeAPI Server Startup"
echo "========================================"
echo ""

# Check if .NET is installed
if ! command -v dotnet &> /dev/null; then
    echo "Error: .NET SDK not found. Please install .NET 9.0"
    echo "Visit: https://dotnet.microsoft.com/en-us/download/dotnet/9.0"
    exit 1
fi

echo ".NET SDK found:"
dotnet --version
echo ""

# Set default port
PORT=5000

# Check if user provided a port argument
if [ ! -z "$1" ]; then
    PORT=$1
    echo "Custom port specified: $PORT"
else
    echo "Default port: $PORT"
fi
echo ""

# Set environment variable for ASPNETCORE URLs
export ASPNETCORE_URLS=http://localhost:$PORT

# Build the project
echo "Compiling BridgeAPI..."
dotnet build --configuration Release > /dev/null 2>&1
if [ $? -ne 0 ]; then
    echo "Error: Build failed. Please check your project files."
    exit 1
fi
echo "Build successful!"
echo ""

# Run the application
echo "Starting BridgeAPI Server on http://localhost:$PORT"
echo ""
echo "Press Ctrl+C to stop the server"
echo "API Documentation: http://localhost:$PORT/swagger"
echo "Health Check: http://localhost:$PORT/api/health"
echo ""
echo "========================================"
echo ""

dotnet run --configuration Release
