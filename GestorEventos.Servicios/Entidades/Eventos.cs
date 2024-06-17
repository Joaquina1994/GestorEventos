namespace GestorEventos.Servicios.Entidades
{
    public class Eventos
    {

        /*IdEvento, Nombre, FechaEvento, CantPersonas, IdTipoDespedida, 
         * IdPersonaAgasajada, IdPersonaContacto*/
        public int IdEvento { get; set; }
        public string NombreEvento { get; set; }

        public DateTime FechaEvento { get; set; }
        public int CantPersonas { get; set; }
        public int IdTipoDespedida { get; set; }
        public int IdPersonaAgasajada { get; set; }

        public int IdUsuario { get; set; }

        public bool Visible {  get; set; }  

    }

    public class EventoModel : Eventos // herencia simple, hereda de Eventos
    {
        /*public Eventos Eventos { get; set; }
        public Persona PersonaContacto { get; set; }
        public Persona PersonaAgasajada { get; set; }
        public IEnumerable<EventosServicios> ListaDeServiciosContratados { get; set; }*/

        public int IdPersona { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public bool Borrado { get; set; }

    }
}
