using SQLite;
namespace Quran
{
    public class Mshaiekh
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string img { get; set; }
    }
}
