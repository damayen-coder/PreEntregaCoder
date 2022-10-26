using ProyectoFinalJoseArmando.Modulos;
using System.Data;
using System.Data.SqlClient;


namespace ProyectoFinalJoseArmando.Repository
{
    /*
     * Modificar usuario: Recibe como parámetro todos los datos del objeto usuario y se
     * deberá modificar el mismo con los datos nuevos (No crear uno nuevo)
     */

    public class UsuarioHandler
    {
        public const string ConnectionString = "Server=DESKTOP-VRSGEG4;DataBase=SistemaGestion;Trusted_Connection=True";

        //Modificar Usuario
        public static bool ModificarUsuario(Usuarios usuario)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "UPDATE Usuario " +
                    "SET Nombre = @nombre ,Apellido = @apellido ,NombreUsuario = @nombreUsuario ,Contraseña = @contraseña ,Mail = @mail WHERE Id = @id";

                SqlParameter idParameter = new SqlParameter("id", SqlDbType.BigInt) { Value = usuario.id };
                SqlParameter nombreParameter = new SqlParameter("nombre", SqlDbType.VarChar) { Value = usuario.Nombre };
                SqlParameter apellidoParameter = new SqlParameter("apellido", SqlDbType.VarChar) { Value = usuario.Apellido };
                SqlParameter nombreUsuarioParameter = new SqlParameter("nombreUsuario", SqlDbType.VarChar) { Value = usuario.NombreUsuario };
                SqlParameter contraseñaParameter = new SqlParameter("contraseña", SqlDbType.VarChar) { Value = usuario.Contraseña };
                SqlParameter mailParameter = new SqlParameter("mail", SqlDbType.VarChar) { Value = usuario.Mail };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(idParameter);
                    sqlCommand.Parameters.Add(nombreParameter);
                    sqlCommand.Parameters.Add(apellidoParameter);
                    sqlCommand.Parameters.Add(nombreUsuarioParameter);
                    sqlCommand.Parameters.Add(contraseñaParameter);
                    sqlCommand.Parameters.Add(mailParameter);

                    int numberOfRows = sqlCommand.ExecuteNonQuery();

                    if (numberOfRows > 0)
                    {
                        resultado = true;
                    }
                }

                sqlConnection.Close();
            }

            return resultado;
        }
    }
}
