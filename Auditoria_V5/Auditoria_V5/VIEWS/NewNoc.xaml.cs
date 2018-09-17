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
        clUbicacion ubicacion;

        public NewNoc ()
		{
			InitializeComponent ();
		}

        protected override  void OnAppearing()
        {


            base.OnAppearing();
            
            grpNs.ItemsSource = lista;
           ubicacion = (clUbicacion)BindingContext;
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
                grpNs.ItemsSource = null;
                grpNs.ItemsSource = lista;
               // grpNs.PropertyChanged
            }
            

        }

        private async void grpNs_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            var action=await DisplayAlert("Elige:", "Quieres Eliminar el Nº "+ e.Item.ToString() + " de la lista??", "YES", "NO");
            if(!action)
            {
                return;
            }
            else
            {
                lista.Remove(e.Item.ToString());
                grpNs.ItemsSource = null;
                grpNs.ItemsSource = lista;
            }

        }

        private async void bFin_clicked(object sender, EventArgs e)
        {
            var action = await DisplayAlert("Finalizar Nuevo NOC", "Quieres grabar el NOC??", "YES", "NO");
            if (!action)
            {
                return;
            }
            else
            {
                UbiNoc nocito;
                var fich = await App.Database.GetFich(ubicacion.Ubicacion);
                nocito = new UbiNoc()
                {
                    Noc = eNOC.Text,
                    Cantidad = 0,
                    CantReal = Convert.ToDouble(eCant.Text),
                    Ubicacion = ubicacion.Ubicacion,
                    Descripcion = eDesc.Text,
                    Error = true,
                    Check = true,
                    Obs = "Nuevo NOC encontrado",
                    Fichero=fich,
                    ControlUnitario="",
                    EstUbicacion="2"



                };

                //Guardar Seriados
                await App.Database.SaveItemAsync(nocito);
                //await Navigation.PopAsync();
            }


        }
    }
}