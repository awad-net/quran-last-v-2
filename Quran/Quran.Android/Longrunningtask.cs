using Android.App;
using Android.Content;
using Android.OS;
using System.Threading.Tasks;
using Quran;
using System.IO;
using System.Net;
using System;
using System.Net.Http;
using Environment = System.Environment;
using System.Security.Cryptography;
using Quran.Model;
namespace Quran.Droid
{
    [Service]
    public class Longrunningtask : Service
    {
        string url;
        string current = "1";
        public string suraid;
        public int sid;
        public int sheid;
        public WebClient webclient = new WebClient();

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }
        public  override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            shaiekhDB dB = new shaiekhDB();

            Task.Run(async () =>
            {
                var url = intent.GetStringExtra("url");
                var sid = intent.GetStringExtra("sid");

                    int c = int.Parse(current);
                c += 1;
                if (c.ToString().Length == 1)
                {
                    suraid = "00" + c.ToString();
                }
                else if (c.ToString().Length == 2)
                {
                    suraid = "0" + c.ToString();
                }
                for (int i = 1; i <= 114; i++)
                {
                    downloadDB qurandb = new downloadDB(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "download2.db3"));
                    QuranDB q = new QuranDB();
                  
                    Quran sura = q.GetItemAsync(i);
                    Random random = new Random();
                    int num = random.Next(1000);


                    var destination = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), num.ToString() + ".mp3");
                    string pathToNewFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), "download");
                    Directory.CreateDirectory(pathToNewFolder);


                    FileStream file = new FileStream(pathToNewFolder + "/" +
                                  num.ToString() + ".mp3", FileMode.Create, System.IO.FileAccess.Write);

                       await webclient.DownloadFileTaskAsync(
                           new Uri(url + suraid + ".mp3"),file.Name);


                       await qurandb.SaveItemAsync(new Download { name = sura.name, Ename = sura.name, sid = this.sid, value =file.Name});
                 
                   
                   
               




               }
           });
            return StartCommandResult.Sticky;

        }

    }
}