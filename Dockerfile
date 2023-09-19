#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CubosChallenge/CubosChallenge.csproj", "CubosChallenge/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Core/Core.csproj", "Core/"]
RUN dotnet restore "CubosChallenge/CubosChallenge.csproj"
COPY . .
WORKDIR "/src/CubosChallenge"
RUN dotnet build "CubosChallenge.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CubosChallenge.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CubosChallenge.dll"]
