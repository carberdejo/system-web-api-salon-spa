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
| **CUN002** |  Crear reserva (Recepcionista) indicando id de recepcionista, trabajador, servicio, cliente y estado inicial. |  Completado |
| **CUN003** | Cambiar estado de reserva: *programada → en_cola → en_progreso → terminada*. |  Completado |
| **CUN004** | Asignación de trabajador: automático si es *programada*, solo disponibles si es *en_cola*. |  Completado |
| **CUN005** | Registro de asistencia (Admin): presente, tardanza, falta; al marcar una asistencia cambia el estado del trabajador |  Completado |
| **CUN006** | Gestión de tipos de servicio (Admin) - CRUD, habilitar/deshabilitar, listar habilitados.       |  Completado |
| **CUN007** | Gestión de usuarios (Admin) - CRUD, detalle, habilitar/deshabilitar (soft delete), roles.      |  Completado |
| **CUN008** | Reportes administrativos: reservas por servicio, clientes atendidos.  |  Completado |

---

##  Reglas de Negocio Implementadas

1. **Roles y permisos**  
   - `ADMIN`: Acceso completo (CRUD de usuarios, servicios y clientes, reportes, control de asistencia).  
   - `RECEPCIONISTA`: Gestión exclusiva de reservas (crear, actualizar estados, consultar historial).  
   - `TRABAJADOR`: No accede al sistema, solo se gestiona su disponibilidad y asistencia.

2. **Estados de una Reserva**  
   - `PROGRAMADA`: Atención futura, trabajador asignado automáticamente.  
   - `EN_COLA`: Atención el mismo día, asignado a un trabajador disponible.  
   - `EN_PROGRESO`: Reserva en ejecución.  
   - `TERMINADA`: Servicio finalizado.

3. **Reglas de creación y actualización**  
   - Una reserva requiere obligatoriamente id de recepcionista, trabajador, servicio y cliente.  
   - Los estados deben respetar el flujo: *programada → en_cola → en_progreso → terminada*.  
   - El trabajador debe estar disponible para ser asignado.  
   - Los clientes registrados incluyen un horario, el cual puede modificarse posteriormente.  

4. **Asistencia de trabajadores**  
   - Al registrar asistencia como *presente*, el trabajador pasa automáticamente a estado **disponible**.  
   - Tardanza y falta se almacenan como historial de control.  

5. **Seguridad**  
   - Autenticación y control de accesos por roles.  
   - Manejo de sesiones de usuario.  

---

##  Estado del Proyecto

### Completado 
- Gestión de clientes con horario.  
- Flujo completo de reservas y estados.  
- CRUD de usuarios y servicios.  
- Control de asistencia de trabajadores.  
- Validaciones de negocio implementadas en reservas y asignaciones.  

### Pendiente 
- Módulo de reportes administrativos (reservas por servicio, productividad por clientes).  
- Documentación de API con Swagger/OpenAPI.  
- Manejo global de errores.  
- Tests unitarios e integración.  

© 2025 COUDEVI. Todos los derechos reservados.
