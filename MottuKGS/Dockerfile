
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Directory.Build.props", "."]
COPY ["MottuKGS/MottuKGS.csproj", "MottuKGS/"]
RUN dotnet restore "MottuKGS/MottuKGS.csproj"
COPY . .
WORKDIR "/src/MottuKGS"
RUN dotnet build "MottuKGS.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MottuKGS.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MottuKGS.dll"]