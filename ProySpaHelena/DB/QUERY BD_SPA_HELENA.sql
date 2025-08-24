
USE master
GO

IF db_id('BD_SPA_HELENA') is not null
begin
	ALTER DATABASE BD_SPA_HELENA
	SET SINGLE_USER WITH ROLLBACK IMMEDIATE

	DROP DATABASE BD_SPA_HELENA
end
go

CREATE DATABASE BD_SPA_HELENA
COLLATE Modern_Spanish_CI_AI
GO

SET LANGUAGE SPANISH
SET NOCOUNT ON
GO

USE BD_SPA_HELENA
GO


 CREATE TABLE roles (
  id_rol int identity PRIMARY KEY,
  nombre VARCHAR(30) UNIQUE NOT NULL  -- ADMIN, TRABAJADORA, CLIENTE
);

-- Tabla de usuarios
CREATE TABLE clientes (
  id INT IDENTITY(1,1) PRIMARY KEY,
  nombre_completo VARCHAR(120) NOT NULL,
  telefono VARCHAR(20),
  correo VARCHAR(120),
  dni int not null,
  creado_en DATETIME DEFAULT GETDATE()
);

-- Trabajadoras (perfil extendido)
CREATE TABLE trabajadoras (
  id INT IDENTITY(1,1) PRIMARY KEY,
  nombre VARCHAR(120) NOT NULL,
  apellido VARCHAR(120) NOT NULL,
  correo VARCHAR(120) UNIQUE NOT NULL,
  telefono VARCHAR(20) NOT NULL,
  dni int NOT NULL,
  contrasena  VARCHAR(35) NOT NULL,
  id_rol int NOT NULL REFERENCES  roles(id_rol),
  activa CHAR(2) DEFAULT 'SI',
  fecha_inicio DATETIME DEFAULT GETDATE()
);

-- Servicios y variantes
CREATE TABLE servicios (
  id INT IDENTITY(1,1) PRIMARY KEY,
  nombre VARCHAR(80) NOT NULL,
  activo CHAR(2) DEFAULT 'SI'
);

CREATE TABLE variantes_servicio (
  id INT IDENTITY(1,1) PRIMARY KEY,
  servicio_id INT NOT NULL REFERENCES servicios(id),
  nombre VARCHAR(80) NOT NULL,
  duracion_min INT NOT NULL,
  precio DECIMAL(10,2) NOT NULL,
  activo CHAR(2) DEFAULT 'SI'
);

CREATE TABLE reserva (
  id INT IDENTITY(1,1) PRIMARY KEY,
  cliente_id INT NOT NULL REFERENCES clientes(id),
  recepcionista_id INT NOT NULL REFERENCES trabajadoras(id),
  fecha DATE NOT NULL,
  estado VARCHAR(20) CHECK (estado IN (
    'PENDIENTE','CONFIRMADA','REPROGRAMADA','CANCELADA','NO_SHOW','EN_SERVICIO','COMPLETADA'
  )),
  notas VARCHAR(100),
  trabajadora_id INT NOT NULL FOREIGN KEY REFERENCES trabajadoras(id)
);

CREATE TABLE detalles_reserva (
  id INT IDENTITY(1,1) PRIMARY KEY,
  reserva_id INT NOT NULL  REFERENCES reserva(id) ON DELETE CASCADE,
  variante_id INT NOT NULL REFERENCES variantes_servicio(id),
  cantidad INT DEFAULT 1,
  precio DECIMAL(10,2) NOT NULL
);
 

-- Disponibilidad
CREATE TABLE disponibilidad (
  id INT IDENTITY(1,1) PRIMARY KEY,
  trabajadora_id INT NOT NULL  REFERENCES trabajadoras(id),
  dia_semana SMALLINT,  -- 0=domingo..6=sábado
  hora_inicio TIME,
  hora_fin TIME,
  valido_desde DATE,
  valido_hasta DATE
);


CREATE TABLE asistencias (
  id INT IDENTITY(1,1) PRIMARY KEY,
  trabajadora_id INT NOT NULL  REFERENCES trabajadoras(id),
  fecha DATE NOT NULL,
  hora_entrada DATETIME,
  hora_salida DATETIME,
  observaciones  VARCHAR(100),
  estado VARCHAR(20) CHECK (estado IN ('PRESENTE', 'TARDE', 'AUSENTE', 'SALIDA_ANTICIPADA')),
  registrada_en DATETIME DEFAULT GETDATE()
);

