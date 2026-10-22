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
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Media;
using Xamarin.Forms;
using static Android.Service.Voice.VoiceInteractionSession;
[assembly: Xamarin.Forms.Dependency(typeof(Quran.Droid.Classdeoid))]
namespace Quran.Droid
{
    public class Classdeoid : Inoti
    {
        [Obsolete]
        public void Remindfajr(int id, int hour, int minute, string title, string message, int day, int year)
        {
            try
            {
                if ((hour * 60 * 60) + (minute * 60) >= ((DateTime.Now.Hour * 60 * 60) + (DateTime.Now.Minute * 60)) & day == DateTime.Now.DayOfYear)
                {
                    Intent alarmIntent = new Intent(Forms.Context, typeof(MainActivity.AlarmReceiver));
                    alarmIntent.PutExtra("message", message);
                    alarmIntent.PutExtra("title", title);
                    int x = DateTime.Now.Millisecond;
                    PendingIntent pendingIntent = PendingIntent.GetBroadcast(Forms.Context, id, alarmIntent, PendingIntentFlags.OneShot);
                    Java.Util.Date date = new Java.Util.Date();
                    Java.Util.Calendar cal = Java.Util.Calendar.Instance;

                    cal.TimeInMillis = Java.Lang.JavaSystem.CurrentTimeMillis();
                    cal.Set(Java.Util.CalendarField.HourOfDay, hour);
                    cal.Set(Java.Util.CalendarField.Year, year);
                    cal.Set(Java.Util.CalendarField.Minute, minute);
                    cal.Set(Java.Util.CalendarField.Second, 0);
                    cal.Set(Java.Util.CalendarField.DayOfYear, day);

                    AlarmManager alarmManager = Xamarin.Forms.Forms.Context.GetSystemService(Android.Content.Context.AlarmService) as AlarmManager;
                    alarmManager.Set(AlarmType.RtcWakeup, cal.TimeInMillis, pendingIntent);
                    Console.WriteLine("time notification" + hour + "" + minute + "" + day);
                }
                else
                {
                    if (day > DateTime.Now.DayOfYear)
                    {
                        Intent alarmIntent = new Intent(Forms.Context, typeof(MainActivity.AlarmReceiver));
                        alarmIntent.PutExtra("message", message);
                        alarmIntent.PutExtra("title", title);
                        int x = DateTime.Now.Millisecond;
                        PendingIntent pendingIntent = PendingIntent.GetBroadcast(Forms.Context, id, alarmIntent, PendingIntentFlags.OneShot);
                        Java.Util.Date date = new Java.Util.Date();
                        Java.Util.Calendar cal = Java.Util.Calendar.Instance;

                        cal.TimeInMillis = Java.Lang.JavaSystem.CurrentTimeMillis();
                        cal.Set(Java.Util.CalendarField.HourOfDay, hour);
                        cal.Set(Java.Util.CalendarField.Year, year);
                        cal.Set(Java.Util.CalendarField.Minute, minute);
                        cal.Set(Java.Util.CalendarField.Second, 0);
                        cal.Set(Java.Util.CalendarField.DayOfYear, day);

                        AlarmManager alarmManager = Xamarin.Forms.Forms.Context.GetSystemService(Android.Content.Context.AlarmService) as AlarmManager;
                        alarmManager.Set(AlarmType.RtcWakeup, cal.TimeInMillis, pendingIntent);
                        Console.WriteLine("time notification" + hour + "" + minute + "" + day);
                    }

                    else { Console.WriteLine("time in past" + hour + "" + minute + "" + day); }
                }
            }
            catch { }
        }

