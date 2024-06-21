using Dapper;
using GestorEventos.Servicios.Entidades;
using System.Data;
using System.Data.SqlClient;

namespace GestorEventos.Servicios.Servicios
{
    public interface IPersonaService
    {
        int AgregarNuevaPersona(Persona persona);
        bool BorrarLogicamentePersona(int idPersona);
        IEnumerable<Persona> GetPersonaDePrueba();
        Persona? GetPersonaDePruebaSegunId(int IdPersona);
        bool ModificarPersona(int idPersona, Persona persona);
    }

    public class PersonaService : IPersonaService
    {
        // IEnumerable establece que es una lista de entidades
        //public IEnumerable<Persona> PersonaDePrueba {  get; set; }

        private string _connectionString;

        // constructor
        public PersonaService()
        {
            _connectionString = "Server=localhost\\SQLEXPRESS;Database=GestorEventos;Trusted_Connection=True;";


        }

        // consulta sobre persona
        public IEnumerable<Persona> GetPersonaDePrueba()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                List<Persona> personas = db.Query<Persona>("SELECT * FROM Persona WHERE Borrado = 0").ToList();// trae todos los registros que no fueron borrados

                return personas;
            }

            //return PersonaDePrueba;
        }

        // metodo que a partir de un Id busque en la lista de prueba segun una condicion
        public Persona? GetPersonaDePruebaSegunId(int IdPersona)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                Persona personas = db.Query<Persona>("SELECT * FROM Persona WHERE IdPersona=" + IdPersona.ToString()).FirstOrDefault();
                return personas;
            }

            
        }

        /*public int AgregarNuevaPersona(Persona persona)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {

                string query = "INSERT INTO Persona (Nombre, Apellido, Direccion, Telefono, Email, Borrado) VALUES (@Nombre, @Apellido, @Direccion, @Telefono, @Email, 0 )";
                db.Execute(query, persona);
                return persona.IdPersona;
            }
        }*/
        public int AgregarNuevaPersona(Persona persona)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = @"
            INSERT INTO Persona (Nombre, Apellido, Direccion, Telefono, Email, Borrado) 
            VALUES (@Nombre, @Apellido, @Direccion, @Telefono, @Email, 0);
            SELECT CAST(SCOPE_IDENTITY() as int)";

                // Ejecutar la consulta y obtener el ID insertado
                int idPersonaInsertada = db.QuerySingle<int>(query, persona);

                // Asignar el ID generado a la persona y devolverlo
                persona.IdPersona = idPersonaInsertada;

                return persona.IdPersona;
            }
        }


        public bool ModificarPersona(int idPersona, Persona persona)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Persona SET Nombre= @Nombre, Apellido= @Apellido, Direccion= @Direccion, Telefono= @Telefono, Email= @Email, Borrado=@Borrado WHERE IdPersona = " + idPersona.ToString();
                db.Execute(query, persona);

                return true;
            }
        }

        //metodo que cambia el valor de Borrado

        public bool BorrarLogicamentePersona(int idPersona)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Persona SET Borrado = 1 WHERE IdPersona = " + idPersona.ToString();
                db.Execute(query);
                return true;
            }
        }
    }
}
