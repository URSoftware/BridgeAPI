@echo off
REM BridgeAPI Quick Start - Deploy package ready
REM Run: run-api.ps1

setlocal
if not defined PORT set PORT=5000
set ASPNETCORE_ENVIRONMENT=Production
set DOTNET_CLI_TELEMETRY_OPTOUT=1

cd /d "%~dp0"
dotnet BridgeAPI.dll
