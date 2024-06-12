namespace GestorEventos.Servicios.Entidades
{
    public class Persona
    {


        public int IdPersona { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public bool Borrado {  get; set; }  
    }
}
