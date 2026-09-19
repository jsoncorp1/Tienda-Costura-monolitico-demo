# Pruebas de punta a punta — resultados STATUS

Verificación de la experiencia de usuario y flujos de integración del proyecto STATUS.

**Fecha:** 19 de septiembre de 2026.
**Método:** Inspección visual y pruebas funcionales de E2E levantando el entorno Blazor local (`http://localhost:5027`).

---

## 1. Flujos de WhatsApp verificados

### Flujo B2B (Empresas)
1. Navegación a `/empresas`.
2. Intento de envío en blanco: **Correcto**. El sistema detiene el envío y muestra error rojo: `⚠️ Por favor completa tu nombre, WhatsApp y los productos que necesitas.`
3. Llenado exitoso: Razón Social: "Colegio XY", Nombre: "Juan", WhatsApp: "71923455", Detalle: "50 chamarras".
4. Redirección: **Correcto**. Redirige a `wa.me/59171923455` con el texto:
   `Hola STATUS! Quiero cotizar un pedido. 🏢 Empresa/Institución: Colegio XY 🪪 NIT: No aplica 👤 Contacto: Juan 📱 WhatsApp: 71923455 📦 Productos que necesito: 50 chamarras`

### Flujo B2C (Detalle de Producto)
1. Navegación a `/producto/st1032-zapatero`.
2. Se selecciona: Cantidad "5", "Logo propio" marcado, "Bordado" marcado.
3. Clic en "Consultar por WhatsApp": **Correcto**. El texto se armó incluyendo "Opciones de personalización: Logo propio, Bordado" y la cantidad 5.

---

## 2. Navegación y Usabilidad (UI/UX)

### Filtros del Catálogo
- Visitar `/catalogo`.
- Clic en píldora "Billeteras": **Correcto**. La grilla se reduce y muestra únicamente la Billetera STATUS (ST1024 desaparece).
- Clic en píldora "Todos": **Correcto**. Se restaura todo el inventario.

### Menú Mobile (Hamburguesa)
- Simulación de viewport móvil (<768px).
- Los enlaces principales se ocultan correctamente.
- Clic en icono `☰`: **Correcto**. Se desliza el Drawer azul petróleo desde la derecha.
- Clic fuera del menú: **Correcto**. El Drawer se cierra.

### Breadcrumbs
- En `/producto/st1032-zapatero`, la barra superior muestra `Inicio > Catálogo > ST1032 Mochila con Zapatero`. Los enlaces hacia atrás funcionan sin problemas.

### Página 404
- Navegar a `http://localhost:5027/ruta-inexistente`
- Resultado: **Correcto**. No se muestra el error genérico de ASP.NET, sino una vista completa con el logo, un "404" de fondo y botones para regresar al Inicio o al Catálogo.

---

## 3. Calidad Visual 

- **Servicios (`/servicios`):** El Hero cuenta con una imagen de fondo (chamarra costura) oscurecida por gradiente, el texto resalta y está alineado al estándar de la Landing Page. La descripción detalla explícitamente "chamarras, chalecos, rompevientos, parkas, polos, poleras, camisas, conjuntos...".
- **Home (`/`):** El Carrusel de Hero maneja transiciones limpias y tiene el componente Glassmorphism (panel translúcido oscuro) protegiendo la legibilidad de la tipografía.
- **Estabilidad de CSS:** Las variables CSS se respetan unificando colores corporativos (Petróleo, Crema, Mostaza, Terracota).
