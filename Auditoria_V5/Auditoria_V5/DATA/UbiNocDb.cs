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

        async public Task<int> GetNumRegs(string filename)
        {
            try
            {
                // var g=  await database.ExecuteScalarAsync<int>("Select Count(Ubicacion) from UbiNoc");
                var g = await database.ExecuteScalarAsync<int>("Select Count(Ubicacion) as Num from [UbiNoc] where [Fichero]='" + filename + "'");
                return g;
            }
            catch
            {
                return 0;
            }
        }


        async public Task<string> GetAlm(string filename)
        {
            try
            {
                // var g=  await database.ExecuteScalarAsync<int>("Select Count(Ubicacion) from UbiNoc");
                var g = await database.ExecuteScalarAsync<string>("Select [Ubicacion] from [UbiNoc] where [Fichero]='" + filename + "'");
                return g;
            }
            catch
            {
                return null;
            }
        }

        async public Task<Boolean> IsCompleted(string filename)
        {
            try
            {
              int g= await database.ExecuteScalarAsync<int>("Select Count([Check]) from [UbiNoc] where [Fichero]='" + filename + "' and [Check]=0");

                if (g>0)
                { 
                    return false;
                }
                else
                {
                    return true;

                }
                
            }
            catch
            {
                return false;
            }
            
        }


        public Task<List<clFicheros>> GetFicheros()
        {
            
            return database.QueryAsync<clFicheros>("select [Fichero] from [UbiNoc] group by [Fichero]");

        }

    }
}
