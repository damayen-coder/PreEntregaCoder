# PreEntregaCoder
 
2º Pre Entrega Proyecto Final C# BackEnd Para Coder House.

Funciones:

* Modificar usuario: Recibe como parámetro todos los datos del objeto usuario y se deberá modificar el mismo con los datos nuevos (No crear uno nuevo).-

Crear producto: Recibe un producto como parámetro, deberá crearlo, puede ser void, pero validar los datos obligatorios.-

* Modificar producto: Recibe un producto como parámetro, debe modificarlo con la nueva información.-

* Eliminar producto: Recibe un id de producto a eliminar y debe eliminarlo de la base de datos (eliminar antes sus productos vendidos también, sino no lo podrá hacer).-

* Cargar Venta: Recibe una lista de productos y el número de IdUsuario de quien la efectuó, primero cargar una nueva Venta en la base de datos, luego debe cargar los productos recibidos en la base de ProductosVendidos uno por uno por un lado, y descontar el stock en la base de productos por el otro.-

Correcciones a pedido de Tutor:

* Agregado conexion a la base de datos desde una sola Clase llamada SQL dentro de la carpeta Repository.-

* Intentando Hacer mi primer ReadMe.-

* Modificacion de Cargar Venta: Permite cargar varios Prudctos a la vez, antes solo permitia uno a la vez.-

* Se Agrego Traer usuario, Traer producto, Traer Productos Vendidos, Traer Ventas, e Inicio de sesión de la anterior Pre Entrega del Proyecto.-


Funciones nuevas agregadas de la anterior Pre-Entrega: 

Traer Usuario:  Recibe como parámetro un nombre del usuario, buscarlo en la base de datos y devolver el objeto con todos sus datos (Esto se hará para la página en la que se mostrara los datos del usuario y en la página para modificar sus datos).

Traer Producto: Recibe un número de IdUsuario como parámetro, debe traer todos los productos cargados en la base de este usuario en particular.

Traer Productos Vendidos: Traer Todos los productos vendidos de un Usuario, cuya información está en su producto (Utilizar dentro de esta función el "Traer Productos" anteriormente hecho para saber que productosVendidos ir a buscar).

Traer Ventas: Recibe como parámetro un IdUsuario, debe traer todas las ventas de la base asignados al usuario particular.

Inicio de sesión: Se le pase como parámetro el nombre del usuario y la contraseña, buscar en la base de datos si el usuario existe y si coincide con la contraseña lo devuelve (el objeto Usuario), caso contrario devuelve uno vacío (Con sus datos vacíos y el id en 0).
