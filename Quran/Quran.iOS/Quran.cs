using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quran.iOS
{
    public class Quran
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string name { get; set; }
        public string value { get; set; }
    }
}
