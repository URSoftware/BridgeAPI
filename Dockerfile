FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

COPY ["BridgeAPI.csproj", "./"]
RUN dotnet restore "BridgeAPI.csproj"

COPY . .
RUN dotnet build "BridgeAPI.csproj" -c Release -o /app/build

RUN dotnet publish "BridgeAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0

WORKDIR /app
EXPOSE 5000

ENV ASPNETCORE_ENVIRONMENT=Production
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
ENV DOTNET_NOLOGO=1

COPY --from=build /app/publish .

HEALTHCHECK --interval=30s --timeout=5s --start-period=10s --retries=3 \
  CMD dotnet --info || exit 1

ENTRYPOINT ["dotnet", "BridgeAPI.dll"]
