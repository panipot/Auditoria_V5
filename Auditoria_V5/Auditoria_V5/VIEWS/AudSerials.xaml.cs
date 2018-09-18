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
        public AudSerials(UbiNoc ubinoci)
        {
            this.BindingContext = ubinoci;
            InitializeComponent();

        }

        protected override async void OnAppearing()

        {

            var ubinoci = (UbiNoc)BindingContext;
            Lista_serial.ItemsSource = await App.Database.GetSerials(ubinoci);

            MessagingCenter.Subscribe<App, string>(this, "Barcode", (sender, arg) =>
            {

                // Add the barcode to a list (first position)
                System.Diagnostics.Debug.WriteLine("Leido en la pagina " + arg);
                serial_leido(arg);

            });
        }

        private void serial_leido(string arg)
        {
            foreach (tNumerosSerie item in Lista_serial.ItemsSource)
            {
                System.Diagnostics.Debug.WriteLine("Revisando lista en Serials_leido " + item.NumSerie);


                if (arg.Replace("\r", "") == "21" + item.NumSerie)
                    item.Check = true;
                Lista_serial.SelectedItem = ((List<tNumerosSerie>)Lista_serial.ItemsSource).Where(x => x.NumSerie == arg.Replace("\r", "")).FirstOrDefault();
                {
                    foreach (ViewCell myViewCell in Lista_serial.TemplatedItems)
                    {
                        var noci = myViewCell.FindByName<Label>("lNumSerie");
                        var ima = myViewCell.FindByName<Image>("iCheck");
                        if (noci.Text == item.NumSerie)
                        {
                            // myViewCell.View.BackgroundColor= this.BackgroundColor;


                            ima.IsVisible = true;
                        }

                    }

                }
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

        private void Add_Clicked(object sender, EventArgs e)
        {

        }
    }
}