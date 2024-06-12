using GestorEventos.Servicios.Entidades;
using GestorEventos.Servicios.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestorEventos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetEventos()
        {
            EventosService eventosService = new EventosService();

            return Ok(eventosService.GetAllEventos());
        }

        [HttpGet("{idEvento:int}")]

        public IActionResult GetEventoPorId(int idEvento)
        {
            EventosService eventosService = new EventosService();
            var evento = eventosService.GetEventoPorId(idEvento);

            if (evento == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(evento);
            }
        }

        [HttpPost("NuevoEvento")]
        public IActionResult PostNuevoEvento([FromBody] Eventos evento)
        {
            EventosService eventoService = new EventosService();
            bool resultado = eventoService.PostNuevoEvento(evento);

            if(resultado) 
            {
                return Ok();
            }else
            {
                return NotFound();   
            }
        }

        [HttpPost("NuevoEventoCompleto")]
        public IActionResult PostNuevoEventoModel([FromBody] EventoModel evento)
        {
            EventosService eventoService = new EventosService();

            eventoService.PostNuevoEventoCompleto(evento);

            return Ok();


        }

        [HttpPut("Modificar/{idEvento:int}")]
        public IActionResult PutNuevoEvento(int idEvento, [FromBody] Eventos evento)
        {
            EventosService eventoService = new EventosService();
            bool resultado = eventoService.PutNuevoEvento(idEvento, evento);

            if (resultado)
            {
                return Ok();
            }
            else
            {
                return UnprocessableEntity();
            }

        }
        [HttpDelete("{idEvento:int}/Borrar")]
        public IActionResult DeleteEvento(int idEvento)
        {
            EventosService eventosService = new EventosService();
            bool resultado = eventosService.DeleteEvento(idEvento);

            if(resultado)
            {
                return Ok();
            }
            else
            {
                return UnprocessableEntity();// devuelve un 422
            }


        }
       
    }
}
