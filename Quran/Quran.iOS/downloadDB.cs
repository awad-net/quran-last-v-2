using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Quran.iOS
{
    public class downloadDB
    {
        public SQLiteAsyncConnection database;

        public downloadDB(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Download>().Wait();
        }

        public Task<List<Download>> GetItemsAsync()
        {
            return database.Table<Download>().ToListAsync();
        }
        public Task<Download> GetFirst()
        {
            return database.Table<Download>().FirstAsync();
        }
        public Task<List<Download>> GetDownloadedSwar(int id)
        {
            return database.Table<Download>().Where(i => i.id == id).ToListAsync();
        }

        public Task<List<Download>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<Download>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public Task<Download> GetItemAsync(int id)
        {
            return database.Table<Download>().Where(i => i.id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Download item)
        {

            return database.InsertAsync(item);

        }


        public Task<int> DeleteItemAsync(Download item)
        {
            return database.DeleteAsync(item);
        }
    }
}
