# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["OrderApi/OrderApi.csproj", "OrderApi/"]
COPY ["OrderApi.Data/OrderApi.Data.csproj", "OrderApi.Data/"]
COPY ["OrderApi.Domain/OrderApi.Domain.csproj", "OrderApi.Domain/"]
COPY ["OrderApi.Messaging.Receive/OrderApi.Messaging.Receive.csproj", "OrderApi.Messaging.Receive/"]
COPY ["OrderApi.Service/OrderApi.Service.csproj", "OrderApi.Service/"]
COPY ["CustomerApi.Data/CustomerApi.Data.csproj", "CustomerApi.Data/"]
COPY ["CustomerApi.Domain/CustomerApi.Domain.csproj", "CustomerApi.Domain/"]
RUN dotnet restore "./OrderApi/OrderApi.csproj"
COPY . .
WORKDIR "/src/OrderApi"
RUN dotnet build "./OrderApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OrderApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderApi.dll"]