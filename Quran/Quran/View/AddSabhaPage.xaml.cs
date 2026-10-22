using Quran.Model;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Quran.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddSabhaPage : ContentPage
    {
        public AddSabhaPage()
        {
            InitializeComponent();
        }

        private readonly TasbihDB dB = new TasbihDB();

        private async void btnAdditem_Clicked(object sender, EventArgs e)
        {
            string txt = entryAdd.Text;
            if (string.IsNullOrEmpty(txt))
            {
                await DisplayAlert("تحذير !!", "يجيب عليك ادخال النص في المكان المناسب", "موافق");
                return;
            }
            dB.Save(new Tasbih { ID = 0, Title = txt, NO = 0, color = "#B5B5B5" });
            await Navigation.PopAsync();
        }
    }
}