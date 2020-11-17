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
    class LoginLogic
    {

        private static Usuario loggedUsuario;
        private static JWT jsonWebToken;

        static LoginLogic()
        {
            loggedUsuario = null;
            jsonWebToken = null;
        }

        public static JWT GetJWT()
        {
            return jsonWebToken;
        }

        public async static Task<JWT> Login(string username, string password)
        {
            var data = new StringContent(SerializeUsuarioLogin(username, password), Encoding.UTF8, "application/json");
            JWT jwt = null;
            var url = Constants.AUTHENTICATE;
            using (HttpClient client = new HttpClient())
            {

                var response = await client.PostAsync(url, data);

                string result = response.Content.ReadAsStringAsync().Result;
                jwt = Newtonsoft.Json.JsonConvert.DeserializeObject<JWT>(result); 
            }
            jsonWebToken = jwt;
            await FetchLoggedUsuario(username, jwt.jwt);
            return jwt; 
        }

        private async static Task<Usuario> FetchLoggedUsuario(string username, string jwt)
        {
            Usuario usuario = new Usuario();
            var url = Constants.USUARIO_BY_USERNAME;
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url + username),
                Method = HttpMethod.Get,
            };
            request.Headers.Add("Authorization", "Bearer " + jwt);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                usuario = Newtonsoft.Json.JsonConvert.DeserializeObject<Usuario>(json);
            }
            loggedUsuario = usuario;
            
            return loggedUsuario;
        }

        public async static Task<Usuario> Register(Usuario usuario)
        {
            var data = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");
            Usuario result = null;
            var url = Constants.USUARIO;
            try
            {
                using (HttpClient client = new HttpClient())
                {

                    var response = await client.PostAsync(url, data);

                    string json = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine("jSON: " + JsonConvert.SerializeObject(usuario));

                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<Usuario>(json);
                }
            }
            catch(Exception e)
            {
                result = null;
            }
            return result;
        }

        public static Usuario GetLoggedUsuario()
        {
            return loggedUsuario;
        }

        public static string SerializeUsuarioLogin(string username, string password)
        {
            Usuario usuario = new Usuario();
            usuario.username = username;
            usuario.password = password;
           return JsonConvert.SerializeObject(usuario);
        }
    }
}
