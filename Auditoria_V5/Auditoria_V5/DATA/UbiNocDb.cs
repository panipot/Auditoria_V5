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
            //database.CreateTableAsync<UbiNoc>().Wait();
            //database.CreateTableAsync<ClAuditoria2>().Wait();
        }

        public Task<UbiNoc> GetItemAsync2()
        {
            return database.Table<UbiNoc>().FirstOrDefaultAsync();
        }
    }
}
