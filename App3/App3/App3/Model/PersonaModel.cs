using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace App3.Model
{
    class PersonaModel
    {
        /*ngrok http -host-header="localhost:50625" 50625*/
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Contraseña { get; set; }
        public string FotoPerfil { get; set; }
        public ObservableCollection<ApuestaModel> LstApuestas { get; set; }



        //Crea un usuario
        public async Task<bool> CrearUsuarioAsync(PersonaModel persona)
        {
            try
            {

            using (HttpClient client = new HttpClient())
            {
                var uri =  new Uri  (Global.RutaApi + Global.ApiPersona );

                var vlojson = JsonConvert.SerializeObject(persona);

               
                var content = new StringContent(vlojson, Encoding.UTF8, "application/json");

                var vloRespuesta = await client.PostAsync(uri, content).ConfigureAwait(false); 

                string ans = vloRespuesta.Content.ReadAsStringAsync().Result;

                bool loginUser = JsonConvert.DeserializeObject<bool>(ans);

                return loginUser;

            }
            }
            catch (Exception)
            {

                return false;
            }

        }

        //Realiza un login
        internal bool Login(PersonaModel personaNueva)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var uri = new Uri(Global.RutaApi + Global.ApiLogin);


                    var vlojson = JsonConvert.SerializeObject(personaNueva);

                    var content = new StringContent(vlojson, Encoding.UTF8, "application/json");

                    var vloRespuesta = client.PostAsync(uri, content).Result;

                    string ans = vloRespuesta.Content.ReadAsStringAsync().Result;

                    bool loginUser = JsonConvert.DeserializeObject<bool>(ans);

                    return loginUser;
                }
            }
            catch (Exception)
            {
                return false;

            }

        }
        }
    }




