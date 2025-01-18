# Usamos una imagen base oficial de .NET 8 SDK para construir la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Establecer el directorio de trabajo
WORKDIR /src

# Copiar el archivo .csproj y restaurar las dependencias
COPY ["PruebaTecnicaSeti/PruebaTecnicaSeti.csproj", "PruebaTecnicaSeti/"]
RUN dotnet restore "PruebaTecnicaSeti/PruebaTecnicaSeti.csproj"

# Copiar el resto de los archivos y construir la aplicación
COPY . .
WORKDIR "/src/PruebaTecnicaSeti"
RUN dotnet publish "PruebaTecnicaSeti.csproj" -c Release -o /app/publish

# Usar una imagen base oficial de .NET 8 Runtime para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

# Establecer el directorio de trabajo para el contenedor
WORKDIR /app

# Copiar los archivos de la aplicación construida
COPY --from=build /app/publish .

# Exponer el puerto en el que se ejecutará la API
EXPOSE 80

# Comando para ejecutar la API
ENTRYPOINT ["dotnet", "PruebaTecnicaSeti.dll"]