using GestorEventos.Servicios.Entidades;

namespace GestorEventos.Servicios.Servicios
{
    public class ServiciosService
    {
        public IEnumerable<ServiciosVM> Servicios { get; set; }

        public ServiciosService()
        {
            new ServiciosVM { IdServicio = 1, Descripcion = "Bar Hopping", PrecioUnitario = 10000 };
            new ServiciosVM { IdServicio = 2, Descripcion = "Traslado", PrecioUnitario = 20000 };
            new ServiciosVM { IdServicio = 3, Descripcion = "Entradas", PrecioUnitario = 5000 };
        }

        public IEnumerable<ServiciosVM> GetServicios()
        {
            return this.Servicios;
        }

        public ServiciosVM GetServiciosPorId(int IdServicio)
        {
            try
            {
                ServiciosVM servicios = Servicios.Where(x => x.IdServicio == IdServicio).First();
                return servicios;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool AgregarServicio(ServiciosVM servicios)
        {
            try
            {
                List<ServiciosVM> lista = this.Servicios.ToList();
                lista.Add(servicios);
                
                return true;
            }catch (Exception ex)
            { 
                return false;   
            }
        }
    }
}
