using Dapper;
using GestorEventos.Servicios.Entidades;
using System.Data;
using System.Data.SqlClient;

namespace GestorEventos.Servicios.Servicios
{
    public class PersonaService
    {
        // IEnumerable establece que es una lista de entidades
        //public IEnumerable<Persona> PersonaDePrueba {  get; set; }

        private string _connectionString;

        // constructor
        public PersonaService()
        {
            _connectionString = "Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;";
            
            
            /*PersonaDePrueba = new List<Persona>
            {
                new Persona{IdPersona = 1, Nombre = "Joaquina", Apellido = "Aguilar", Direccion = "Chacabuco 1400", Email = "jota@gmail.com", Telefono = "11111"},
                new Persona{IdPersona = 2, Nombre = "Franco", Apellido = "Schwab", Direccion = "Alemanes del Volga 2322", Email = "franco@gmail.com", Telefono = "222222" },
                new Persona{IdPersona = 3, Nombre = "Sandra", Apellido = "Dos Santos", Direccion = "Fatima 1400", Email = "sandra@gmail.com", Telefono = "33333"}
            };*/

        }

        // consulta sobre persona
        public IEnumerable<Persona> GetPersonaDePrueba() 
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                List<Persona> personas = db.Query<Persona>("SELECT * FROM Personas WHERE Borrado = 0").ToList();// trae todos los registros que no fueron borrados

                return personas;
            }

            //return PersonaDePrueba;
        }

        // metodo que a partir de un Id busque en la lista de prueba segun una condicion
        public Persona? GetPersonaDePruebaSegunId(int IdPersona)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                Persona personas = db.Query<Persona>("SELECT * FROM Personas WHERE IdPersona=" + IdPersona.ToString()).FirstOrDefault();
                return personas;
            }

                /*try
                {
                    Persona persona = PersonaDePrueba.Where(x => x.IdPersona == IdPersona).First();
                    return persona;
                }
                catch (Exception ex)
                {
                    return null;
                }*/
        }

        public int AgregarNuevaPersona(Persona persona)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {

                string query = "INSERT INTO Personas (Nombre, Apellido, Direccion, Telefono, Email) VALUES (@Nombre, @Apellido, @Direccion, @Telefono, @Email ); SELECT Inserted.IdPersona";
                db.Execute(query, persona);
                return persona.IdPersona;
            }
        }

        public bool ModificarPersona(int idPersona, Persona persona)
        {
            using (IDbConnection db=new SqlConnection(_connectionString))
            {
                string query = "UPDATE Personas SET Nombre= @Nombre, Apellido= @Apellido, Direccion= @Direccion, Telefono= @Telefono, Email= @Email WHERE IdPersona = " + idPersona.ToString();
                db.Execute(query, persona);

                return true;
            }
        }

        //metodo que cambia el valor de Borrado
        
        public bool BorrarLogicamentePersona (int idPersona)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Personas SET Borrado = 1 WHERE IdPersona = " + idPersona.ToString();
                db.Execute(query);
                return true;    
            }
        }
    }
}
