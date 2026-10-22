using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quran
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
        public Task<Download> GetFirst(int id)
        {
            return database.Table<Download>().Where(i => i.sid == id).FirstOrDefaultAsync();
        }
        public Task<List<Download>> GetDownloadedSwar(int id)
        {
            return database.QueryAsync<Download>($"SELECT * FROM Download WHERE sid={id} order by suraid ASC");
        }
        public void Delete(int id)
        {
            database.Table<Download>().Where(i => i.sid == id).DeleteAsync();
        }
        public Task<List<Download>> GetDownloadedShiouk(int id, int sid)
        {
            return database.Table<Download>().Where(i => i.suraid == id && i.sid == sid).ToListAsync();
        }
        public Task<List<Download>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<Download>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }
        public Task<Download> getnext(int id, int sid)
        {
            return database.Table<Download>().Where(i => i.id > id && i.sid == sid).FirstAsync();
        }
        public Task<List<Download>> getprev(int id, int sid)
        {
            return database.QueryAsync<Download>($"SELECT * FROM Download WHERE id<{id} and sid={sid} order by id desc limit 1");
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
