# CarritoComprasADA
Prueba tecnica para puesto .net en ADA

El desarrollo se realizó bajo el modelo MVC, c#, .NET frameWork 4.8, Entity FrameWork Visual Studio 2019.

1. para probar localmente el primer punto se debe:

- Clonar el repositorio de github:
https://github.com/canojuancamilo/CarritoComprasADA

- Configura la cadena de conexión: esta se encuentra en el archivo Web.config el name es 'CadenaConexion' para que apunte al servidor
local de su preferencia

- Ejecuta los comandos de migraciones: desde la consola de Administrador de Paquetes ejecuta el Add-Migration y Update-Database

- Ejecutar script SQL: El script tiene el nombre de 'SQLQueryADA' el cual está en el proyecto en una carpeta llamada ScriptSQL

Con el script anterior registramos rol, categorías, 4 productos y un administrado con clave encriptada.

Las credenciales del administrador son:
Usuario: administrador
Contraseña: 123

Para el login utilicé BCrypt para encriptar la contraseña, también se guardan las credenciales del usuario en caché y se utilizan filtros personalizados para evitar el ingreso a lugares que no pertenecen de su rol, se utiliza Enums para el rol por lo cual se recomienda mantener los identificadores en BD de los roles para el correcto funcionamiento, para el resto del desarrollo  en aplicación se utilizó razor, JavaScript, JQuery, boostrap 3.4.

Para la BD se utilizó el entity para crear las tablas y se trabajó con procedimientos almacenados.

2. para probar este punto y facilitar un poco las pruebas les comparto el CURL de las Apis creadas  

Obtener Productos
curl --location 'https://localhost:44388/api/Productos?usuario=Administrador&null=null&contrasena=123'

Craer Producto
curl --location 'https://localhost:44388/api/Productos' \
--header 'Content-Type: application/json' \
--data '{
    "id_categoria": 1,
    "nombre": "Producto1",
    "descripcion": "Descripción del producto 1",
    "cantidad_disponible": 10,
    "url":"https://i.blogs.es/e8f8ff/kimetsu-no-yaiba/1366_2000.jpg",
    "usuario": "administrador",
    "contrasena": "123"
}'

Actulizar producto
curl --location --request PUT 'https://localhost:44388/api/Productos?idProducto=9' \
--header 'Content-Type: application/json' \
--data '{
    "id_categoria": 1,
    "nombre": "Producto1",
    "descripcion": "Descripción del producto 1",
    "cantidad_disponible": 10,
    "url":"https://i.blogs.es/e8f8ff/kimetsu-no-yaiba/1366_2000.jpg",
    "usuario": "administrador",
    "contrasena": "123"
}'

Actualizar UsuariosClientes
curl --location 'https://localhost:44388/api/UsuarioCompras?usuario=Administrador&contrasena=123'
