#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Directory.Build.props", "."]
COPY ["MottuApi/MottuApi.csproj", "MottuApi/"]
COPY ["MottuShared/MottuShared.csproj", "MottuShared/"]
RUN dotnet restore "MottuApi/MottuApi.csproj"
COPY . .
WORKDIR "/src/MottuApi"
RUN dotnet build "MottuApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MottuApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MottuApi.dll"]