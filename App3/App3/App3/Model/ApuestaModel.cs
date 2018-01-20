using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace App3.Model
{
   public class ApuestaModel
    {
        public int Id { get; set; }
        public string Jornada { get; set; }
        public bool Finalizada { get; set; }
        public double Puntuacion { get; set; }
        public string Descripcion { get; set; }
        public string equipo1 { get; set; }
        public string equipo2 { get; set; }
        public string resultado1 { get; set; }
        public string resultado2 { get ;  set; }
        public string Versus { get => equipo1+" VS "+equipo2; }
        

        internal static async Task<ObservableCollection<ApuestaModel>> ObtenerJornadaActualAsync()
        {
            try
            {

                using (HttpClient client = new HttpClient())
                {
                    var uri = new Uri(Global.RutaApi + Global.ApiJornada);


                    var vloRespuesta = client.GetAsync(uri).Result;

                    string ans = vloRespuesta.Content.ReadAsStringAsync().Result;

                    var apuesta = JsonConvert.DeserializeObject<ObservableCollection<ApuestaModel>>(ans);

                    return apuesta;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        internal async Task<bool> ApuestaNuevaAsync(string user, ApuestaModel miApuesta, List<EquipoModel> lista)
        {
            
                try
                {
                List<object> listaObjetos = new List<object>();
                listaObjetos.Add(user);
                listaObjetos.Add(miApuesta);
                listaObjetos.Add(lista);

                    using (HttpClient client = new HttpClient())
                    {
                        var uri = new Uri(Global.RutaApi + Global.ApiNuevaApuesta);
                        

                        var vlojson = JsonConvert.SerializeObject(listaObjetos);


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
        }
    }

