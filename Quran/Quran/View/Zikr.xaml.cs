using Quran.ViewModel;
using SlideOverKit;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace Quran
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Zikr : ContentPage, IMenuContainerPage
    {
        public MainPage main;
        public List<Azkar> items;
        public Azkar result;
        public int zikrid;
        public SlideMenuView SlideMenu { get; set; }
        public Action ShowMenuAction { get; set; }
        public Action HideMenuAction { get; set; }

        private ViewModelAzkar model;
        private readonly AzkarDB azkar;
        public Zikr()
        {
            InitializeComponent();
            ToolbarItem menu = ToolbarItems.Last();
            azkar = new AzkarDB();

            menu.Command = new Command(() =>
            {
                if (SlideMenu.IsShown)
                {
                    HideMenuAction?.Invoke();
                }
                else
                {
                    ShowMenuAction?.Invoke();
                }
            });
            SlideMenu = new right();

        }

        protected override void OnAppearing()
        {
            model = new ViewModelAzkar();

            CSpecialAzlkar.ItemsSource = model.AllAzkarsSub;
            base.OnAppearing();
        }

        private void AltasbihNO_Clicked(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int x = (int)btn.CommandParameter;
            ZakrContentDB dB = new ZakrContentDB();
            ZakrContent sup = model.AllAzkarsSub.Where(c => c.id == x).FirstOrDefault();
            if (sup != null)
            {
                sup.Count -= 1;
                if (sup.Count == 0)
                {
                    model.AllAzkarsSub.Remove(sup);
                    return;
                }
                btn.Text = $"{ sup.Count}";
            }


        }

    }
}