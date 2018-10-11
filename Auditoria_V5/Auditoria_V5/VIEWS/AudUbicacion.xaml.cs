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
                if (arg.Replace("\r", "").Length == 13)
                {
                    main_leido(arg);
                }

            });


        }


        protected override async void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<App, string>(this, "Barcode");
            //await Navigation.PopAsync();
        }

        private  void main_leido(string arg)
        {
            foreach (UbiNoc item in listView.ItemsSource)
            {
                if (arg.Replace("\r", "") == item.Noc)
                {
                    listView.SelectedItem = ((List<UbiNoc>)listView.ItemsSource).Where(x => x.Noc == arg.Replace("\r", "")).FirstOrDefault();

                    var index = listView.SelectedItem;
                    foreach (ViewCell myViewCell in listView.TemplatedItems)
                    {
                        var entry = myViewCell.FindByName<Entry>("eCant");
                        var noci = myViewCell.FindByName<Label>("eNOC");
                        if (noci.Text == item.Noc)
                        {
                            // myViewCell.View.BackgroundColor= this.BackgroundColor;

                            Device.BeginInvokeOnMainThread(() =>
                            {
                                entry.CursorPosition = 1;
                            });
                            entry?.Focus();
                            
                           
                        }
                        //else { myViewCell.View.BackgroundColor = this.BackgroundColor; }


                    }

                }

                else
                {
                    listView.SelectedItem = null;
                }
            }

        }

    

        private async void Button_Clicked(object sender, EventArgs e)
            {
                await Navigation.PushAsync(new SelBth());
            }
        

        private async void listaserial_Clicked(object sender, EventArgs e)
        {
            auditado = (UbiNoc)((Button)sender).BindingContext;

            await Navigation.PushAsync(new AudSerials()
            {
                BindingContext = auditado
            });

            
        }

        private async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            auditado = (UbiNoc)e.SelectedItem;
            if (auditado.ControlUnitario == null || auditado.ControlUnitario == "")
            {

                Device.BeginInvokeOnMainThread(() =>
                {
                    lCant.Text = auditado.Cantidad.ToString();
                });
            }
            else
            {
                var a= await App.Database.GetNumsSerie(auditado);
                Device.BeginInvokeOnMainThread(() =>
                {
                    lCant.Text = a.ToString() + "/" + auditado.Cantidad.ToString();
                });
            }

            Device.BeginInvokeOnMainThread(() =>
            {
                eObs.IsEnabled = true;
                eObs.Text = auditado.Obs;
            });
        }

        private async void Entry_Completed(object sender, EventArgs e)
        {
            var texto = ((Entry)sender).Text;
            auditado = (UbiNoc)((Entry)sender).BindingContext;
            Double CuentaNumerosSerie;

            if (auditado.ControlUnitario != null)
            {
                var a = await App.Database.GetNumsSerie(auditado);
                CuentaNumerosSerie = Convert.ToDouble(a);
            }
            else
            {
                CuentaNumerosSerie = 0;
            }



            if ((auditado.ControlUnitario == null && auditado.Cantidad != Convert.ToDouble(texto))|| (auditado.ControlUnitario == "L" && auditado.Cantidad != Convert.ToDouble(texto)) || (auditado.ControlUnitario == "" && auditado.Cantidad != Convert.ToDouble(texto)))

            {

                if (await DisplayAlert("", "Cantidad No concuerda, Confirmamos??", "Si", "No"))
                {

                        auditado.Error = true;
                        auditado.Check = true;

                    auditado.CantReal = Convert.ToDouble(texto);
                    await App.Database.SaveItemAsync(auditado);
                    listView.ItemsSource = null;
                    listView.ItemsSource = await App.Database.GetUbiNoc(auditado.Ubicacion); 


                }
                else
                {
                    return;
                }
            }
            else
            {

                if (auditado.ControlUnitario == null || auditado.ControlUnitario == "L" || auditado.ControlUnitario == "")
                {
                    auditado.Error = false;
                    auditado.Check = true;
                    auditado.CantReal = Convert.ToDouble(texto);
                    await App.Database.SaveItemAsync(auditado);
                    listView.ItemsSource = null;
                    listView.ItemsSource = await App.Database.GetUbiNoc(auditado.Ubicacion);
                }
                else
                {
                    if (auditado.ControlUnitario!=null && auditado.ControlUnitario != "L" &&  auditado.ControlUnitario != "" && CuentaNumerosSerie != Convert.ToDouble(texto))
                    {
                        if (await DisplayAlert("", "Cantidad No concuerda, Confirmamos??", "Si", "No"))
                        {

                            auditado.Error = true;
                            auditado.Check = true;

                            auditado.CantReal = Convert.ToDouble(texto);
                            await App.Database.SaveItemAsync(auditado);
                            listView.ItemsSource = null;
                            listView.ItemsSource = await App.Database.GetUbiNoc(auditado.Ubicacion);


                        }
                        else
                        {
                            return;
                        }


                    }
                    else
                    {
                        auditado.Error = false;
                        auditado.Check = true;
                        auditado.CantReal = Convert.ToDouble(texto);
                        await App.Database.SaveItemAsync(auditado);
                        listView.ItemsSource = null;
                        listView.ItemsSource = await App.Database.GetUbiNoc(auditado.Ubicacion);



                    }



                }
                
               
            }
            

            //listView.SelectedItem = null;

        }

        private async void Fin_Clicked(object sender, EventArgs e)
        {
            clUbicacion ubicacion = (clUbicacion)BindingContext;
            //List<UbiNoc> milista = await App.Database.GetUbiNoc(ubicacion.Ubicacion);
            int numNoc = await App.Database.Get_Num_NocsUbi(ubicacion.Ubicacion);
            var action = await DisplayAlert("Aviso", "La ubicacion: " + ubicacion.Ubicacion + " Se marcará como finalizada, ¿Seguro?", "Si", "No");
            
            if (!action)
            {
                //await Navigation.PushAsync(new MainPage());

            }
            else
            {
                //Si queremos marcar como error todas aquellas que no se han revisado una a una
                //foreach (UbiNoc item in milista)
                //{

                //    if(item.Check==false && item.CantReal==0)
                //    {
                //        await App.Database.Set_UbiNoc_eror(item);
                //    }


                //}
                if (numNoc==0)
                {
                    await App.Database.Set_Ubi_Done_Vacia(ubicacion.Ubicacion);
                }

               

                await App.Database.Set_Ubi_Done(ubicacion.Ubicacion);
                MessagingCenter.Unsubscribe<App, string>(this, "Barcode");
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