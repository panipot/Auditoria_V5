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
	public partial class NewNoc : ContentPage
	{
        List<string> lista = new List<string>();
        public NewNoc ()
		{
			InitializeComponent ();
		}

        protected override  void OnAppearing()
        {


            base.OnAppearing();
            
            grpNs.ItemsSource = lista;
            clUbicacion ubicacion = (clUbicacion)BindingContext;
            this.Title = ubicacion.Ubicacion.Substring(0, 3) + "-" + ubicacion.Ubicacion.Substring(3, 2) + "-" + ubicacion.Ubicacion.Substring(5, 2) + "-" + ubicacion.Ubicacion.Substring(7, 2) + "-" + ubicacion.Ubicacion.Substring(9, 2);
        }

        private async void eNoc_completed(object sender, EventArgs e)
        {
            var texto = ((Entry)sender).Text;
            if (texto.ToString().Length!=13)
            {
                eNOC.Text = "";
                await DisplayAlert("ALERTA", "NOC introducido no tiene 13 caracteres", "OK");
            }


        }

        private void bAdd_clicked(object sender, EventArgs e)
        {
            Boolean existe = false;
            foreach (string item in lista)
            {
                if (eNs.Text==item)
                {
                    existe = true;
                }
            }

            if (existe == false)
            {
                lista.Add(eNs.Text);
                grpNs.ItemsSource = lista;
               // grpNs.PropertyChanged
            }
            

        }
    }
}