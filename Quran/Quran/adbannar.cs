using Xamarin.Forms;

namespace Quran
{
    public class AdBanner : Xamarin.Forms.View
    {
        public enum Sizes { Standardbanner, LargeBanner, MediumRectangle, FullBanner, Leaderboard, SmartBannerPortrait }
        public Sizes Size { get; set; }
        public AdBanner()
        {
            BackgroundColor = Color.Accent;
        }
    }
}