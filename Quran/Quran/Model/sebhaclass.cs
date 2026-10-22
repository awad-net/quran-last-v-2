using SQLite;

namespace Quran
{
    public class sebhaclass
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string name { get; set; }
        public int value { get; set; }
        public string rowcolor { get; set; }
    }
}
