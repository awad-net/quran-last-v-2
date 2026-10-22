using SQLite;
using System.Collections.Generic;
namespace Quran.Model
{
    internal class Tasbih
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Title { get; set; }
        public string color { get; set; }
        public int NO { get; set; }
    }

    internal class TasbihDB
    {
        private readonly SQLiteConnection con = null;
        public TasbihDB()
        {
            con = DBConnecton.Connection("Azkar.db3");
            try
            {
                TableQuery<Tasbih> x = con.Table<Tasbih>();
                int s = x.Count();


            }
            catch (SQLiteException)
            {
                con.CreateTable<Tasbih>();
                AddSomeDate();
            }
        }

        public List<Tasbih> getAllItem()
        {
            return con.Table<Tasbih>().ToList();
        }
        public Tasbih getItem(int id)
        {
            return con.Table<Tasbih>().Where(c => c.ID == id).FirstOrDefault();
        }

        public int DeleteItem(int id)
        {
            return con.Delete<Tasbih>(id);
        }




        public int Save(Tasbih item)
        {
            if (item.ID != 0)
            {
                return con.Update(item);
            }
            else
            {
                return con.Insert(item);
            }
        }
        public void AddSomeDate()
        {
            List<Tasbih> az = new List<Tasbih>()
            {
                new Tasbih()
                {
                    Title="سبحان الله",
                    NO=0,
                    color ="#B5B5B5"
                },
                  new Tasbih()
                {
                    Title="الحمدلله الله",
                    NO=0,
                     color ="#B5B5B5"
                },
                new Tasbih()
                {
                    Title=" الله اكبر",
                    NO=0,
                    color ="#B5B5B5"
                },

            };

            foreach (Tasbih item in az)
            {
                Save(item);
            }

        }
    }
}