using GestorEventos.Servicios.Servicios;
using GestorEventos.Servicios.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace GestorEventos.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonaController : Controller
    {
        // trae lista de personas
        [HttpGet]
        public IActionResult Get()
        {
            PersonaService personaService = new PersonaService();
            return Ok(personaService.GetPersonaDePrueba());
        }

        // trae una persona
        [HttpGet("{idPersona:int}")]

        public IActionResult GetPersonaPorId(int idPersona)
        {
            PersonaService personaService= new PersonaService();
            Persona persona = personaService.GetPersonaDePruebaSegunId(idPersona);

            if(persona == null)
            {
                return NotFound();
            } else
            {
                return Ok(persona);
            }
        }

        [HttpPost]
        public IActionResult PostPersona([FromBody] Persona persona)
        {
            PersonaService personaService = new PersonaService();

            personaService.AgregarNuevaPersona(persona);

            return Ok();
        }

        [HttpPut("{idPersona:int}")]
        public IActionResult PutPersona(int idPersona, [FromBody] Persona persona)
        {
            PersonaService personaService = new PersonaService();
            personaService.ModificarPersona(idPersona, persona);

            return Ok();
        }

        [HttpPatch("BorradoLogico/{idPersona:int}")]
        public IActionResult BorradoLogicoPersona(int idPersona)
        {
            PersonaService personaService = new PersonaService();
            personaService.BorrarLogicamentePersona(idPersona);
            return Ok();
        }


    }
}
