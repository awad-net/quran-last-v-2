using SQLite;
using System;
using System.IO;

namespace Quran

{
    public class DBConnecton
    {
        public static SQLiteConnection Connection(string dbname)
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), dbname);
            return new SQLiteConnection(path);


        }

    }
}
