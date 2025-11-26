CREATE DATABASE SimulacroDB
GO

USE SimulacroDB
GO

CREATE TABLE Producto(
	IdProducto INT IDENTITY(1, 1)
	NombreProducto VARCHAR(50) NOT NULL,
	Precio DECIMAL NOT NULL,
	Stock INT NOT NULL,
	Descripcion VARCHAR(50) NOT NULL
)

INSERT INTO Producto (NombreProducto, Precio, Stock, Descripcion)
VALUES
('Laptop Lenovo Ideapad', 2499.90, 15, 'Laptop de uso general'),
('Mouse Inalámbrico Logitech', 59.90, 120, 'Mouse inalámbrico USB'),
('Teclado Mecánico Redragon', 189.50, 40, 'Teclado mecánico RGB'),
('Monitor Samsung 24"', 699.00, 25, 'Monitor LED 24 pulgadas'),
('Audífonos Sony WH-1000XM4', 1299.00, 10, 'Audífonos con cancelación'),
('Silla Gamer Cougar', 899.90, 8, 'Silla ergonómica gamer'),
('Memoria USB Kingston 64GB', 35.00, 200, 'Memoria USB 3.0'),
('Disco SSD Samsung 1TB', 499.00, 30, 'SSD NVMe 1TB'),
('Impresora HP DeskJet 2700', 349.00, 12, 'Impresora multifuncional'),
('Parlante JBL Flip 6', 399.90, 50, 'Parlante portátil bluetooth');

SELECT * FROM Producto;
GO

CREATE OR ALTER PROC spU_productos
AS
	SELECT
		IdProducto,
		NombreProducto,
		Precio,
		Stock,
		Descripcion  
	FROM
		Producto
GO

CREATE OR ALTER PROC spU_Producto_id
@id INT
AS
BEGIN
	SELECT
		IdProducto,
		NomProducto,
		PrecioUnidad,
		UnidadesEnExistencia
	FROM
		Productos
	WHERE
		IdProducto = @id
END
GO

EXEC spU_Producto_id 1
GO

CREATE OR ALTER PROC spU_productos_id
@id INT
AS
BEGIN
	SELECT
		p.IdProducto,
		p.NombreProducto,
		p.Precio,
		p.Stock,
		p.Descripcion
	FROM
		Producto p
	WHERE
		IdProducto = @id
END
GO

EXEC spU_productos_id 1
GO