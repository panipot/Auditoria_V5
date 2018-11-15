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

            public Task<List<clUbicacion>> Rellena_lista_ubics2(clUbicacion ubicacion)
            {

                return App.Database.GetClUbic(ubicacion.Ubicacion);


               }




        public async Task<List<clUbicacion>> Actualiza_lista_ubics(List<clUbicacion> milista)
        {

            List<clUbicacion> milista_pre = new List<clUbicacion>();
            List<clUbicacion> milista_res = new List<clUbicacion>();
            foreach (clUbicacion item in milista)
            {
                milista_pre = await Rellena_lista_ubics2(item);
                foreach (clUbicacion ubi in milista_pre)
                {
                    milista_res.Add(ubi);
                }


            }

            return milista_res;



        }
  
        
    }
}
