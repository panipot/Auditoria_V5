using Auditoria_V5.DATA;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace Auditoria_V5
{


    // REGLAS APLICACION 
    // LEER FICHERO DEBE ESTAR ORDENADO POR UBICACION NOC
    // UNA UBICACION SOlO UN VALOR PARA Datamining y EST UBICACION

	public partial class App : Application
	{

        static UbiNocDb database;

        public static string SelectedBthDevice2;
        public static UbiNocDb Database
        {
            get
            {
                if (database == null)
                {
                    database = new UbiNocDb(DependencyService.Get<IFileHelper>().GetLocalFilePath("UbicNoc.db3"));
                }
                return database;
            }
        }



        public App ()
		{
			InitializeComponent();
            MainPage = new NavigationPage(new MainPage()) { BarBackgroundColor = Color.FromHex("3e606f") };

        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            MessagingCenter.Send<App>(this, "Sleep"); // When app sleep, send a message so I can "Cancel" the connection
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            MessagingCenter.Send<App>(this, "Resume"); // When app resume, send a message so I can "Resume" the connection
        }
    }
}
