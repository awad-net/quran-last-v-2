using SQLite;
using System.Collections.Generic;
namespace Quran
{
    public class Azkar
    {
        [PrimaryKey]
        public int AID { get; set; }
        public string name { get; set; }
    }
    public class ZakrContent
    {
        public int AID { get; set; }
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string name { get; set; }
        public string Benifit { get; set; }
        public int Count { get; set; }
    }
    public class A
    {
        public string name { get; set; }
        public string url { get; set; }
        public List<C> content { get; set; }
    }
    public class C
    {

        public string name { get; set; }
        public string Benifit { get; set; }
        public string Count { get; set; }
    }
    public class RootObjectAzkar
    {
        public List<A> azkar { get; set; }
    }

}