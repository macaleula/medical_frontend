using MedicalApp.Helpers;
using MedicalApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MedicalApp.Logic
{
    class ConsultaLogic
    {

        public async static Task<List<Consulta>> GetConsultas()
        {
            List<Consulta> consultas = new List<Consulta>();
            var url = Constants.LIST_CONSULTAS;
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Get,
            };
            request.Headers.Add("Authorization", "Bearer " + LoginLogic.GetJWT().jwt);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine(json);
                consultas = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Consulta>>(json);
            }
            
            return consultas;
        }

        public async static Task<List<Usuario>> GetMedicoMunicipio(string municipio)
        {
            List<Usuario> consultas = new List<Usuario>();
            var url = Constants.USUARIO_BY_MUNICIPIO;
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url + municipio),
                Method = HttpMethod.Get,
            };
            request.Headers.Add("Authorization", "Bearer " + LoginLogic.GetJWT().jwt);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine(json);
                consultas = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Usuario>>(json);
            }

            return consultas;
        }


        public async static Task<Consulta> NovaConsulta(Consulta novaConsulta)
        {
            var data = new StringContent(JsonConvert.SerializeObject(novaConsulta), Encoding.UTF8, "application/json");
            Consulta responseObject = null;
            var url = Constants.NOVA_CONSULTA;

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Get,
                Content = data,
            };
            request.Headers.Add("Authorization", "Bearer " + LoginLogic.GetJWT().jwt);
            using (HttpClient client = new HttpClient())
            {

                var response = await client.SendAsync(request);

                string result = response.Content.ReadAsStringAsync().Result;
                responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject<Consulta>(result);
            }
            return responseObject;
        }
    }
}
    