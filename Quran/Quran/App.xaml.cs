using Plugin.SimpleAudioPlayer;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Quran
{
    public partial class App : Application
    {
        public static int zakrContentID = 0;

        public static NavigationPage Mainpage;
        private static readonly shaiekhDB database;
        public static int AzkarId = 1;
        public static string ShaiekName = "";
        public static string ShaiekNameLocal = "";

        public static string lbldateHajre;
        public static string lbldate;
        public static string lbldateE;
        public static string lbldateHajreE;
        public static BindingList<string> down = new BindingList<string>();
        public static ISimpleAudioPlayer player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;


        public static ObservableCollection<Quran> AllQurans = new ObservableCollection<Quran>();


        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new Salaty())
            {
                BarBackgroundColor = Color.FromHex("2C7648")
                ,
                Title = "مواقيت الصلاة",
                BarTextColor = Color.White
            };

            //Mainpage = new NavigationPage(new AzkarPage());
        }



        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }


    }
}
