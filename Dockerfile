#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
RUN apt-get update \
    && apt-get install -y ca-certificates \
    && rm -rf /var/lib/apt/lists/*
COPY weather-tracker.crt /usr/local/share/ca-certificates/weather-tracker.crt
RUN update-ca-certificates

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["weather-tracker.csproj", "."]
RUN dotnet restore "./weather-tracker.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "weather-tracker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "weather-tracker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "weather-tracker.dll"]