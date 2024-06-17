using GestorEventos.Servicios.Entidades;
using GestorEventos.Servicios.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestorEventos.WebAdmin.Controllers
{
    public class EventosController : Controller
    {
        private IEventosService eventosService;
        private IPersonaService personaService;

        public EventosController(IEventosService _eventosService, IPersonaService personaService)
        {
            this.eventosService = _eventosService;
            this.personaService = personaService;
            
        }
        // GET: EventosController1
        public ActionResult Index()
        {
            return View();
        }

        // GET: EventosController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EventosController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EventosController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                Persona personaAgasajada = new Persona();
                personaAgasajada.Nombre = collection["Nombre"].ToString();  
                personaAgasajada.Apellido = collection["Apellido"].ToString() ;
                personaAgasajada.Direccion = collection["Direccion"].ToString();
                personaAgasajada.Telefono = collection["Telefono"].ToString();
                personaAgasajada.Email = collection["Email"].ToString();
                personaAgasajada.Borrado = false;

                int IdPersonaAgasajada = personaService.AgregarNuevaPersona(personaAgasajada);   

                Eventos eventoNuevo = new Eventos();
                eventoNuevo.IdPersonaAgasajada = IdPersonaAgasajada;
                eventoNuevo.CantPersonas =int.Parse(collection["CantPersonas"].ToString());
                eventoNuevo.Visible = true;
                eventoNuevo.IdUsuario = 1;
                eventoNuevo.FechaEvento = DateTime.Parse(collection["FechaEvento"].ToString()) ;
                eventoNuevo.IdTipoDespedida = int.Parse(collection["IdTipoDespedida"].ToString());
                eventoNuevo.NombreEvento = collection["NombreEvento"].ToString();
                

                this.eventosService.PostNuevoEvento(eventoNuevo);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EventosController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EventosController1/Edit/5
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

        // GET: EventosController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EventosController1/Delete/5
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
    }
}
