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
	public partial class Resultados : ContentPage
	{
		public Resultados ()
		{
			InitializeComponent ();
           
          


		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            List<ClAuditoria2> milista = new List<ClAuditoria2>();

            List<clFicheros> ficheros = await App.Database.GetFicheros();

            foreach (clFicheros item in ficheros)
            {
                ClAuditoria2 auditoria = new ClAuditoria2();
                auditoria.Fichero = item.Fichero;

                milista.Add(auditoria);
            }


            Lista_resultados.ItemsSource = milista;

        }
    }
}