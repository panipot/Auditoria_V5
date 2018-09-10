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
	public partial class AudUbicacion : ContentPage
	{
        UbiNoc auditado = new UbiNoc();

        public AudUbicacion ()
		{
			InitializeComponent ();
		}


        protected override async void OnAppearing()
        {


            base.OnAppearing();
           //////////// listView.ItemTapped += OnTapEventAsync;
            clUbicacion ubicacion = (clUbicacion)BindingContext;
            this.Title = ubicacion.Ubicacion.Substring(0, 3) + "-" + ubicacion.Ubicacion.Substring(3, 2) + "-" + ubicacion.Ubicacion.Substring(5, 2) + "-" + ubicacion.Ubicacion.Substring(7, 2) + "-" + ubicacion.Ubicacion.Substring(9, 2);
            listView.ItemsSource = await App.Database.GetUbiNoc(ubicacion.Ubicacion);

            MessagingCenter.Subscribe<App, string>(this, "Barcode", (sender, arg) => {

                // Add the barcode to a list (first position)
                System.Diagnostics.Debug.WriteLine("Leido en la pagina " + arg);
                //////////main_leido(arg);

            });


        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        private void listaserial_Clicked(object sender, EventArgs e)
        {

        }

        private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private void Entry_Completed(object sender, EventArgs e)
        {

        }

        private void Fin_Clicked(object sender, EventArgs e)
        {

        }

        private void Add_Clicked(object sender, EventArgs e)
        {

        }
    }
}