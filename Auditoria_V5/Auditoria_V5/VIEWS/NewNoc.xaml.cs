using Auditoria_V5.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Auditoria_V5
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewNoc : ContentPage
	{
        List<string> lista = new List<string>();
        clUbicacion ubicacion;
        private MediaFile Foto;
        string RutaFoto;

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
            pTipoSeriado.Items.Clear();
            pTipoSeriado.Items.Add("L");
            pTipoSeriado.Items.Add("S");
            MessagingCenter.Subscribe<App, string>(this, "Barcode", (sender, arg) =>
            {

                // Add the barcode to a list (first position)
                System.Diagnostics.Debug.WriteLine("Leido en la pagina " + arg);
                NOC_leido(arg);

            });


        }

        private void NOC_leido(string arg)
        {
            if (eNOC.IsFocused)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    eNOC.Text = arg.Replace("\r", "").ToString();
                });
            }
            
            if (eNs.IsFocused)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    eNs.Text = arg.Replace("\r", "").Substring(2);
                });

            }




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
            if (eNs.Text == null || eNs.Text == "")
            {
                return;
            }
            else
            {
                foreach (string item in lista)
                {
                    if (eNs.Text == item)
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
                eNs.Text = "";
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
                
                UbiNoc nocito2 = await App.Database.GetFichAsync(fich);
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
                    ControlUnitario= pTipoSeriado.SelectedItem?.ToString() ?? "",
                    EstUbicacion=ubicacion.EstUbicacion,
                    Uco=nocito2.Uco,
                    DsUco=nocito2.DsUco,
                    FhAuditoria=nocito2.FhAuditoria,
                    DataMining=ubicacion.DataMining



                };

                //Guardar Seriados

                if (lista.Count()>0)
                {
                    foreach (string item in lista)
                    {
                        tNumerosSerie seriado = new tNumerosSerie()
                        {
                            Ubicacion = ubicacion.Ubicacion,
                            Noc = nocito.Noc,
                            NumSerie = item,
                            Check = true,
                            Error = true,
                            Fichero = fich

                        };

                        await App.Database.SaveItemAsync2(seriado);


                    }




                }


                int numnocs= await App.Database.Get_Num_NocsUbi(ubicacion.Ubicacion);
                if (numnocs==0)
                {
                    await App.Database.Del_Ubi(ubicacion.Ubicacion);
                }

                await App.Database.SaveItemAsync(nocito);
                MessagingCenter.Unsubscribe<App, string>(this, "Barcode");
                await Navigation.PopAsync();
            }


        }

        private async void FotoNoc(object sender, EventArgs e)
        {
            if (eNOC.Text != null && eNOC.Text != "")
            {
                string nuevo_noc = ubicacion.Ubicacion + "_" + eNOC.Text + ".jpg";
            //await Navigation.PushAsync(new CamaraView());
            await TomarFotoAsync(nuevo_noc);
            }
            else
            {
                await DisplayAlert("Alert", "Introduzca primero el NOC", "OK");
            }


        }

        public async Task<bool> TomarFotoAsync(string nombrefoto)
        {
            int nErrores = 0;
            try
            {
                Foto = await ServicioFoto.Instancia.CapturarFotoAsync(nombrefoto);
                if (Foto != null)
                {
                    RutaFoto = Foto.Path;
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                nErrores++;
            }
            return (nErrores == 0);
        }



    }
}