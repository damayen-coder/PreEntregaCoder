using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinalJoseArmando.Modulos;
using ProyectoFinalJoseArmando.Repository;

namespace ProyectoFinalJoseArmando.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoVendidoController : ControllerBase
    {
        //Traer Productos vendidos de cierto Usuario
        [HttpGet("{idVenta}")]
        public List<ProductoVendidoProducto> ProductoVendidoProducto(int idVenta)
        {
            return ProductoVendidoHandler.ProductoVendidoProducto(idVenta);
        }
    }
}
