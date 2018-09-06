using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Auditoria_V5.VIEWS
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Selecc_File : ContentPage
	{
        FileHelper fileHelper = new FileHelper();
        string filenam;
        public Selecc_File ()
		{
			InitializeComponent ();
            RefreshListView();
        }

        void RefreshListView()
        {
            fileListView.ItemsSource = fileHelper.GetFiles();
            fileListView.SelectedItem = null;
        }

        async void OnFileListViewItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            filenam = (string)args.SelectedItem;
            string errorMessage = null;
            btnProcesa.IsEnabled = true;

            try
            {
                string texte = fileHelper.ReadText((string)args.SelectedItem);
                string[] lines = texte.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.None);
                fileEditor.Text = lines[0].ToString();
            }
            catch (Exception exc)
            {
                errorMessage = exc.Message;
            }

            if (errorMessage != null)
            {
                await DisplayAlert("TextFileTryout", errorMessage, "OK");
            }
        }
    }
}