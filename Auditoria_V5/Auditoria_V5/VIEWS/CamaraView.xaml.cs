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
    public partial class CamaraView : ContentPage
    {
        public CamaraView()
        {
            InitializeComponent();
            this.BindingContext = new CamaraViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}