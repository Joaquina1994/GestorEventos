//CODIGO DEL PROFESOR

using GestorEventos.Servicios.Entidades;
using GestorEventos.Servicios.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace GestorEventos.WebUsuario.Controllers
{
    public class EventosController : Controller
    {
        private IEventosService eventoService;
        private IPersonaService personaService;
        private IUsuarioService usuarioService;
        
        public EventosController(IEventosService _eventoService, IPersonaService _personaService, IUsuarioService _usuarioService)
        {
            this.eventoService = _eventoService;
            this.personaService = _personaService;
            this.usuarioService = _usuarioService;
           

        }

        // GET: EventosController
        public ActionResult Index()
        {
            var eventos = this.eventoService.GetAllEventosViewModel();

            return View(eventos);
        }

        // GET: EventosController/Details/5
        public ActionResult Details(int id)
        {
                
            // FALTA 


            return View();
        }

        // GET: EventosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: EventosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EventosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {

                Persona personaAgasajada = new Persona();
                personaAgasajada.Nombre = collection["Nombre"].ToString();
                personaAgasajada.Apellido = collection["Apellido"].ToString();
                personaAgasajada.Email = collection["Email"].ToString();
                personaAgasajada.Telefono = collection["Telefono"].ToString();
                personaAgasajada.Borrado = false;
                personaAgasajada.Direccion = collection["Direccion"].ToString();

                int IdPersonaAgasajada = personaService.AgregarNuevaPersona(personaAgasajada);

                Eventos eventoNuevo = new Eventos();
                eventoNuevo.IdPersonaAgasajada = IdPersonaAgasajada;

                eventoNuevo.CantidadPersonas = int.Parse(collection["CantidadPersonas"].ToString());
                eventoNuevo.Visible = true;
                eventoNuevo.IdUsuario = int.Parse(HttpContext.User.Claims.First(x => x.Type == "usuarioSolterout").Value); // HttpContext.User.Identity.Id;
                eventoNuevo.FechaEvento = DateTime.Parse(collection["FechaEvento"].ToString());
                eventoNuevo.IdTipoEvento = int.Parse(collection["IdTipoEvento"].ToString());
                eventoNuevo.NombreEvento = collection["NombreEvento"].ToString();
                eventoNuevo.IdEstadoEvento = 1; //Pendiente de Aprobacion
                eventoNuevo.Borrado = false;

                this.eventoService.PostNuevoEvento(eventoNuevo);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
            return View(collection);
        }

        // GET: EventosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EventosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }



        // POST: EventosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost("AprobarEvento")]
        public IActionResult AprobarEvento(int idEvento, IFormCollection collection)
        {
            // Intentar obtener el valor asociado con la clave "idEvento"
            if (collection.TryGetValue("idEvento", out var idEventoValues))
            {
                // Verificar que no esté vacío
                if (!StringValues.IsNullOrEmpty(idEventoValues) && idEventoValues.Count > 0)
                {
                    try
                    {
                        // Realizar la operación si existe al menos un elemento
                        int idEventoParsed = int.Parse(idEventoValues[0]);
                        this.eventoService.CambiarEstadoEvento(idEventoParsed, 2);
                    }
                    catch (FormatException ex)
                    {
                        // Manejar el caso cuando el valor no se puede convertir a int
                        return BadRequest("El valor 'idEvento' no es un número válido.");
                    }
                }
                else
                {
                    // Manejar el caso cuando la colección está vacía
                    return BadRequest("La colección 'idEvento' está vacía.");
                }
            }
            else
            {
                // Manejar el caso cuando la clave no existe
                return BadRequest("La clave 'idEvento' no existe en la colección.");
            }

            // Continuar con el resto de tu código
            return RedirectToAction("Index");
        }

        [HttpPost("RechazarEvento")]
        public IActionResult RechazarEvento(int idEvento, IFormCollection collection)
        {
            // Intentar obtener el valor asociado con la clave "idEvento"
            if (collection.TryGetValue("idEvento", out var idEventoValues))
            {
                // Verificar que no esté vacío
                if (!StringValues.IsNullOrEmpty(idEventoValues) && idEventoValues.Count > 0)
                {
                    try
                    {
                        // Realizar la operación si existe al menos un elemento
                        int idEventoParsed = int.Parse(idEventoValues[0]);
                        this.eventoService.CambiarEstadoEvento(idEventoParsed, 3);
                    }
                    catch (FormatException ex)
                    {
                        // Manejar el caso cuando el valor no se puede convertir a int
                        return BadRequest("El valor 'idEvento' no es un número válido.");
                    }
                }
                else
                {
                    // Manejar el caso cuando la colección está vacía
                    return BadRequest("La colección 'idEvento' está vacía.");
                }
            }
            else
            {
                // Manejar el caso cuando la clave no existe
                return BadRequest("La clave 'idEvento' no existe en la colección.");
            }

            // Continuar con el resto de tu código
            return RedirectToAction("Index");
        }

    }
}