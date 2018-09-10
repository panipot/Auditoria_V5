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
	public partial class ListaUbics : ContentPage
	{
		public ListaUbics (ClAuditoria2 auditoria)
		{
            this.BindingContext = auditoria;
            InitializeComponent();

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var auditoria = (ClAuditoria2)BindingContext;

            listView.ItemsSource = await App.Database.GetUbicsFich(auditoria);


        }

        private void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}