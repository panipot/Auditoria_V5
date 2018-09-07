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
        [Indexed]
        public int UbiNocID { get; set; }
        public string Ubicacion { get; set; }
        public string Noc { get; set; }
        public string NumSerie { get; set; }
        public DateTime FhVidaProb { get; set; }
        public bool Check { get; set; }
        public bool Error { get; set; }
        public string Fichero { get; set; }
    }

    public class ClAuditoria2
    {

        public int Id { get; set; }
        public string Fichero {get; set;}
        public bool completa
        {
            get; set;
        }
        public int comprobados { get; set; }
        public int num_reg_totales
        {
            get; set;
        }
        public int num_ubicaciones
        {
            get; set;
        }
        public string Almacen { get; set; }
    }
}
