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
	public partial class FiltroUbics : ContentPage
	{

        List<clUbicacion> Ubicaciones = new List<clUbicacion>();
        public FiltroUbics (ClAuditoria2 auditoria)
		{
			InitializeComponent ();
            this.BindingContext = auditoria;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var auditoria = (ClAuditoria2)BindingContext;
            
            Ubicaciones = await App.Database.GetUbicsFich(auditoria);
            List<string> zonas = new List<string>();
            pPasillo.IsEnabled = false;

            foreach (clUbicacion ubicacion in Ubicaciones)
            {
                zonas.Add(ubicacion.Zona);
            }

            pZona.ItemsSource = zonas.Distinct().ToList();
            pDm.Items.Add("Si");
            pDm.Items.Add("No");


            // }



            //pPasillo.ItemsSource = await App.Database.GetPasillos(auditoria);
            //pDm.ItemsSource = await App.Database.GetDm(auditoria);

        }




        private void Filtrar_Clicked(object sender, EventArgs e)
        {

        }

        private void pZona_SelectedIndexChanged(object sender, EventArgs e)
        {
            pPasillo.IsEnabled = true;
            List<string> pasillos = new List<string>();
            
            var ubicaciones2 = Ubicaciones.Where(x => x.Zona == pZona.SelectedItem.ToString());



            foreach (clUbicacion ubicacion in ubicaciones2)
            {
                pasillos.Add(ubicacion.Pasillo);
            }
            pPasillo.ItemsSource = pasillos.Distinct().ToList();

        }
    }
}