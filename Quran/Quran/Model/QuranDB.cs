using Newtonsoft.Json;
using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Quran
{
    public class QuranDB
    {
        public SQLiteConnection database;

        public QuranDB()
        {
            database = DBConnecton.Connection("QuranDatabase.db3");
            try
            {

                TableQuery<Quran> x = database.Table<Quran>();
                int d = x.Count();


            }
            catch (SQLiteException)
            {
                database.CreateTable<Quran>();
                AddSomeDate();
            }
        }

        private void AddSomeDate()
        {


            Assembly assembly = typeof(MainPage).GetTypeInfo().Assembly;

            Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{"quranname.txt"}");
            Stream stream1 = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{"quran-simple-en.txt"}");
            Stream stream2 = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{"quran-simple.txt"}");

            StreamReader reader = new StreamReader(stream);
            string text = reader.ReadToEnd();

            RootObject test = JsonConvert.DeserializeObject<RootObject>(text);

            reader = new StreamReader(stream1);
            text = reader.ReadToEnd();

            RootObject1 test1 = JsonConvert.DeserializeObject<RootObject1>(text);

            reader = new StreamReader(stream2);
            text = reader.ReadToEnd();
            RootObject2 test2 = JsonConvert.DeserializeObject<RootObject2>(text);
            for (int i = 0; i < 114; i++)
            {
                string a = "";
                string Ea = "";

                foreach (ArbicFileAya aya in test2.quran.sura[i].aya)
                {
                    a += aya._text;
                    a += "'";
                }
                foreach (EngilsFileAya aya in test1.quran.sura[i].aya)
                {
                    Ea += aya.xtext;
                    Ea += "'";

                }



                Quran item = new Quran
                {
                    id = int.Parse(test.suras.sura[i].index),
                    name = test.suras.sura[i].name,
                    Ename = test.suras.sura[i].tname,
                    aya = a,
                    Eaya = Ea,
                    PravoiusSura = test.suras.sura[i].type
                };


                SaveItemAsync(item);
            }
        }

        public List<Quran> GetItemsAsync()
        {
            return database.Table<Quran>().ToList();
        }

        public List<Quran> GetItemsNotDoneAsync()
        {
            return database.Query<Quran>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public Quran GetItemAsync(int id)
        {
            return database.Table<Quran>().Where(i => i.id == id).FirstOrDefault();
        }


        public int SaveItemAsync(Quran item)
        {

            return database.Insert(item);

        }

        public void Update(Quran quran)
        {
            database.Update(quran);
        }
        public int DeleteItemAsync(Quran item)
        {
            return database.Delete(item);
        }
    }
}
