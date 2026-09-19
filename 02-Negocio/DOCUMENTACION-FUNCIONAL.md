# Documentación funcional — STATUS (Costura)

Qué hace el sistema y dónde está implementada cada regla para el proyecto STATUS.

**Cliente:** STATUS (Json Corp)
**Alcance:** Sitio web público (Landing Page) + Catálogo de productos + Formularios B2B/B2C integrados con WhatsApp.

---

## 1. El problema que resuelve

STATUS fabrica y vende productos textiles y ofrece servicios de costura, sublimado, bordado y estampado DTF. Vende por dos vías principales que se deben capturar:

1. **Cliente individual (B2C):** Quiere comprar un producto terminado (mochila, billetera, etc.) al por menor. Necesita ver un catálogo con filtros y redirigirse a WhatsApp para completar el pedido indicando cantidad y personalización.
2. **Cliente corporativo (B2B):** Instituciones (colegios, empresas) que necesitan volumen (chamarras, mochilas con logo). No buscan un "carrito de compras", sino un formulario formal para adjuntar su requerimiento y cotizar la producción.

El sistema resuelve esto centralizando el catálogo y canalizando ambas intenciones de compra hacia el WhatsApp de ventas con mensajes estructurados.

---

## 2. Los dos flujos de venta

### Flujo Individual (Catálogo B2C)
Navega el `/catalogo`. Puede filtrar por "Mochilas", "Billeteras", etc. Entra al detalle de un producto. Selecciona si quiere logo o bordado, y la cantidad. Hace clic en "Consultar por WhatsApp" y se genera el mensaje pre-armado con todos los detalles del SKU.

### Flujo Corporativo (Cotización B2B)
Entra a `/empresas`. Llena Razón Social, NIT, Nombre, Teléfono y describe el pedido ("Necesito 50 mochilas con logo"). El sistema valida los datos y al presionar "Enviar por WhatsApp", redirige al chat con un resumen corporativo profesional.

---

## 3. Servicios Ofrecidos

De acuerdo a los requerimientos del cliente, la empresa ofrece y la plataforma documenta en `/servicios` los siguientes:
1. **Costura:** Ropa institucional y deportiva (chamarras, chalecos, rompevientos, parkas, polos, poleras, camisas, conjuntos). También fabricamos mochilas, maletines, bolsos, billeteras, carpeteros, portadocumentos y riñoneras con acabados de alta calidad.
2. **Sublimado Textil Alta Definición**
3. **Bordado Industrial Computarizado**
4. **Estampado DTF Vanguardista**

---

## 4. Reglas de Negocio (RN)

| Regla | Qué dice | Dónde vive |
|---|---|---|
| **RN-01** | Categorización automática | `Catalogo.razor` (El filtro detecta la categoría leyendo palabras clave del nombre del producto, ej. "Mochila", "Billetera"). |
| **RN-02** | Breadcrumbs de orientación | `ProductoDetalle.razor` (Inicio > Catálogo > [Nombre del Producto]). |
| **RN-03** | Generación de leads B2B | `Empresas.razor` (El botón de envío valida datos obligatorios y formatea el payload para WhatsApp). |
| **RN-04** | Redirección transversal | Desde `ProductoDetalle.razor` se puede saltar al flujo B2B pasando parámetros en la URL (`?producto=ST1032&qty=50`). |
| **RN-05** | UI de Alta Conversión | Elementos Glassmorphism, animaciones CSS y contrastes modernos (mostaza sobre petróleo) guiando hacia la acción. |

---

## 5. Superficie del sistema

- **`/` (Home):** Carrusel interactivo, presentación de la marca, enlaces rápidos.
- **`/catalogo`:** Grilla tipo asimétrica/bento de productos, barra de búsqueda, píldoras de filtrado.
- **`/producto/{slug}`:** Detalle del producto, imagen, precio, casillas de personalización y CTA a WhatsApp.
- **`/servicios`:** Landing explicativa de los 4 servicios con opción de cotizar.
- **`/empresas`:** Formulario corporativo B2B.
- **Página 404 Personalizada:** Para rutas inexistentes, con opciones de retorno.

---

## 6. Puntos abiertos

- **Conexión Real a Base de Datos:** Actualmente el catálogo puede operar en memoria (dummy data). Cuando se conecte a PostgreSQL en producción, se vaciará el seed de datos.
- **URLs de Redes Sociales:** Faltan los enlaces finales de Facebook/Instagram/TikTok para actualizar el Footer.
