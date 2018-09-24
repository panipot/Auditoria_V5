using Auditoria_V5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Auditoria_V5
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            MuestraBtn();
        }

        async void MuestraBtn()
        {
            try
            {
                FileHelper fileHelper = new FileHelper();
                if (fileHelper.Exists("UbicNoc.db3"))
               
                {
                    var esta2 = await App.Database.GetItemAsync2();
                    if (esta2.Ubicacion != null)
                    {
                        btnContinua.IsEnabled = true;
                        btnResults.IsEnabled = true;
                    }

                }

            }
            catch
            {
                btnContinua.IsEnabled = false;
            }
        }

        private async void onBtnStart(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Selecc_File());
        }

        private async void onBtnContinue(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListaAuditorias());
        }

        private async void onResults(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Resultados());
        }
    }
}
