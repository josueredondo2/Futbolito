using App3.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App3.ViewModel
{
    public class JuegoViewModel : INotifyPropertyChanged
    {
        #region Singleton
        private static JuegoViewModel instance = null;

        public JuegoViewModel()
        {
            InitClass();
            InitCommands();
        }

        public static JuegoViewModel GetInstance(JuegoViewModel viewModel =null)
        {
            if (instance == null)
            {
                instance = new JuegoViewModel();
                if (viewModel!=null)
                {
                    instance = viewModel;
                }
            }
            return instance;
        }

        public static void DeleteInstance()
        {
            if (instance != null)
            {
                instance = null;
            }
        }
        #endregion

        #region Instances


        private ObservableCollection<ApuestaModel> _lstApuestas = new ObservableCollection<ApuestaModel>();

        public ObservableCollection<ApuestaModel> lstApuestas
        {
            get
            {
                return _lstApuestas;
            }
            set
            {
                _lstApuestas = value;
                OnPropertyChanged("lstApuestas");
            }
        }


        private ApuestaModel _miApuesta = new ApuestaModel();


        public ApuestaModel miApuesta
        {
            get
            {
                return _miApuesta;
            }
            set
            {
                _miApuesta = value;
                OnPropertyChanged("miApuesta");
            }
        }

        private ObservableCollection<EquipoModel> _lstEquipos = new ObservableCollection<EquipoModel>();

        public ObservableCollection<EquipoModel> lstEquipos
        {
            get
            {
                return _lstEquipos;
            }
            set
            {
                _lstEquipos = value;
                OnPropertyChanged("lstEquipos");
            }
        }

        private EquipoModel _miEquipo1 = new EquipoModel();
        public EquipoModel miEquipo1
        {
            get
            {
                return _miEquipo1;
            }
            set
            {
                _miEquipo1 = value;
                OnPropertyChanged("miEquipo1");
            }
        }


        private EquipoModel _miEquipo2 = new EquipoModel();
        public EquipoModel miEquipo2
        {
            get
            {
                return _miEquipo2;
            }
            set
            {
                _miEquipo2 = value;
                OnPropertyChanged("miEquipo2");
            }
        }




        public ICommand AgregarPersonaCommand { get; set; }
        public ICommand AbrirApuestaCommand { get; set; }
        public ICommand RealizarApuestaCommand { get; set; }


        #endregion

        #region Methods





        private async Task InitClass()
        {

            lstApuestas = await ApuestaModel.ObtenerJornadaActualAsync();

        }

        private void InitCommands()
        {
            AgregarPersonaCommand = new Command(AgregarPersona);
            RealizarApuestaCommand = new Command(RealizarApuesta);
            AbrirApuestaCommand = new Command<int>(AbrirApuesta);

        }

        private void RealizarApuesta(object obj)
        {
            List<EquipoModel> lista = new List<EquipoModel>();
            lista.Add(miEquipo1);
            lista.Add(miEquipo2);
            new ApuestaModel().ApuestaNuevaAsync(Global.user, miApuesta, lista);
        }

        private void AbrirApuesta(int id)
        {
            miApuesta = lstApuestas.Where(x => x.Id.Equals(id)).FirstOrDefault();

            lstEquipos= EquipoModel.ObtenerEquipos(miApuesta);

            miEquipo1 = lstEquipos.First();
            miEquipo2 = lstEquipos.Last();

            ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new View.AbrirApuesta(this));
        }

        private void AgregarPersona(object obj)
        {
            throw new NotImplementedException();
        }

        #endregion
        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) // if there is any subscribers 
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
