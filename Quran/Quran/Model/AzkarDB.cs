using Newtonsoft.Json;
using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Quran
{
    internal class AzkarDB
    {
        private readonly SQLiteConnection con;

        public AzkarDB()
        {
            con = DBConnecton.Connection("newQuran.db3");
            try
            {


                TableQuery<Azkar> x = con.Table<Azkar>();
                int d = x.Count();

            }
            catch (SQLiteException)
            {
                con.CreateTable<Azkar>();

                AddSomeData();

            }

        }

        public void SaveItem(Azkar item)
        {
            con.Insert(item);

        }

        public List<Azkar> GetAzkars()
        {
            return con.Table<Azkar>().ToList();
        }

        private void AddSomeData()
        {
            Assembly assembly = typeof(MainPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{"Azkar.txt"}");
            StreamReader reader = new StreamReader(stream);
            string text = reader.ReadToEnd();
            RootObjectAzkar test = JsonConvert.DeserializeObject<RootObjectAzkar>(text);
            ZakrContentDB dB = new ZakrContentDB();
            int inde = 0;

            foreach (A item in test.azkar)
            {
                SaveItem(new Azkar { AID = inde, name = item.name });
                foreach (C item2 in item.content)
                {
                    if (!string.IsNullOrEmpty(item2.name))
                    {
                        int count = int.Parse(item2.Count);
                        if (count == 0)
                        {
                            count = 1;
                        }

                        dB.SaveItem(new ZakrContent { AID = inde, name = item2.name, Benifit = item2.Benifit, Count = count });

                    }
                }
                inde++;
            }

        }



    }

    public class ZakrContentDB
    {
        private readonly SQLiteConnection con;

        public ZakrContentDB()
        {
            con = DBConnecton.Connection("newQuran.db3");
            try
            {
                TableQuery<ZakrContent> x = con.Table<ZakrContent>();
                int d = x.Count();

            }
            catch (SQLiteException)
            {
                con.CreateTable<ZakrContent>();

            }
        }


        public List<ZakrContent> GetZakrsContent()
        {
            return con.Table<ZakrContent>().Where(c => c.AID == App.zakrContentID).ToList();
        }
        public void SaveItem(ZakrContent zakr)
        {
            con.Insert(zakr);
        }
    }
}
