using Auditoria_V5.DATA;
using Auditoria_V5;
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
	public partial class ListaAuditorias : ContentPage
	{
		public ListaAuditorias ()
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
                auditoria.Fichero=item.Fichero;

                milista.Add(auditoria);
            }


            Lista_aud.ItemsSource = milista;

        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {


            await Navigation.PushAsync(
                   new Selecc_Op
                   {
                       BindingContext = e.SelectedItem as ClAuditoria2
                   }
                   );
        }
    }
}