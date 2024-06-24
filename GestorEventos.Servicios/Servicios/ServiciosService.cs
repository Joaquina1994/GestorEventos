using Dapper;
using GestorEventos.Servicios.Entidades;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace GestorEventos.Servicios.Servicios
{
    public interface IServiciosService
    {
       

        bool AgregarServicio(ServiciosVM servicios);
        bool BorradoLogicoServicio(int IdServicio);
        IEnumerable<ServiciosVM> GetServicios();
        ServiciosVM GetServiciosPorId(int IdServicio);
        bool ModificarServicio(int IdServicio, ServiciosVM servicios);
        
    }

    public class ServiciosService : IServiciosService
    {
       
        private string _connectionString;

        public ServiciosService()
        {
            _connectionString = "Server=localhost\\SQLEXPRESS;Database=GestorEventos;Trusted_Connection=True;";

        }

        public IEnumerable<ServiciosVM> GetServicios()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                List<ServiciosVM> servicios = db.Query<ServiciosVM>("SELECT * FROM Servicios WHERE Borrado = 0").ToList();
                return servicios;
            }


        }
        /*public IEnumerable<ServiciosVM> GetServicios()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                try
                {
                    var servicios = db.Query<ServiciosVM>("SELECT * FROM Servicios WHERE Borrado = 0").ToList();
                    if (servicios == null || !servicios.Any())
                    {
                        Console.WriteLine("No services found.");
                    }
                    return servicios ?? new List<ServiciosVM>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception in GetServicios: {ex.Message}");
                    // Log la excepción
                    return new List<ServiciosVM>(); // Siempre devolver una lista no nula
                }
            }
        }*/


        public ServiciosVM GetServiciosPorId(int IdServicio)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                ServiciosVM servicios = db.Query<ServiciosVM>("SELECT * FROM Servicios WHERE IdServicio = " + IdServicio.ToString()).First();
                return servicios;
            }
            
        }




        public bool AgregarServicio(ServiciosVM servicios)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))

            {
                string query = "INSERT INTO Servicios(Descripcion, PrecioServicio, Borrado) VALUES(@Descripcion, @PrecioServicio, 0)";
                db.Execute(query, servicios);
                return true;
            }
        }

        public bool ModificarServicio(int IdServicio, ServiciosVM servicios)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))

            {
                string query = "UPDATE Servicios SET Descripcion = @Descripcion, PrecioServicio = @PrecioServicio WHERE IdServicio = " + IdServicio.ToString();
                db.Execute(query, servicios);
                return true;
            }
        }

        public bool BorradoLogicoServicio(int IdServicio)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))

            {
                string query = "UPDATE Servicios SET Borrado = 1 WHERE IdServicio = " + IdServicio.ToString();
                db.Execute(query);
                return true;
            }
        }

    }
}
