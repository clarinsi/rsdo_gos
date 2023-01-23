FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# RESTORE PACKAGES (with project files only)
WORKDIR /src
COPY src/Gos.Console/Gos.Console.csproj Gos.Console/Gos.Console.csproj
COPY src/Gos.Core/Gos.Core.csproj Gos.Core/Gos.Core.csproj 
COPY src/Gos.Infrastructure/Gos.Infrastructure.csproj Gos.Infrastructure/Gos.Infrastructure.csproj 
COPY src/Gos.ServiceModel/Gos.ServiceModel.csproj Gos.ServiceModel/Gos.ServiceModel.csproj
COPY src/Gos.Services/Gos.Services.csproj Gos.Services/Gos.Services.csproj

WORKDIR /src/Gos.Console
RUN dotnet restore Gos.Console.csproj

# COPY ALL FILES
WORKDIR /src
COPY src/Gos.Console/. Gos.Console/
COPY src/Gos.Core/. Gos.Core/
COPY src/Gos.Infrastructure/. Gos.Infrastructure/
COPY src/Gos.ServiceModel/. Gos.ServiceModel/
COPY src/Gos.Services/. Gos.Services/

# BUILD APP
FROM build AS publish
WORKDIR /src/Gos.Console
RUN dotnet publish "Gos.Console.csproj" -c Release -o /app/publish

# COPY TO FINAL
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD tail -f /dev/null