using Dapper;
using GestorEventos.Servicios.Entidades;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace GestorEventos.Servicios.Servicios
{
    public class ServiciosService
    {
        public IEnumerable<ServiciosVM> Servicios { get; set; }

        private string _connectionString;

        public ServiciosService()
        {
            _connectionString = "Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;";


            /*new ServiciosVM { IdServicio = 1, Descripcion = "Bar Hopping", PrecioUnitario = 10000 };
            new ServiciosVM { IdServicio = 2, Descripcion = "Traslado", PrecioUnitario = 20000 };
            new ServiciosVM { IdServicio = 3, Descripcion = "Entradas", PrecioUnitario = 5000 };*/

        }

        public IEnumerable<ServiciosVM> GetServicios()
        {
            using (IDbConnection db = new SqlConnection(_connectionString)) 
            {
                List<ServiciosVM> servicios = db.Query<ServiciosVM>("SELECT * FROM Servicios WHERE Borrado = 0").ToList();
                return servicios;
            }   

            
        }

        public ServiciosVM GetServiciosPorId(int IdServicio)
        {


            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                ServiciosVM servicios = db.Query<ServiciosVM>("SELECT * FROM servicios WHERE IdServicio = " + IdServicio.ToString()).First();
                return servicios;
            }
            /*try
            {
                ServiciosVM servicios = Servicios.Where(x => x.IdServicio == IdServicio).First();
                return servicios;
            }
            catch (Exception ex)
            {
                return null;
            }*/
        }

        public bool AgregarServicio(ServiciosVM servicios)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))

            {
                string query = "INSERT INTO Servicios(Descripcion, PrecioServicio, Borrado) VALUES(@Descripcion, @PrecioServicio, @Borrado)";
                db.Execute(query, servicios);
                return true;
            }
        }



        /*public int AgregarServicio(ServiciosVM servicios)
        {

            using (IDbConnection db = new SqlConnection(_connectionString))
            {

                string query = "INSERT INTO Servicios (IdServicio, Descripcion, PrecioUnitario, Borrado) VALUES (@IdServicio, @Descripcion, @PrecioUnitario, @Borrado)";
                db.Execute(query, servicios);
                return servicios.IdServicio;
            }

            try
            {
                List<ServiciosVM> lista = this.Servicios.ToList();
                lista.Add(servicios);

                return true;
            }catch (Exception ex)
            { 
                return false;   
            }
    }*/

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
