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

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "BridgeAPI.dll"]
