using GestorEventos.Servicios.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorEventos.Servicios.Servicios
{
    public interface IEventosService
    {
        IEnumerable<Eventos> Eventos { get; set; }

        bool DeleteEvento(int idEvento);
        IEnumerable<Eventos> GetAllEventos();
        Eventos GetEventoPorId(int IdEvento);
        bool PostNuevoEvento(Eventos eventos);
        void PostNuevoEventoCompleto(EventoModel eventoModel);
        bool PutNuevoEvento(int idEvento, Eventos eventos);
    }

    public class EventosService : IEventosService
    {

        public IEnumerable<Eventos> Eventos { get; set; }

        public EventosService()
        {
            Eventos = new List<Eventos>
            {
                new Eventos {IdEvento = 1, CantPersonas = 5, FechaEvento = DateTime.Now, IdPersonaAgasajada = 1, IdPersonaContacto = 1, IdTipoDespedida = 2, NombreEvento= "Evento de despedidas Diego", Visible= true},
                new Eventos { IdEvento = 2, CantPersonas = 10, FechaEvento = DateTime.Now, IdPersonaAgasajada = 3, IdPersonaContacto = 2, IdTipoDespedida = 1, NombreEvento = "Evento de despedidas Juana", Visible= true }
            };

        }

        public IEnumerable<Eventos> GetAllEventos()
        {
            return Eventos.Where(x => x.Visible == true); // consulta a la base
        }

        public Eventos GetEventoPorId(int IdEvento)
        {
            try
            {
                Eventos eventos = Eventos.Where(x => x.IdEvento == IdEvento).First();
                return eventos;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool PostNuevoEvento(Eventos eventos)
        {
            try
            {
                List<Eventos> lista = this.Eventos.ToList();
                lista.Add(eventos);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool PutNuevoEvento(int idEvento, Eventos eventos)
        {
            try
            {
                var eventoDeLista = this.Eventos.Where(x => x.IdEvento == idEvento).First();

                eventoDeLista.FechaEvento = eventoDeLista.FechaEvento;
                eventoDeLista.NombreEvento = eventoDeLista.NombreEvento;
                eventoDeLista.CantPersonas = eventoDeLista.CantPersonas;
                eventoDeLista.IdPersonaContacto = eventoDeLista.IdPersonaContacto;
                eventoDeLista.IdPersonaAgasajada = eventoDeLista.IdPersonaAgasajada;

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool DeleteEvento(int idEvento)
        {
            try
            {

                var eventoAEliminar = this.Eventos.Where(x => x.IdEvento == idEvento).First();
                var listaEventos = this.Eventos.ToList();

                // Borrado fisico
                //listaEventos.Remove(eventoAEliminar);   

                // Borrado logico
                eventoAEliminar.Visible = false;



                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void PostNuevoEventoCompleto(EventoModel eventoModel)
        {
            PersonaService personaService = new PersonaService();
            int idPersonaAgasajada = personaService.AgregarNuevaPersona(eventoModel.PersonaAgasajada);
            int idPersonaDeContacto = personaService.AgregarNuevaPersona(eventoModel.PersonaContacto);

            eventoModel.evento.IdPersonaAgasajada = idPersonaAgasajada;
            eventoModel.evento.IdPersonaContacto = idPersonaDeContacto;
            eventoModel.evento.Visible = true;

            this.PostNuevoEvento(eventoModel.evento);

            foreach (ServiciosVM servicio in eventoModel.ListaDeServiciosContratados)
            {
                ServiciosService serviciosService = new ServiciosService();
                serviciosService.AgregarServicio(servicio);
            }

        }
    }
}
