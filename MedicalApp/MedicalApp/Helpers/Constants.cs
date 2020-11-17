using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalApp.Helpers
{
    class Constants
    {
        private const string BASE_ADDRESS = "http://medicalapp-env.eba-adnrfbri.us-east-1.elasticbeanstalk.com/";

        public const string LIST_CONSULTAS = BASE_ADDRESS + "consulta";
        public const string AUTHENTICATE = BASE_ADDRESS + "authenticate";
        public const string USUARIO = BASE_ADDRESS + "usuario/";
        public const string USUARIO_BY_USERNAME = BASE_ADDRESS + "usuario/username/";
        public const string USUARIO_BY_MUNICIPIO = BASE_ADDRESS + "usuario/municipio/";
        public const string NOVA_CONSULTA = BASE_ADDRESS + "consulta";
    }
}
