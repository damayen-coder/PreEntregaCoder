using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinalJoseArmando.Controllers.NewFolder;
using ProyectoFinalJoseArmando.Modulos;
using ProyectoFinalJoseArmando.Repository;

namespace ProyectoFinalJoseArmando.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {


        //Cargar Ventas Actualizado
        [HttpPost("CargarVenta")]
        public void CargarVenta([FromBody] List<Producto> pv, int idUsuario)

        {
            VentaHandler.CargarVenta(pv, idUsuario);
        }

        //Traer Venta
        [HttpGet("TraerVentas")]
        public List<ProductoVendidoVenta> ProductoVendidoVenta()
        {
            return VentaHandler.ProductoVendidoVenta();
        }

        ////Cargar Venta VIEJITO
        //[HttpPost]
        //public bool CargarVenta([FromBody] PostVenta venta, int Stock, int IdProducto)
        //{
        //    try
        //    {
        //        return VentaHandler.CargarVenta(Stock, IdProducto, new Venta
        //        {

        //            Comentarios = venta.Comentarios,
        //            IdUsuario = venta.IdUsuario

        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return false;
        //    }
        //}
    }
}
