using Futbolito.View;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Futbolito.ViewModel
{
    public class PersonaViewModel : INotifyPropertyChanged
    {
        #region Singleton
        private static PersonaViewModel instance = null;

        private PersonaViewModel()
        {
            InitClass();
            InitCommands();
        }

        public static PersonaViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new PersonaViewModel();
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
        #region Propiedades
        public string _IdUser { get; set; }
        public string IdUser
        {
            get
            {
                return _IdUser;
            }
            set
            {
                _IdUser = value;
                OnPropertyChanged("IdUser");
            }
        }
        public string _Nombre { get; set; }
        public string Nombre
        {
            get
            {
                return _Nombre;
            }
            set
            {
                _Nombre = value;
                OnPropertyChanged("Nombre");
            }
        }
        public string _Apellido { get; set; }
        public string Apellido
        {
            get
            {
                return _Apellido;
            }
            set
            {
                _Apellido = value;
                OnPropertyChanged("Apellido");
            }
        }
        #endregion
        #region Commands

        public ICommand LoginCommand { get; set; }
        public ICommand NewUserCommand { get; set; }

        public ICommand CrearUsuarioCommand { get; set; }

        public ICommand CargarImagenCommand { get; set; }
        public ICommand TomarImagenCommand { get; set; }

        #endregion
        #region Metodos
        private async Task InitClass()
        {

            //lstPersonas = await PersonaModel.ObtenerPersonas();

            //lstOriginalPersonas = lstPersonas.ToList();

            //lstArticulos = await ArticuloModel.ObtenerArticulos();
        }
        private void InitCommands()
        {
            LoginCommand = new Command(Login);
            NewUserCommand = new Command(AbrirCrearUsuario);
            CrearUsuarioCommand = new Command(CrearUsuario);
            CargarImagenCommand = new Command(CargarImagenAsync);
            TomarImagenCommand = new Command(TomarImagenAsync);
        }

        private async void TomarImagenAsync(object obj)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg"
            });
        }

        private async void CargarImagenAsync(object obj)
        {

            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                var photo = await CrossMedia.Current.PickPhotoAsync();
            }
        }

        private void CrearUsuario()
        {
             Application.Current.MainPage.DisplayAlert("UsuarioCreado","El Usuario fue creado correctamente","OK");
             App.Current.MainPage = new Futbolito.View.NuevoUsuario();

        }

        private void Login()
        {
            //LoginModel login = LoginModel.ObtenerUsuarios().Result.Where(x => x.Id == IdUsuario && x.Pass == Pass).FirstOrDefault();

            //if (login != null)
            //{
            //    NavigationPage navigation = new NavigationPage(new HomePage());

            //    App.Current.MainPage = new MasterDetailPage
            //    {
            //        Master = new HomeMenu(),
            //        Detail = navigation
            //    };
            //}
            //else
            //{
            //    Resultado = "Usuario o contraseña incorrecta.";
            //}

            //NavigationPage navigation = new NavigationPage(new HomePage());

            //App.Current.MainPage = new MasterDetailPage
            //{
            //    Master = new HomeMenu(),
            //    Detail = navigation
            //};



        }

        private void AbrirCrearUsuario()
        {
            App.Current.MainPage = new Futbolito.View.NuevoUsuario();
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
