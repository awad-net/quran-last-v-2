using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreFoundation;
using Foundation;
using Quran.Model;
using SystemConfiguration;
using UIKit;

namespace Quran.iOS
{
    public  class NetworkConnection : IConnection
    {
        public bool IsConnection { get; set; }

        public void CheckConnection()
        {
            internetStatus();


        }
        public bool internetStatus()
        {
            NetworkReachabilityFlags flags;
            bool defualtNetwork = IsNetworkAvailabye(out flags);
            if (defualtNetwork && ((flags & NetworkReachabilityFlags.IsDirect) != 0))
            {
                return false;
            }
            else if ((flags & NetworkReachabilityFlags.IsWWAN) != 0)
            {
                return true;
            }
            else if (flags == 0)
            {
                return false;
            }
            return true;
        }
        private event EventHandler ReachabilityChange;
        private void onChange(NetworkReachabilityFlags flags)
        {
            var h = ReachabilityChange;
            if (h != null)
                h(null, EventArgs.Empty);
        }
        private NetworkReachability defualtReachability;
        public bool IsNetworkAvailabye(out NetworkReachabilityFlags flags)
        {
            if (defualtReachability == null)
            {
                defualtReachability = new NetworkReachability(new System.Net.IPAddress(0));
                defualtReachability.SetNotification(onChange);
                defualtReachability.Schedule(CFRunLoop.Current, CFRunLoop.ModeDefault);
            }
            if (!defualtReachability.TryGetFlags(out flags))
            {
                return false;
            }
            return IsReachableWithoutRequit(flags);
        }

        private bool IsReachableWithoutRequit(NetworkReachabilityFlags flags)
        {
            bool IsReachable = (flags & NetworkReachabilityFlags.Reachable) != 0;
            bool noConnectionRequired = (flags & NetworkReachabilityFlags.ConnectionRequired) == 0;
            if ((flags & NetworkReachabilityFlags.IsWWAN) != 0)
                noConnectionRequired = true;
            return IsReachable && noConnectionRequired;




        }
    }
}