using MedicalApp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalApp.Model
{
    class Venue
    {
        public Response response { get; set; }

        public static string GenerateURL()
        {
            return Constants.VENUE_SEARCH;//Constants.VENUE_SEARCH, user);
        }


    }

    public class TipoUsuario
    {
        public int id { get; set; }
        public string nome { get; set; }
    }

    public class Usuario
    {
        public int id { get; set; }
        public TipoUsuario tipoUsuario { get; set; }
        public string username { get; set; }
        public string nome { get; set; }
    }

    public class Consulta
    {
        public int id { get; set; }
        public Usuario medico { get; set; }
        public Usuario paciente { get; set; }
        public string data { get; set; }
    }

    public class Response
    {
        public IList<Consulta> consultas { get; set; }
    }
}
