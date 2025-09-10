# Sistema de Salon Spa

**Versión:** 1.0.0  
**Tecnologías:**
- ASP.NET core 
- C#
- Entity Framework + SQLServer
---

##  Descripción
El **Sistema de Salon Spa** permite la gestión integral de un salon spa, incluyendo registro de clientes, control de reservas de servicio, asignación de trabajadores, seguimiento de estados, gestión de usuarios y tipos de servicio, y emisión de reportes administrativos,control de asistencia y horarios.

---

##  Casos de Uso del Negocio (CUN)

| CUN     | Descripción                                                                                   | Estado |
|---------|-----------------------------------------------------------------------------------------------|--------|
| **CUN001** | Registrar cliente (Recepcionista/Admin) - Alta de cliente con validaciones.                    |  Completado |
| **CUN002** | Registrar ingreso/orden (Recepcionista/Admin) - Crear orden con precio calculado y estado inicial `REGISTRADO`. |  Completado |
| **CUN003** | Asignación de órdenes - Trabajador toma orden (`/take`) o Admin asigna (`/assign`).             |  Completado |
| **CUN004** | Progreso de órdenes - Trabajador marca como `TERMINADO`, Recepcionista/Admin entrega (`/deliver`) registrando receptor. |  Completado |
| **CUN005** | Historial del cliente - Consulta con filtros de fecha/estado y paginación.                     |  Completado |
| **CUN006** | Gestión de tipos de servicio (Admin) - CRUD, habilitar/deshabilitar, listar habilitados.       |  Completado |
| **CUN007** | Gestión de usuarios (Admin) - CRUD, detalle, habilitar/deshabilitar (soft delete), roles.      |  Completado |
| **CUN008** | Asignación manual (Admin) - Incluido en CUN003 (`/assign`).                                     |  Completado |
| **CUN009** | Reportes (Admin) - Ingresos por período, productividad por trabajador, órdenes por estado.     |  Pendiente |

---

##  Reglas de Negocio Implementadas

1. **Roles y permisos**
    - `ADMIN`: Acceso completo a todas las operaciones (CRUD de usuarios, servicios, clientes, órdenes y reportes).
    - `RECEPCIONISTA`: Registrar clientes y órdenes, asignar órdenes, entregar órdenes.
    - `TRABAJADOR`: Ver órdenes asignadas, marcar como `TERMINADO`.
    - `CLIENTE`: Consultar historial propio.

2. **Estados de una Orden**
    - `REGISTRADO`: Orden creada, sin asignación.
    - `ASIGNADO`: Orden asignada a un trabajador.
    - `EN_PROCESO`: Trabajador comenzó el trabajo.
    - `TERMINADO`: Trabajo completado, pendiente de entrega.
    - `ENTREGADO`: Entrega realizada y receptor registrado.

3. **Reglas de creación y actualización**
    - Las órdenes calculan el `totalPrice` automáticamente (`weightKg * unitPrice`).
    - No se puede eliminar físicamente un usuario: se deshabilita (`enabled = false`).
    - Tipos de servicio también se pueden deshabilitar en lugar de eliminar.
    - Los usuarios creados por Admin reciben un rol explícito (mínimo un rol por usuario).
    - Validaciones de existencia de cliente, servicio y usuario asignado antes de crear o modificar órdenes.
    - Recepción de órdenes solo con estado válido (no se puede asignar si ya está `TERMINADO` o `ENTREGADO`).

4. **Filtros y búsquedas**
    - Historial de cliente con paginación y filtros por fecha (`from`/`to`) y estado.
    - Listados para Admin con paginación.
    - Búsqueda de órdenes por usuario asignado.

5. **Seguridad**
    - Autenticación vía JWT.
    - Control de permisos centralizado en `SecurityConfig` (sin anotaciones en métodos por ahora).
    - Filtros personalizados para extracción y validación del token.

---

##  Estado del Proyecto

### Completado 
- Implementación de todos los CUN del 001 al 008.
- Control de roles y permisos centralizado.
- Manejo de soft delete en usuarios y tipos de servicio.
- Validaciones y reglas de negocio en servicios.
- Migraciones con Flyway.
- Arquitectura limpia con DTOs, Mappers y separación de capas.

### Pendiente 
- **CUN009**: Módulo de reportes administrativos.
- Documentación API con Swagger/OpenAPI.
- Manejo global de errores (`@ControllerAdvice`).
- Tests unitarios y de integración.
- Auditoría (`createdAt`, `updatedAt`) extendida a más entidades.

© 2025 COUDEVI. Todos los derechos reservados.
