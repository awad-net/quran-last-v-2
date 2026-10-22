using Plugin.MediaManager;
using Plugin.MediaManager.Abstractions.Enums;
using SlideOverKit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Quran
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class playquran : ContentPage, IMenuContainerPage
    {
        public SlideMenuView SlideMenu { get; set; }
        public Action ShowMenuAction { get; set; }
        public Action HideMenuAction { get; set; }

        private readonly string url;
        private string suraid;
        private int current = 1;
        private readonly bool isRowEven;
        public int suid;
        public int sid;
        public static double Rbytes = 0;
        public WebClient webclient = new WebClient();
        private bool toggle = false;
        private int font = 0, font1 = 0;
        private readonly QuranDB qurandb = new QuranDB();

        public playquran(int id, string _url, string img)
        {
            InitializeComponent();
            BindingContext = this;

            try
            {
                lblDownStatus.Text = Application.Current.Properties["savevalue"].ToString();
            }
            catch { }




            url = _url;
            sid = id;
            suid = id;
            getswar(img);
            App.player.Stop();
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


            scrollViewVisable.IsVisible = false;
            StackLayoutMap2.IsVisible = true;
            backFrame.IsVisible = false;

            Device.StartTimer(TimeSpan.FromSeconds(0.5), CheckIFInternetOverTime);

        }
        private bool CheckIFInternetOverTime()
        {
            IConnection networkConnection = DependencyService.Get<IConnection>();
            networkConnection.CheckConnection();
            if (!networkConnection.IsConnection)
            {
                //async aftar ( async() =>
                Device.BeginInvokeOnMainThread(() =>
                {
                    internetStatus = false;

                });
            }
            else
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    internetStatus = true;


                });
            }
            return true;
        }

        private readonly List<Quran> qurans;
        protected override void OnAppearing()
        {
            Device.StartTimer(TimeSpan.FromSeconds(0.5), UpdatePosition);
            base.OnAppearing();
        }


        public void getswar(string img)
        {
            imgShie.Source = img;

            CQuranlst.ItemsSource = qurandb.GetItemsAsync();
        }

        private void pquran(object o, EventArgs e)
        {


            downloadDB qurandb = new downloadDB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "download2.db3"));
            MenuItem mi = (MenuItem)o;
            Quran sura = (Quran)mi.CommandParameter;
            if (sura.id.ToString().Length == 1)
            {
                suraid = "00" + sura.id.ToString();
            }
            else if (sura.id.ToString().Length == 2)
            {
                suraid = "0" + sura.id.ToString();

            }
            else
            {
                suraid = sura.id.ToString();

            }
            try { CrossMediaManager.Current.Play(url + suraid + ".mp3"); } catch { }

        }

        private async void dquran(object o, EventArgs e)
        {

            if (internetStatus)
            {

                Progress<DownloadBytesProgress> i = new Progress<DownloadBytesProgress>();
                MenuItem mi = (MenuItem)o;
                Quran sura = (Quran)mi.CommandParameter;
                if (sura.id.ToString().Length == 1)
                {
                    suraid = "00" + sura.id.ToString();
                }
                else if (sura.id.ToString().Length == 2)
                {
                    suraid = "0" + sura.id.ToString();

                }
                else
                {
                    suraid = sura.id.ToString();

                }
                await CreateDownloadTask(url + suraid + ".mp3", i, sura);
            }
            else
            {
                await DisplayAlert("", "لايوجد اتصال بالانترنت", "موافق");
            }






        }

        private readonly bool check = false;
        private readonly int postion = 0;
        private int bytesRead;
        private byte[] buffer;
        private int download = 1;
        public async Task<int> CreateDownloadTask(string urlToDownload, IProgress<DownloadBytesProgress> progessReporter, Quran s)
        {

            int receivedBytes = 0;
            int totalBytes = 0;
            WebClient client = new WebClient();
            HttpClient httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromHours(5)
            };
            downloadDB qurandb = new downloadDB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "download2.db3"));
            List<Download> count = await qurandb.GetDownloadedShiouk(s.id, sid);



            if (count.Count < 1)
            {
                try
                {
                    using (Stream stream = await client.OpenReadTaskAsync(urlToDownload))
                    using (MemoryStream ms = new MemoryStream())
                    {


                        buffer = new byte[4096];
                        totalBytes = int.Parse(client.ResponseHeaders[HttpResponseHeader.ContentLength]);
                        for (; ; )
                        {


                            bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                            Debug.WriteLine(bytesRead);
                            ms.Write(buffer, 0, buffer.Length);
                            if (bytesRead == 0)
                            {
                                await Task.Yield();
                                break;
                            }

                            receivedBytes += bytesRead;

                        }
                        lblDownStatus.IsVisible = true;
                        download++;
                        string stringvalue = "جاري التحميل " + download.ToString() + "%";



                        Application.Current.Properties["savevalue"] = stringvalue;
                        await Application.Current.SavePropertiesAsync();
                        lblDownStatus.Text = stringvalue;
                        Debug.WriteLine(stringvalue);
                        Debug.WriteLine("lable" + lblDownStatus.Text);

                        Rbytes = (double)receivedBytes / totalBytes;

                        if (download == 114) { await DisplayAlert("", $"تم تحميل {((int)Rbytes * 100).ToString()} %", "موافق"); }

                        if (progessReporter != null)
                        {
                            DownloadBytesProgress args = new DownloadBytesProgress(urlToDownload, receivedBytes, totalBytes);
                            progessReporter.Report(args);
                            if (receivedBytes == totalBytes)
                            {




                                Random random = new Random();
                                int num = random.Next(1000000000);
                                string destination = Environment.GetFolderPath(Environment.SpecialFolder.Personal) +
                                        num.ToString() + ".mp3";
                                string pathToNewFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), "download");
                                Directory.CreateDirectory(pathToNewFolder);
                                using (FileStream file = new FileStream(pathToNewFolder + "/" +
                                        num.ToString() + ".mp3", FileMode.Create, System.IO.FileAccess.Write))
                                {
                                    ms.WriteTo(file);
                                    _ = await qurandb.SaveItemAsync(new Download { name = s.name, Ename = s.Ename, suraid = s.id, sid = sid, value = file.Name });

                                }
                            }
                        }
                    }
                }
                catch
                {
                    Debug.Write("error");
                }

            }
            else
            {
                await DisplayAlert("خطا", "تم تحميل هذا الملف سابقا", "OK");
                return 0;
            }
            Rbytes = receivedBytes / totalBytes;
            return receivedBytes / totalBytes;
        }

        private readonly QuranDB dB = new QuranDB();

        public async void down(object sender, EventArgs e)
        {

            if (internetStatus)
            {
                downloadDB qurandb = new downloadDB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "download2.db3"));
                qurandb.Delete(sid);
                for (int s = 1; s <= 114; s++)
                {
                    Progress<DownloadBytesProgress> i = new Progress<DownloadBytesProgress>();

                    if (s.ToString().Length == 1)
                    {
                        suraid = "00" + s.ToString();
                    }
                    else if (s.ToString().Length == 2)
                    {
                        suraid = "0" + s.ToString();

                    }
                    else
                    {
                        suraid = s.ToString();

                    }
                    Quran sura = dB.GetItemAsync(s);

                    await CreateDownloadTask(url + suraid + ".mp3", i, sura);

                }

            }
            else
            {
                await DisplayAlert("", "لايوجد اتصال بالانترنت", "موافق");
            }





            // string[] values = { url, sid.ToString() };
            //MessagingCenter.Send<object, String[]>(this, "url", values);
        }
        private void CQuranlst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Quran q = (Quran)e.CurrentSelection.FirstOrDefault();
            lblMaxValue.Text = "0:0:0";
            lblMinValue.Text = "0:0:0";
            current = q.id;
            CrossMediaManager.Current.Stop();
        }



        private  void ImageButton_Clicked(object sender, EventArgs e)
        {
            ImageButton button = sender as ImageButton;
            object id = button.CommandParameter;

            Quran su = dB.GetItemAsync((int)id);
            current = su.id;
            show(su);
            // ShowStack( su.name, int.Parse(suraid));
        }


        public void show(Quran quran)
        {
          {  
                suraname.Text = quran.name;
            //foreach (Xamarin.Forms.View item in StackLayoutMap1.Children)
            foreach (Xamarin.Forms.View item in StackLayoutMap1.Children.ToList())

                StackLayoutMap1.Children.Remove(item);
        
            }

            surat.Text = "بسم الله الرحمن الرحيم";
            scrollViewVisable.IsVisible = true;
            StackLayoutMap2.IsVisible = false;
            backFrame.IsVisible = true;

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
                font = 17;
                font1 = 10;
                Label x = new Label
                {
                    Text = ayat[i].Split('(')[0],
                    FontSize = 17,
                    TextColor = Color.Black,
                    HorizontalTextAlignment = TextAlignment.Start,
                    Margin = 10,
                };
                Label ex = new Label
                {
                    Text = Eayat[i].Split('(')[0],
                    FontSize = 10,
                    TextColor = Color.FromHex("#2C7648"),
                    HorizontalTextAlignment = TextAlignment.Start,
                    Margin = 10,
                };
                x.FontFamily = Device.RuntimePlatform == Device.iOS ? "ArbFONTS-Al-QuranAlKareem-Bold.ttf#ArbFONTS" : Device.RuntimePlatform == Device.Android ? "ArbFONTS-Al-QuranAlKareem-Bold.ttf#ArbFONTS2" : "Assets/ArbFONTS-Al-QuranAlKareem-Bold.ttf#ArbFONTS";
                ex.FontFamily = Device.RuntimePlatform == Device.iOS ? "ArbFONTS-Al-QuranAlKareem.ttf#ArbFONTS" : Device.RuntimePlatform == Device.Android ? "ArbFONTS-Al-QuranAlKareem.ttf#ArbFONTS2" : "Assets/ArbFONTS-Al-QuranAlKareem.ttf#ArbFONTS";
                y.FontFamily = Device.RuntimePlatform == Device.iOS ? "ArbFONTS-HSDream-Light.otf#ArbFONTS" : Device.RuntimePlatform == Device.Android ? "ArbFONTS-HSDream-Light.otf#ArbFONTS#ArbFONTS" : "Assets/ArbFONTS-HSDream-Light.otf#ArbFONTS#ArbFONTS";
                StackLayoutMap1.Children.Add(y);
                StackLayoutMap1.Children.Add(x);
                StackLayoutMap1.Children.Add(ex);
                StackLayoutMap1.Children.Add(box);





            }



        }
        private void btnBack_Clicked(object sender, EventArgs e)
        {
            AduioStack.IsVisible = true;

            MenuPopUp.IsVisible = false;
            scrollViewVisable.IsVisible = false;
            StackLayoutMap2.IsVisible = true;
            backFrame.IsVisible = false;


        }
      



        private async void prev_Clicked(object sender, EventArgs e)
        {

                if (current == 1)
                {
                    return;
                }

                current--;

                if (current.ToString().Length == 1)
                {
                    suraid = "00" + current.ToString();
                }
                else if (current.ToString().Length == 2)
                {
                    suraid = "0" + current.ToString();

                }
              

                    await CrossMediaManager.Current.Play(url + suraid + ".mp3");
                    flag = true;
                    Device.StartTimer(TimeSpan.FromSeconds(0.5), UpdatePosition);

               
                    await DisplayAlert("خطأ", "لا يمكن تحميل سورة سابقة", "OK");
                
            
            

        }

        private async void play_Clicked(object sender, EventArgs e)
        {
            
                Quran su = dB.GetItemAsync(current);
                show(su);
                if (current.ToString().Length == 1)
                {
                    suraid = "00" + current.ToString();
                }
                else if (current.ToString().Length == 2)
                {
                    suraid = "0" + current;

                }

                await CrossMediaManager.Current.Play(url + suraid + ".mp3");
                flag = true;

                Device.StartTimer(TimeSpan.FromSeconds(0.5), UpdatePosition);
            
          


        }

        private async void next_Clicked(object sender, EventArgs e)
        {
            if (current == 114)
            {
                return;
            }

            current++;

            if (current.ToString().Length == 1)
            {
                suraid = "00" + current.ToString();
            }
            else if (current.ToString().Length == 2)
            {
                suraid = "0" + current.ToString();

            }

            {
                await CrossMediaManager.Current.Play(url + suraid + ".mp3");
                flag = true;
                Device.StartTimer(TimeSpan.FromSeconds(0.5), UpdatePosition);


           
          
                await DisplayAlert("خطأ", "لا يمكن تحميل سورة سابقة", "OK");
            }
        }

        private async void stop_Clicked(object sender, EventArgs e)
        {

            await CrossMediaManager.Current.Stop();
            lblMinValue.Text = $"0:0:0";
            lblMaxValue.Text = $"0:0:0";

        }

        private bool flag = true;
        private TimeSpan postion1 = new TimeSpan(0, 0, 0);
        private bool internetStatus;

        private bool UpdatePosition()
        {
            MediaPlayerStatus mediaPlayerStatus = CrossMediaManager.Current.Status;
            if (mediaPlayerStatus == MediaPlayerStatus.Playing)
            {
                if (flag)
                {
                    int minD = (int)CrossMediaManager.Current.Duration.TotalSeconds / 60;
                    int hourD = (int)CrossMediaManager.Current.Duration.TotalSeconds / (60 * 60);
                    int secondD = (int)CrossMediaManager.Current.Duration.TotalSeconds % 60;
                    lblMaxValue.Text = $" {hourD}:{minD}:{secondD} ";
                    lblMaxValue.Text = $" {hourD}:{minD}:{secondD} ";

                    SliderPostion.Maximum = CrossMediaManager.Current.Duration.TotalSeconds;
                }
                int min = (int)CrossMediaManager.Current.Position.TotalSeconds / 60;
                int hour = (int)CrossMediaManager.Current.Position.TotalSeconds / (60 * 60);
                int second = (int)CrossMediaManager.Current.Position.TotalSeconds % 60;
                lblMinValue.Text = $"{hour}:{min}:{second}";
                postion1 = CrossMediaManager.Current.Position;
                flag = false;
                SliderPostion.Value = CrossMediaManager.Current.Position.TotalSeconds;
            }
            return true;
        }

        private async void btnSpeicalDown_Clicked(object sender, EventArgs e)
        {
            if (internetStatus)
            {
                ImageButton button = sender as ImageButton;

                object id = button.CommandParameter;

                Quran sura = dB.GetItemAsync((int)id);


                Progress<DownloadBytesProgress> i = new Progress<DownloadBytesProgress>();

                if (sura.id.ToString().Length == 1)
                {
                    suraid = "00" + sura.id.ToString();
                }
                else if (sura.id.ToString().Length == 2)
                {
                    suraid = "0" + sura.id.ToString();

                }
                else
                {
                    suraid = sura.id.ToString();

                }
                await CreateDownloadTask(url + suraid + ".mp3", i, sura);
            }
            else
            {
                await DisplayAlert("", "لايوجد اتصال بالانترنت", "موافق");
            }
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
    }
}
