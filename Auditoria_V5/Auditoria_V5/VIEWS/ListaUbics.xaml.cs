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
	public partial class ListaUbics : ContentPage
	{

        clUbicacion bueno;
        public ListaUbics (ClAuditoria2 auditoria)
		{
            this.BindingContext = auditoria;
            InitializeComponent();

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var auditoria = (ClAuditoria2)BindingContext;

            listView.ItemsSource = await App.Database.GetUbicsFich(auditoria);
            MessagingCenter.Subscribe<App, string>(this, "Barcode", (sender, arg) =>
            {

                // Add the barcode to a list (first position)
                System.Diagnostics.Debug.WriteLine("Leido en la pagina " + arg);
                ubic_leido(arg);

            });

        }

        private async void ubic_leido(string arg)
        {
           
            foreach (clUbicacion ubicacion in listView.ItemsSource)
            {
                if (arg.Replace("\r", "") == ubicacion.Ubicacion)
                {
                    // listView.SelectedItem = ((List<clUbicacion>)listView.ItemsSource).Where(x => x.Ubicacion == arg.Replace("\r", "")).FirstOrDefault();
                   bueno = ubicacion;
                   break;
                }

            }

            Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PushAsync(
               new AudUbicacion()
               {
                   BindingContext = bueno

               });
            });

        }




        private  async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            var ubicacion2 = (clUbicacion)e.SelectedItem;
            //MessagingCenter.Unsubscribe<App, string>(this, "Barcode");

         
           await      Navigation.PushAsync(
                    new AudUbicacion()
                    {
                        BindingContext = ubicacion2
                    });
            

        }
    }
}