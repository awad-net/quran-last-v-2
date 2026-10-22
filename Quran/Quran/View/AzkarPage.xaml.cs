using Quran.ViewModel;
using SlideOverKit;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace Quran
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AzkarPage : ContentPage, IMenuContainerPage
    {
        public SlideMenuView SlideMenu { get; set; }
        public Action ShowMenuAction { get; set; }
        public Action HideMenuAction { get; set; }

        private ViewModelAzkar model;
        public AzkarPage()
        {
            InitializeComponent();

            AzkarDB dB = new AzkarDB();


        }

        protected override void OnAppearing()
        {
            model = new ViewModelAzkar();
            CAzkarlst.ItemsSource = model.AllAzkars;
        }

        private async void CAzkarlst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                Zikr azkar = new Zikr();
                Azkar azkar1 = e.CurrentSelection.FirstOrDefault() as Azkar;
                App.zakrContentID = azkar1.AID;

                await Navigation.PushAsync(azkar, true);
            }
        }
    }
}