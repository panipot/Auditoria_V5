using System;
using System.Collections.Generic;
using System.Text;

namespace Auditoria_V5.DATA
{
   public  class ExportDb
    {
        public async void Exporta(string filename, string almacen, string dbname)
        {
            FileHelper fileHelper = new FileHelper();
            string file_exit = "RES_ALMACEN_"+ almacen + ".csv"; 
            string errorMessage = null;

            try
            {
                List<UbiNoc> ubics = new List<UbiNoc>();
                //ubics = await App.Database.GetItemsAsync();
                ubics = await App.Database.GetUbiNocFich(filename);
                fileHelper.AppendText(file_exit, "CO_UNIDAD|"
                        +  "DS_UNIDAD|"
                        +  "FhAuditoria|"
                         + "UBICACION|"
                         + "DM|"
                        +  "NOC|"
                        +  "DS_NOC|"
                        +  "CANT_SIGLE|"
                        +  "CANT_REAL|"
                        +  "ESTADO_OP|"
                        +  "OBS|"
                        +  "ERROR|" +  "REVISADA|FICHERO" 
                        + Environment.NewLine);
                foreach (UbiNoc ubinoci in ubics)
                {
                    fileHelper.AppendText(file_exit, ubinoci.Uco + "|"
                        + ubinoci.DsUco + "|"
                        + ubinoci.FhAuditoria + "|"
                        + ubinoci.Ubicacion + "|"
                        + ubinoci.DataMining + "|"
                        + ubinoci.Noc + "|"
                        + ubinoci.Descripcion + "|"
                        + ubinoci.Cantidad + "|"
                        + ubinoci.CantReal + "|"
                        + ubinoci.EstadoOp + "|" 
                        + ubinoci.Obs + "|"
                        + ubinoci.Error + "|" + ubinoci.Check + "|" + ubinoci.Fichero
                        + Environment.NewLine);

                }

            }
            catch (Exception exc)
            {
                errorMessage = exc.Message;
            }
        }

    }
}

