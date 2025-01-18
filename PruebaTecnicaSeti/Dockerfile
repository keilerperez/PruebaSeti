FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PruebaTecnicaSeti/PruebaTecnicaSeti.csproj", "PruebaTecnicaSeti/"]
RUN dotnet restore "./PruebaTecnicaSeti/PruebaTecnicaSeti.csproj"
COPY . .
WORKDIR "/src/PruebaTecnicaSeti"
RUN dotnet build "./PruebaTecnicaSeti.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PruebaTecnicaSeti.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=true

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PruebaTecnicaSeti.dll"]