using SQLite;
using System.Collections.Generic;

namespace Quran
{
    public class Quran
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string name { get; set; }
        public string Ename { get; set; }
        public string aya { get; set; }
        public string Eaya { get; set; }
        public string PravoiusSura { get; set; }
    }

    /// <summary>
    /// Name File Json
    /// </summary>
    public class NameFileSura
    {
        public string index { get; set; }
        public string ayas { get; set; }
        public string start { get; set; }
        public string name { get; set; }
        public string tname { get; set; }
        public string ename { get; set; }
        public string type { get; set; }
        public string order { get; set; }
        public string rukus { get; set; }
    }

    public class NameFileSuras
    {
        public List<NameFileSura> sura { get; set; }
    }

    public class RootObject
    {
        public NameFileSuras suras { get; set; }
    }


    /// <summary>
    /// Engils File Json
    /// </summary>
    public class EngilsFileAya
    {
        public string xindex { get; set; }
        public string xtext { get; set; }
    }

    public class EngilsFileSura
    {
        public string xindex { get; set; }
        public string xname { get; set; }
        public List<EngilsFileAya> aya { get; set; }
    }

    public class EngilsFileQuran
    {
        public List<EngilsFileSura> sura { get; set; }
    }

    public class RootObject1
    {
        public EngilsFileQuran quran { get; set; }
    }

    /// <summary>
    /// Arbic File Json
    /// </summary>
    /// 
    public class ArbicFileAya
    {
        public string _index { get; set; }
        public string _text { get; set; }
    }

    public class ArbicFileSura
    {
        public string _index { get; set; }
        public string _name { get; set; }
        public List<ArbicFileAya> aya { get; set; }
    }

    public class ArbicFileQuran1
    {
        public List<ArbicFileSura> sura { get; set; }
    }

    public class RootObject2
    {
        public ArbicFileQuran1 quran { get; set; }
    }
}
