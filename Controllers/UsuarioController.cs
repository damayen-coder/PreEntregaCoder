using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinalJoseArmando.Controllers.NewFolder; //
using ProyectoFinalJoseArmando.Modulos;
using ProyectoFinalJoseArmando.Repository;

namespace ProyectoFinalJoseArmando.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        //MODIFICAR UN USUARIO
        [HttpPut]
        public bool ModificarUsuario([FromBody] PutUsuario usuario)
        {
            return UsuarioHandler.ModificarUsuario(new Usuarios
            {
                id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                NombreUsuario = usuario.NombreUsuario,
                Contraseña = usuario.Contraseña,
                Mail = usuario.Mail
            });
        }


        //TRAER UN USUARIO
        [HttpGet("TraerUsuario")]

        public Usuarios TraerUsuario(string nombreUsuario)
        {
            return UsuarioHandler.TraerUsuario(nombreUsuario);
        }



        //Inicio de sesion
        [HttpGet("{nombreUsuario}/{contraseña}")]
        public bool InicioSesionUsuarios(string nombreUsuario, string contraseña)
        {
            Usuarios usuario = UsuarioHandler.InicioSesionUsuarios(nombreUsuario, contraseña);
            if (usuario.Nombre == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
