using System;
using Android.Preferences;
using Android.Media;
using Android.Content;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using Android.Content;
using Android.Media;
using Android;
using ImageCircle.Forms.Plugin.Droid;
using Android.Preferences;


using Plugin.MediaManager;
using Android.Gms.Ads;

namespace Quran.Droid
{
    [Activity(Label = "Quran", Theme = "@style/splashscreen", Icon = "@drawable/icon", ScreenOrientation = ScreenOrientation.Portrait, MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, LaunchMode = LaunchMode.SingleTop)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private Bundle bundle;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
          

            if (CheckSelfPermission(Manifest.Permission.AccessNotificationPolicy) != (int)Permission.Granted)
            {
                RequestPermissions(new string[] { Manifest.Permission.AccessNotificationPolicy, Manifest.Permission.AccessNotificationPolicy }, 0);
            }
            base.Window.RequestFeature(WindowFeatures.ActionBar);
            // Name of the MainActivity theme you had there before.
            // Or you can use global::Android.Resource.Style.ThemeHoloLight
            base.SetTheme(Resource.Style.MainTheme);
            base.OnCreate(savedInstanceState);
            Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");
            MobileAds.Initialize(ApplicationContext, "ca-app-pub-4625361618752481~7640085320");
          
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            ImageCircleRenderer.Init();
           //CrossMediaManager.Current.Init(this);

            LoadApplication(new App());

            MessagingCenter.Subscribe<object, string[]>(this, "url", (s, e) => {
                var intent = new Intent(this, typeof(Longrunningtask));
                intent.PutExtra("url", e[0]);
                intent.PutExtra("sid", e[1]);
                StartService(intent);
            });
        }
        [BroadcastReceiver]
        public class AlarmReceiver : BroadcastReceiver
        {

            [Obsolete]
            public override void OnReceive(Context context, Intent intent)
            {


                var message = intent.GetStringExtra("message");
                var title = intent.GetStringExtra("title");
                Console.WriteLine("sayed" + title);

                playingazan _playingazan = new playingazan();
                _playingazan.play(context);

                using (var notificationManager = NotificationManager.FromContext(context))
                {

                    Notification notification;


                    var myUrgentChannel = context.PackageName;
                    const string channelName = "SushiHangover Urgent";

                    NotificationChannel channel;
                    channel = notificationManager.GetNotificationChannel(myUrgentChannel);

                    var alarmAttributes = new AudioAttributes.Builder()
                .SetContentType(AudioContentType.Sonification)
                .SetUsage(AudioUsageKind.Notification).Build();


                    var customReceiver = new CustomActionReceiver();

                    // Create intent for action 1 (ARCHIVE)
                    var actionIntent1 = new Intent();
                    actionIntent1.SetAction("ARCHIVE");
                    var pIntent1 = PendingIntent.GetBroadcast(context, 0, actionIntent1, PendingIntentFlags.CancelCurrent);

                    // Create intent for action 2 (REPLY)
                    var actionIntent2 = new Intent();
                    actionIntent2.SetAction("REPLY");
                    var pIntent2 = PendingIntent.GetBroadcast(context, 0, actionIntent2, PendingIntentFlags.CancelCurrent);


                    Intent resultIntent = context.PackageManager.GetLaunchIntentForPackage(context.PackageName);
                    var contentIntent = PendingIntent.GetActivity(context, 0, resultIntent, PendingIntentFlags.CancelCurrent);



                    if (channel == null)
                    {
                        channel = new NotificationChannel(myUrgentChannel, channelName, NotificationImportance.Max);
                        channel.EnableVibration(true);
                        channel.EnableLights(true);


                        channel.LockscreenVisibility = NotificationVisibility.Public;
                        notificationManager.CreateNotificationChannel(channel);

                    }

                    Notification.InboxStyle mediaStyle = new Notification.InboxStyle();

                    notification = new Notification.Builder(context)
                                .SetContentIntent(contentIntent)
                                .SetChannelId(myUrgentChannel)
                                .SetContentTitle(title)
                                .SetContentText(message)
                                .SetAutoCancel(false)
                                .SetPriority(1)
                                .SetFullScreenIntent(pIntent1, true)
                                .AddAction(Resource.Drawable.icon, "إيقاف الصوت", pIntent1)
                               .SetDeleteIntent(pIntent2)

                                .SetStyle(mediaStyle)

                                .SetFullScreenIntent(pIntent1, true)

                                .SetSmallIcon(Resource.Drawable.icon)

                                .Build();

                    // Add intent filters for each action and register them on a broadcast receiver
                    var intentFilter = new IntentFilter();
                    intentFilter.AddAction("ARCHIVE");
                    intentFilter.AddAction("REPLY");

                    context.ApplicationContext.RegisterReceiver(customReceiver, intentFilter);

                    notificationManager.Notify(1331, notification);

                }

            }

        }


        public class CustomActionReceiver : BroadcastReceiver
        {

            public override void OnReceive(Context context, Intent intent)
            {
                ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(context);
                prefs.Edit().PutString("stop_val", "stop").Commit();

            }
        }

    }
}