using MedicalApp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalApp.Model
{
    class Venue
    {
        public Response response { get; set; }


    }

    public class TipoUsuario
    {
        public int id { get; set; }
        public string nome { get; set; }

        public static TipoUsuario GetInstance(int id)
        {
            TipoUsuario newInstance = new TipoUsuario();
            newInstance.id = -1;
            if(id == 1)
            {
                newInstance.id = 1;
                newInstance.nome = "Médico";
            }
            else if(id == 2)
            {
                newInstance.id = 2;
                newInstance.nome = "Paciente";
            }
            return newInstance;
        }
    }

    public class Usuario
    {
        public int id { get; set; }
        public TipoUsuario tipoUsuario { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string nome { get; set; }

        public override string ToString()
        {
            return this.nome;
        }
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
