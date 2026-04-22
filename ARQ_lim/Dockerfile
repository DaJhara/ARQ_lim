# Imagen base para ejecutar la app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Imagen para compilar
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar todo el proyecto
COPY . .

# Ir al proyecto WebApi
WORKDIR /src/src/WebApi

# Publicar la app
RUN dotnet publish -c Release -o /app/publish

# Imagen final
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:80

# Ejecutar la app
ENTRYPOINT ["dotnet", "WebApi.dll"]