SELECT 'BASE DE DATOS CARGADA CORRECTAMENTE'
GO

INSERT INTO roles (nombre )VALUES ('Administrados'),('Recepcionista'),('Estilista');
SELECT * FROM roles
SELECT * FROM trabajadoras
SELECT * FROM reserva
SELECT * FROM detalles_reserva
go


INSERT INTO servicios (nombre)VALUES ('CORTES DE PELO'),('Manicure')
GO

INSERT INTO clientes (nombre_completo, telefono, correo, dni)
VALUES 
('Ana Pérez', '987654321', 'ana.perez@mail.com', 12345678),
('Luis Gómez', '912345678', 'luis.gomez@mail.com', 23456789),
('María Fernández', '998877665', 'maria.fernandez@mail.com', 34567890),
('Carlos Ruiz', '976543210', 'carlos.ruiz@mail.com', 45678901),
('Sofía Ramírez', '965432109', 'sofia.ramirez@mail.com', 56789012);

INSERT INTO trabajadoras (nombre, apellido, correo, telefono, dni, contrasena, id_rol, activa)
VALUES
('Lucía', 'Torres', 'lucia.torres@mail.com', '987123456', 67890123, 'Pass1234', 1, 'SI'),
('Valentina', 'Mendoza', 'valentina.mendoza@mail.com', '976543219', 78901234, 'Clave5678', 2, 'SI'),
('Camila', 'Rojas', 'camila.rojas@mail.com', '965432198', 89012345, 'Segura9012', 1, 'SI'),
('Fernanda', 'López', 'fernanda.lopez@mail.com', '954321987', 90123456, 'Fuerte3456', 3, 'SI'),
('Paola', 'García', 'paola.garcia@mail.com', '943210987', 12345012, 'Clave7890', 2, 'SI');

INSERT INTO reserva (cliente_id, recepcionista_id, fecha, estado, notas, trabajadora_id)
VALUES
(1, 2, '2025-08-25', 'PENDIENTE', 'Primera cita, solicitar masaje relajante', 1),
(2, 2, '2025-08-26', 'CONFIRMADA', 'Tratamiento facial, llevar crema hidratante', 3),
(3, 2, '2025-08-27', 'REPROGRAMADA', 'Cambio de horario, preferencia por la tarde', 4),
(4, 2, '2025-08-28', 'CANCELADA', 'Cliente canceló por viaje', 5),
(5, 2, '2025-08-29', 'EN_SERVICIO', 'Masaje corporal completo, incluir aromaterapia', 1);

INSERT INTO detalles_reserva(reserva_id,variante_id,cantidad,precio) VALUES
(5,1,2,20),(5,1,3,10)

SELECT * FROM variantes_servicio
GO
SELECT * FROM detalles_reserva
GO	


create or alter Procedure Reporte_1 
AS
BEGIN
	Select t.dni,t.nombre, t.apellido, t.telefono,d.hora_inicio,d.hora_fin,d.dia_semana, DATEDIFF(HOUR, hora_inicio,hora_fin) from trabajadoras t INNER JOIN disponibilidad d ON t.id = d.trabajadora_id 
	GROUP BY t.dni,t.nombre,t.apellido,t.telefono,d.hora_inicio,d.hora_fin,d.dia_semana
	END
	GO

	exec Reporte_1
	go

create or alter Procedure Reporte_Reserva
AS
BEGIN
	Select r.id,c.nombre_completo,c.dni,t.nombre,t.apellido,c.telefono,t.telefono,t.dni,r.fecha,r.estado,r.notas,SUM(dr.precio * cantidad)as total from reserva r 
	INNER JOIN clientes c ON r.cliente_id = c.id 
	INNER JOIN trabajadoras t ON r.trabajadora_id = t.id INNER JOIN detalles_reserva dr ON r.id = dr.reserva_id
	GROUP BY 
		r.id, c.nombre_completo, c.dni, 
		t.nombre, t.apellido, 
		c.telefono, t.telefono, t.dni, 
		r.fecha, r.estado, r.notas
	ORDER BY r.id ASC
	END 
	GO

	exec Reporte_Reserva