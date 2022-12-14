using ProyectoFinalJoseArmando.Modulos;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoFinalJoseArmando.Repository
{
    public class ProductoHandler
    {
        

        /*
         Crear producto: Recibe un producto como parámetro, deberá crearlo, puede ser
         void, pero validar los datos obligatorios.
         */

        //Crear Un Producto

        public static bool CrearProducto(Producto producto)
        {
            bool resulta2 = false;
            using (SqlConnection sqlConnection = new SqlConnection(SQL.ConnectionString()))
            {
                string queryInsert = "IF NOT EXISTS(SELECT * FROM Producto WHERE Descripciones = @descripcionesParameter) BEGIN INSERT INTO Producto " +
                    "(Descripciones,Costo,PrecioVenta,Stock,IdUsuario) VALUES " +
                    "(@descripcionesParameter, @costoParameter, @precioVentaParameter, @stockParameter, @idUsuarioParameter) END;";

                SqlParameter descripcionesParameter = new SqlParameter("descripcionesParameter", SqlDbType.VarChar) { Value = producto.Descripcion };
                SqlParameter costoParameter = new SqlParameter("costoParameter", SqlDbType.BigInt) { Value = producto.Costo };
                SqlParameter precioVentaParameter = new SqlParameter("precioVentaParameter", SqlDbType.BigInt) { Value = producto.PrecioVenta };
                SqlParameter stockParameter = new SqlParameter("stockParameter", SqlDbType.BigInt) { Value = producto.Stock };
                SqlParameter idUsuarioParameter = new SqlParameter("idUsuarioParameter", SqlDbType.BigInt) { Value = producto.IdUsuario };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(descripcionesParameter);
                    sqlCommand.Parameters.Add(costoParameter);
                    sqlCommand.Parameters.Add(precioVentaParameter);
                    sqlCommand.Parameters.Add(stockParameter);
                    sqlCommand.Parameters.Add(idUsuarioParameter);

                    int numberOfRows = sqlCommand.ExecuteNonQuery();

                    if (numberOfRows > 0)
                    {
                        resulta2 = true;
                    }

                }

                sqlConnection.Close();

            }
            return resulta2;
        }

        /*
         Modificar producto: Recibe un producto como parámetro, debe modificarlo con la
         nueva información.
         */


        //Modificar Producto
        public static bool ModificarProducto(Producto producto)
        {
            bool resulta2 = false;
            using (SqlConnection sqlConnection = new SqlConnection(SQL.ConnectionString()))
            {
                string queryInsert = "UPDATE Producto " +
                    "SET Descripciones = @descripciones, Costo = @costo, PrecioVenta = @precioVenta, Stock = @stock, IdUsuario = @idUsuario WHERE Id = @id ";

                SqlParameter idParameter = new SqlParameter("id", SqlDbType.BigInt) { Value = producto.Id };
                SqlParameter descripcionesParameter = new SqlParameter("descripciones", SqlDbType.VarChar) { Value = producto.Descripcion };
                SqlParameter costoParameter = new SqlParameter("costo", SqlDbType.BigInt) { Value = producto.Costo };
                SqlParameter precioVentaParameter = new SqlParameter("precioVenta", SqlDbType.BigInt) { Value = producto.PrecioVenta };
                SqlParameter stockParameter = new SqlParameter("stock", SqlDbType.BigInt) { Value = producto.Stock };
                SqlParameter idUsuarioParameter = new SqlParameter("idUsuario", SqlDbType.BigInt) { Value = producto.IdUsuario };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {

                    sqlCommand.Parameters.Add(idParameter);
                    sqlCommand.Parameters.Add(descripcionesParameter);
                    sqlCommand.Parameters.Add(costoParameter);
                    sqlCommand.Parameters.Add(precioVentaParameter);
                    sqlCommand.Parameters.Add(stockParameter);
                    sqlCommand.Parameters.Add(idUsuarioParameter);

                    int numberOfRows = sqlCommand.ExecuteNonQuery();

                    if (numberOfRows > 0)
                    {
                        resulta2 = true;
                    }
                }

                sqlConnection.Close();
            }

            return resulta2;
        }


        /*
         Eliminar producto: Recibe un id de producto a eliminar y debe eliminarlo de la base
         de datos (eliminar antes sus productos vendidos también, sino no lo podrá hacer)
         
         */


        //Eliminar Producto
        public static bool EliminarProducto(int idProducto)
        {
            bool resulta2 = false;
            using (SqlConnection sqlConnection = new SqlConnection(SQL.ConnectionString()))
            {
                string queryDelete2 = "DELETE FROM Producto WHERE Id = @idParam";

                SqlParameter idParameter = new SqlParameter("idParam", SqlDbType.BigInt) { Value = idProducto };


                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryDelete2, sqlConnection))
                {
                    sqlCommand.Parameters.Add(idParameter);
                    int numberOfRows = sqlCommand.ExecuteNonQuery();
                    if (numberOfRows > 0)
                    {
                        resulta2 = true;
                    }
                }

                sqlConnection.Close();
            }

            return resulta2;
        }


       
        
        //TRAER UN PRODUCTO
        public static List<Producto> GetProductos()
        {
            List<Producto> resultados = new List<Producto>();

            using (SqlConnection sqlConnection = new SqlConnection(SQL.ConnectionString()))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Producto", sqlConnection))
                {

                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Producto producto = new Producto();
                                producto.Id = Convert.ToInt32(dataReader["Id"]);
                                producto.Stock = Convert.ToInt32(dataReader["Stock"]);
                                producto.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"]);
                                producto.Costo = Convert.ToInt32(dataReader["Costo"]);
                                producto.PrecioVenta = Convert.ToInt32(dataReader["PrecioVenta"]);
                                producto.Descripcion = dataReader["Descripciones"].ToString();

                                resultados.Add(producto);
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }

            return resultados;
        }


        //Traer Productos cargados por cierto usuario
        public static List<Producto> TraerProductos(int idUsuario)
        {
            List<Producto> resultados = new List<Producto>();

            using (SqlConnection sqlConnection = new SqlConnection(SQL.ConnectionString()))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Producto WHERE IdUsuario = @idUsuario", sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@idUsuario", idUsuario);

                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Producto producto = new Producto();
                                producto.Id = Convert.ToInt32(dataReader["Id"]);
                                producto.Stock = Convert.ToInt32(dataReader["Stock"]);
                                producto.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"]);
                                producto.Costo = Convert.ToInt32(dataReader["Costo"]);
                                producto.PrecioVenta = Convert.ToInt32(dataReader["PrecioVenta"]);
                                producto.Descripcion = dataReader["Descripciones"].ToString();

                                resultados.Add(producto);
                            }
                        }
                        else
                        {
                            Producto producto = new Producto();
                            producto.Id = 0;
                            producto.Descripcion = "No cargo ningun Producto, el Usuario: " + idUsuario;
                            producto.Costo = 0;
                            producto.PrecioVenta = 0;
                            producto.Stock = 0;
                            producto.IdUsuario = idUsuario;

                            resultados.Add(producto);

                        }
                    }
                    sqlConnection.Close();
                }
            }

            return resultados;
        }

    }

}
