using System.Globalization;

namespace GestorEventos.Servicios.Entidades
{
    public class Eventos
    {
        public int IdEvento { get; set; }
        public string NombreEvento { get; set; }
        public DateTime FechaEvento { get; set; }
        public int CantidadPersonas { get; set; }
        public int IdPersonaAgasajada { get; set; }
        public int IdTipoEvento { get; set; }
        public bool Visible { get; set; }
        public int IdUsuario { get; set; }
        public int IdEstadoEvento { get; set; }
        public bool Borrado { get; set; }
    }

    public class EventoViewModel : Eventos 
    {
        public string EstadoEvento { get; set; }
        public string NombrePersonaAgasajada { get; set; }

        public string TipoEvento { get; set; }
    }

    public class EventoModel : Eventos // herencia simple, hereda de Eventos
    {

        public int IdPersona { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public bool Borrado { get; set; }
        public IEnumerable<EventosServicios> ListaDeServiciosContratados { get; set; }
        public IEnumerable<ServiciosVM>? ListaDeServiciosDisponibles { get; set; }
        

    }
}
