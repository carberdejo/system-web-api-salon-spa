
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
  estado VARCHAR(20) ,
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
  hora_inicio TIME,
  hora_fin TIME,
  estado_horario VARCHAR(20),
  valido_desde DATE
);


CREATE TABLE asistencias (
  id INT IDENTITY(1,1) PRIMARY KEY,
  trabajadora_id INT NOT NULL  REFERENCES trabajadoras(id),
  fecha DATE NOT NULL,
  hora_entrada TIME,
  hora_salida TIME,
  observaciones  VARCHAR(100),
  estado VARCHAR(20) CHECK (estado IN ('PRESENTE', 'TARDE', 'AUSENTE', 'SALIDA_ANTICIPADA'))
);

SELECT 'BASE DE DATOS CARGADA CORRECTAMENTE'
GO

INSERT INTO roles (nombre) VALUES
('ADMIN'),
('TRABAJADORA'),
('RECEPCIONISTA');


INSERT INTO clientes (nombre_completo, telefono, correo, dni, creado_en) VALUES
('Lucía Castillo', '600123456', 'lucia.castillo@example.com', 12345678, GETDATE()),
('Carlos Pérez', '611234567', 'carlos.perez@example.com', 23456789, GETDATE()),
('María Gómez', '622345678', 'maria.gomez@example.com', 34567890, GETDATE()),
('Jorge Ruiz', '633456789', 'jorge.ruiz@example.com', 45678901, GETDATE()),
('Elena Torres', '644567890', 'elena.torres@example.com', 56789012, GETDATE()),
('Ana Morales', '655678901', 'ana.morales@example.com', 67890123, GETDATE()),
('David López', '666789012', 'david.lopez@example.com', 78901234, GETDATE()),
('Carmen Díaz', '677890123', 'carmen.diaz@example.com', 89012345, GETDATE()),
('Raúl Ortega', '688901234', 'raul.ortega@example.com', 90123456, GETDATE()),
('Isabel Ramos', '699012345', 'isabel.ramos@example.com', 11223344, GETDATE());

INSERT INTO trabajadoras 
(nombre, apellido, correo, telefono, dni, contrasena, id_rol, estado, activa, fecha_inicio) VALUES
('Laura', 'Sánchez', 'laura.sanchez@example.com', '600111222', 11111111, 'pass123', 1, 'DISPONIBLE', 'SI', GETDATE()),
('Paula', 'Ramírez', 'paula.ramirez@example.com', '611111222', 22222222, 'pass234', 3, 'OCUPADO', 'SI', GETDATE()),
('Lucía', 'Martínez', 'lucia.martinez@example.com', '622222333', 33333333, 'pass345', 2, 'DISPONIBLE', 'SI', GETDATE()),
('Sara', 'García', 'sara.garcia@example.com', '633333444', 44444444, 'pass456', 2, 'FUERA_DE_SERVICIO', 'SI', GETDATE()),
('Clara', 'Hernández', 'clara.hernandez@example.com', '644444555', 55555555, 'pass567', 2, 'DISPONIBLE', 'SI', GETDATE()),
('Andrea', 'Ruiz', 'andrea.ruiz@example.com', '655555666', 66666666, 'pass678', 2, 'OCUPADO', 'SI', GETDATE()),
('Noelia', 'Navarro', 'noelia.navarro@example.com', '666666777', 77777777, 'pass789', 2, 'DISPONIBLE', 'SI', GETDATE()),
('Eva', 'Ortiz', 'eva.ortiz@example.com', '677777888', 88888888, 'pass890', 2, 'OCUPADO', 'SI', GETDATE()),
('Rocío', 'Domínguez', 'rocio.dominguez@example.com', '688888999', 99999999, 'pass901', 2, 'FUERA_DE_SERVICIO', 'SI', GETDATE()),
('Marta', 'Moreno', 'marta.moreno@example.com', '699999000', 10101010, 'pass012', 3, 'DISPONIBLE', 'SI', GETDATE());


INSERT INTO servicios (nombre, activo) VALUES
('Depilación', 'SI'),
('Manicura', 'SI'),
('Pedicura', 'SI'),
('Masajes', 'SI'),
('Limpieza Facial', 'SI'),
('Tinte de Cejas', 'SI'),
('Peinado', 'SI'),
('Maquillaje', 'SI'),
('Extensiones de Pestañas', 'SI'),
('Tratamiento Capilar', 'SI');

INSERT INTO variantes_servicio (servicio_id, nombre, duracion_min, precio, activo) VALUES
(1, 'Depilación Piernas', 45, 25.00, 'SI'),
(2, 'Manicura Gel', 60, 30.00, 'SI'),
(3, 'Pedicura Completa', 60, 35.00, 'SI'),
(4, 'Masaje Relajante', 60, 50.00, 'SI'),
(5, 'Facial Básico', 45, 40.00, 'SI'),
(6, 'Cejas con Hilo', 30, 15.00, 'SI'),
(7, 'Peinado Rápido', 30, 20.00, 'SI'),
(8, 'Maquillaje Día', 45, 40.00, 'SI'),
(9, 'Pestañas Clásicas', 60, 60.00, 'SI'),
(10, 'Botox Capilar', 90, 80.00, 'SI'),
(4, 'Masaje Relajacion :)',40,100.70,'SI');

