using Quran.Model;
using Quran.View;
using Quran.ViewModel;
using SlideOverKit;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
namespace Quran
{
    public partial class Sebha : ContentPage, IMenuContainerPage
    {
        private ViewModelAzkar model = new ViewModelAzkar();

        public SlideMenuView SlideMenu { get; set; }
        public Action ShowMenuAction { get; set; }
        public Action HideMenuAction { get; set; }
        public int sebhaid = 1;
        public List<string> list = new List<string>();
        public Sebha()
        {
            InitializeComponent();
            try
            {
                engtimeday.Text = Application.Current.Properties["engdate"].ToString();
                timeday.Text = Application.Current.Properties["ardate"].ToString();
                hijriday.Text = Application.Current.Properties["arhijri"].ToString();
                enghijriday.Text = Application.Current.Properties["enghijri"].ToString();
            }
            catch { }



        }

        protected override void OnAppearing()
        {

            model = new ViewModelAzkar();
            Tasbih tasbih = model.AllTasbihs.Where(c => c.color == "#CEFF00").FirstOrDefault();
            if (tasbih != null)
            {
                lblTas.Text = $"{tasbih.NO}";
            }
            else
            {
                lblTas.Text = $"0";
            }

            CTasbihlst.ItemsSource = model.AllTasbihs;

            base.OnAppearing();
        }

        private readonly TasbihDB dB = new TasbihDB();

        private void CTasbihlst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                Tasbih tasbih = e.CurrentSelection.FirstOrDefault() as Tasbih;

                if (tasbih != null)
                {
                    Tasbih tasbih1 = model.AllTasbihs.Where(c => c.ID == tasbih.ID).FirstOrDefault();
                    foreach (Tasbih item in model.AllTasbihs)
                    {
                        if (item.ID == tasbih1.ID)
                        {
                            continue;
                        }

                        item.color = "#B5B5B5";
                        dB.Save(item);

                    }
                    tasbih1.NO++;
                    tasbih1.color = "#CEFF00";
                    dB.Save(tasbih1);
                    model = new ViewModelAzkar();
                    CTasbihlst.ItemsSource = model.AllTasbihs;
                    lblTas.Text = $"{tasbih1.NO}";
                }

            }

            CTasbihlst.SelectedItem = null;

        }

        private void btnDelete_Clicked(object sender, EventArgs e)
        {
            ImageButton button = sender as ImageButton;
            object id = button.CommandParameter;
            dB.DeleteItem((int)id);
            Tasbih tasbih = dB.getItem((int)id);
            model = new ViewModelAzkar();
            CTasbihlst.ItemsSource = model.AllTasbihs;
            if (model.AllTasbihs.Count == 0)
            {
                lblTas.Text = "0";
            }
        }

        private void btnClean_Clicked(object sender, EventArgs e)
        {

            foreach (Tasbih item in model.AllTasbihs)
            {
                item.NO = 0;
                item.color = "#B5B5B5";
                dB.Save(item);
            }
            lblTas.Text = "0";
            model = new ViewModelAzkar();
            CTasbihlst.ItemsSource = model.AllTasbihs;
        }

        private void btnDeleteAll_Clicked(object sender, EventArgs e)
        {

            foreach (Tasbih item in model.AllTasbihs)
            {

                dB.DeleteItem(item.ID);
            }
            lblTas.Text = "0";
            model = new ViewModelAzkar();
            CTasbihlst.ItemsSource = model.AllTasbihs;
        }

        private async void btnAdd_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new AddSabhaPage(), true);
        }


    }
}
