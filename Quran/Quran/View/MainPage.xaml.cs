using Plugin.MediaManager;
using Quran.ViewModel;
using SlideOverKit;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Xamarin.Forms;
namespace Quran
{
    public partial class MainPage : ContentPage, IMenuContainerPage
    {
        private readonly ViewModelAzkar model;
        private readonly XmlDocument doc;
        private readonly XmlDocument docName;

        private readonly bool isRowEven = true;
        public SQLiteConnection conn;
        public Mshaiekh sheikh;
        public List<Mshaiekh> mshiekh;
        public List<Quran> quran = new List<Quran>();
        public DataTemplate EvenTemplate { get; set; }
        public DataTemplate UnevenTemplate { get; set; }

        private readonly List<string> flatList;
        public SlideMenuView SlideMenu { get; set; }
        public Action ShowMenuAction { get; set; }
        public Action HideMenuAction { get; set; }
        public MainPage()
        {
            InitializeComponent();

            model = new ViewModelAzkar();
            getquran();

            MessagingCenter.Subscribe<right, string>(this, "Hi", (sender, arg) =>
            {
                switch (arg)
                {
                    case "الشيوخ":
                        Navigation.PushAsync(new MainPage());
                        break;
                    case "التنزيلات":
                        Navigation.PushAsync(new downloadedshioukh());
                        break;
                    case "الاذكار":
                        Navigation.PushAsync(new AzkarPage());
                        break;
                    case "السبحة":
                        Navigation.PushAsync(new Sebha());
                        break;
                    case "القبله":
                        Navigation.PushAsync(new qipla());
                        break;
                    case "إعدادات الأذان":
                        Navigation.PushAsync(new About());
                        break;
                }
            });
        }

        protected override void OnAppearing()
        {
            Cshaiekhlst.ItemsSource = model.AllMshaiekh;
            Cshaiekhlst.SelectedItem = null;
            base.OnAppearing();
        }

        private void getquran()
        {
            QuranDB dB = new QuranDB();
        }



        private async void Cshaiekhlst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {

                Mshaiekh shie = e.CurrentSelection.FirstOrDefault() as Mshaiekh;
                if (shie != null)
                {
                    if (shie.name != App.ShaiekName)
                    {
                        await CrossMediaManager.Current.Stop();
                        App.ShaiekName = shie.name;


                    }
                    await Navigation.PushAsync(new playquran(shie.id, shie.url, shie.img));
                    Cshaiekhlst.SelectedItem = null;

                }
            }

        }


    }
}
