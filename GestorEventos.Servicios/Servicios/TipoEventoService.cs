using GestorEventos.Servicios.Entidades;

namespace GestorEventos.Api.Servicios
{
    public class TipoEventoService
    {

        public IEnumerable<TipoEvento> TiposDeEvento {  get; set; } 

        public TipoEventoService() {
            TiposDeEvento = new List<TipoEvento>
            {
                new TipoEvento{IdTipoEvento = 1, Descripcion = "Despedida de solteros"},
                new TipoEvento{IdTipoEvento = 2, Descripcion = "Despedida de solteras"}

            };
        }

        public  IEnumerable<TipoEvento> GetTipoEventos()
        {
            return this.TiposDeEvento;
        }

        public TipoEvento GetTipoEventoPorId(int IdTipoEvento)
        {
            try
            {
                TipoEvento tipoEvento = TiposDeEvento.Where(x => x.IdTipoEvento == IdTipoEvento).First();
                return tipoEvento;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
