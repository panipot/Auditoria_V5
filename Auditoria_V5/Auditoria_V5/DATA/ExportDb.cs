using System;
using System.Collections.Generic;
using System.Text;

namespace Auditoria_V5.DATA
{
   public  class ExportDb
    {
        public async void Exporta(string filename, string dbname)
        {
            FileHelper fileHelper = new FileHelper();

            string errorMessage = null;

            try
            {
                List<UbiNoc> ubics = new List<UbiNoc>();
                ubics = await App.Database.GetItemsAsync();
                fileHelper.AppendText(filename, "CO_UNIDAD|"
                        +  "DS_UNIDAD|"
                        +  "FhAuditoria|"
                         + "UBICACION|"
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
                    fileHelper.AppendText(filename, ubinoci.Uco + "|"
                        + ubinoci.DsUco + "|"
                        + ubinoci.FhAuditoria + "|"
                        + ubinoci.Ubicacion + "|"
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

