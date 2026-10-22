using SQLite;

namespace Quran
{
    public class Download
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public int sid { get; set; }
        public int suraid { get; set; }
        public string name { get; set; }
        public string Ename { get; set; }
        public string value { get; set; }
    }
}
