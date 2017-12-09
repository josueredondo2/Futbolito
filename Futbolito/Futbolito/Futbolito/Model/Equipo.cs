using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Futbolito.Model
{
    public class Equipo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Image Imagen { get; set; }
        public int Marcador { get; set; }

    }
}
