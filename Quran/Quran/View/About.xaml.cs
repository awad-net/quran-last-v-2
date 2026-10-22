using SlideOverKit;
using System;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Quran
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class About : ContentPage, IMenuContainerPage
    {
        public SlideMenuView SlideMenu { get; set; }
        public Action ShowMenuAction { get; set; }
        public Action HideMenuAction { get; set; }
        public About()
        {

            InitializeComponent();

            ToolbarItem menu = ToolbarItems.Last();
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




            if (Application.Current.Properties.ContainsKey("fajr_notification"))
            {
                try
                {
                    string onoroff = Application.Current.Properties["fajr_notification"].ToString();
                    Debug.WriteLine(onoroff);
                    if (onoroff == "True") { switch1.IsToggled = true; } else { switch1.IsToggled = false; }

                }
                catch { }



            }
            else { switch1.IsToggled = true; }
            if (Application.Current.Properties.ContainsKey("duhr_notification"))
            {
                try
                {
                    string onoroff = Application.Current.Properties["duhr_notification"].ToString();
                    Debug.WriteLine(onoroff);
                    if (onoroff == "True") { switch2.IsToggled = true; } else { switch2.IsToggled = false; }

                }
                catch { }

            }
            else { switch2.IsToggled = true; }
            if (Application.Current.Properties.ContainsKey("asr_notification"))
            {
                try
                {
                    string onoroff = Application.Current.Properties["asr_notification"].ToString();
                    Debug.WriteLine(onoroff);
                    if (onoroff == "True") { switch3.IsToggled = true; } else { switch3.IsToggled = false; }
                }
                catch { }


            }
            else { switch3.IsToggled = true; }
            if (Application.Current.Properties.ContainsKey("maghreb_notification"))
            {
                try
                {
                    string onoroff = Application.Current.Properties["maghreb_notification"].ToString();
                    Debug.WriteLine(onoroff);
                    if (onoroff == "True") { switch4.IsToggled = true; } else { switch4.IsToggled = false; }
                }
                catch { }


            }
            else { switch4.IsToggled = true; }
            if (Application.Current.Properties.ContainsKey("isha_notification"))
            {
                try
                {
                    string onoroff = Application.Current.Properties["isha_notification"].ToString();
                    Debug.WriteLine(onoroff);
                    if (onoroff == "True") { switch5.IsToggled = true; } else { switch5.IsToggled = false; }

                }
                catch { }

            }
            else { switch5.IsToggled = true; }





        }
        private async void Switch_fajr(object sender, ToggledEventArgs e)
        {

            fireorno = 1;
            Debug.WriteLine(e.Value);
            switchvalue1 = e.Value.ToString();
            Application.Current.Properties["fajr_notification"] = e.Value;
            await Application.Current.SavePropertiesAsync();
            Debug.WriteLine("run");
        }

        private async void Switch_duhr(object sender, ToggledEventArgs e)
        {
            fireorno = 1;
            Application.Current.Properties["duhr_notification"] = e.Value;
            await Application.Current.SavePropertiesAsync();
            switchvalue2 = e.Value.ToString();
            Debug.WriteLine("run");
        }

        private async void Switch_asr(object sender, ToggledEventArgs e)
        {
            fireorno = 1;
            switchvalue3 = e.Value.ToString();
            Application.Current.Properties["asr_notification"] = e.Value;
            await Application.Current.SavePropertiesAsync();
            Debug.WriteLine("run");
        }

        private async void Switch_maghreb(object sender, ToggledEventArgs e)
        {
            fireorno = 1;
            switchvalue4 = e.Value.ToString();
            Application.Current.Properties["maghreb_notification"] = e.Value;
            await Application.Current.SavePropertiesAsync();
            Debug.WriteLine("run");
        }

        private async void Switch_isha(object sender, ToggledEventArgs e)
        {
            fireorno = 1;
            switchvalue5 = e.Value.ToString();
            Application.Current.Properties["isha_notification"] = e.Value;
            await Application.Current.SavePropertiesAsync();

        }

        private int fireorno = 0;
        private string switchvalue1;
        private string switchvalue2;
        private string switchvalue3;
        private string switchvalue4;
        private string switchvalue5;
        protected override async void OnDisappearing()
        {


            if (fireorno == 1)
            {
                Application.Current.Properties["fajr_notification"] = switchvalue1;
                await Application.Current.SavePropertiesAsync();
                Debug.WriteLine("run");



                Application.Current.Properties["duhr_notification"] = switchvalue2;
                await Application.Current.SavePropertiesAsync();
                Debug.WriteLine("run");



                Application.Current.Properties["asr_notification"] = switchvalue3;
                await Application.Current.SavePropertiesAsync();
                Debug.WriteLine("run");



                Application.Current.Properties["maghreb_notification"] = switchvalue4;
                await Application.Current.SavePropertiesAsync();
                Debug.WriteLine("run");



                Application.Current.Properties["isha_notification"] = switchvalue5;
                await Application.Current.SavePropertiesAsync();

                Application.Current.Properties["page1"] = "change";
                await Application.Current.SavePropertiesAsync();
                fireorno = 0;
            }







        }

        private async void ViewCell_Tapped(object sender, EventArgs e)
        {


            bool result = await DisplayAlert("تقييم التطبيق", "تقييم التطبيق يساعدنا في مواصلة الدعم :)", "تقيم", "ليس الان");
            if (result == true) // if it's equal to Ok
            {
                Debug.WriteLine("yes");

                switch (Device.RuntimePlatform)
                {
                    case Device.iOS:
                        Device.OpenUri(new Uri("https://apps.apple.com/app/id1296917314?action=write-review"));
                        break;
                    case Device.Android:
                        Device.OpenUri(new Uri("https://play.google.com/store/apps/details?id=com.muslim.Quran"));
                        break;
                }





            }
        }
    }
}