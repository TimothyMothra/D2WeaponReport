# Stage 1: Build application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
RUN apt-get update && apt-get install -y nodejs npm
WORKDIR /src
COPY . .
WORKDIR /src/src/D2WeaponReportWeb
RUN npm install
WORKDIR /src
RUN dotnet restore src/DestinySandbox.sln
RUN dotnet publish src/D2WeaponReportWeb/D2WeaponReportWeb.csproj -c Release -o /app/publish

# Stage 2: Download Bungie manifest
FROM build AS init
RUN dotnet run --project src/InitEnvironment/InitEnvironment.csproj

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
COPY --from=init /src/environment ./environment
RUN touch root.marker
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production
EXPOSE 80
ENTRYPOINT ["dotnet", "D2WeaponReportWeb.dll"]
