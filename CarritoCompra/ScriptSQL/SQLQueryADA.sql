SET IDENTITY_INSERT Perfil ON
INSERT INTO Perfil (id_perfil, rol)
VALUES(1, 'Administrador'),
(2, 'Cliente')
SET IDENTITY_INSERT Perfil OFF

GO

INSERT INTO CategoriaProducto (id_categoria, nombre)
VALUES('Alimentos'),
('Hogar'),
('Tecnología')

GO

INSERT INTO Usuario(nombre, identificacion, direccion, telefono, usuario, contrasena, id_perfil)
VALUES('Administrador', '123456', 'carrera 87', '2581781', 'administrador', '$2a$10$nCrkRY8.hW7Yh0jaZpt5IutaljjcyRDcc96wtiPuUXoOUVRiRuegm', 1)

GO

INSERT INTO Producto (id_categoria, nombre, Descripcion, Cantidad_disponible, url)
VALUES
(1, 'Naranja', 'La naranja es una fruta cítrica obtenida del naranjo dulce (Citrus × sinensis), del naranjo amargo (Citrus × aurantium) y de naranjos de otras variedades', 25, 'https://media.istockphoto.com/id/185284489/es/foto/naranja.jpg?s=612x612&w=0&k=20&c=V_kmzGGofV9oTdQMU-SfT4Y9n3q9ksFZliED5O_eYPE='),
(2, 'Mesa', 'La mesa (del latín mēnsa) es un mueble compuesto de un tablero horizontal liso y sostenido a la altura conveniente, generalmente por una o varias patas, para diferentes usos.', 7, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ7VRCThRAEf0HR8zX8mgjg1jBfqWx1GBrFwQ&usqp=CAU'),
(3, 'PlayStation 5', 'PlayStation 5 (プレイステーション 5 Pureisutēshon Faibu, abreviada como PS5) es la quinta consola de videojuegos de sobremesa desarrollada por la empresa Sony.', 11, 'https://i.blogs.es/885597/ps5apfinal/1366_2000.jpg'),
(1, 'hamburguesa', 'Sándwich de hamburguesa con un pan blando y redondo al que se le suelen añadir otros ingredientes o condimentos, como tomate, cebolla, queso, ketchup, mostaza, etc.', 23, 'https://vivaburger.es/wp-content/uploads/2017/08/hamburguesa.jpg')

GO

CREATE PROCEDURE SP_Registrar_Usuario
(
	@nombre VARCHAR(150) ,
	@direccion VARCHAR(100),
	@telefono VARCHAR(20),
	@identificacion VARCHAR(20),
	@usuario VARCHAR(20),
	@contrasena VARCHAR(MAX),
	@id_perfil INT
)
AS
BEGIN
	INSERT INTO Usuario(nombre, direccion, telefono, identificacion, usuario, contrasena, id_perfil)
	VALUES (@nombre, @direccion, @telefono, @identificacion, @usuario, @contrasena, @id_perfil)

	SELECT CAST(SCOPE_IDENTITY() AS INT) AS id_usuario
END

GO

CREATE PROCEDURE SP_Obtener_cliente_identificacion_usuario
(
	@identificacion VARCHAR(20),
	@usuario VARCHAR(20)
)
AS
BEGIN

	SELECT id_usuario, nombre, direccion, telefono, identificacion, usuario, contrasena, id_perfil FROM Usuario
	WHERE
	identificacion = @identificacion
	OR
	usuario = @usuario
END

GO

CREATE PROCEDURE SP_Validar_Login
(
	@usuario VARCHAR(20)
)
AS
BEGIN

	SELECT id_usuario, nombre, direccion, telefono, identificacion, usuario, contrasena, id_perfil FROM Usuario
	WHERE
	usuario = @usuario
END

GO

CREATE PROCEDURE SP_Retornar_Productos
AS
BEGIN
	SELECT P.id_producto, P.id_categoria, P.nombre, P.Descripcion, P.Cantidad_disponible, P.url, C.nombre AS nombre_categoria
	FROM Producto P
	JOIN CategoriaProducto C ON C.id_categoria = p.id_categoria
	WHERE P.Cantidad_disponible > 0
END

GO

CREATE PROCEDURE SP_Registrar_Transaccion
(
	@id_usuario INT,
	@fecha_transaccion DATETIME
)
AS
BEGIN
	INSERT INTO Transaccion(id_usuario, fecha_transaccion)
	VALUES (@id_usuario, @fecha_transaccion)
	SELECT CAST(SCOPE_IDENTITY() AS INT) AS id_transaccion
END

GO

ALTER PROCEDURE SP_Registrar_Pedido
(
	@id_producto INT,
	@cantidad INT,
	@fecha_pedido DATETIME,
	@id_transaccion INT
)
AS
BEGIN
	UPDATE Producto
	SET Cantidad_disponible = Cantidad_disponible - @cantidad
	WHERE id_producto = @id_producto

	INSERT INTO Pedido(id_producto, cantidad, fecha_pedido, id_transaccion)
	VALUES (@id_producto, @cantidad, @fecha_pedido, @id_transaccion)

	SELECT CAST(SCOPE_IDENTITY() AS INT)	
END

GO

CREATE PROCEDURE SP_Validar_cantidad_producto 
(
	@id_producto INT
)
AS
BEGIN
DECLARE @cantidad_disponible INT
    DECLARE @resultado INT

    SELECT @cantidad_disponible = Cantidad_disponible
    FROM Producto
    WHERE id_producto = @id_producto

	SELECT @cantidad_disponible
END

GO

CREATE PROCEDURE SP_Retornar_Transacciones
AS
BEGIN
	SELECT t.id_transaccion, t.id_usuario, t.fecha_transaccion, u.nombre, u.identificacion
	FROM Transaccion t
	JOIN Usuario u ON t.id_usuario = u.id_usuario
END

GO

CREATE PROCEDURE SP_Retornar_Pedidos
(
	@id_transaccion INT
)
AS
BEGIN
	SELECT p.id_pedido, p.id_producto, p.cantidad, p.fecha_pedido, p.id_transaccion, r.nombre
	FROM Pedido p
	JOIN Producto r ON p.id_producto = r.id_producto
	WHERE p.id_transaccion =@id_transaccion
END

GO

CREATE PROCEDURE SP_Registrar_Producto
(
	@id_categoria INT,
	@nombre VARCHAR(150),
	@Descripcion VARCHAR(200),
	@Cantidad_disponible INT,
	@url VARCHAR(MAX)
)
AS
BEGIN
	INSERT INTO Producto(id_categoria, nombre, Descripcion, Cantidad_disponible, url)
	VALUES (@id_categoria, @nombre, @Descripcion, @Cantidad_disponible, @url)

	SELECT CAST(SCOPE_IDENTITY() AS INT)	
END

GO

CREATE PROCEDURE SP_Actualizar_Producto
(
	@id_producto INT,
	@id_categoria INT,
	@nombre VARCHAR(150),
	@Descripcion VARCHAR(200),
	@Cantidad_disponible INT,
	@url VARCHAR(MAX)
)
AS
BEGIN
	UPDATE Producto SET  id_categoria = @id_categoria, nombre = @nombre,
	Descripcion = @Descripcion, Cantidad_disponible =@Cantidad_disponible, url = @url
	WHERE id_producto = @id_producto
END

GO

CREATE PROCEDURE SP_Retornar_Clientes
AS
BEGIN
	SELECT p.id_usuario, p.nombre, p.identificacion, p.direccion, p.telefono, r.rol
	FROM Usuario p
	JOIN Perfil r ON p.id_perfil = r.id_perfil
	WHERE p.id_perfil = 2
END