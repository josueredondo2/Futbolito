﻿using App3.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NuevoUsuario : ContentPage
	{
		public NuevoUsuario ()
		{
			InitializeComponent ();
            BindingContext = PersonaViewModel.GetInstance();
        }
	}
}