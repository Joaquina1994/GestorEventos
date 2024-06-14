using GestorEventos.Servicios.Entidades;
using GestorEventos.Servicios.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestorEventos.WebAdmin.Controllers
{
    public class ServiciosController : Controller
    {
        // GET: ServiciosController
        public ActionResult Index()
        {

            // antes de pasar el modelo a la vista se asegura que no sea null
            ServiciosService serviciosService = new ServiciosService();
            var model = serviciosService.GetServicios(); // Método para obtener todos los servicios
            if (model == null)
            {
                model = new List<ServiciosVM>(); // Inicializa una lista vacía si el modelo es null
            }
            return View(model);
            /*ServiciosService serviciosService = new ServiciosService();
            serviciosService.GetServicios();

            return View();*/
        }

        // GET: ServiciosController/Details/5
        public ActionResult Details(int id)
        {

            ServiciosService servicioService = new ServiciosService();  
            

            return View(servicioService.GetServiciosPorId(id));
        }

        // GET: ServiciosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServiciosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                ServiciosService serviciosService = new ServiciosService();

                ServiciosVM servicios = new ServiciosVM();
                servicios.Descripcion = collection["Descripcion"].ToString();
                servicios.PrecioServicio = decimal.Parse(collection["PrecioServicio"]);

                serviciosService.AgregarServicio(servicios);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ServiciosController/Edit/5
        public ActionResult Edit(int id)
        {
            ServiciosService servicioService = new ServiciosService();


            return View(servicioService.GetServiciosPorId(id));
        }

        // POST: ServiciosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                ServiciosService serviciosService = new ServiciosService();
                ServiciosVM servicios = new ServiciosVM();

                servicios.IdServicio = int.Parse(collection["IdServicio"].ToString());
                servicios.Descripcion = collection["Descripcion"].ToString();
                servicios.PrecioServicio = decimal.Parse(collection["PrecioServicio"].ToString());

                serviciosService.ModificarServicio(id, servicios);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ServiciosController/Delete/5
        public ActionResult Delete(int id)
        {
            ServiciosService servicioService = new ServiciosService();


            return View(servicioService.GetServiciosPorId(id));
        }

        // POST: ServiciosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                ServiciosService servicioService = new ServiciosService();

                servicioService.BorradoLogicoServicio(id);  


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
