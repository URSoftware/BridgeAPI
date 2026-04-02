#!/bin/bash
# BridgeAPI Quick Start - Deploy package ready
# Run: ./run-api.sh  (or ./run-api.ps1 on Windows)

PORT=${PORT:-5000}
export ASPNETCORE_ENVIRONMENT=Production
export DOTNET_CLI_TELEMETRY_OPTOUT=1

cd "$(dirname "$0")"
dotnet BridgeAPI.dll
