﻿using Auditoria_V5.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Auditoria_V5
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListaUbicsFiltr : ContentPage
	{
		public ListaUbicsFiltr (List<clUbicacion> milista)
		{
			InitializeComponent ();
            listView.ItemsSource = milista;
		}

        private async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var ubicacion2 = (clUbicacion)e.SelectedItem;

            await Navigation.PushAsync(
                    new AudUbicacion()
                    {
                        BindingContext = ubicacion2
                    });
        }
    }
}