        [Obsolete]
        public void Remindduhr(int id, int hour, int minute, string title, string message, int day, int year)
        {
            try
            {
                if ((hour * 60 * 60) + (minute * 60) >= ((DateTime.Now.Hour * 60 * 60) + (DateTime.Now.Minute * 60)) & day == DateTime.Now.DayOfYear)
                {
                    Intent alarmIntent = new Intent(Forms.Context, typeof(MainActivity.AlarmReceiver));
                    alarmIntent.PutExtra("message", message);
                    alarmIntent.PutExtra("title", title);
                    int x = DateTime.Now.Millisecond;
                    PendingIntent pendingIntent = PendingIntent.GetBroadcast(Forms.Context, id, alarmIntent, PendingIntentFlags.OneShot);
                    Java.Util.Date date = new Java.Util.Date();
                    Java.Util.Calendar cal = Java.Util.Calendar.Instance;

                    cal.TimeInMillis = Java.Lang.JavaSystem.CurrentTimeMillis();
                    cal.Set(Java.Util.CalendarField.HourOfDay, hour);
                    cal.Set(Java.Util.CalendarField.Year, year);
                    cal.Set(Java.Util.CalendarField.Minute, minute);
                    cal.Set(Java.Util.CalendarField.Second, 0);
                    cal.Set(Java.Util.CalendarField.DayOfYear, day);

                    AlarmManager alarmManager = Xamarin.Forms.Forms.Context.GetSystemService(Android.Content.Context.AlarmService) as AlarmManager;
                    alarmManager.Set(AlarmType.RtcWakeup, cal.TimeInMillis, pendingIntent);
                    Console.WriteLine("time notification" + hour + "" + minute + "" + day);
                }
                else
                {
                    if (day > DateTime.Now.DayOfYear)
                    {
                        Intent alarmIntent = new Intent(Forms.Context, typeof(MainActivity.AlarmReceiver));
                        alarmIntent.PutExtra("message", message);
                        alarmIntent.PutExtra("title", title);
                        int x = DateTime.Now.Millisecond;
                        PendingIntent pendingIntent = PendingIntent.GetBroadcast(Forms.Context, id, alarmIntent, PendingIntentFlags.OneShot);
                        Java.Util.Date date = new Java.Util.Date();
                        Java.Util.Calendar cal = Java.Util.Calendar.Instance;

                        cal.TimeInMillis = Java.Lang.JavaSystem.CurrentTimeMillis();
                        cal.Set(Java.Util.CalendarField.HourOfDay, hour);
                        cal.Set(Java.Util.CalendarField.Year, year);
                        cal.Set(Java.Util.CalendarField.Minute, minute);
                        cal.Set(Java.Util.CalendarField.Second, 0);
                        cal.Set(Java.Util.CalendarField.DayOfYear, day);

                        AlarmManager alarmManager = Xamarin.Forms.Forms.Context.GetSystemService(Android.Content.Context.AlarmService) as AlarmManager;
                        alarmManager.Set(AlarmType.RtcWakeup, cal.TimeInMillis, pendingIntent);
                        Console.WriteLine("time notification" + hour + "" + minute + "" + day);
                    }
                    else { Console.WriteLine("time in past" + hour + "" + minute + "" + day); }
                }
            }
            catch { }

        }

