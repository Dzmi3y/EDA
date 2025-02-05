FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER app
WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=Staging

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ./EDA.Services.Order .


RUN dotnet restore EDA.Services.Order.csproj
COPY . .
WORKDIR "/src/EDA.Services.Order"
RUN dotnet build EDA.Services.Order.csproj -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release


RUN dotnet publish EDA.Services.Order.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "EDA.Services.Order.dll"]
