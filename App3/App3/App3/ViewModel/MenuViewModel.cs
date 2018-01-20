using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace App3.ViewModel
{
     class MenuViewModel 
    {
        #region Singleton
        private static MenuViewModel instance = null;

        private MenuViewModel()
        {
            //InitClass();
            InitCommands();
        }

        public static MenuViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new MenuViewModel();
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

        private void InitCommands()
        {
            LoginAbreApuestaCommand = new Command(AbreApuesta);
            LoginAbreResultadoCommand = new Command(AbreResultado);
            LoginAbreGlobalCommand = new Command(AbreGlobal);
            LoginAbreOpcionesCommand = new Command(AbreOpciones);
        }


        #endregion
        #region Comandos
        public ICommand LoginAbreApuestaCommand { get; set; }
        public ICommand LoginAbreResultadoCommand { get; set; }
        public ICommand LoginAbreGlobalCommand { get; set; }
        public ICommand LoginAbreOpcionesCommand { get; set; }
        #endregion

        #region Metodos
        //Abre la pantalla de apuestas
        private void AbreApuesta()
        {
            ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new View.Juegos());
        }
        private void AbreResultado()
        {
            ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new View.Juegos());
        }
        private void AbreGlobal()
        {
            ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new View.Juegos());
        }
        private void AbreOpciones()
        {
            ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new View.Juegos());
        }
        #endregion
    }
}
