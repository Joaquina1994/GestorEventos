using GestorEventos.Servicios.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace GestorEventos.Servicios.Servicios
{
    public interface IEventosService
    {
       

        bool DeleteEvento(int idEvento);
        IEnumerable<Eventos> GetAllEventos();
        IEnumerable<EventoViewModel> GetAllEventosViewModel();
        IEnumerable<EventoViewModel> GetMisEventos(int IdUsuario);
        Eventos GetEventoPorId(int IdEvento);
        int PostNuevoEvento(Eventos eventos);
       
        bool PutNuevoEvento(int idEvento, Eventos eventos);
        bool CambiarEstadoEvento(int IdEvento, int IdEstado);
        //Task<bool> CambiarEstadoEventoAsync(int IdEvento, int IdEstado);
    }

    public class EventosService : IEventosService
    {
        private string _connectionString;



        public EventosService()
        {

            //Connection string 
            _connectionString = "Server=localhost\\SQLEXPRESS;Database=GestorEventos;Trusted_Connection=True;";


        }

 
        public IEnumerable<Eventos> GetAllEventos()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                List<Eventos> eventos = db.Query<Eventos>("SELECT * FROM Eventos WHERE Borrado = 0").ToList();

                return eventos;

            }
        }

        public IEnumerable<EventoViewModel> GetAllEventosViewModel()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                List<EventoViewModel> eventos = db.Query<EventoViewModel>("SELECT Eventos.*," +
                        "EstadoEventos.Descripcion AS EstadoEvento," +
                        "Persona.Nombre AS NombrePersonaAgasajada," +
                        "TipoEvento.Descripcion AS TipoEvento," +
                        "Usuario.NombreCompleto AS NombreUsuario " + // Nuevo campo a seleccionar
                        "FROM Eventos " + // Asegúrate de tener espacios después de cada tabla
                        "LEFT JOIN EstadoEventos ON EstadoEventos.IdEstadoEvento = Eventos.IdEstadoEvento " +
                        "LEFT JOIN Persona ON Persona.IdPersona = Eventos.IdPersonaAgasajada " +
                        "LEFT JOIN TipoEvento ON TipoEvento.IdTipoEvento = Eventos.IdTipoEvento " +
                        "LEFT JOIN Usuario ON Usuario.IdUsuario = Eventos.IdUsuario").ToList();



                //List<EventoViewModel> eventos = db.Query<EventoViewModel>("SELECT Eventos.*, EstadoEventos.Descripcion EstadoEvento FROM Eventos LEFT JOIN EstadoEventos on EstadoEventos.IdEstadoEvento = Eventos.idEstadoEvento").ToList();

                return eventos;

            }
        }
        public IEnumerable<EventoViewModel> GetMisEventos(int idUsuario)
        {

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                List<EventoViewModel> eventos = db.Query<EventoViewModel>(@"
                            SELECT Eventos.*, 
                            EstadoEventos.Descripcion AS EstadoEvento,
                            Persona.Nombre AS NombrePersonaAgasajada,
                            TipoEvento.Descripcion AS TipoEvento,
                            Usuario.NombreCompleto AS NombreUsuario
                            FROM Eventos
                            LEFT JOIN EstadoEventos ON EstadoEventos.IdEstadoEvento = Eventos.IdEstadoEvento
                            LEFT JOIN Persona ON Persona.IdPersona = Eventos.IdPersonaAgasajada
                            LEFT JOIN TipoEvento ON TipoEvento.IdTipoEvento = Eventos.IdTipoEvento
                            LEFT JOIN Usuario ON Usuario.IdUsuario = Eventos.IdUsuario
                            WHERE Eventos.IdUsuario = @IdUsuario",
                            new { IdUsuario = idUsuario })
                            .ToList();




                //List<EventoViewModel> eventos = db.Query<EventoViewModel>("SELECT Eventos.*, EstadoEventos.Descripcion EstadoEvento FROM Eventos LEFT JOIN EstadoEventos ON EstadoEventos.IdEstadoEvento = Eventos.idEstadoEvento WHERE Eventos.IdUsuario =" + idUsuario.ToString()).ToList();

                return eventos;

            }
        }

        public Eventos GetEventoPorId(int IdEvento)
        {
            try
            {
                using(IDbConnection db = new SqlConnection(_connectionString))
                {
                    Eventos eventos = db.Query<Eventos>("SELECT * FROM Eventos WHERE Borrado = 0").First();

                    return eventos;

                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool CambiarEstadoEvento(int IdEvento, int IdEstadoEvento)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    string query = "UPDATE Eventos SET IdEstadoEvento = " + IdEstadoEvento.ToString() + " WHERE IdEvento = " + IdEvento.ToString();
                    

                    db.Execute(query);

                    return true;
                }
            }
            catch (Exception ex)
            {
                // Loguear el error
                // Console.WriteLine(ex.Message);

                return false;
            }
        }

        /*public async Task<bool> CambiarEstadoEventoAsync(int IdEvento, int IdEstado)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    string query = "UPDATE Eventos SET IdEstadoEvento = @IdEstado WHERE IdEvento = @IdEvento";
                    var parameters = new { IdEstado = IdEstado, IdEvento = IdEvento };

                    await db.ExecuteAsync(query, parameters);

                    return true;
                }
            }
            catch (Exception ex)
            {
                // Loguear el error
                // Console.WriteLine(ex.Message);

                return false;
            }
        }*/


    
    public int PostNuevoEvento(Eventos eventos)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    string query = @"INSERT INTO Eventos (NombreEvento, FechaEvento, CantidadPersonas, IdPersonaAgasajada, IdTipoEvento, Visible, IdUsuario, IdEstadoEvento, Borrado)
                             VALUES (@NombreEvento, @FechaEvento, @CantidadPersonas, @IdPersonaAgasajada, @IdTipoEvento, @Visible, @IdUsuario, @IdEstadoEvento, 0);
                             SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    eventos.IdEvento = db.QuerySingle<int>(query, eventos);
                    return eventos.IdEvento;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw; // O puedes registrar el error y retornar 0 como lo tenías antes
            }
        }


        


        public bool PutNuevoEvento(int idEvento, Eventos eventos)
        {
            try
            {
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
                return true;
            }
            catch (Exception ex)
            { 
                return false ;  
            }
            /*try
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
            }*/
        }

        /*public void PostNuevoEventoCompleto(EventoModel eventoModel)
        {
            PersonaService personaService = new PersonaService();
            int idPersonaAgasajada = personaService.AgregarNuevaPersona(eventoModel.PersonaAgasajada);
            //int idPersonaDeContacto = personaService.AgregarNuevaPersona(eventoModel.PersonaContacto);

            eventoModel.Eventos.IdPersonaAgasajada = idPersonaAgasajada;
            //eventoModel.evento.IdPersonaContacto = idPersonaDeContacto;
            eventoModel.Eventos.Visible = true;

            this.PostNuevoEvento(eventoModel.Eventos);

            foreach (EventosServicios servicio in eventoModel.ListaDeServiciosContratados)
            {
                ServiciosService serviciosService = new ServiciosService();
                serviciosService.AgregarServicio(servicio);
            }


            /*foreach (ServiciosVM servicio in eventoModel.ListaDeServiciosContratados)
            {
                ServiciosService serviciosService = new ServiciosService();
                serviciosService.AgregarServicio(servicio);
            }*/

        }
    }

