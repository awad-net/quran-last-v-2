using SQLite;
using System.Collections.Generic;
namespace Quran.Model
{
    internal class shak
    {
        private readonly SQLiteConnection con;
        public shak()
        {
            con = DBConnecton.Connection("Test1.db3");
            try
            {
                TableQuery<Mshaiekh> x = con.Table<Mshaiekh>();
                int d = x.Count();
            }
            catch (SQLiteException)
            {
                con.CreateTable<Mshaiekh>();
                AddSomeData();
            }
        }
        public List<Mshaiekh> getAll()
        {
            return con.Table<Mshaiekh>().ToList();
        }
        public Mshaiekh getshak(int id)
        {
            return con.Table<Mshaiekh>().Where(c => c.id == id).FirstOrDefault();
        }
        public int Save(Mshaiekh item)
        {
            if (item.id != 0)
            {
                return con.Update(item);
            }
            else
            {
                return con.Insert(item);
            }
        }
        public Mshaiekh GetMshaiekhByURl(string url)
        {
            return con.Table<Mshaiekh>().Where(c => c.url == url).FirstOrDefault();
        }
        private void AddSomeData()
        {
            Save(new Mshaiekh { id = 0, name = "محمد صديق المنشاوي", url = "https://download.tvquran.com/download/TvQuran.com__Al-Minshawi-Mojawad/", img = "elmenshawy.jpg" });
            Save(new Mshaiekh { id = 0, name = "عبد الباسط عبد الصمد", url = "https://download.tvquran.com/download/TvQuran.com__basit/", img = "abdulbasitabdulsamad.jpg" });
            Save(new Mshaiekh { id = 0, name = "مشاري العفاسي", url = "https://download.tvquran.com/download/TvQuran.com__Alafasi/", img = "mashari.jpg" });
            Save(new Mshaiekh { id = 0, name = "ماهر المعيقلي", url = "https://download.tvquran.com/download/TvQuran.com__Maher/", img = "maher.jpg" });
            Save(new Mshaiekh { id = 0, name = "سعد الغامدي", url = "https://download.tvquran.com/download/TvQuran.com__Al-Ghamdi/", img = "saad.jpg" });
            Save(new Mshaiekh { id = 0, name = "عبدالرحمن السديس", url = "https://download.tvquran.com/download/TvQuran.com__Alsdes/", img = "sudes.jpg" });
            Save(new Mshaiekh { id = 0, name = "ادريس ابكر", url = "https://download.tvquran.com/download/TvQuran.com__Abkar/", img = "edresapkar.jpg" });
            Save(new Mshaiekh { id = 0, name = "أحمد العجمي", url = "https://download.tvquran.com/download/TvQuran.com__Al-Ajmy/", img = "agami.jpg" });
            Save(new Mshaiekh { id = 0, name = "ياسر الدوسري", url = "https://download.tvquran.com/download/TvQuran.com__Yasser/", img = "dousary.jpg" });
            Save(new Mshaiekh { id = 0, name = "فارس عباد", url = " https://download.tvquran.com/download/TvQuran.com__Fares.Abbad/", img = "faresabaad.jpg" });
            Save(new Mshaiekh { id = 0, name = "خالد الجليل", url = "https://download.tvquran.com/download/recitations/21/252/", img = "khaledgalel.jpg" });
            Save(new Mshaiekh { id = 0, name = "محمود خليل الحصري", url = "https://download.tvquran.com/download/recitations/83/229/", img = "elhosary.jpg" });
            Save(new Mshaiekh { id = 0, name = "خالد القحطاني", url = "https://download.tvquran.com/download/TvQuran.com__Al-Qahtani/", img = "kahtany.jpg" });
            Save(new Mshaiekh { id = 0, name = "محمد جبريل", url = "https://download.tvquran.com/download/TvQuran.com__Jibrel/", img = "gepril.jpg" });
            Save(new Mshaiekh { id = 0, name = "محمود علي البنا", url = "https://download.tvquran.com/download/TvQuran.com__Albana-Mjwd/", img = "elpanaa.jpg" });
            Save(new Mshaiekh { id = 0, name = "ناصر القطامي", url = "https://download.tvquran.com/download/recitations/90/25/", img = "nasser.jpg" });
            Save(new Mshaiekh { id = 0, name = "هزاع البلوشي", url = "https://download.tvquran.com/download/recitations/183/216/", img = "elballloushi.jpg" });
            Save(new Mshaiekh { id = 0, name = "محمد رفعت", url = "https://download.tvquran.com/download/recitations/314/221/", img = "refaat.jpg" });
            Save(new Mshaiekh { id = 0, name = "محمد محمود الطبلاوي", url = "https://download.tvquran.com/download/TvQuran.com__Tablawi/", img = "taplawy.jpg" });
            Save(new Mshaiekh { id = 0, name = "أحمد علي الحذيفي", url = "https://download.tvquran.com/download/recitations/103/110/", img = "huzayfy.jpg" });
            Save(new Mshaiekh { id = 0, name = "العيون الكوشي", url = "https://download.tvquran.com/download/TvQuran.com__koshi/", img = "koshe.jpg" });
            Save(new Mshaiekh { id = 0, name = "حاتم فريد الواعر", url = "https://download.tvquran.com/download/TvQuran.com__Hatem/", img = "hatem.jpg" });
            Save(new Mshaiekh { id = 0, name = "ابو بكر الشاطري", url = "https://download.tvquran.com/download/TvQuran.com__Shatri/", img = "abubakr.jpg" });
            Save(new Mshaiekh { id = 0, name = "ياسين الجزائري", url = "https://download.tvquran.com/download/TvQuran.com__Yaseen/", img = "yassin.jpg" });
            Save(new Mshaiekh { id = 0, name = "الدوكالي محمد", url = "https://download.tvquran.com/download/TvQuran.com__doukkali/", img = "eldoukali.jpg" });
            Save(new Mshaiekh { id = 0, name = "English translation", url = "https://download.tvquran.com/download/recitations/332/256/", img = "translate.png" });
            Save(new Mshaiekh { id = 0, name = "French translation", url = "https://download.tvquran.com/download/recitations/338/262/", img = "translate.png" });
            Save(new Mshaiekh { id = 0, name = "German translation", url = "https://download.tvquran.com/download/recitations/331/254/", img = "translate.png" });
            Save(new Mshaiekh { id = 0, name = "Spanish translation", url = "https://download.tvquran.com/download/recitations/334/258/", img = "translate.png" });
            Save(new Mshaiekh { id = 0, name = "Chinese translation", url = "https://download.tvquran.com/download/recitations/342/268/", img = "translate.png" });
            Save(new Mshaiekh { id = 0, name = "Hindi translation", url = "https://download.tvquran.com/download/recitations/333/257/", img = "translate.png" });
            Save(new Mshaiekh { id = 0, name = "Indonesian translation", url = "https://download.tvquran.com/download/recitations/335/259/", img = "translate.png" });
            Save(new Mshaiekh { id = 0, name = "Kurdish translation", url = "https://download.tvquran.com/download/recitations/343/266/", img = "translate.png" });
        }
    }
}
