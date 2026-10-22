using Android.Gms.Ads;
using Android.OS;
using Google.Ads.Mediation.Admob;
using Quran.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(AdInterstitial_Droid))]
namespace Quran.Droid
{
    public class AdInterstitial_Droid : IAdInterstitial
    {
        InterstitialAd interstitialAd;

        public AdInterstitial_Droid()
        {
            interstitialAd = new InterstitialAd(Android.App.Application.Context);
            //ca-app-pub-4625361618752481/3109320123
            // TODO: change this id to your admob id
            interstitialAd.AdUnitId = "ca-app-pub-4625361618752481/3109320123";
            LoadAd();
        }

		public void LoadAd()
		{
            var adMobAdapter = new AdMobAdapter();
            var requestbuilder = new AdRequest.Builder();
			interstitialAd.LoadAd(requestbuilder.Build());

            Bundle extras = new Bundle();
            extras.PutBoolean("is_designed_for_families", true);
            requestbuilder.AddNetworkExtrasBundle(adMobAdapter.Class, extras);
            requestbuilder.TagForChildDirectedTreatment(true);
        }

        void IAdInterstitial.LoadAd()
        {

            var requestbuilder = new AdRequest.Builder();
            interstitialAd.LoadAd(requestbuilder.Build());
        }

        void IAdInterstitial.ShowAd()
        {
            if (interstitialAd.IsLoaded)
                interstitialAd.Show();

            LoadAd();
        }

        //public void LoadAd()
        //{
        //    var requestbuilder = new AdRequest.Builder();
        //    interstitialAd.LoadAd(requestbuilder.Build());
        //}

        //public void ShowAd()
        //{
        //    if (interstitialAd.IsLoaded)
        //        interstitialAd.Show();

        //    LoadAd();
        //}
    }
}