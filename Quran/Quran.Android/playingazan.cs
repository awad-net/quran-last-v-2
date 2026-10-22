using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Diagnostics;
using Android.Media;
using Android.Preferences;

namespace Quran.Droid
{
    public class playingazan
    {
        string actionstop;
        System.Timers.Timer timer = new System.Timers.Timer();
        System.Timers.Timer timer2 = new System.Timers.Timer();
        MediaPlayer playmp3 = new MediaPlayer();
        public void play(Context context)
        {
            ISharedPreferences prefs2 = PreferenceManager.GetDefaultSharedPreferences(context);
            prefs2.Edit().PutString("stop_val", "play").Commit();


            playmp3 = MediaPlayer.Create(context, Resource.Raw.azanharm);
            playmp3.Start();



            timer2.Elapsed += (object sender, System.Timers.ElapsedEventArgs e) =>
            {

                ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(context);
                string mString = prefs.GetString("stop_val", null);





                if (mString == "stop")
                {
                    playmp3.Stop();
                   // ISharedPreferences prefs2 = PreferenceManager.GetDefaultSharedPreferences(context);
                    prefs2.Edit().PutString("stop_val", "play").Commit();



                }

                System.Diagnostics.Debug.WriteLine(mString);


            };
            timer2.Start();

        }






    }
}