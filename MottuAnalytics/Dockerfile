
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Directory.Build.props", "."]
COPY ["MottuAnalytics/MottuAnalytics.csproj", "MottuAnalytics/"]
RUN dotnet restore "MottuAnalytics/MottuAnalytics.csproj"
COPY . .
WORKDIR "/src/MottuAnalytics"
RUN dotnet build "MottuAnalytics.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MottuAnalytics.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MottuAnalytics.dll"]