using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Auditoria_V5.DATA
{
   public  class UbiNocDb
    {
        readonly SQLiteAsyncConnection database;

        public UbiNocDb(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<UbiNoc>().Wait();
            database.CreateTableAsync<tNumerosSerie>().Wait();
        }

        public Task<UbiNoc> GetItemAsync2()
        {
            return database.Table<UbiNoc>().FirstOrDefaultAsync();
        }

        public Task<List<UbiNoc>> GetItemsAsync()
        {
            return database.Table<UbiNoc>().ToListAsync();
        }

        public Task<UbiNoc> GetFichAsync(string fich)
        {
            return database.Table<UbiNoc>().Where(i => i.Fichero == fich).FirstOrDefaultAsync();
        }

         public Task<int> GetNumUbics(string filename)
        {
            try
            {
                // var g=  await database.ExecuteScalarAsync<int>("Select Count(Ubicacion) from UbiNoc");
                var g =  database.ExecuteScalarAsync<int>("Select Count(DISTINCT Ubicacion)  from [UbiNoc] where [Fichero]='" + filename + "'");
                return g;
            }
            catch
            {
                return null;
            }
        }
        public Task<int> GetNumNocs(string filename)
        {
            try
            {
                // var g=  await database.ExecuteScalarAsync<int>("Select Count(Ubicacion) from UbiNoc");
                var g = database.ExecuteScalarAsync<int>("Select Count(DISTINCT Noc)  from [UbiNoc] where [Fichero]='" + filename + "'");
                return g;
            }
            catch
            {
                return null;
            }
        }

        public Task<string> GetFich(string ubicacion)
        {
            try
            {
                // var g=  await database.ExecuteScalarAsync<int>("Select Count(Ubicacion) from UbiNoc");
                var g = database.ExecuteScalarAsync<string>("Select [Fichero] from [UbiNoc] where [Ubicacion]='" + ubicacion + "' group by [Fichero]");
                return g;
            }
            catch
            {
                return null;
            }
        }

        public Task<int> GetNumUbicsDone(string filename)
        {

            try
            {
                var g = database.ExecuteScalarAsync<int>("Select Count(Ubicacion) as Num from [UbiNoc] where [Check]<>0 and [Fichero]='" + filename + "'");
                return g;
            }
            catch
            {
                return null;
            }



        }

        public Task<int> GetNumsSerie(UbiNoc auditado)
        {

            try
            {
                var g = database.ExecuteScalarAsync<int>("Select Count(NOC) as Num from [tNumerosSerie] where [NOC]='" + auditado.Noc + "' and [Ubicacion]='" + auditado.Ubicacion + "'");
                return g;
            }
            catch
            {
                return null;
            }



        }

        public Task<int> GetCuentaNS_fich( string Fichero )
        {

            try
            {
                var g = database.ExecuteScalarAsync<int>("Select Count(NOC) as Num from [tNumerosSerie] where [Fichero]='" + Fichero + "'");
                return g;
            }
            catch
            {
                return null;
            }



        }
        public Task<int> GetCuentaNSErr_fich(string Fichero)
        {

            try
            {
                var g = database.ExecuteScalarAsync<int>("Select Count(NOC) as Num_error from [tNumerosSerie] where [Fichero]='" + Fichero + "' and Error=1;");
                return g;
            }
            catch
            {
                return null;
            }



        }

        public Task<int> DeleteAudit(string filename)
        {

            var g =database.ExecuteAsync("DELETE FROM [tNumerosSerie] where [Fichero]='" + filename + "'");
            return database.ExecuteAsync("DELETE FROM [UbiNoc] where [Fichero]='" + filename + "'");

        }

        public Task<int> SaveItemAsync(UbiNoc item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> SaveItemObs(UbiNoc item, string Obs )
        {
            return database.ExecuteAsync("Update [UbiNoc] set [Obs]='"+ Obs +"' where [ID]='" + item.ID + "';");
            //return database.UpdateAsync(item);
            
        }

        public Task<int> SaveItemAsync2(tNumerosSerie item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

         public Task<int> GetNumRegs(string filename)
        {
            try
            {
                // var g=  await database.ExecuteScalarAsync<int>("Select Count(Ubicacion) from UbiNoc");
                var g =  database.ExecuteScalarAsync<int>("Select Count(Ubicacion) as Num from [UbiNoc] where [Fichero]='" + filename + "'");
                return g;
            }
            catch
            {
                return null;
            }
        }


         public Task<string> GetAlm(string filename)
        {
            try
            {
                // var g=  await database.ExecuteScalarAsync<int>("Select Count(Ubicacion) from UbiNoc");
                var g =  database.ExecuteScalarAsync<string>("Select [Ubicacion] from [UbiNoc] where [Fichero]='" + filename + "'");
                return g;
            }
            catch
            {
                return null;
            }
        }

         public  Task<int> IsCompleted(string filename)
        {
            try
            {
              var g=   database.ExecuteScalarAsync<int>("Select Count([Check]) from [UbiNoc] where [Fichero]='" + filename + "' and [Check]=0");

                return g;
                
            }
            catch
            {
                return null;
            }
            
        }


        public Task<List<clFicheros>> GetFicheros()
        {
            
            return database.QueryAsync<clFicheros>("select [Fichero] from [UbiNoc] where [Fichero] is not null group by [Fichero]");

        }

        public Task<int> GetErrorUbi(string ubicacion)
        {
            try
            {
                // var g=  await database.ExecuteScalarAsync<int>("Select Count(Ubicacion) from UbiNoc");
                var g = database.ExecuteScalarAsync<int>("Select Count(Ubicacion) as Num from [UbiNoc] where [Ubicacion]='" + ubicacion + "' and [Error]<>0");
                return g;
            }
            catch
            {
                return null;
            }
        }
        public Task<int> GetErrorUbi2(string fichero)
        {
            try
            {
                // var g=  await database.ExecuteScalarAsync<int>("Select Count(Ubicacion) from UbiNoc");
                var g = database.ExecuteScalarAsync<int>("Select Count(DISTINCT Ubicacion) as Num from [UbiNoc] where [Fichero]='" + fichero + "' and [Error]<>0");
                return g;
            }
            catch
            {
                return null;
            }
        }
        public Task<int> GetErrorRegs(string fichero)
        {
            try
            {
                // var g=  await database.ExecuteScalarAsync<int>("Select Count(Ubicacion) from UbiNoc");
                var g = database.ExecuteScalarAsync<int>("Select Count(Ubicacion) as num_reg_error from [UbiNoc] where [Fichero]='" + fichero + "' and [Error]<>0");
                return g;
            }
            catch
            {
                return null;
            }
        }

        public Task<List<clUbicacion>> GetUbicsFich(ClAuditoria2 auditoria)
        {
            string param = auditoria.Fichero;
            //return database.QueryAsync<clUbicacion>("SELECT [Ubicacion] FROM [UbiNoc] where [Fichero]='" + param + "' group by [Ubicacion]");

            return database.QueryAsync<clUbicacion>("select [Ubicacion], [EstUbicacion], [DataMining], count(NOC) as Num_nocs, sum([Check]) as SumCk from UbiNOC where [Fichero] = '" + param + "' group by [Ubicacion], [EstUbicacion],[DataMining]");

        }
        


        public Task<List<UbiNoc>> GetUbiNoc(string ubi)
        {
            string param = ubi;
            return database.QueryAsync<UbiNoc>("select * from [UbiNoc] where [Ubicacion]='" + param + "'");
            //return database.Table<UbiNoc>().Where(i => i.Ubicacion == ubi).ToListAsync();
        }

        public Task<int> Set_Ubi_Done(string ubicacion)
        {
            return database.ExecuteAsync("Update [UbiNoc] set [Check]=1 where [Ubicacion]='" + ubicacion + "'");
        }
        public Task<int> Set_Serial_Check(tNumerosSerie seriado)
        {
            return database.UpdateAsync(seriado);
            //return database.ExecuteAsync("Update [tNumerosSerie] set [Check]=1 where [Ubicacion]='" + seriado.Ubicacion+ "'");
        }

        public Task<List<tNumerosSerie>> GetSerials(UbiNoc ubinoci)
        {
            
            return database.Table<tNumerosSerie>().Where(i => i.Noc == ubinoci.Noc && i.Ubicacion==ubinoci.Ubicacion).ToListAsync();
            
        }
        
    }
}
