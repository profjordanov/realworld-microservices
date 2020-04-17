#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/ProductCatalog.Api/ProductCatalog.Api.csproj", "src/ProductCatalog.Api/"]
COPY ["src/OrderManager.Common/OrderManager.Common.csproj", "src/OrderManager.Common/"]
RUN dotnet restore "src/ProductCatalog.Api/ProductCatalog.Api.csproj"
COPY . .
WORKDIR "/src/src/ProductCatalog.Api"
RUN dotnet build "ProductCatalog.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductCatalog.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductCatalog.Api.dll"]