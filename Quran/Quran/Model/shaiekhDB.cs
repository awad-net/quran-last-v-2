using Quran.Model;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quran
{
    public class shaiekhDB
    {
        public SQLiteAsyncConnection database;

        public shaiekhDB()
        {
            database = new SQLiteAsyncConnection("");
            database.CreateTableAsync<Mshaiekh>().Wait();
        }

        public Task<List<Mshaiekh>> GetItemsAsync()
        {
            return database.Table<Mshaiekh>().ToListAsync();
        }

        public Task<List<Mshaiekh>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<Mshaiekh>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public Task<Mshaiekh> GetItemAsync(int id)
        {
            return database.Table<Mshaiekh>().Where(i => i.id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Mshaiekh item)
        {

            return database.InsertAsync(item);

        }


        public Task<int> DeleteItemAsync(Mshaiekh item)
        {
            return database.DeleteAsync(item);
        }


        public Mshaiekh mshaiekh(string url)
        {
            shak shak = new shak();
            shak.GetMshaiekhByURl(url);
            return null;
        }


    }
}
