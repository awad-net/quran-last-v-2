using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quran
{
    public class sebhadb
    {
        public SQLiteAsyncConnection database;
        public int tasbih;

        public sebhadb(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<sebhaclass>().Wait();
        }

        public Task<List<sebhaclass>> GetItemsAsync()
        {
            return database.QueryAsync<sebhaclass>("SELECT id,name,value FROM sebhaclass WHERE 1");
        }

        public Task<List<sebhaclass>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<sebhaclass>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }
        public void zerotasbih(int id)
        {
            Task<List<sebhaclass>> x = database.QueryAsync<sebhaclass>($"update sebhaclass set value=0 WHERE id={id}");
        }
        public string update(int id)
        {
            Task<List<sebhaclass>> x = database.QueryAsync<sebhaclass>($"select * FROM sebhaclass WHERE id={id}");
            foreach (sebhaclass tas in x.Result)
            {
                tasbih = tas.value + 1;
                database.QueryAsync<sebhaclass>($"update sebhaclass set value={tasbih} where  id={id}");
            }
            return " عدد مرات التسبيح" + " " + tasbih;
        }



        public Task<sebhaclass> GetItemAsync(int id)
        {
            return database.Table<sebhaclass>().Where(i => i.id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(sebhaclass item)
        {

            return database.InsertAsync(item);

        }


        public Task<int> DeleteItemAsync(sebhaclass item)
        {
            return database.DeleteAsync(item);
        }
    }
}
