using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auditoria_V5.DATA
{
    public class UbiNoc
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Uco { get; set; }
        public string DsUco { get; set; }
        public DateTime FhAuditoria { get; set; }
        public string Ubicacion { get; set; }
        public string EstUbicacion { get; set; }
        public string Noc { get; set; }
        public string Descripcion { get; set; }
        public double Cantidad { get; set; }
        public string EstadoOp { get; set; }
        public double CantReal { get; set; }
        public string ControlUnitario { get; set; }
        public bool DataMining { get; set; }
        public string Obs { get; set; }
        public bool Error { get; set; }
        public bool Check { get; set; }
        public string Fichero { get; set; }

        public string Almacen
        {
            get
            {
                return string.Format("{0}", this.Ubicacion.Substring(2, 1));
            }
        }
        public string Area
        {
            get
            {
                return string.Format("{0}", this.Ubicacion.Substring(1, 1));
            }
        }

    } 

    public class tNumerosSerie
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }   
        public string Ubicacion { get; set; }
        public string Noc { get; set; }
        public string NumSerie { get; set; }
        public DateTime FhVidaProb { get; set; }
        public bool Check { get; set; }
        public bool Error { get; set; }
        public string Fichero { get; set; }
    }


    public class clFicheros
    {
        public string Fichero { get; set; }
    }

    public class ClAuditoria2
    {

       
        public string Fichero {get; set;}
        public string Almacen
        {
            get
            {
                var a = (App.Database.GetAlm(Fichero));
                return string.Format("{0}", a.Result.Substring(0, 2));
            }
        }
        public bool completa
        {
            get
            {
                var a =  (App.Database.IsCompleted(Fichero));
                if (a.Result > 0)
                {
                    return true;
                }
                else
                { return false; }
            }
        }
        public int comprobados
        {
            get
            {
                var a = App.Database.GetNumUbicsDone(Fichero);
                return a.Result;
            }
        }
        public int num_reg_totales
        {
            get
            {
                var a = App.Database.GetNumRegs(Fichero);
                return a.Result;
            } 
        }
        public int num_ubicaciones
        {
            get
            {
                var a = App.Database.GetNumUbics(Fichero);
                return a.Result;
            }
        }
        
    }

    public class clUbicacion
    {

        public string Ubicacion { get; set; }
        public bool Error
        {

            get
            {
                var a = (App.Database.GetErrorUbi(Ubicacion));
                if (Convert.ToInt32(a.Result) != 0)
                { return true; }
                else
                { return false; };
            }


        }
        public bool Check { get; set; }
        public int Num_nocs
        {
            get; set;

        }
        public string EstUbicacion
        {
            get; set;


        }
        public string Zona { get { return string.Format("{0}", this.Ubicacion.Substring(2, 1)); } }
        public string Pasillo { get { return string.Format("{0}", this.Ubicacion.Substring(3, 2)); } }
        public string DataMining { get; set; }

    }
}
