using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Auditoria_V5.DATA
{
    public class clListaUbics
    {
            
        
            public Task<List<clUbicacion>> Rellena_lista_ubics(ClAuditoria2 auditoria)
            {

                return App.Database.GetUbicsFich(auditoria);


            }

  
        //public Task<List<clUbicacion>> Rellena_lista_ubics(ClAuditoria2 auditoria, string fZona, string fPass, string fDm)
        //{

        //    if (fDm == null)
        //    {
        //        if (fPass == null)
        //        { //fdm nulo y f pasillo nulo
        //            if (fZona == null)
        //            { //1.TODO NULO ALMACEN COMPLETYO
        //                return App.Database.GetUbicsFich(auditoria);
                        
        //            }
        //            else
        //            {// 2.Solo fZona
        //                return App.Database.GetUbicsZona(auditoria, fZona);
                        
        //            }


        //        }
        //        else
        //        {//3.fdm nulo y f pasillo no nulo---> fzona es NO NULO
        //            return App.Database.GetUbicsPass(auditoria, fZona, fPass);

        //        }

        //    }
        //    else
        //    {
        //        if (fPass == null)
        //        { //fdm nulo y f pasillo nulo
        //            if (fZona == null)
        //            { //4.Solo DM NO Nulo
        //                return App.Database.GetUbicsDm(auditoria, fDm);


        //            }
        //            else
        //            {//5 .Solo Fpas nulo
        //                return App.Database.GetUbicsZonaD(auditoria, fZona, fDm);

        //            }


        //        }
        //        else
        //        {//6.fdm no nulo  f pasillo no nulo---> fzona es NO NULO
        //            return App.Database.GetUbicsfiltr(auditoria, fZona, fPass, fDm);

        //        }

        //    }



            
        //}
    }
}
