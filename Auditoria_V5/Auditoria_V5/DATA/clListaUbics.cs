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
        
    }
}
