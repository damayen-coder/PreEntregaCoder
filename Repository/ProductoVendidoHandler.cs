using System.Data;
using System.Data.SqlClient;
using ProyectoFinalJoseArmando.Modulos;

namespace ProyectoFinalJoseArmando.Repository
{
    public class ProductoVendidoHandler
    {


        //Traer Productos Vendidos de un usuario
        public static List<ProductoVendidoProducto> ProductoVendidoProducto(int idVenta)
        {
            List<ProductoVendidoProducto> productosVendido = new List<ProductoVendidoProducto>();

            using (SqlConnection sqlConnection = new SqlConnection(SQL.ConnectionString()))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT ProductoVendido.Id,ProductoVendido.Stock,ProductoVendido.IdProducto,ProductoVendido.IdVenta,Producto.Descripciones,Producto.IdUsuario FROM Producto INNER JOIN ProductoVendido ON Producto.Id = ProductoVendido.IdProducto WHERE IdVenta = @idVenta", sqlConnection))
                {

                    sqlCommand.Parameters.AddWithValue("@idVenta", idVenta);

                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {

                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {

                                ProductoVendidoProducto productoVendido = new ProductoVendidoProducto();

                                productoVendido.ProductoVendidoId = Convert.ToInt32(dataReader["Id"]);
                                productoVendido.Stock = Convert.ToInt32(dataReader["Stock"]);
                                productoVendido.IdProducto = Convert.ToInt32(dataReader["IdProducto"]);
                                productoVendido.IdVenta = Convert.ToInt32(dataReader["IdVenta"]);
                                productoVendido.Descripciones = dataReader["Descripciones"].ToString();
                                productoVendido.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"]);

                                productosVendido.Add(productoVendido);

                            }
                        }
                        else
                        {
                            ProductoVendidoProducto productoVendido = new ProductoVendidoProducto();

                            productoVendido.ProductoVendidoId = 0;
                            productoVendido.Stock = 0;
                            productoVendido.IdProducto = 0;
                            productoVendido.IdVenta = idVenta;
                            productoVendido.Descripciones = "";
                            productoVendido.IdUsuario = 0;


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