        [Obsolete]
        public void Remindasr(int id, int hour, int minute, string title, string message, int day, int year)
        {
            try
            {
                if ((hour * 60 * 60) + (minute * 60) >= ((DateTime.Now.Hour * 60 * 60) + (DateTime.Now.Minute * 60)) & day == DateTime.Now.DayOfYear)
                {
                    Intent alarmIntent = new Intent(Forms.Context, typeof(MainActivity.AlarmReceiver));
                    alarmIntent.PutExtra("message", message);
                    alarmIntent.PutExtra("title", title);
                    int x = DateTime.Now.Millisecond;
                    PendingIntent pendingIntent = PendingIntent.GetBroadcast(Forms.Context, id, alarmIntent, PendingIntentFlags.OneShot);
                    Java.Util.Date date = new Java.Util.Date();
                    Java.Util.Calendar cal = Java.Util.Calendar.Instance;

                    cal.TimeInMillis = Java.Lang.JavaSystem.CurrentTimeMillis();
                    cal.Set(Java.Util.CalendarField.HourOfDay, hour);
                    cal.Set(Java.Util.CalendarField.Year, year);
                    cal.Set(Java.Util.CalendarField.Minute, minute);
                    cal.Set(Java.Util.CalendarField.Second, 0);
                    cal.Set(Java.Util.CalendarField.DayOfYear, day);

                    AlarmManager alarmManager = Xamarin.Forms.Forms.Context.GetSystemService(Android.Content.Context.AlarmService) as AlarmManager;
                    alarmManager.Set(AlarmType.RtcWakeup, cal.TimeInMillis, pendingIntent);
                    Console.WriteLine("time notification" + hour + "" + minute + "" + day);

                }
                else
                {

                    if (day > DateTime.Now.DayOfYear)
                    {
                        Intent alarmIntent = new Intent(Forms.Context, typeof(MainActivity.AlarmReceiver));
                        alarmIntent.PutExtra("message", message);
                        alarmIntent.PutExtra("title", title);
                        int x = DateTime.Now.Millisecond;
                        PendingIntent pendingIntent = PendingIntent.GetBroadcast(Forms.Context, id, alarmIntent, PendingIntentFlags.OneShot);
                        Java.Util.Date date = new Java.Util.Date();
                        Java.Util.Calendar cal = Java.Util.Calendar.Instance;

                        cal.TimeInMillis = Java.Lang.JavaSystem.CurrentTimeMillis();
                        cal.Set(Java.Util.CalendarField.HourOfDay, hour);
                        cal.Set(Java.Util.CalendarField.Year, year);
                        cal.Set(Java.Util.CalendarField.Minute, minute);
                        cal.Set(Java.Util.CalendarField.Second, 0);
                        cal.Set(Java.Util.CalendarField.DayOfYear, day);

                        AlarmManager alarmManager = Xamarin.Forms.Forms.Context.GetSystemService(Android.Content.Context.AlarmService) as AlarmManager;
                        alarmManager.Set(AlarmType.RtcWakeup, cal.TimeInMillis, pendingIntent);
                        Console.WriteLine("time notification" + hour + "" + minute + "" + day);
                    }
                    else { Console.WriteLine("time in past" + hour + "" + minute + "" + day); }
                }
            }
            catch { }
        }

        [Obsolete]
        public void Remindmaghrb(int id, int hour, int minute, string title, string message, int day, int year)
        {
            try
            {
                if ((hour * 60 * 60) + (minute * 60) >= ((DateTime.Now.Hour * 60 * 60) + (DateTime.Now.Minute * 60)) & day == DateTime.Now.DayOfYear)
                {
                    Intent alarmIntent = new Intent(Forms.Context, typeof(MainActivity.AlarmReceiver));
                    alarmIntent.PutExtra("message", message);
                    alarmIntent.PutExtra("title", title);
                    int x = DateTime.Now.Millisecond;
                    PendingIntent pendingIntent = PendingIntent.GetBroadcast(Forms.Context, id, alarmIntent, PendingIntentFlags.OneShot);
                    Java.Util.Date date = new Java.Util.Date();
                    Java.Util.Calendar cal = Java.Util.Calendar.Instance;

                    cal.TimeInMillis = Java.Lang.JavaSystem.CurrentTimeMillis();
                    cal.Set(Java.Util.CalendarField.HourOfDay, hour);
                    cal.Set(Java.Util.CalendarField.Year, year);
                    cal.Set(Java.Util.CalendarField.Minute, minute);
                    cal.Set(Java.Util.CalendarField.Second, 0);
                    cal.Set(Java.Util.CalendarField.DayOfYear, day);

                    AlarmManager alarmManager = Xamarin.Forms.Forms.Context.GetSystemService(Android.Content.Context.AlarmService) as AlarmManager;
                    alarmManager.Set(AlarmType.RtcWakeup, cal.TimeInMillis, pendingIntent);
                    Console.WriteLine("time notification" + hour + "" + minute + "" + day);
                }
                else
                {
                    if (day > DateTime.Now.DayOfYear)
                    {
                        Intent alarmIntent = new Intent(Forms.Context, typeof(MainActivity.AlarmReceiver));
                        alarmIntent.PutExtra("message", message);
                        alarmIntent.PutExtra("title", title);
                        int x = DateTime.Now.Millisecond;
                        PendingIntent pendingIntent = PendingIntent.GetBroadcast(Forms.Context, id, alarmIntent, PendingIntentFlags.OneShot);
                        Java.Util.Date date = new Java.Util.Date();
                        Java.Util.Calendar cal = Java.Util.Calendar.Instance;

                        cal.TimeInMillis = Java.Lang.JavaSystem.CurrentTimeMillis();
                        cal.Set(Java.Util.CalendarField.HourOfDay, hour);
                        cal.Set(Java.Util.CalendarField.Year, year);
                        cal.Set(Java.Util.CalendarField.Minute, minute);
                        cal.Set(Java.Util.CalendarField.Second, 0);
                        cal.Set(Java.Util.CalendarField.DayOfYear, day);

                        AlarmManager alarmManager = Xamarin.Forms.Forms.Context.GetSystemService(Android.Content.Context.AlarmService) as AlarmManager;
                        alarmManager.Set(AlarmType.RtcWakeup, cal.TimeInMillis, pendingIntent);
                        Console.WriteLine("time notification" + hour + "" + minute + "" + day);
                    }
                    else { Console.WriteLine("time in past" + hour + "" + minute + "" + day); }
                }
            }
            catch { }
        }

