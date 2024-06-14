using GestorEventos.Servicios.Entidades;
using GestorEventos.Servicios.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace GestorEventos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiciosController : ControllerBase
    {

        private IServiciosService serviciosService;

        // constructor
        public ServiciosController(IServiciosService _servicioService)
        {
            serviciosService = _servicioService;
        }

        // devuelve todos los servicios
        [HttpGet]
        public IActionResult GetServicios()
        {
            

            return Ok(serviciosService.GetServicios());
        }

        // devuelve un servicio segun su id
        [HttpGet("{idServicio:int}")]
        public IActionResult GetServicioPorId(int idServicio)
        {         
            var servicio = serviciosService.GetServiciosPorId(idServicio);

            if (servicio == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(servicio);
            }
        }

        // sube un nuevo servicio
        [HttpPost("nuevo")]
        public IActionResult PostNuevoServicio([FromBody] ServiciosVM servicioNuevo)
        {
            
            serviciosService.AgregarServicio(servicioNuevo);

            return Ok();   
        }

    }
}
