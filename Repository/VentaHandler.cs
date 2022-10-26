using ProyectoFinalJoseArmando.Modulos;
using System.Data;
using System.Data.SqlClient;
namespace ProyectoFinalJoseArmando.Repository
{
    public class VentaHandler
    {
        public const string ConnectionString = "Server=DESKTOP-VRSGEG4;DataBase=SistemaGestion;Trusted_Connection=True";


        /*
         Cargar Venta: Recibe una lista de productos y el número de IdUsuario de quien la
         efectuó, primero cargar una nueva Venta en la base de datos, luego debe cargar los
         productos recibidos en la base de ProductosVendidos uno por uno por un lado, y
         descontar el stock en la base de productos por el otro.
         */

        //Cargar Venta

        public static bool CargarVenta(int Stock, int IdProducto, Venta venta)
        {
            bool resulta2 = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "IF EXISTS(SELECT * FROM Producto WHERE Descripciones = @comentariosParameter) BEGIN INSERT INTO Venta (Comentarios,IdUsuario) VALUES (@comentariosParameter,@idUsuarioParameter) END;";

                SqlParameter comentariosParameter = new SqlParameter("comentariosParameter", SqlDbType.VarChar) { Value = venta.Comentarios };
                SqlParameter idUsuarioParameter = new SqlParameter("idUsuarioParameter", SqlDbType.BigInt) { Value = venta.IdUsuario };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(comentariosParameter);
                    sqlCommand.Parameters.Add(idUsuarioParameter);

                    int numberOfRows = sqlCommand.ExecuteNonQuery();

                    if (numberOfRows > 0)
                    {
                        resulta2 = true;
                    }

                }
                sqlConnection.Close();

            }

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert2 = "IF EXISTS(SELECT Descripciones FROM Producto WHERE Descripciones = @comentariosParam) BEGIN INSERT INTO ProductoVendido(Stock,IdProducto,IdVenta) VALUES (@stockParam,@idProductoParam,@idUsuarioParam) END;";

                SqlParameter comentariosParameter = new SqlParameter("comentariosParam", SqlDbType.VarChar) { Value = venta.Comentarios };
                SqlParameter stockParameter = new SqlParameter("stockParam", SqlDbType.BigInt) { Value = Stock };
                SqlParameter idProductoParameter = new SqlParameter("idProductoParam", SqlDbType.BigInt) { Value = IdProducto };
                SqlParameter idUsuarioParameter = new SqlParameter("idUsuarioParam", SqlDbType.BigInt) { Value = venta.IdUsuario };


                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert2, sqlConnection))
                {
                    sqlCommand.Parameters.Add(comentariosParameter);
                    sqlCommand.Parameters.Add(stockParameter);
                    sqlCommand.Parameters.Add(idProductoParameter);
                    sqlCommand.Parameters.Add(idUsuarioParameter);


                    int numberOfRows = sqlCommand.ExecuteNonQuery();

                }

                sqlConnection.Close();
            }

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {

                string queryUpdate = "UPDATE Producto SET Stock = Stock - @stockParam WHERE Descripciones = @descripcionParam;";

                SqlParameter stockParameter = new SqlParameter("stockParam", SqlDbType.BigInt) { Value = Stock };
                SqlParameter descripcionParameter = new SqlParameter("descripcionParam", SqlDbType.VarChar) { Value = venta.Comentarios };


                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryUpdate, sqlConnection))
                {
                    sqlCommand.Parameters.Add(descripcionParameter);
                    sqlCommand.Parameters.Add(stockParameter);

                    int numberOfRows = sqlCommand.ExecuteNonQuery();

                }

                sqlConnection.Close();
            }

            return resulta2;
        }
    }
}
