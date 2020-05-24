#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/RemoteProxyApi/RemoteProxyApi.csproj", "src/RemoteProxyApi/"]
COPY ["src/ArticlesClient/ArticlesClient.csproj", "src/ArticlesClient/"]
COPY ["src/ArticlesProtos/ArticlesProtos.csproj", "src/ArticlesProtos/"]
COPY ["src/TagsClient/TagsClient.csproj", "src/TagsClient/"]
COPY ["src/TagsProtos/TagsProtos.csproj", "src/TagsProtos/"]
RUN dotnet restore "src/RemoteProxyApi/RemoteProxyApi.csproj"
COPY . .
WORKDIR "/src/src/RemoteProxyApi"
RUN dotnet build "RemoteProxyApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RemoteProxyApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RemoteProxyApi.dll"]