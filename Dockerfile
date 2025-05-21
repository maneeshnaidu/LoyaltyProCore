# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the solution and project files
COPY api.sln ./
COPY src/api/api.csproj ./src/api/

# Restore dependencies
RUN dotnet restore ./src/api/api.csproj

# Copy the rest of the application code
COPY . ./

# Build and publish the application
WORKDIR /src/src/api
RUN dotnet publish api.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy the published application from the build stage
COPY --from=build /app/publish .

# Expose the port your application listens on
EXPOSE 8080
# EXPOSE 80

# Set the entry point for the container
ENTRYPOINT ["dotnet", "api.dll"]