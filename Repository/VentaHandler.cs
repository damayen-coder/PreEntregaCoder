using ProyectoFinalJoseArmando.Modulos;
using System.Data;
using System.Data.SqlClient;


namespace ProyectoFinalJoseArmando.Repository
{
    public class VentaHandler
    {

        /*
         Cargar Venta: Recibe una lista de productos y el número de IdUsuario de quien la
         efectuó, primero cargar una nueva Venta en la base de datos, luego debe cargar los
         productos recibidos en la base de ProductosVendidos uno por uno por un lado, y
         descontar el stock en la base de productos por el otro.
         */

        //Cargar Venta Nuevo

        public static void CargarVenta(VentaProducto vtaProductos)
        {

            long idVenta;            
            using (SqlConnection conn = new SqlConnection(SQL.ConnectionString()))
            {
                //INSERT en tabla venta y obtener el id de la venta
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Venta (Comentarios, IdUsuario) VALUES (@Comentarios, @IdUsuario); Select scope_identity();", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("Comentarios", SqlDbType.NVarChar)).Value = vtaProductos.Comentarios;
                cmd.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.BigInt)).Value = vtaProductos.IdUsuario;
                idVenta = Convert.ToInt64(cmd.ExecuteScalar());

                //INSERT en tabla producto vendido con lista de productos enviados
                foreach (ProductoVendido producto in vtaProductos.Productos)
                {
                    //Agregar Venta
                    cmd = new SqlCommand("INSERT INTO ProductoVendido (Stock,IdProducto,IdVenta)  VALUES   (@Stock,@IdProducto,@IdVenta) ", conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("Stock", SqlDbType.Int)).Value = producto.Stock;
                    cmd.Parameters.Add(new SqlParameter("IdProducto", SqlDbType.BigInt)).Value = producto.IdProducto;
                    cmd.Parameters.Add(new SqlParameter("IdVenta", SqlDbType.BigInt)).Value = idVenta;
                    cmd.ExecuteNonQuery();
                    //Actualizar Stock en Productos
                    cmd = new SqlCommand("UPDATE Producto SET Stock = Stock - @Stock WHERE IdProducto = @IdProducto", conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("Stock", SqlDbType.Int)).Value = producto.Stock;
                    cmd.Parameters.Add(new SqlParameter("IdProducto", SqlDbType.BigInt)).Value = producto.IdProducto;
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }

        }




        //Traer Ventas
        public static List<ProductoVendidoVenta> ProductoVendidoVenta()
        {
            List<ProductoVendidoVenta> productosVendido = new List<ProductoVendidoVenta>();

            using (SqlConnection sqlConnection = new SqlConnection(SQL.ConnectionString()))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT Venta.Id,Venta.Comentarios,Venta.IdUsuario,ProductoVendido.Stock,ProductoVendido.IdProducto,ProductoVendido.IdVenta " +
                    "FROM ProductoVendido INNER JOIN Venta ON Venta.IdUsuario = ProductoVendido.IdVenta", sqlConnection))
                {

                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {

                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {

                                ProductoVendidoVenta productoVendido = new ProductoVendidoVenta();

                                productoVendido.VentaId = Convert.ToInt32(dataReader["Id"]);
                                productoVendido.Comentarios = dataReader["Comentarios"].ToString();
                                productoVendido.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"]);
                                productoVendido.Stock = Convert.ToInt32(dataReader["Stock"]);
                                productoVendido.IdProducto = Convert.ToInt32(dataReader["IdProducto"]);
                                productoVendido.IdVenta = Convert.ToInt32(dataReader["IdVenta"]);

                                productosVendido.Add(productoVendido);

                            }
                        }
                        else
                        {
                            ProductoVendidoVenta productoVendido = new ProductoVendidoVenta();

                            productoVendido.VentaId = 0;
                            productoVendido.Comentarios = "";
                            productoVendido.IdUsuario = 0;
                            productoVendido.Stock = 0;
                            productoVendido.IdProducto = 0;
                            productoVendido.IdVenta = 0;


                            productosVendido.Add(productoVendido);
                        }
                    }
                    sqlConnection.Close();
                }
            }
            return productosVendido;
        }


        //Traer Ventas de cierto Usuario
        public static List<ProductoVendidoVenta> ProductoVendidoVenta(int idVenta)
        {
            List<ProductoVendidoVenta> productosVendido = new List<ProductoVendidoVenta>();

            using (SqlConnection sqlConnection = new SqlConnection(SQL.ConnectionString()))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT Venta.Id,Venta.Comentarios,Venta.IdUsuario,ProductoVendido.Stock,ProductoVendido.IdProducto,ProductoVendido.IdVenta " +
                    "FROM ProductoVendido INNER JOIN Venta ON Venta.IdUsuario = ProductoVendido.IdVenta WHERE IdVenta = @idVenta", sqlConnection))
                {

                    sqlCommand.Parameters.AddWithValue("@idVenta", idVenta);

                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {

                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {

                                ProductoVendidoVenta productoVendido = new ProductoVendidoVenta();

                                productoVendido.VentaId = Convert.ToInt32(dataReader["Id"]);
                                productoVendido.Comentarios = dataReader["Comentarios"].ToString();
                                productoVendido.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"]);
                                productoVendido.Stock = Convert.ToInt32(dataReader["Stock"]);
                                productoVendido.IdProducto = Convert.ToInt32(dataReader["IdProducto"]);
                                productoVendido.IdVenta = Convert.ToInt32(dataReader["IdVenta"]);

                                productosVendido.Add(productoVendido);

                            }
                        }
                        else
                        {
                            ProductoVendidoVenta productoVendido = new ProductoVendidoVenta();

                            productoVendido.VentaId = 0;
                            productoVendido.Comentarios = "";
                            productoVendido.IdUsuario = 0;
                            productoVendido.Stock = 0;
                            productoVendido.IdProducto = 0;
                            productoVendido.IdVenta = idVenta;


                            productosVendido.Add(productoVendido);
                        }
                    }
                    sqlConnection.Close();
                }
            }
            return productosVendido;
        }





    }
}
