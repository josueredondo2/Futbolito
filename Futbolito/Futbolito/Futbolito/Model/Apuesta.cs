using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Futbolito.Model
{
    public class Apuesta
    {
        public int Id { get; set; }
        public string Jornada { get; set; }
        public bool Finalizada { get; set; }
        public double Puntuacion{ get; set; }
        public string Descripcion { get; set; }
        public bool Ganador { get; set; }
        public ObservableCollection<Equipo> LstApuestaEquipos{ get; set; }
        public ObservableCollection<Equipo> LstApuestaResultado{ get; set; }

    }
}
