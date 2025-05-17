# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files first
COPY ["api.sln", "."]
COPY ["src/api/api.csproj", "src/api/"]
RUN dotnet restore "api.sln"

# Copy everything else
COPY . .

# Build and publish
WORKDIR "/src/src/api"
RUN dotnet publish "api.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Connection string will be injected at runtime
ENV ConnectionStrings__DefaultConnection=""

# Expose the port the app runs on
EXPOSE 8080
EXPOSE 80
EXPOSE 443

# Set the entry point for the container
ENTRYPOINT ["dotnet", "api.dll"]