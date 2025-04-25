# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy project and restore dependencies
COPY . ./
RUN dotnet restore

# Build & publish the application
RUN dotnet publish -c Release -o /app/out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy the published application from the build stage
COPY --from=build /app/out ./

# Set the entrypoint to run the app
ENTRYPOINT ["dotnet", "BookingApp.dll"]
