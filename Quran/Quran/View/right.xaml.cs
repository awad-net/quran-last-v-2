using SlideOverKit;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Quran
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class right : SlideMenuView
    {
        public right()
        {
            InitializeComponent();
            DependencyService.Get<IAdInterstitial>().LoadAd();
            WidthRequest = 250;
            // You must set IsFullScreen in this case, 
            // otherwise you need to set WidthRequest, 
            // just like the QuickInnerMenu sample
            HeightRequest = 1000;
            MenuOrientations = MenuOrientation.RightToLeft;

            // You must set BackgroundColor, 
            // and you cannot put another layout with background color cover the whole View
            // otherwise, it cannot be dragged on Android
            BackgroundColor = Color.FromHex("#1e6a25");
            BackgroundViewColor = Color.Transparent;

        }


        public async void nav(object sender, EventArgs args)
        {
            Button b = (Button)sender;
            MessagingCenter.Send<right, string>(this, "Hi", b.Text);
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            DependencyService.Get<IAdInterstitial>().ShowAd();
            nav(new Button() { Text = "الشيوخ" }, null);
            DependencyService.Get<IAdInterstitial>().LoadAd();
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            DependencyService.Get<IAdInterstitial>().ShowAd();
            nav(new Button() { Text = "التنزيلات" }, null);

            DependencyService.Get<IAdInterstitial>().LoadAd();
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            DependencyService.Get<IAdInterstitial>().ShowAd();

            nav(new Button() { Text = "الاذكار" }, null);
            DependencyService.Get<IAdInterstitial>().LoadAd();
        }

        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            DependencyService.Get<IAdInterstitial>().ShowAd();
            nav(new Button() { Text = "القبله" }, null);

            DependencyService.Get<IAdInterstitial>().LoadAd();
        }
        private void TapGestureRecognizer_Tapped_4(object sender, EventArgs e)
        {
            DependencyService.Get<IAdInterstitial>().ShowAd();
            nav(new Button() { Text = "السبحة" }, null);

            DependencyService.Get<IAdInterstitial>().LoadAd();
        }

        private void TapGestureRecognizer_Tapped_5(object sender, EventArgs e)
        {
            DependencyService.Get<IAdInterstitial>().ShowAd();

            nav(new Button() { Text = "إعدادات الأذان" }, null);

            DependencyService.Get<IAdInterstitial>().LoadAd();

        }

    }
}
