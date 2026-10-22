using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Foundation;
using UIKit;
using Xamarin.Forms;
namespace Quran.iOS
{
    public class LongRunning
    {

        public string filen;

        public async Task Start(string msg, string id)
        {
            string current = "1";
            string suraid = "1";
            WebClient webclient = new WebClient();
            var url = msg;
            var sid = id;
            int c = int.Parse(current);
            string pathToNewFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), "download");
                Directory.CreateDirectory(pathToNewFolder);
                for (int i = 1; i <= 114; i++)
                {
                if (c.ToString().Length == 1)
                {
                    suraid = "00" + c.ToString();
                }
                else if (c.ToString().Length == 2)
                {
                    suraid = "0" + c.ToString();
                }
                downloadDB qurandb = new downloadDB(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "download2.db3"));
                    QuranDB q = new QuranDB();
                    Quran sura =  q.GetItemAsync(c);

                Random random = new Random();
                int num = random.Next(1000);
                string destination =pathToNewFolder+"/"+
                        num.ToString() + ".mp3";
                Task t ;
                await Task.Run(() =>
                {
                    webclient.DownloadFile(new Uri(url + suraid + ".mp3"),
                     destination);
                    qurandb.SaveItemAsync(new Download { name = sura.name, suraid = sura.id, sid = int.Parse(id), value = destination });

                });
                        

            
                    
                c += 1;

            }
        }
        }
    }