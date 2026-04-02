FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

COPY ["BridgeAPI.csproj", "./"]
COPY ["appsettings.json", "./"]
COPY ["appsettings.Development.json", "./"]

RUN dotnet restore "BridgeAPI.csproj"

COPY . .

RUN dotnet build "BridgeAPI.csproj" -c Release -o /app/build

RUN dotnet publish "BridgeAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0

WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Production
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
ENV DOTNET_NOLOGO=1

EXPOSE 5000

CMD ["dotnet", "BridgeAPI.dll"]
