# Stage 1: Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Stage 2: Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy and restore dependencies
COPY ["Offloader.Runner/Offloader.Runner.csproj", "Offloader.Runner/"]
COPY ["Offloader.ServiceDefaults/Offloader.ServiceDefaults.csproj", "Offloader.ServiceDefaults/"]
RUN dotnet restore "Offloader.Runner/Offloader.Runner.csproj"

# Copy the entire project and build
COPY . .
WORKDIR "/src/Offloader.Runner"
RUN dotnet build "Offloader.Runner.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
RUN dotnet publish "Offloader.Runner.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Stage 3: Final image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Offloader.Runner.dll"]
