@echo off
REM BridgeAPI Startup Script for Windows
REM This script starts the BridgeAPI server on a specified port

setlocal enabledelayedexpansion

echo.
echo ========================================
echo    BridgeAPI Server Startup
echo ========================================
echo.

REM Check if .NET is installed
dotnet --version >nul 2>&1
if errorlevel 1 (
    echo Error: .NET SDK not found. Please install .NET 9.0 from:
    echo https://dotnet.microsoft.com/en-us/download/dotnet/9.0
    pause
    exit /b 1
)

echo .NET SDK found: 
dotnet --version
echo.

REM Set default port
set PORT=5000

REM Check if user provided a port argument
if not "%1"=="" (
    set PORT=%1
    echo Custom port specified: !PORT!
) else (
    echo Default port: !PORT!
)
echo.

REM Set environment variable for ASPNETCORE URLs
set ASPNETCORE_URLS=http://localhost:!PORT!

REM Build the project
echo Compiling BridgeAPI...
dotnet build --configuration Release >nul 2>&1
if errorlevel 1 (
    echo Error: Build failed. Please check your project files.
    pause
    exit /b 1
)
echo Build successful!
echo.

REM Run the application
echo Starting BridgeAPI Server on http://localhost:!PORT!
echo.
echo Press Ctrl+C to stop the server
echo API Documentation: http://localhost:!PORT!/swagger
echo Health Check: http://localhost:!PORT!/api/health
echo.
echo ========================================
echo.

dotnet run --configuration Release

pause
