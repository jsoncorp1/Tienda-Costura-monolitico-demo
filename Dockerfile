# ---------------------------------------------------------------------------
# STATUS (Costura) - Blazor Server / .NET 8
# Imagen para despliegue en Coolify (ver DESPLIEGUE.md)
# Contexto de build: raíz del repositorio
# ---------------------------------------------------------------------------

# --- Etapa 1: restore + build --------------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiar solo los manifiestos primero: así la capa de restore se cachea
# mientras no cambien los .csproj (los cambios de código no invalidan NuGet).
COPY backend/Status.slnx ./
COPY backend/Status.Domain/Status.Domain.csproj           Status.Domain/
COPY backend/Status.Application/Status.Application.csproj Status.Application/
COPY backend/Status.Infrastructure/Status.Infrastructure.csproj Status.Infrastructure/
COPY backend/Status.Web/Status.Web.csproj                 Status.Web/

RUN dotnet restore Status.Web/Status.Web.csproj

# Ahora sí, el resto del código fuente
COPY backend/ ./

RUN dotnet publish Status.Web/Status.Web.csproj \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    --no-restore \
    /p:UseAppHost=false

# --- Etapa 2: runtime -----------------------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=Production \
    ASPNETCORE_HTTP_PORTS=8080 \
    DOTNET_RUNNING_IN_CONTAINER=true \
    TZ=America/La_Paz

# Punto de montaje del volumen persistente de Data Protection.
# Se crea aquí (y con el dueño correcto) para que el usuario no-root
# pueda escribir las llaves cuando Coolify monte el volumen en /keys.
RUN mkdir -p /keys && chown -R $APP_UID:$APP_UID /keys
VOLUME /keys

COPY --from=build /app/publish .

# Usuario sin privilegios provisto por la imagen base de .NET 8
USER $APP_UID

EXPOSE 8080

ENTRYPOINT ["dotnet", "Status.Web.dll"]
