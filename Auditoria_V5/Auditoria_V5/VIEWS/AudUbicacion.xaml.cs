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
	public partial class AudUbicacion : ContentPage
	{
        UbiNoc auditado = new UbiNoc();

        public AudUbicacion ()
		{
			InitializeComponent ();
            eObs.IsEnabled = false;
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

  
            private async void Button_Clicked(object sender, EventArgs e)
            {
                await Navigation.PushAsync(new SelBth());
            }
        

        private void listaserial_Clicked(object sender, EventArgs e)
        {

        }

        private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            auditado = (UbiNoc)e.SelectedItem;
            eObs.IsEnabled = true;
            eObs.Text = auditado.Obs;

        }

        private async void Entry_Completed(object sender, EventArgs e)
        {
            var texto = ((Entry)sender).Text;
            auditado = (UbiNoc)((Entry)sender).BindingContext;

            if (auditado.Cantidad != Convert.ToDouble(texto))

            {

                if (await DisplayAlert("", "Cantidad No concuerda, Confirmamos??", "Si", "No"))
                {
                    if (auditado.ControlUnitario == null)
                    {
                        auditado.Error = true;
                        auditado.Check = true;
                    }
                    else
                    {
                        //ABRIMOS VERIFICACION DE SERIADOS
                    }

                    auditado.CantReal = Convert.ToDouble(texto);
                    await App.Database.SaveItemAsync(auditado);



                }
                else
                {
                    return;
                }
            }
            else
            {
                auditado.Error = false;
                auditado.CantReal = Convert.ToDouble(texto);
                await App.Database.SaveItemAsync(auditado);
                if (auditado.ControlUnitario != null)
                {
                    //ABRIMOS VERIFICACION DE SERIADOS
                }




            }

            //listView.SelectedItem = null;

        }

        private async void Fin_Clicked(object sender, EventArgs e)
        {
            clUbicacion ubicacion = (clUbicacion)BindingContext;
            var action = await DisplayAlert("Aviso", "La ubicacion: " + ubicacion.Ubicacion + " Se marcará como finalizada, ¿Seguro?", "Si", "No");
            if (!action)
            {
                //await Navigation.PushAsync(new MainPage());

            }
            else
            {
                await App.Database.Set_Ubi_Done(ubicacion.Ubicacion);
                await Navigation.PopAsync();
            }
        }




        private async void Add_Clicked(object sender, EventArgs e)
        {
            clUbicacion ubicacion = (clUbicacion)BindingContext;
            await Navigation.PushAsync(new NewNoc()
            {
                BindingContext = ubicacion
            }
                );
        }





        private void eCant_Focused(object sender, FocusEventArgs e)
        {
            auditado = (UbiNoc)((Entry)sender).BindingContext;
            listView.SelectedItem = auditado;
            eObs.Text = auditado.Obs;
        }

        private async void eObs_completed(object sender, EventArgs e)
        {
            listView.SelectedItem = auditado;

            await App.Database.SaveItemObs(auditado, eObs.Text);


        }
    }
}