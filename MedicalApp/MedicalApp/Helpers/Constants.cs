using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalApp.Helpers
{
    class Constants
    {
        private const string BASE_ADDRESS = "http://medicalapp-env.eba-adnrfbri.us-east-1.elasticbeanstalk.com/";
        public const string VENUE_SEARCH = BASE_ADDRESS + "consulta";
        public const string AUTHENTICATE = BASE_ADDRESS + "authenticate";

    }
}
