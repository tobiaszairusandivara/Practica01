CREATE DATABASE Practica01
GO
USE Practica01
GO

CREATE TABLE Articulos(
id_articulo INT IDENTITY,
nombre VARCHAR(40) not null,
pre_unitario INT NOT NULL,
CONSTRAINT pk_articulo PRIMARY KEY (id_articulo)
)

CREATE TABLE FormasPago(
id_forma_pago INT IDENTITY,
nombre VARCHAR(50) NOT NULL,
CONSTRAINT pk_FormasPago PRIMARY KEY(id_forma_pago)
)

CREATE TABLE Facturas(
nro_factura INT IDENTITY,
fecha DATE NOT NULL,
id_forma_pago INT NOT NULL,
cliente VARCHAR(40),
CONSTRAINT pk_Facturas PRIMARY KEY (nro_factura),
CONSTRAINT fk_FormasPago FOREIGN KEY (id_forma_pago)
REFERENCES FormasPago(id_forma_pago)
)

CREATE TABLE DetallesFactura(
id_detalle INT IDENTITY,
nro_factura INT NOT NULL,
id_articulo INT NOT NULL,
cantidad INT NOT NULL
CONSTRAINT pk_DetallesFactura PRIMARY KEY (id_detalle),
CONSTRAINT fk_Facturas FOREIGN KEY (nro_factura)
REFERENCES Facturas(nro_factura),
CONSTRAINT fk_Articulos FOREIGN KEY (id_articulo)
REFERENCES Articulos(id_articulo)
)
GO


-------------INSERTS
INSERT INTO FormasPago (nombre)
VALUES 
    ('Efectivo'),
    ('Tarjeta de Crédito'),
    ('Transferencia Bancaria'),
    ('PayPal'),
    ('Cheque');

INSERT INTO Articulos (nombre, pre_unitario)
VALUES 
    ('Laptop', 1200),
    ('Mouse', 25),
    ('Teclado', 50),
    ('Monitor', 300),
    ('Impresora', 150);

INSERT INTO DetallesFactura (nro_factura, id_articulo, cantidad)
VALUES 
    (1, 1, 2),  -- Laptop, Juan Pérez
    (1, 2, 1),  -- Mouse, Juan Pérez
    (2, 3, 1),  -- Teclado, Ana Gómez
    (3, 4, 1),  -- Monitor, Carlos Rodríguez
    (4, 5, 1);  -- Impresora, Laura Fernández

INSERT INTO Facturas (fecha, id_forma_pago, cliente)
VALUES 
    ('2024-09-01', 1, 'Juan Pérez'),   -- id_forma_pago = 1
    ('2024-09-02', 2, 'Ana Gómez'),    -- id_forma_pago = 2
    ('2024-09-03', 3, 'Carlos Rodríguez'), -- id_forma_pago = 3
    ('2024-09-04', 4, 'Laura Fernández'), -- id_forma_pago = 4
    ('2024-09-05', 5, 'Pedro Martínez'); -- id_forma_pago = 5

	


-------------PROCEDIMIENTOS ALMACENADOS
CREATE PROCEDURE SP_GET_ALL_FACTURA
AS
BEGIN
SELECT *
FROM Facturas
END
GO


CREATE PROCEDURE SP_GET_ID_FACTURA
@nro_factura INT
AS
BEGIN
SELECT *
FROM Facturas
WHERE nro_factura = @nro_factura
END
GO


CREATE PROCEDURE SP_DEL_FACTURA
@nro_factura INT
AS
BEGIN
DELETE DetallesFactura
FROM DetallesFactura
WHERE nro_factura = @nro_factura

DELETE Facturas
FROM Facturas
WHERE nro_factura = @nro_factura
END
GO


--CREA UNA FACTURA HUECA
CREATE PROCEDURE SP_CREAR_FACTURA
@fecha datetime, @id_forma_pago int, @cliente varchar(70)
AS
BEGIN
INSERT INTO Facturas(fecha, id_forma_pago,cliente) VALUES(@fecha, @id_forma_pago, @cliente)
END
GO


-------------VERIFICAR INSERTS
	select * from Articulos
	select * from Facturas
	select * from FormasPago
	select * from DetallesFactura

