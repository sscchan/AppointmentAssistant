#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# The service requires browsers to be installed for playwright .NET to work.
# This playwright docker image contains the required browsers already pre-installed.
FROM mcr.microsoft.com/playwright/dotnet:v1.46.0-jammy AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AppointmentAssistant.WebApplication/AppointmentAssistant.WebApplication.csproj", "AppointmentAssistant.WebApplication/"]
RUN dotnet restore "./AppointmentAssistant.WebApplication/AppointmentAssistant.WebApplication.csproj"
COPY . .
WORKDIR "/src/AppointmentAssistant.WebApplication"
RUN dotnet build "./AppointmentAssistant.WebApplication.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AppointmentAssistant.WebApplication.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Provide application user with permissions to the /app folder
# See: https://github.com/microsoft/playwright-dotnet/issues/2770
USER root
RUN chmod +x /app/.playwright/node/*/node
USER app

ENTRYPOINT ["dotnet", "AppointmentAssistant.WebApplication.dll"]