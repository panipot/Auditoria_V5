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

        private void Lista_Clicked(object sender, EventArgs e)
        {

        }

        private void Almacen_Clicked(object sender, EventArgs e)
        {

        }

        private void Export_Clicked(object sender, EventArgs e)
        {
            DATA.ExportDb expo = new DATA.ExportDb();
            ClAuditoria2 auditoria = (ClAuditoria2)BindingContext;
            expo.Exporta("ALMACEN_" + auditoria.Almacen + ".txt", "UbicNoc.db3");
            Navigation.PopAsync();
        }
    }
}