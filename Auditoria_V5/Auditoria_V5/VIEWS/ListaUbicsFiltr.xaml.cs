using Auditoria_V5.DATA;
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
        clUbicacion bueno;
        Boolean oki;
        List<clUbicacion> milista;
        public ListaUbicsFiltr (List<clUbicacion> milista2)
		{
            milista = milista2;
            InitializeComponent ();
            listView.ItemsSource = milista2;
            
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            bueno = new clUbicacion();
            List<clUbicacion> milista2= new List<clUbicacion>();

            foreach (clUbicacion item in listView.ItemsSource)
            {
                milista2.Add(item);
            }

            clListaUbics arch = new clListaUbics();
            milista = await arch.Actualiza_lista_ubics(milista2);
            listView.ItemsSource = milista;


            MessagingCenter.Subscribe<App, string>(this, "Barcode", (sender, arg) =>
            {

                // Add the barcode to a list (first position)
                System.Diagnostics.Debug.WriteLine("Leido en la pagina " + arg);
                if (arg.Replace("\r", "").Length == 11)
                { ubic_leido(arg); }

            });

        }

        private async void ubic_leido(string arg)
        {
            oki = false;
            foreach (clUbicacion ubicacion in listView.ItemsSource)
            {
                if (arg.Replace("\r", "") == ubicacion.Ubicacion)
                {
                    // listView.SelectedItem = ((List<clUbicacion>)listView.ItemsSource).Where(x => x.Ubicacion == arg.Replace("\r", "")).FirstOrDefault();
                    bueno = ubicacion;
                    oki = true;
                    break;

                }

            }

            if (bueno.Ubicacion != null && oki == true)
            {
                MessagingCenter.Unsubscribe<App, string>(this, "Barcode");
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PushAsync(
                   new AudUbicacion()
                   {
                       BindingContext = bueno

                   });
                });
               
            }
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