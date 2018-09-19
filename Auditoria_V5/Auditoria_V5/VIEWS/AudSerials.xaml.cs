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
	public partial class AudSerials : ContentPage
	{
        tNumerosSerie seriado = new tNumerosSerie();
        UbiNoc ubinoci;
        public AudSerials ()
		{

            InitializeComponent ();
		}

        protected override async void OnAppearing()

        {
            
            ubinoci = (UbiNoc)BindingContext;
            Lista_serial.ItemsSource = await App.Database.GetSerials(ubinoci);

            MessagingCenter.Subscribe<App, string>(this, "Barcode", (sender, arg) =>
            {

                // Add the barcode to a list (first position)
                System.Diagnostics.Debug.WriteLine("Leido en la pagina " + arg);
                serial_leido(arg);

            });
        }

        private async void serial_leido(string arg)
        {
            foreach (tNumerosSerie item in Lista_serial.ItemsSource)
            {
                System.Diagnostics.Debug.WriteLine("Revisando lista en Serials_leido " + item.NumSerie);
                if (arg.Replace("\r", "") == "21" + item.NumSerie)
                {

                    seriado = item;
                    item.Check = true;
                    Lista_serial.ItemsSource = null;
                    Lista_serial.ItemsSource = await App.Database.GetSerials(ubinoci);

                }
            }
        }

        private async void Serial_Click(object sender, SelectedItemChangedEventArgs e)
        {
            seriado = (tNumerosSerie)e.SelectedItem;
            var action = await DisplayAlert("Aviso", "Marcar el NSeriado " + seriado.NumSerie + " como encontrado.", "Si", "No");
            if (action)
            {
                seriado.Check = true;
                await App.Database.SaveItemAsync2(seriado);

                Lista_serial.ItemsSource = null;
                Lista_serial.ItemsSource = await App.Database.GetSerials(ubinoci);
            }
        }

        private async void Fin_Clicked(object sender, EventArgs e)
        {
            var ubinoci = (UbiNoc)BindingContext;
            var action = await DisplayAlert("Aviso", "Los seriados de la ubicacion: " + ubinoci.Ubicacion + " y NOC:" + ubinoci.Noc + " Se marcarán como finalizada, ¿Seguro?", "Si", "No");
            if (!action)
            {
                //await Navigation.PushAsync(new MainPage());

            }
            else
            {

                foreach (tNumerosSerie item in Lista_serial.ItemsSource)
                {
                    // await App.Database.Set_Serial_Done(item);
                }

                MessagingCenter.Unsubscribe<App, string>(this, "Barcode");
                await Navigation.PopAsync();

            }
        }

        private async void Add_Clicked(object sender, EventArgs e)
        {
            //Añadir nuevo Num Serie
            Boolean existe = false;
            foreach (tNumerosSerie item in Lista_serial.ItemsSource)
            {
                if (eNewSerial.Text == item.NumSerie)
                {
                    existe = true;
                }
            }

            if (existe == false)
            {
                
                tNumerosSerie Nuevo = new tNumerosSerie()
                {
                    Ubicacion=ubinoci.Ubicacion,
                    Noc=ubinoci.Noc,
                    NumSerie= eNewSerial.Text,
                    Check=true,
                    Error=true,
                    Fichero=ubinoci.Fichero
                };
                await App.Database.SaveItemAsync2(Nuevo);


                Lista_serial.ItemsSource = null;
                Lista_serial.ItemsSource = await App.Database.GetSerials(ubinoci);
                // grpNs.PropertyChanged
            }
        }
    }
}