using App3.Model;
using App3.View;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App3.ViewModel
{
    class PersonaViewModel : INotifyPropertyChanged
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
        public string _Contraseña { get; set; }
        public string Contraseña
        {
            get
            {
                return _Contraseña;
            }
            set
            {
                _Contraseña = value;
                OnPropertyChanged("Contraseña");
            }
        }
        public string _Imagen { get; set; }
        public string Imagen
        {
            get
            {
                return _Imagen;
            }
            set
            {
                _Imagen = value;
                OnPropertyChanged("Imagen");
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
            try
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
            var stream = file.GetStream();
            var bytes = new byte[stream.Length];
            await stream.ReadAsync(bytes, 0, (int)stream.Length);
            this.Imagen = System.Convert.ToBase64String(bytes);

            }
            catch (Exception)
            {

                throw;
            }
        }

        private async void CargarImagenAsync(object obj)
        {
            try
            {

            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                
                var file = await CrossMedia.Current.PickPhotoAsync();
                var stream = file.GetStream();
                Image a = new Image();
                
                var bytes = new byte[stream.Length];
                await stream.ReadAsync(bytes, 0, (int)stream.Length);
                this.Imagen = System.Convert.ToBase64String(bytes);
                
            }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void CrearUsuario()
        {
            if (ValidaCampos())
            {
                var personaNueva = new PersonaModel
                {
                    Id = this.IdUser,
                    Nombre = this.Nombre,
                    Apellidos = this.Apellido,
                    Contraseña = this.Contraseña,
                    FotoPerfil = this.Imagen
                };
                if (new PersonaModel().CrearUsuarioAsync(personaNueva).Result)
                {
                    Application.Current.MainPage.DisplayAlert("Usuario Creado", "El usuario ha sido creado correctamente", "OK");
                    App.Current.MainPage = new App3.View.Login();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Usuario Error", "Error al crear el usuario compruebe que el usuario no existe", "OK");
                }
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Verificar Datos", "Verifique que todos los datos esten correctos", "OK");
            }


            //Application.Current.MainPage.DisplayAlert("UsuarioCreado", "El Usuario fue creado correctamente", "OK");
            //

        }

        /// <summary>
        ///Valida que esten cargado los campos
        /// </summary>
        /// <returns></returns>
        private bool ValidaCampos()
        {
            if (string.IsNullOrEmpty(this.Nombre))
            {
                return false;
            }
            else if (string.IsNullOrEmpty(this.Contraseña))
            {
                return false;
            }
            else if (this.Imagen == null)
            {
                return false;
            }
            return true;
        }

        private void Login()
        {
            try
            {

            var personaNueva = new PersonaModel
            {
                Id = this.IdUser,
                Contraseña = this.Contraseña
            };
                if (new PersonaModel().Login(personaNueva))
                {
                    NavigationPage navigation = new NavigationPage(new HomePage());

                    App.Current.MainPage = new MasterDetailPage
                    {
                        Master = new HomeMenu(),
                        Detail = navigation
                    };
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Usuario Error", "Error de credenciales", "OK");
                }

            }
            catch (Exception)
            {

                throw;
            }
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
            App.Current.MainPage = new App3.View.NuevoUsuario();
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
