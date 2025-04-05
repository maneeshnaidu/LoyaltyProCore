# Use official .NET 8 SDK for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the solution and project file first
COPY api.sln ./
COPY api.csproj ./

# Restore dependencies
RUN dotnet restore api.csproj

# Copy everything else
COPY . ./
RUN dotnet build api.csproj --no-restore
RUN dotnet publish api.csproj -c Release -o /publish

# Use a smaller runtime image for production
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /publish ./

# Expose the API port
EXPOSE 8080
ENTRYPOINT ["dotnet", "api.dll"]

