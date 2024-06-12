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

        public int IdPersonaContacto { get; set; }

        public bool Visible {  get; set; }  

    }

    public class EventoModel
    {
        public Eventos evento { get; set; }
        public Persona PersonaContacto { get; set; }
        public Persona PersonaAgasajada { get; set; }
        public IEnumerable<ServiciosVM> ListaDeServiciosContratados { get; set; }
        
    }
}
