namespace GestorEventos.Servicios.Entidades
{
    public class ServiciosVM
    {
        /*Servicios: IdServicio, Descripcion, PrecioUnitario
 */
        public int IdServicio {  get; set; }
        public string Descripcion { get; set;}
        public decimal PrecioServicio { get; set; }
        public bool Borrado { get; set; }   
  
    }
}