        [Obsolete]
        public void Remindisha(int id, int hour, int minute, string title, string message, int day, int year)
        {
            try
            {
                if ((hour * 60 * 60) + (minute * 60) >= ((DateTime.Now.Hour * 60 * 60) + (DateTime.Now.Minute * 60)) & day == DateTime.Now.DayOfYear)
                {
                    Intent alarmIntent = new Intent(Forms.Context, typeof(MainActivity.AlarmReceiver));
                    alarmIntent.PutExtra("message", message);
                    alarmIntent.PutExtra("title", title);
                    int x = DateTime.Now.Millisecond;
                    PendingIntent pendingIntent = PendingIntent.GetBroadcast(Forms.Context, id, alarmIntent, PendingIntentFlags.OneShot);
                    Java.Util.Date date = new Java.Util.Date();
                    Java.Util.Calendar cal = Java.Util.Calendar.Instance;

                    cal.TimeInMillis = Java.Lang.JavaSystem.CurrentTimeMillis();
                    cal.Set(Java.Util.CalendarField.HourOfDay, hour);
                    cal.Set(Java.Util.CalendarField.Year, year);
                    cal.Set(Java.Util.CalendarField.Minute, minute);
                    cal.Set(Java.Util.CalendarField.Second, 0);
                    cal.Set(Java.Util.CalendarField.DayOfYear, day);

                    AlarmManager alarmManager = Xamarin.Forms.Forms.Context.GetSystemService(Android.Content.Context.AlarmService) as AlarmManager;
                    alarmManager.Set(AlarmType.RtcWakeup, cal.TimeInMillis, pendingIntent);
                    Console.WriteLine("time notification" + hour + "" + minute + "" + day);
                }
                else
                {
                    if (day > DateTime.Now.DayOfYear)
                    {
                        Intent alarmIntent = new Intent(Forms.Context, typeof(MainActivity.AlarmReceiver));
                        alarmIntent.PutExtra("message", message);
                        alarmIntent.PutExtra("title", title);
                        int x = DateTime.Now.Millisecond;
                        PendingIntent pendingIntent = PendingIntent.GetBroadcast(Forms.Context, id, alarmIntent, PendingIntentFlags.OneShot);
                        Java.Util.Date date = new Java.Util.Date();
                        Java.Util.Calendar cal = Java.Util.Calendar.Instance;

                        cal.TimeInMillis = Java.Lang.JavaSystem.CurrentTimeMillis();
                        cal.Set(Java.Util.CalendarField.HourOfDay, hour);
                        cal.Set(Java.Util.CalendarField.Year, year);
                        cal.Set(Java.Util.CalendarField.Minute, minute);
                        cal.Set(Java.Util.CalendarField.Second, 0);
                        cal.Set(Java.Util.CalendarField.DayOfYear, day);

                        AlarmManager alarmManager = Forms.Context.GetSystemService(Android.Content.Context.AlarmService) as AlarmManager;
                        alarmManager.Set(AlarmType.RtcWakeup, cal.TimeInMillis, pendingIntent);
                        Console.WriteLine("time notification" + hour + "" + minute + "" + day);
                    }
                    else { Console.WriteLine("time in past" + hour + "" + minute + "" + day); }
                }
            }
            catch { }
        }
    }
}