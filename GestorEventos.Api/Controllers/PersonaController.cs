using GestorEventos.Servicios.Servicios;
using GestorEventos.Servicios.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace GestorEventos.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonaController : Controller
    {
        private IPersonaService personaService;

        // constructor
        public PersonaController(IPersonaService _personaService) 
        { 
            personaService = _personaService;
        }

        // trae lista de personas
        [HttpGet]
        public IActionResult Get()
        {    
            return Ok(personaService.GetPersonaDePrueba());
        }

        // trae una persona segun su id
        [HttpGet("{idPersona:int}")]
        public IActionResult GetPersonaPorId(int idPersona)
        {
            var persona = this.personaService.GetPersonaDePruebaSegunId(idPersona); 

            if(persona == null)
            {
                return NotFound();
            } else
            {
                return Ok(persona);
            }
        }

        // agrega una persona
        [HttpPost]
        public IActionResult PostPersona([FromBody] Persona persona)
        {
            personaService.AgregarNuevaPersona(persona);

            return Ok();
        }

        // modifica una persona
        [HttpPut("{idPersona:int}")]
        public IActionResult PutPersona(int idPersona, [FromBody] Persona persona)
        {
            personaService.ModificarPersona(idPersona, persona);

            return Ok();
        }

        // hace un borrado logico de una persona segun su id
        [HttpPatch("BorradoLogico/{idPersona:int}")]
        public IActionResult BorradoLogicoPersona(int idPersona)
        { 
            personaService.BorrarLogicamentePersona(idPersona);
            return Ok();
        }


    }
}
