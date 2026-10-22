using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Quran.iOS
{
    public class QuranDB
    {
        public SQLiteAsyncConnection database;

        public QuranDB(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Quran>().Wait();
        }

        public Task<List<Quran>> GetItemsAsync()
        {
            return database.QueryAsync<Quran>("SELECT id,name,value FROM Quran WHERE 1");
        }

        public Task<List<Quran>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<Quran>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public Task<Quran> GetItemAsync(int id)
        {
            return database.Table<Quran>().Where(i => i.id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Quran item)
        {

            return database.InsertAsync(item);

        }


        public Task<int> DeleteItemAsync(Quran item)
        {
            return database.DeleteAsync(item);
        }
    }
}
