using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AVFoundation;
using Foundation;

using UIKit;
using UserNotifications;
[assembly: Xamarin.Forms.Dependency(typeof(Quran.iOS.EmptyClassios))]
namespace Quran.iOS
{
    public class EmptyClassios : InotiiosAndandorid
    {
        public void Remindfajr(string id, string title, string body, int hour, int minute, int day, int mounth, int year)
        {
            try
            {
                var notification = new UNMutableNotificationContent();
                notification.Title = title;
                notification.Body = body;
                notification.Badge = 1;
                notification.Sound = UNNotificationSound.Default;

                var components = new NSDateComponents();
                components.Hour = hour;
                components.Minute = minute;
                components.Day = day;
                components.Month = mounth;
                components.Year = year;
                components.Second = 0;

                var trigger = UNCalendarNotificationTrigger.CreateTrigger(components, false);
                var requestID = "" + id;
                var request = UNNotificationRequest.FromIdentifier(requestID, notification, trigger);
                UNUserNotificationCenter.Current.AddNotificationRequest(request, (err) => {
                    if (err != null)
                    {
                        // Do something with error...
                    }
                });
            }
            catch { }
        }
        public void Remindduhr(string id, string title, string body, int hour, int minute, int day, int mounth, int year)
        {
            try
            {
                var notification2 = new UNMutableNotificationContent();
                notification2.Title = title;
                notification2.Body = body;
                notification2.Badge = 1;
                notification2.Sound = UNNotificationSound.Default;

                var components = new NSDateComponents();
                components.Hour = hour;
                components.Minute = minute;
                components.Day = day;
                components.Month = mounth;
                components.Year = year;
                components.Second = 0;

                var trigger = UNCalendarNotificationTrigger.CreateTrigger(components, false);
                var requestID = "" + id;
                var request = UNNotificationRequest.FromIdentifier(requestID, notification2, trigger);
                UNUserNotificationCenter.Current.AddNotificationRequest(request, (err) => {
                    if (err != null)
                    {
                        // Do something with error...
                    }
                });

            }
            catch { }
        }
        public void Remindasr(string id, string title, string body, int hour, int minute, int day, int mounth, int year)
        {
            try
            {
                var notification3 = new UNMutableNotificationContent();
                notification3.Title = title;
                notification3.Body = body;
                notification3.Badge = 1;
                notification3.Sound = UNNotificationSound.Default;

                var components = new NSDateComponents();
                components.Hour = hour;
                components.Minute = minute;
                components.Day = day;
                components.Month = mounth;
                components.Year = year;
                components.Second = 0;

                var trigger = UNCalendarNotificationTrigger.CreateTrigger(components, false);
                var requestID = "" + id;
                var request = UNNotificationRequest.FromIdentifier(requestID, notification3, trigger);
                UNUserNotificationCenter.Current.AddNotificationRequest(request, (err) => {
                    if (err != null)
                    {

                    }
                });

            }
            catch { }

        }

        public void Remindmaghreb(string id, string title, string body, int hour, int minute, int day, int mounth, int year)
        {
            try
            {
                var notification4 = new UNMutableNotificationContent();
                notification4.Title = title;
                notification4.Body = body;
                notification4.Badge = 1;
                notification4.Sound = UNNotificationSound.Default;

                var components = new NSDateComponents();
                components.Hour = hour;
                components.Minute = minute;
                components.Day = day;
                components.Month = mounth;
                components.Year = year;
                components.Second = 0;



                var trigger = UNCalendarNotificationTrigger.CreateTrigger(components, false);
                var requestID = "" + id;
                var request = UNNotificationRequest.FromIdentifier(requestID, notification4, trigger);
                UNUserNotificationCenter.Current.AddNotificationRequest(request, (err) => {
                    if (err != null)
                    {
                        // Do something with error...
                    }
                });
            }
            catch { }
        }
        public void Remindisha(string id, string title, string body, int hour, int minute, int day, int mounth, int year)
        {
            try
            {
                var notification5 = new UNMutableNotificationContent();
                notification5.Title = title;
                notification5.Body = body;
                notification5.Badge = 1;
                notification5.Sound = UNNotificationSound.Default;

                var components = new NSDateComponents();
                components.Hour = hour;
                components.Minute = minute;
                components.Day = day;
                components.Month = mounth;
                components.Year = year;
                components.Second = 0;

                var trigger = UNCalendarNotificationTrigger.CreateTrigger(components, false);
                var requestID = "" + id;
                var request = UNNotificationRequest.FromIdentifier(requestID, notification5, trigger);
                UNUserNotificationCenter.Current.AddNotificationRequest(request, (err) => {
                    if (err != null)
                    {
                        // Do something with error...
                    }
                });

            }
            catch { }
        }
        public void repeting(string id, string title, string body)
        {
            try
            {
                var notification6 = new UNMutableNotificationContent();
                notification6.Title = title;
                notification6.Body = body;
                notification6.Badge = 1;
                notification6.Sound = UNNotificationSound.Default;

                var trigger = UNTimeIntervalNotificationTrigger.CreateTrigger(86400 * 12, true);
                var requestID = "" + id;
                var request = UNNotificationRequest.FromIdentifier(requestID, notification6, trigger);
                UNUserNotificationCenter.Current.AddNotificationRequest(request, (err) => {
                    if (err != null)
                    {
                        // Do something with error...
                    }
                });

            }
            catch { }
        }

        public void cancellallnotification()
        {
            UIApplication.SharedApplication.CancelAllLocalNotifications();
        }
    }
}