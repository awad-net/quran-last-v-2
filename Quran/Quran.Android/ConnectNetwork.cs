using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Quran.Droid;
using Quran.Model;
[assembly: Xamarin.Forms.Dependency(typeof(ConnectNetwork))]

namespace Quran.Droid
{
    public class ConnectNetwork : IConnection
    {
        public bool IsConnection { get; set; }

        public void CheckConnection()
        {
            var connectionmanger = (ConnectivityManager)Application.Context.GetSystemService(Context.ConnectivityService);
            var ActiveNetwork = connectionmanger.ActiveNetworkInfo;


            if (ActiveNetwork != null && ActiveNetwork.IsConnectedOrConnecting)
            {
                IsConnection = true;
            }
            else
            {
                IsConnection = false;
            }
        }
    }
}