INSERT INTO reserva (cliente_id, recepcionista_id, fecha, estado, notas, trabajadora_id) VALUES
(1, 2, '2025-08-10', 'PENDIENTE', 'Cliente nueva', 3),
(2, 1, '2025-08-11', 'CONFIRMADA', 'Quiere gel', 4),
(3, 3, '2025-08-12', 'CONFIRMADA', 'Cambio de horario', 5),
(4, 2, '2025-08-13', 'CANCELADA', 'Por lluvia', 6),
(5, 4, '2025-08-14', 'PENDIENTE', 'No vino', 7),
(6, 5, '2025-08-15', 'CANCELADA', 'Está dentro', 8),
(7, 6, '2025-08-16', 'CONFIRMADA', 'Quedó feliz', 9),
(8, 7, '2025-08-17', 'CANCELADA', 'Primera vez', 10),
(9, 8, '2025-08-18', 'CONFIRMADA', 'Recurrente', 1),
(10, 9, '2025-08-19', 'PENDIENTE', 'Solicitó cambio', 2);

INSERT INTO detalles_reserva (reserva_id, variante_id, cantidad, precio) VALUES
(1, 1, 1, 25.00),
(2, 2, 1, 30.00),
(3, 3, 1, 35.00),
(4, 4, 1, 50.00),
(5, 5, 1, 40.00),
(6, 6, 2, 30.00),
(7, 7, 1, 20.00),
(8, 8, 1, 40.00),
(9, 9, 1, 60.00),
(10, 10, 1, 80.00);


INSERT INTO disponibilidad (trabajadora_id, hora_inicio, hora_fin, estado_horario, valido_desde) VALUES
(1, '09:00:00', '13:00:00', 'HABILITADO', '2025-08-01'),
(2,  '10:00:00', '14:00:00', 'HABILITADO', '2025-08-01'),
(3,  '12:00:00', '16:00:00', 'HABILITADO', '2025-08-01'),
(4,  '08:00:00', '12:00:00', 'HABILITADO', '2025-08-01'),
(5,  '13:00:00', '17:00:00', 'HABILITADO', '2025-08-01'),
(6,  '09:00:00', '13:00:00', 'HABILITADO', '2025-08-01'),
(7, '10:00:00', '14:00:00', 'HABILITADO', '2025-08-01'),
(8,  '11:00:00', '15:00:00', 'HABILITADO', '2025-08-01'),
(9,  '14:00:00', '18:00:00', 'HABILITADO', '2025-08-01'),
(7,  '08:00:00', '12:00:00', 'HABILITADO', '2025-08-01'),
(3, '10:00:00', '16:00:00', 'HABILITADO', '2025-08-01'),
(2,  '07:00:00', '21:00:00', 'HABILITADO', '2025-08-01');


INSERT INTO asistencias (trabajadora_id, fecha, hora_entrada, hora_salida, observaciones, estado) VALUES
(1, '2025-08-20', '09:00:00', '13:00:00', 'Todo normal', 'PRESENTE'),
(2, '2025-08-20', '10:15:00', '14:00:00', 'Llegó tarde', 'TARDE'),
(3, '2025-08-20', NULL, NULL, 'No vino', 'AUSENTE'),
(4, '2025-08-20', '08:00:00', '10:30:00', 'Salió antes', 'SALIDA_ANTICIPADA'),
(5, '2025-08-21', '13:00:00', '17:00:00', 'Normal', 'PRESENTE'),
(6, '2025-08-21', '09:10:00', '13:00:00', 'Pequeño retraso', 'TARDE'),
(7, '2025-08-21', NULL, NULL, 'Faltó', 'AUSENTE'),
(8, '2025-08-21', '11:00:00', '14:00:00', 'Horario corto', 'SALIDA_ANTICIPADA'),
(9, '2025-08-22', '14:00:00', '18:00:00', 'Perfecto', 'PRESENTE'),
(10, '2025-08-22', '08:30:00', '12:00:00', 'Todo bien', 'PRESENTE');



Select * FROM asistencias;
Select * FROM roles;
Select * FROM clientes;
Select * FROM trabajadoras;
Select * FROM servicios
Select * FROM variantes_servicio;
Select * FROM reserva;
Select * FROM detalles_reserva;
Select * FROM disponibilidad;
GO

Create or Alter Procedure Reporte_Servicios
AS
BEGIN
	Select s.id,s.nombre, 
	Count(v.id) AS [CantidadVariantes],MIN(v.precio) AS [PrecioMinimo] , MAX(v.precio) AS [PrecioMaximo]
	from Servicios s INNER JOIN variantes_servicio v ON s.id = v.servicio_id
	GROUP BY s.id,s.nombre
	ORDER BY s.id
	END 
	GO

	exec Reporte_Servicios;
	GO

Create or Alter Procedure Reporte_Trabajadores
AS
BEGIN
	Select T.id, t.nombre,t.dni, t.telefono, 
	SUM(DATEDIFF(Hour, d.hora_inicio,d.hora_fin)) AS [HorasTotales], COUNT(a.trabajadora_id) AS [Dias Trabajar]
	from trabajadoras t INNER JOIN disponibilidad d ON d.trabajadora_id = t.id
	join asistencias a on a.trabajadora_id = t.id
	GROUP BY t.id, t.nombre , t.dni, t.telefono
	END
	GO

	exec Reporte_Trabajadores
	go

