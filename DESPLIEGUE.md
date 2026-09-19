# Despliegue en Coolify (STATUS)

Guía de despliegue de la aplicación web STATUS (Costura) en contenedores mediante Coolify, integrando base de datos PostgreSQL.

---

## Variables de entorno

Se cargan en Coolify, en *Environment Variables* del recurso (la aplicación web). 

| Variable | Valor | Obligatoria |
|---|---|---|
| `ConnectionStrings__Default` | `Host=38.49.214.57;Port=5434;Database=postgres;Username=postgres;Password=L7kXAC3jFGsI6YDMrqsC9Fw28woSL1NEweemdC63CO2AN7wAgOPc7z31HWaFmrqq` | **Sí** |
| `ASPNETCORE_ENVIRONMENT` | `Production` | No (ya viene en la imagen) |
| `TZ` | `America/La_Paz` | No |

> **Nota importante sobre Npgsql:** EF Core / Npgsql para .NET no entiende de forma nativa el formato URI estándar (`postgres://usuario:clave@host/base`). Por lo tanto, la cadena original:
> `postgres://postgres:L7kXAC3jFGsI6YDMrqsC9Fw28woSL1NEweemdC63CO2AN7wAgOPc7z31HWaFmrqq@38.49.214.57:5434/postgres`
> Ha sido desglosada en la tabla superior en su formato `Host=...;Port=...;Username=...` para que ASP.NET Core pueda conectarse exitosamente.

Marcá la cadena de conexión como secreta en Coolify para que no aparezca en los registros de despliegue.

---

## Almacenamiento persistente para Data Protection

En *Storages*, agregar un volumen montado en **`/keys`**.

Ahí van las claves que cifran las cookies de sesión y los tokens antiforgery de ASP.NET Core. Sin este volumen persistente montado en el contenedor, cada actualización o redespliegue de la imagen destruirá las llaves de encriptación, forzando un cierre de sesión a todos los usuarios y causando fallos en los formularios (CSRF validation failed) que se estuvieran llenando en ese momento.

---

## Pasos para la puesta en marcha

1. En Coolify: **New Resource → Application → Dockerfile**, apuntando al repositorio de STATUS.
2. Build Pack: **Dockerfile**. La ruta debe apuntar al Dockerfile en la raíz de la solución.
3. Puerto expuesto del contenedor: **8080** (o 80, dependiendo de tu Dockerfile, típicamente 8080 en .NET 8).
4. Cargar la variable `ConnectionStrings__Default` (ver arriba).
5. Agregar el volumen `/keys` persistente.
6. Asignar el dominio de la marca (ej. `status.com.bo`) y habilitar Let's Encrypt (Coolify emitirá el certificado SSL/TLS).
7. Desplegar.

El primer arranque ejecutará las migraciones pendientes en PostgreSQL (creando la tabla de Productos y Contactos si existieran).

---

## Proxy Inverso

Al estar detrás de Traefik/Caddy en Coolify:
- Coolify finaliza el TLS (HTTPS). 
- El contenedor .NET debe configurarse (usualmente en `Program.cs` usando `app.UseForwardedHeaders()`) para confiar en el proxy interno de Coolify, de forma que el ruteo funcione correctamente sin generar bucles de redirección SSL infinitos.
