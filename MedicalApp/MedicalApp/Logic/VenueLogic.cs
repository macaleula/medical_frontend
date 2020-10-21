using MedicalApp.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MedicalApp.Logic
{
    class VenueLogic
    {

        public async static Task<List<Consulta>> GetVenues()
        {
            List<Venue> venues = new List<Venue>();
            var url = Venue.GenerateURL();
            var temp = new List<Consulta>();
             using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine(json);
                temp = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Consulta>>(json);

            }
            
            return temp;
        }
    }
}
    