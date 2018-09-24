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
	public partial class Selecc_Op : ContentPage
	{
		public Selecc_Op ()
		{
			InitializeComponent ();
		}

        async void Lista_Clicked(object sender, EventArgs e)
        {
            ClAuditoria2 auditoria = (ClAuditoria2)BindingContext;

            await Navigation.PushAsync(new ListaUbics(auditoria));
        }

        async void Almacen_Clicked(object sender, EventArgs e)
        {
            clListaUbics arch = new clListaUbics();
            List<clUbicacion> lista = new List<clUbicacion>();
            var auditoria = (ClAuditoria2)BindingContext;

            lista = await arch.Rellena_lista_ubics(auditoria);


            await Navigation.PushAsync(new ListaUbicsFiltr(lista));
        }

        private void Export_Clicked(object sender, EventArgs e)
        {
            DATA.ExportDb expo = new DATA.ExportDb();
            ClAuditoria2 auditoria = (ClAuditoria2)BindingContext;
            expo.Exporta("ALMACEN_" + auditoria.Almacen + ".txt", "UbicNoc.db3");
            Navigation.PopAsync();
            Navigation.PopAsync();
        }

        

        async void Filtro_Clicked(object sender, EventArgs e)
        {
            ClAuditoria2 auditoria = (ClAuditoria2)BindingContext;

            await Navigation.PushAsync(new FiltroUbics(auditoria));
        }
    }
}