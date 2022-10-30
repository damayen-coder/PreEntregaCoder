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
        

        //Modificar Usuario
        public static bool ModificarUsuario(Usuarios usuario)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(SQL.ConnectionString()))
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

        //TraerUsuario
        public static Usuarios TraerUsuario(string nombreUsuario)
        {
            var usuario = new Usuarios();

            using (SqlConnection connection = new SqlConnection(SQL.ConnectionString()))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Usuario where NombreUsuario = @idusu";

                var parametro = new SqlParameter();
                parametro.ParameterName = "idusu";
                parametro.SqlDbType = SqlDbType.VarChar;
                parametro.Value = nombreUsuario;

                cmd.Parameters.Add(parametro);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    usuario.id = Convert.ToInt32(reader.GetValue(0));
                    usuario.Nombre = reader.GetValue(1).ToString();
                    usuario.Apellido = reader.GetValue(2).ToString();
                    usuario.NombreUsuario = reader.GetValue(3).ToString();
                    usuario.Contraseña = reader.GetValue(4).ToString();
                    usuario.Mail = reader.GetValue(5).ToString();

                }

                reader.Close();
                connection.Close();

                if (usuario.id == 0) usuario = null;

                return usuario;

            }
        }

        public static Usuarios InicioSesionUsuarios(string nombreUsuario, string contraseña)
        {
            Usuarios resultado = new Usuarios();

            using (SqlConnection sqlConnection = new SqlConnection(SQL.ConnectionString()))
            {

                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Usuario WHERE NombreUsuario = @nombreUsuarioParam AND Contraseña = @contraseñaParam", sqlConnection))
                {

                    sqlCommand.Parameters.AddWithValue("@nombreUsuarioParam", nombreUsuario);
                    sqlCommand.Parameters.AddWithValue("@contraseñaParam", contraseña);

                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Usuarios usuario = new Usuarios();

                                usuario.id = Convert.ToInt32(dataReader["Id"]);
                                usuario.NombreUsuario = dataReader["NombreUsuario"].ToString();
                                usuario.Nombre = dataReader["Nombre"].ToString();
                                usuario.Apellido = dataReader["Apellido"].ToString();
                                usuario.Contraseña = dataReader["Contraseña"].ToString();
                                usuario.Mail = dataReader["Mail"].ToString();

                                resultado = usuario;
                            }
                        }
                    }

                    sqlConnection.Close();
                }

                return resultado;
            }

        }
    }
}
