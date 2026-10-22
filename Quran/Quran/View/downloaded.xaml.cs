
using Plugin.MediaManager;
using SlideOverKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Quran
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class downloaded : ContentPage, IMenuContainerPage
    {
        public SlideMenuView SlideMenu { get; set; }
        public Action ShowMenuAction { get; set; }
        public Action HideMenuAction { get; set; }
        public int shiekhid;
        public int current;
        private List<Download> downloads;
        private int count = 0;
        private bool toggle = false;
        private int font = 0;
        private int font1;

        public downloaded(int id, string imgShai, string name)
        {
            InitializeComponent();
            shiekhid = id;
            lblTitle.Text = name;
            download();
            imgShie.Source = imgShai;

            NavigationPage.SetHasBackButton(this, false);
        }

        private async void download()
        {
            downloadDB qurandb = new downloadDB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "download2.db3"));
            Cdownloadlst.ItemsSource = await qurandb.GetDownloadedSwar(shiekhid);
            downloads = await qurandb.GetDownloadedSwar(shiekhid);

            current = qurandb.GetFirst(shiekhid).Id;
        }

        protected override void OnAppearing()
        {
            CrossMediaManager.Current.Stop();
            if (App.player.IsPlaying)
            {
                Device.StartTimer(TimeSpan.FromSeconds(0.5), UpdatePosition);
            }

            base.OnAppearing();
        }
        private void Cdownloadlst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                Download file = e.CurrentSelection.FirstOrDefault() as Download;
                if (file != null)
                {
                    for (int i = 0; i < downloads.Count; i++)
                    {
                        if (downloads[i].id == file.id)
                        {
                            count = i;
                            break;
                        }
                    }
                    flgBack = true;
                    PlayAudio(file);

                }
            }

        }

        private void PlayAudio(Download download)
        {
            Stream audioStram = File.OpenRead(download.value);

            App.player.Load(audioStram);
            App.player.Play();

            StackLayoutMap1.Children.Clear();

            Show(download.suraid, download.name);

            Device.StartTimer(TimeSpan.FromSeconds(0.5), UpdatePosition);
        }

        public void Show(int suraid, string name)
        {

            suraname.Text = name;
            surat.Text = "بِسْمِ اللَّهِ الرَّحْمَنِ الرَّحِيمِ";
            scrollViewVisable.IsVisible = true;
            backFrame.IsVisible = true;
            Cdownloadlst.IsVisible = false;

            QuranDB dB = new QuranDB();
            Quran quran = dB.GetItemAsync(suraid);

            int index = 0;
            string[] ayat = quran.aya.Split('\'');
            string[] Eayat = quran.Eaya.Split('\'');
            for (int i = 0; i < ayat.Length; i++)
            {
                Label y = new Label();
                BoxView box = new BoxView();

                if (index < ayat.Length - 1)
                {
                    y = new Label
                    {
                        Text = "﴾" + (++index) + "﴿",
                        FontSize = 13,
                        HorizontalOptions = LayoutOptions.EndAndExpand,
                        HorizontalTextAlignment = TextAlignment.End,
                        Margin = 0,
                        TextColor = Color.Black
                    };
                    box = new BoxView
                    {
                        Opacity = 0.2,
                        HeightRequest = 0.8,
                        BackgroundColor = Color.Black

                    };

                }

                Label x = new Label
                {
                    Text = ayat[i].Split('(')[0],
                    FontSize = 17,
                    TextColor = Color.Black,
                    HorizontalOptions = LayoutOptions.EndAndExpand,
                    HorizontalTextAlignment = TextAlignment.Start,
                    Margin = 10,



                };
                Label ex = new Label
                {
                    Text = Eayat[i].Split('(')[0],
                    FontSize = 10,
                    TextColor = Color.FromHex("#2C7648"),
                    HorizontalOptions = LayoutOptions.EndAndExpand,
                    HorizontalTextAlignment = TextAlignment.Start,
                    Margin = 10,
                };
                font = 15;
                font1 = 10;
                x.FontFamily = Device.RuntimePlatform == Device.iOS ? "ArbFONTS-Al-QuranAlKareem-Bold.ttf#ArbFONTS" : Device.RuntimePlatform == Device.Android ? "ArbFONTS-Al-QuranAlKareem-Bold.ttf#ArbFONTS2" : "Assets/ArbFONTS-Al-QuranAlKareem-Bold.ttf#ArbFONTS";
                ex.FontFamily = Device.RuntimePlatform == Device.iOS ? "ArbFONTS-Al-QuranAlKareem.ttf#ArbFONTS" : Device.RuntimePlatform == Device.Android ? "ArbFONTS-Al-QuranAlKareem.ttf#ArbFONTS2" : "Assets/ArbFONTS-Al-QuranAlKareem.ttf#ArbFONTS";
                y.FontFamily = Device.RuntimePlatform == Device.iOS ? "ArbFONTS-HSDream-Light.otf#ArbFONTS" : Device.RuntimePlatform == Device.Android ? "ArbFONTS-HSDream-Light.otf#ArbFONTS#ArbFONTS" : "Assets/ArbFONTS-HSDream-Light.otf#ArbFONTS#ArbFONTS";


                x.FontAttributes = FontAttributes.Bold;
                StackLayoutMap1.Children.Add(y);
                StackLayoutMap1.Children.Add(x);
                StackLayoutMap1.Children.Add(ex);
                StackLayoutMap1.Children.Add(box);
            }



        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            MenuPopUp.IsVisible = false;

            StackLayoutMap1.Children.Clear();
            scrollViewVisable.IsVisible = false;
            Cdownloadlst.IsVisible = true;
            backFrame.IsVisible = false;

            Cdownloadlst.SelectedItem = null;

        }

        private bool UpdatePosition()
        {
          
            SliderPostion.Maximum = App.player.Duration;
            SliderPostion.IsEnabled = App.player.CanSeek;
            SliderPostion.ValueChanged -= SliderPostion_ValueChanged;
            SliderPostion.Value = App.player.CurrentPosition;
            SliderPostion.ValueChanged += SliderPostion_ValueChanged;



            lblMaxValue.Text = $"{(int)App.player.Duration / (60 * 60)}:{(int)App.player.Duration / 60}:{(int)App.player.Duration}";
            lblMinValue.Text = $"{(int)App.player.CurrentPosition / (60 * 60)}:{(int)App.player.CurrentPosition / 60}:{(int)App.player.CurrentPosition}";

            if (!App.player.IsPlaying)
            {
                if (count != downloads.Count - 1)
                {
                    count++;
                    Download file = downloads[count];
                    PlayAudio(file);
                }

            }
            return App.player.IsPlaying;


        }

        private void btnMore_Clicked(object sender, EventArgs e)
        {

            if (toggle == false)
            {
                MenuPopUp.IsVisible = true;
                toggle = true;
            }
            else
            {
                MenuPopUp.IsVisible = false;
                toggle = false;

            }
        }

        private void btnIncress_Clicked(object sender, EventArgs e)
        {

            font++;
            font1++;
            foreach (Xamarin.Forms.View item in StackLayoutMap1.Children)
            {
                if (item is Label)
                {

                    if (((Label)item).FontSize == font1 - 1)
                    {
                        ((Label)item).FontSize = font1;
                    }
                    else if (((Label)item).FontSize == font - 1)
                    {
                        ((Label)item).FontSize = font;
                    }
                }
            }

        }

        private void btnDecres_Clicked(object sender, EventArgs e)
        {
            font--;
            font1--;
            foreach (Xamarin.Forms.View item in StackLayoutMap1.Children)
            {
                if (item is Label)
                {

                    //((Label)item).FontSize = font;

                    if (((Label)item).FontSize == font1 + 1)
                    {
                        ((Label)item).FontSize = font1;
                    }
                    else if (((Label)item).FontSize == font + 1)
                    {
                        ((Label)item).FontSize = font;
                    }
                }
            }
        }

        private async void prev_Clicked(object sender, EventArgs e)
        {
            if (count == 0)
            {
                await DisplayAlert("خطأ", "لا يمكن تحميل سورة سابقة", "OK");
                return;
            }
            count--;
            Download file = downloads[count];
            PlayAudio(file);
        }

        private void play_Clicked(object sender, EventArgs e)
        {

            Download file = downloads[count];
            PlayAudio(file);
        }

        private async void next_Clicked(object sender, EventArgs e)
        {
            if (count == downloads.Count - 1)
            {
                await DisplayAlert("خطأ", "لا يمكن تحميل سورة الاحقة", "OK");
                return;
            }
            count++;
            Download file = downloads[count];
            PlayAudio(file);

        }

        private void stop_Clicked(object sender, EventArgs e)
        {
            if (App.player.IsPlaying)
            {
                App.player.Pause();
            }
            else
            {
                App.player.Play();
                if (App.player.IsPlaying)
                {
                    Device.StartTimer(TimeSpan.FromSeconds(0.5), UpdatePosition);
                }
            }
        }

        private void SliderPostion_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (SliderPostion.Value != App.player.Duration)
            {
                App.player.Seek(SliderPostion.Value);
                Device.StartTimer(TimeSpan.FromSeconds(0.5), UpdatePosition);


            }
        }

        private bool flgBack = false;


        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            if (!flgBack)
            {
                await Navigation.PopAsync();
            }
            else
            {
                MenuPopUp.IsVisible = false;

                StackLayoutMap1.Children.Clear();
                scrollViewVisable.IsVisible = false;
                Cdownloadlst.IsVisible = true;
                backFrame.IsVisible = false;

                Cdownloadlst.SelectedItem = null;
                flgBack = false;
            }
        }
    }
}


