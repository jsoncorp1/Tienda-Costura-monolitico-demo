# Estándar técnico JsonCorp — Monolito web

Contrato de construcción para aplicaciones web monolíticas. Define **cómo** se construye, no
**qué** hace: el dominio va en un documento aparte.

**Cómo se usa.** Se entrega a un agente de IA junto con la especificación de negocio. Las
reglas están en imperativo y son de cumplimiento obligatorio; debajo de cada una está el
motivo, para que ante un caso que el documento no previó se pueda decidir por analogía en vez
de copiar a ciegas. Cuando una regla se rompa a propósito, se documenta el desvío y su razón
(ver *Desvíos deliberados*).

Este estándar aplica a la rama **monolítica**. Los proyectos de microservicios tienen el suyo.

---

## 1. Stack

Fijo. El agente no elige nada de esta tabla.

| Pieza | Versión | Notas |
|---|---|---|
| Runtime | .NET 8 (`net8.0`) | `Nullable` e `ImplicitUsings` habilitados en los cuatro proyectos |
| Web | ASP.NET Core Blazor Web App | Render por página, ver §4 |
| Base de datos | PostgreSQL | Cadena proporcionada en `DESPLIEGUE.md` |
| ORM | EF Core + `Npgsql.EntityFrameworkCore.PostgreSQL` | `EnableRetryOnFailure(3)` |
| Identidad | ASP.NET Core Identity | (Si aplica) |
| Caché | `Microsoft.Extensions.Caching.Memory` | |
| Solución | formato `.slnx` | |

Nada de AutoMapper, MediatR ni librerías de CQRS. Un monolito de este tamaño no las amortiza:
agregan indirección y vuelven ilegible el salto entre la petición y la consulta.

---

## 2. Arquitectura

Cuatro proyectos. Las dependencias apuntan **hacia adentro** y no hay excepciones.

```
<Proyecto>.slnx
├── backend/
│   ├── <P>.Domain/           Entidades y enums. Cero dependencias de framework.
│   ├── <P>.Application/      Interfaces, DTOs y reglas puras. Depende solo de Domain.
│   ├── <P>.Infrastructure/   EF Core, DbContext, migraciones, servicios, seed, Identity.
│   └── <P>.Web/              Presentación: componentes, endpoints, generación de archivos.
```

**Reglas:**

1. **`Domain` no referencia a nadie.** Solo entidades y enums. 
2. **`Application` define las interfaces; `Infrastructure` las implementa.** Toda capacidad del sistema entra por una interfaz declarada en `Application`.
3. **El usuario de Identity vive en `Infrastructure`, nunca en `Domain`.** 
4. **La capa web consume interfaces, jamás EF para lógica de negocio.**
5. **Acceso a datos por `IDbContextFactory<T>`.** Porque Blazor renderiza en paralelo y un `DbContext` compartido no es seguro entre hilos.

### El patrón del dueño único

Cada dato sensible o derivado tiene exactamente un servicio dueño, y nadie más lo consulta.

---

## 3. Organización interna

`Application` se organiza **por capacidad**, no por tipo técnico.
`Domain/Entidades` agrupa **por área**, no un archivo por clase: entidades que se leen juntas viven en el mismo archivo.
`Infrastructure/Persistencia/Configuraciones/` tiene las configuraciones de EF agrupadas por área.

---

## 4. Render: SSR estático por defecto

| Modo | Dónde | Por qué |
|---|---|---|
| **SSR estático** | Todo lo público e indexable | Tiene que indexarse y funcionar sin JavaScript |
| **InteractiveServer** | Backoffice, Carruseles interactivos | Requiere estado e interactividad dinámica real |

**`@rendermode InteractiveServer` se declara por página, nunca en `App.razor` ni en el
layout.** 

**Si el layout usa componentes que dependen de JS para mostrarse (ej. menú hamburguesa), el estado se maneja a nivel JS interop o se asegura que el render interactivo esté habilitado para la capa de layout en páginas específicas.**

---

## 5. Endpoints y formularios

Minimal API y POSTs tradicionales (salvo donde se requiera render interactivo).

### Binding — el error más caro
Todo parámetro que venga de un formulario lleva `[FromForm]` explícito en Minimal APIs.

---

## 6. Seguridad
- Cifrado de Data Protection persiste en un volumen externo en Docker.
- Autorización en componente, endpoint y servicio.
- Rate limiting para envíos de formularios.

---

## 7. Pruebas
Las pruebas deben cubrir flujos, redirecciones de WhatsApp, y restricciones de acceso. (Implementación mediante testing E2E manual/automatizado).

---

## 8. SEO y rendimiento
- URLs limpias por recurso.
- Componentes 404 reales y específicos.
- Uso eficiente de imágenes.

---

## 9. Despliegue
Contenedor Docker sobre Coolify. Ver `DESPLIEGUE.md`.

---

## 10. Convenciones de código
Nombres de dominio en español (ej. `Mochila`, `Servicio`).
Comentarios explican el "por qué", no el "qué".
