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

        public static void CargarVenta(List<Producto> pv, int idUsuario)
        {
            using (SqlConnection connection = new SqlConnection(SQL.ConnectionString()))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT into Venta (Comentarios, IdUsuario) values ('', @idUsuario); select @@identity";

                var paramIdUsuario = new SqlParameter();
                paramIdUsuario.ParameterName = "idUsuario";
                paramIdUsuario.SqlDbType = SqlDbType.BigInt;
                paramIdUsuario.Value = idUsuario;

                cmd.Parameters.Add(paramIdUsuario);
                int ventaId = Convert.ToInt32(cmd.ExecuteScalar());

                List<string> productoSinStock = new List<string>();
                string prodDesc = string.Empty;
                List<string> prodDescList = new List<string>();


                foreach (var Producto in pv)
                {

                    cmd.Parameters.Add(new SqlParameter("Stock", Producto.Stock));
                    cmd.Parameters.Add(new SqlParameter("IdProd", Producto.Id));
                    cmd.Parameters.Add(new SqlParameter("IdVent", ventaId));

                    cmd.CommandText = "SELECT Stock From Producto where Id = @IdProd ";
                    int stockExistente = Convert.ToInt32(cmd.ExecuteScalar());


                    if (stockExistente >= Producto.Stock)

                    {
                        cmd.CommandText = "INSERT into ProductoVendido (Stock, IdProducto, IdVenta) " +
                          "values (@Stock, @IdProd, @IdVent)";


                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "UPDATE Producto set Stock = Stock - @Stock where Id = @IdProd";

                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "SELECT Descripciones From Producto where Id = @IdProd";
                        prodDesc = Convert.ToString(cmd.ExecuteScalar());
                        prodDescList.Add(prodDesc.ToString());
                        productoSinStock.Add(Producto.Id.ToString());

                        cmd.CommandText = "UPDATE Venta set Comentarios = @Comentarios where Id = @IdVent";

                        string comentario = "Productos vendidos: " + string.Join(", ", prodDescList);
                        cmd.Parameters.Add(new SqlParameter("Comentarios", comentario));
                        cmd.ExecuteNonQuery();
                    }

                    cmd.Parameters.Clear();
                }

                connection.Close();
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








        // viejito

        //public static bool CargarVenta(int Stock, int IdProducto, Venta venta)
        //{
        //    bool resulta2 = false;
        //    using (SqlConnection sqlConnection = new SqlConnection(SQL.ConnectionString))
        //    {
        //        string queryInsert = "IF EXISTS(SELECT * FROM Producto WHERE Descripciones = @comentariosParameter) BEGIN INSERT INTO Venta (Comentarios,IdUsuario) VALUES (@comentariosParameter,@idUsuarioParameter) END;";

        //        SqlParameter comentariosParameter = new SqlParameter("comentariosParameter", SqlDbType.VarChar) { Value = venta.Comentarios };
        //        SqlParameter idUsuarioParameter = new SqlParameter("idUsuarioParameter", SqlDbType.BigInt) { Value = venta.IdUsuario };

        //        sqlConnection.Open();

        //        using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
        //        {
        //            sqlCommand.Parameters.Add(comentariosParameter);
        //            sqlCommand.Parameters.Add(idUsuarioParameter);

        //            int numberOfRows = sqlCommand.ExecuteNonQuery();

        //            if (numberOfRows > 0)
        //            {
        //                resulta2 = true;
        //            }

        //        }
        //        sqlConnection.Close();

        //    }

        //    using (SqlConnection sqlConnection = new SqlConnection(SQL.ConnectionString))
        //    {
        //        string queryInsert2 = "IF EXISTS(SELECT Descripciones FROM Producto WHERE Descripciones = @comentariosParam) BEGIN INSERT INTO ProductoVendido(Stock,IdProducto,IdVenta) VALUES (@stockParam,@idProductoParam,@idUsuarioParam) END;";

        //        SqlParameter comentariosParameter = new SqlParameter("comentariosParam", SqlDbType.VarChar) { Value = venta.Comentarios };
        //        SqlParameter stockParameter = new SqlParameter("stockParam", SqlDbType.BigInt) { Value = Stock };
        //        SqlParameter idProductoParameter = new SqlParameter("idProductoParam", SqlDbType.BigInt) { Value = IdProducto };
        //        SqlParameter idUsuarioParameter = new SqlParameter("idUsuarioParam", SqlDbType.BigInt) { Value = venta.IdUsuario };


        //        sqlConnection.Open();

        //        using (SqlCommand sqlCommand = new SqlCommand(queryInsert2, sqlConnection))
        //        {
        //            sqlCommand.Parameters.Add(comentariosParameter);
        //            sqlCommand.Parameters.Add(stockParameter);
        //            sqlCommand.Parameters.Add(idProductoParameter);
        //            sqlCommand.Parameters.Add(idUsuarioParameter);


        //            int numberOfRows = sqlCommand.ExecuteNonQuery();

        //        }

        //        sqlConnection.Close();
        //    }

        //    using (SqlConnection sqlConnection = new SqlConnection(SQL.ConnectionString))
        //    {

        //        string queryUpdate = "UPDATE Producto SET Stock = Stock - @stockParam WHERE Descripciones = @descripcionParam;";

        //        SqlParameter stockParameter = new SqlParameter("stockParam", SqlDbType.BigInt) { Value = Stock };
        //        SqlParameter descripcionParameter = new SqlParameter("descripcionParam", SqlDbType.VarChar) { Value = venta.Comentarios };


        //        sqlConnection.Open();

        //        using (SqlCommand sqlCommand = new SqlCommand(queryUpdate, sqlConnection))
        //        {
        //            sqlCommand.Parameters.Add(descripcionParameter);
        //            sqlCommand.Parameters.Add(stockParameter);

        //            int numberOfRows = sqlCommand.ExecuteNonQuery();

        //        }

        //        sqlConnection.Close();
        //    }

        //    return resulta2;
        //}
    }
}
