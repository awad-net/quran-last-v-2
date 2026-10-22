using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quran
{
    public class Download
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public int sid { get; set; }
        public string name { get; set; }
        public string value { get; set; }
    }
}
