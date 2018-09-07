using Auditoria_V5.DATA;
using Auditoria_V5.VIEWS;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Auditoria_V5
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

        async  void btnProcesa_Clicked(object sender, EventArgs e)
        {
            filenam = (string)fileListView.SelectedItem;
            if (fileHelper.Exists("UbicNoc.db3"))
            { // cargar fichero en bb dd linea a linea
                try
                {
                    var esta = await App.Database.GetFichAsync(filenam);
                    var cuenta = await App.Database.GetNumUbics(filenam);
                    var cuenta2 = await App.Database.GetNumUbicsDone(filenam);
                    var action = await DisplayAlert("Aviso", "Ya existe una Auditoria previa de este fichero:" + esta.Fichero + " " + cuenta2.ToString() + "/" + cuenta.ToString() + " Si continua se iniciará de nuevo, ¿Seguro?", "Si", "No");
                    if (!action)
                    {
                        //await Navigation.PushAsync(new MainPage());
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await App.Database.DeleteAudit(filenam);
                        ReadAdd(filenam);
                    }


                }
                catch (Exception f)
                {
                    Console.WriteLine("{0} Exception", f);
                    ReadAdd(filenam);
                }


            }

            else
            {
                ReadAdd(filenam);
            }
        }

        async void ReadAdd(string filenam)
        {
            
            string errorMessage = null;
            string ubica = "";
            try
            {
                string texte = fileHelper.ReadText(filenam);
                string[] lines = texte.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.None);
                string[][] values = lines.Select(line => line.Split(new string[] { ";" }, StringSplitOptions.None)).ToArray();
                string noc_ant="";
                string ubi_ant="";
                for (int i = 0; i <= lines.Length - 1; i++)
                {

                    if (values[i][0].ToString() != "")
                    {
                       
                        bool dminin;
                        DateTime Fhprob;
                        string uco = values[i][0].ToString();
                        string dsuco = values[i][1].ToString();
                        DateTime Fhaudi = DateTime.ParseExact(values[i][2].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        ubica = values[i][3].ToString();
                        string estadoubi = values[i][4].ToString();
                        string noca = values[i][5].ToString();
                        string desca = values[i][6].ToString();
                        double canta = Convert.ToDouble(values[i][7]);
                        string estadoop = values[i][8].ToString();
                        string contunit = values[i][9].ToString();
                        string numls = values[i][10].ToString();
                        if (values[i][11].ToString() != "")
                        { Fhprob = DateTime.ParseExact(values[i][11].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture); }
                        else
                        { Fhprob = DateTime.ParseExact("01/01/0001", "dd/MM/yyyy", CultureInfo.InvariantCulture); ; }

                        if (values[i][12].ToString() == "S")
                        { dminin = true; }
                        else
                        { dminin = false; }
                        string obs= values[i][13].ToString();

                        if (ubica!=ubi_ant || noca!=noc_ant)
                        {
                            var ubinoci = new UbiNoc
                            {
                                Uco = uco,
                                DsUco = dsuco,
                                FhAuditoria = Fhaudi,
                                Ubicacion = ubica,
                                EstUbicacion = estadoubi,
                                Noc = noca,
                                Descripcion = desca,
                                Cantidad = canta,
                                EstadoOp = estadoop,
                                ControlUnitario = contunit,
                                DataMining = dminin,
                                Error = false,
                                Fichero = filenam
                            };
                            await App.Database.SaveItemAsync(ubinoci);
                            if (contunit != null)
                            {
                                var numseri = new tNumerosSerie
                                {
                                    Ubicacion = ubica,
                                    Noc=noca,
                                    NumSerie=numls,
                                    FhVidaProb=Fhprob,
                                    Fichero=filenam
                                };
                            await App.Database.SaveItemAsync2(numseri);
                            }


                        }
                        else
                        {
                            if (contunit != null)
                            {
                                var numseri = new tNumerosSerie
                                {
                                    Ubicacion = ubica,
                                    Noc = noca,
                                    NumSerie = numls,
                                    FhVidaProb = Fhprob,
                                    Fichero = filenam
                                };
                            await App.Database.SaveItemAsync2(numseri);
                            }
                        }
                        
                    }
                }
                await DisplayAlert("Fichero procesado", "Se han leido " + (lines.Length).ToString() + " registros", "OK");
            

                
                var audi = new ClAuditoria2
                {
                    Fichero = filenam,
                    completa = false,
                    comprobados = 0,
                    num_reg_totales = await App.Database.GetNumRegs(filenam),
                    num_ubicaciones = await App.Database.GetNumUbics(filenam),
                    Almacen = ubica.Substring(0, 2)


                };


                await Navigation.PushAsync(
                    new Selecc_Op
                    {
                       // BindingContext = audi
                    }
                    );
            }
            catch (Exception exc)
            {
                errorMessage = exc.Message;
            }

            if (errorMessage != null)
            {
                await DisplayAlert("Error Lectura", errorMessage, "OK");
            }
        }
    }
}