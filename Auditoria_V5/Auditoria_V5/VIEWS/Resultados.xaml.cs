using Auditoria_V5.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microcharts;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkiaSharp;

namespace Auditoria_V5
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Resultados : ContentPage
	{
        ClAuditoria2 auditoria = new ClAuditoria2();

       



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

        private void Almacen_clicked(object sender, SelectedItemChangedEventArgs e)
        {
            int value1;
            auditoria = (ClAuditoria2)e.SelectedItem;
            System.Diagnostics.Debug.WriteLine("NumREg totales: " +auditoria.num_reg_totales);
            System.Diagnostics.Debug.WriteLine("Comprobasdos: " + auditoria.comprobados);



            //List<Microcharts.Entry> entradas = new List<Microcharts.Entry>()
            //{



            //    new Microcharts.Entry((auditoria.num_reg_error * 100 / auditoria.num_reg_totales))
            //    {
            //        Label = "Registros",
            //        ValueLabel = (auditoria.num_reg_error * 100 / auditoria.num_reg_totales).ToString() + "%",
            //        Color = SKColor.Parse("#FF0033"),
            //    },
            //    new Microcharts.Entry((auditoria.num_ubics_error*100/auditoria.num_ubicaciones))
            //    {
            //        Label = "Ubicaciones",
            //        ValueLabel = (auditoria.num_ubics_error*100 / auditoria.num_ubicaciones).ToString()+"%",
            //        Color = SKColor.Parse("#FF8000"),
            //    },
            //    new Microcharts.Entry((auditoria.num_seriados_error*100/auditoria.num_seriados))
            //    {
            //        Label = "Seriados",
            //        ValueLabel = (auditoria.num_seriados_error*100 / auditoria.num_seriados).ToString()+"%",
            //        Color = SKColor.Parse("#FFE600"),
            //    },
            //     new Microcharts.Entry((auditoria.comprobados*100/auditoria.num_reg_totales))
            //    {
            //        Label = "Revisados",
            //        ValueLabel = (auditoria.comprobados*100 / auditoria.num_reg_totales).ToString()+"%",
            //        Color = SKColor.Parse("#1AB34D"),

            //    },

            //};
            List<Microcharts.Entry> entradas = new List<Microcharts.Entry>();
            if (auditoria.num_reg_totales>0)
            {
                entradas.Add(new Microcharts.Entry((auditoria.num_reg_error * 100 / auditoria.num_reg_totales))
                {
                    Label = "Registros",
                    ValueLabel = (auditoria.num_reg_error * 100 / auditoria.num_reg_totales).ToString() + "%",
                    Color = SKColor.Parse("#FF0033"),
                });
                entradas.Add(new Microcharts.Entry((auditoria.comprobados * 100 / auditoria.num_reg_totales))
                {
                    Label = "Revisados",
                    ValueLabel = (auditoria.comprobados * 100 / auditoria.num_reg_totales).ToString() + "%",
                    Color = SKColor.Parse("#1AB34D"),

                });


            };
            entradas.Add(new Microcharts.Entry((auditoria.num_ubics_error * 100 / auditoria.num_ubicaciones))
            {
                Label = "Ubicaciones",
                ValueLabel = (auditoria.num_ubics_error * 100 / auditoria.num_ubicaciones).ToString() + "%",
                Color = SKColor.Parse("#FF8000"),
            });
            if (auditoria.num_seriados > 0)
            {
                entradas.Add(new Microcharts.Entry((auditoria.num_seriados_error * 100 / auditoria.num_seriados))
                {
                    Label = "Seriados",
                    ValueLabel = (auditoria.num_seriados_error * 100 / auditoria.num_seriados).ToString() + "%",
                    Color = SKColor.Parse("#FFE600"),
                });
            }

            MyBarChart.Chart = new BarChart { Entries = entradas };

        }
    }
}