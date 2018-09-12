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




        async void Filtrar_Clicked(object sender, EventArgs e)
        {
            string fZona = pZona.SelectedItem?.ToString()??"";
            string fPass = pPasillo.SelectedItem?.ToString()??"";
            string fDm = pDm.SelectedItem?.ToString()??"";
            clListaUbics arch = new clListaUbics();
            List<clUbicacion> lista = new List<clUbicacion>();
            List<clUbicacion> lista2 = new List<clUbicacion>();
            var auditoria = (ClAuditoria2)BindingContext;
            lista = await arch.Rellena_lista_ubics(auditoria);

            if (fDm=="")
            {
                if (fPass == "")
                { //fdm nulo y f pasillo nulo
                    if (fZona == "")
                    { //1.TODO NULO ALMACEN COMPLETYO
                        lista2 = lista;

                    }
                    else
                    {// 2.Solo fZona
                        lista2 = lista.Where(x => x.Zona == fZona).ToList();

                    }


                }
                else
                {//3.fdm nulo y f pasillo no nulo---> fzona es NO NULO
                    lista2 = lista.Where(x => (x.Pasillo == fPass) && (x.Zona == fZona)).ToList();

                }

            }
            else
            {
                if (fPass == "")
                { //fdm nocnulo y f pasillo nulo
                    if (fZona == "")
                    { //4.Solo DM NO Nulo
                        lista2 = lista.Where(x => (x.DataMining == fDm) ).ToList();


                    }
                    else
                    {//5 .Solo Fpas nulo
                        lista2 = lista.Where(x => (x.DataMining == fDm) && (x.Zona == fZona)).ToList();

                    }


                }
                else
                {//6.fdm no nulo  f pasillo no nulo---> fzona es NO NULO
                    lista2 = lista.Where(x => (x.DataMining == fDm) &&  (x.Pasillo == fPass) && (x.Zona == fZona)).ToList();

                }
            }











            for (int i = 0; i < lista2.Count - 1; i++)
            {
                await Navigation.PushAsync(
                    new AudUbicacion()
                    {
                        BindingContext = lista2[i] as clUbicacion
                    });
            }



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