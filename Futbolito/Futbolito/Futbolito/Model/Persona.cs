using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Futbolito.Model
{
    public class Persona
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public Image FotoPerfil { get; set; }
        public ObservableCollection<Apuesta> LstApuestas{ get; set; }

    }
}
