FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# RESTORE PACKAGES (with project files only)
WORKDIR /src
COPY src/Gos.Web/Gos.Web.csproj Gos.Web/Gos.Web.csproj
COPY src/Gos.Core/Gos.Core.csproj Gos.Core/Gos.Core.csproj 
COPY src/Gos.Infrastructure/Gos.Infrastructure.csproj Gos.Infrastructure/Gos.Infrastructure.csproj 
COPY src/Gos.ServiceModel/Gos.ServiceModel.csproj Gos.ServiceModel/Gos.ServiceModel.csproj
COPY src/Gos.Services/Gos.Services.csproj Gos.Services/Gos.Services.csproj

WORKDIR /src/Gos.Web
RUN dotnet restore Gos.Web.csproj

# COPY ALL FILES
WORKDIR /src
COPY src/Gos.Web/. Gos.Web/
COPY src/Gos.Core/. Gos.Core/
COPY src/Gos.Infrastructure/. Gos.Infrastructure/
COPY src/Gos.ServiceModel/. Gos.ServiceModel/
COPY src/Gos.Services/. Gos.Services/

# BUILD WEBAPP
FROM build AS publish
WORKDIR /src/Gos.Web
RUN dotnet publish "Gos.Web.csproj" -c Release -o /app/publish

# COPY TO FINAL
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gos.Web.dll"]