using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;

namespace App3.Model
{
    public class EquipoModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public int Marcador { get; set; }


        //Realiza un login
        public static ObservableCollection<EquipoModel> ObtenerEquipos(ApuestaModel miApuesta)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var uri = new Uri(Global.RutaApi + Global.ApiEquipos);


                    var vlojson = JsonConvert.SerializeObject(miApuesta);

                    var content = new StringContent(vlojson, Encoding.UTF8, "application/json");

                    var vloRespuesta = client.PostAsync(uri, content).Result;

                    string ans = vloRespuesta.Content.ReadAsStringAsync().Result;

                    var loginUser = JsonConvert.DeserializeObject<ObservableCollection<EquipoModel>>(ans);

                    return loginUser;
                }
            }
            catch (Exception)
            {
                return null;

            }

        }
    }
}
