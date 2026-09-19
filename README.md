# Documentación del proyecto STATUS

Dos carpetas, dos propósitos distintos. Se entregan por separado.

## `01-Estandar-Tecnico/`

**[ESTANDAR-TECNICO-MONOLITO.md](01-Estandar-Tecnico/ESTANDAR-TECNICO-MONOLITO.md)** — contrato de construcción de JsonCorp para aplicaciones monolíticas. Describe **cómo** se construye, sin nada del dominio: stack, arquitectura de capas, patrones, render, endpoints, seguridad, pruebas, SEO, despliegue y convenciones de código.

Es reusable. Se le pasa a un agente de IA junto con la especificación de negocio de *otro* proyecto para que salga con la misma estructura técnica que este.

## `02-Negocio/`

**[DOCUMENTACION-FUNCIONAL.md](02-Negocio/DOCUMENTACION-FUNCIONAL.md)** — qué hace el sistema: los flujos de venta y cotización, servicios ofrecidos (costura, DTF, sublimado, bordado), las reglas de negocio con su ubicación en el código, el catálogo y superficie de pantallas.

**[PRUEBAS-PUNTA-A-PUNTA.md](02-Negocio/PRUEBAS-PUNTA-A-PUNTA.md)** — resultados medidos de ejercer la aplicación levantada: flujos de WhatsApp, filtros de catálogo, menú móvil, páginas 404 personalizadas, y verificación de integración general.

## Suelto

**[DESPLIEGUE.md](DESPLIEGUE.md)** — puesta en marcha en Coolify: variables de entorno, volumen de claves, proxy inverso y conexión a PostgreSQL. Es operativo de *este* proyecto, no encaja en ninguna de las dos categorías anteriores.
