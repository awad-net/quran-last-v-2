using Newtonsoft.Json.Linq;
using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using SlideOverKit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Quran

{
    public partial class Salaty : ContentPage, IMenuContainerPage
    {
        public SlideMenuView SlideMenu { get; set; }
        public Action ShowMenuAction { get; set; }
        public Action HideMenuAction { get; set; }

        public Salaty()

        {

            InitializeComponent();
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if (Application.Current.Properties.ContainsKey("page1"))
                {
                    if (Application.Current.Properties["page1"] == "change")
                    {
                        Application.Current.Properties["page1"] = "done";
                        Application.Current.SavePropertiesAsync();
                        switch (Device.RuntimePlatform)
                        {

                            case Device.iOS:
                                Notificatioios();
                                break;
                            case Device.Android:
                                notificatioandorid();
                                break;

                        }
                        Debug.WriteLine("done");


                    }

                }



                return true;
            });
            int today = (int)DateTime.Now.DayOfWeek;
            if (today == 6) { timeday.Text = "السبت" + " " + DateTime.Now.ToString("yyy/MM/dd", CultureInfo.InvariantCulture); }
            if (today == 0) { timeday.Text = "الاحد" + " " + DateTime.Now.ToString("yyy/MM/dd", CultureInfo.InvariantCulture); }
            if (today == 1) { timeday.Text = "الاثنين" + " " + DateTime.Now.ToString("yyy/MM/dd", CultureInfo.InvariantCulture); }
            if (today == 2) { timeday.Text = "الثلاثاء" + " " + DateTime.Now.ToString("yyy/MM/dd", CultureInfo.InvariantCulture); }
            if (today == 3) { timeday.Text = "الاربعاء" + " " + DateTime.Now.ToString("yyy/MM/dd", CultureInfo.InvariantCulture); }
            if (today == 4) { timeday.Text = "الخميس" + " " + DateTime.Now.ToString("yyy/MM/dd", CultureInfo.InvariantCulture); }
            if (today == 5) { timeday.Text = "الجمعه" + " " + DateTime.Now.ToString("yyy/MM/dd", CultureInfo.InvariantCulture); }

            if (today == 6) { engtimeday.Text = "Saturday" + " " + DateTime.Now.ToString("yyy/MM/dd", CultureInfo.InvariantCulture); }
            if (today == 0) { engtimeday.Text = "Sunday" + " " + DateTime.Now.ToString("yyy/MM/dd", CultureInfo.InvariantCulture); }
            if (today == 1) { engtimeday.Text = "Monday" + " " + DateTime.Now.ToString("yyy/MM/dd", CultureInfo.InvariantCulture); }
            if (today == 2) { engtimeday.Text = "Tuesday" + " " + DateTime.Now.ToString("yyy/MM/dd", CultureInfo.InvariantCulture); }
            if (today == 3) { engtimeday.Text = "Wednesday" + " " + DateTime.Now.ToString("yyy/MM/dd", CultureInfo.InvariantCulture); }
            if (today == 4) { engtimeday.Text = "Thursday" + " " + DateTime.Now.ToString("yyy/MM/dd", CultureInfo.InvariantCulture); }
            if (today == 5) { engtimeday.Text = "Friday" + " " + DateTime.Now.ToString("yyy/MM/dd", CultureInfo.InvariantCulture); }

            try
            {
                string hijrykey = "hijrijson" + DateTime.Now.Month;
                if (Application.Current.Properties.ContainsKey(hijrykey))
                {
                    //Debug.WriteLine("sayedss" + hijrykey);
                    realhijrijson = Application.Current.Properties[hijrykey].ToString();

                    JObject jsonhijri = JObject.Parse(realhijrijson);
                    string hijridayx = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["day"].ToString();



                    string hijrimounth = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["month"]["ar"].ToString();





                    string _hijrimounth = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["month"]["en"].ToString();



                    string hijriyear = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["year"].ToString();

                    hijriday.Text = hijridayx + " " + hijrimounth + " " + hijriyear + " ";
                    enghijriday.Text = hijridayx + " " + _hijrimounth + " " + hijriyear + " ";
                }
                else
                { hijri(); }

            }
            catch { }

            if (Application.Current.Properties.ContainsKey("fajr_notification"))
            { }
            else
            {

                Application.Current.Properties["fajr_notification"] = "True";
                Application.Current.Properties["duhr_notification"] = "True";
                Application.Current.Properties["asr_notification"] = "True";
                Application.Current.Properties["maghreb_notification"] = "True";
                Application.Current.Properties["isha_notification"] = "True";

            }




            MessagingCenter.Subscribe<right, string>(this, "Hi", (sender, arg) =>
            {
                switch (arg)
                {
                    case "الصفحة الرئيسة":
                        ToolbarItem menu2 = ToolbarItems.Last();

                        menu2.Command = new Command(() =>
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
                        break;
                    case "اوقات الصلاه":
                        ToolbarItem menu3 = ToolbarItems.Last();

                        menu3.Command = new Command(() =>
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
                        break;
                    case "الشيوخ":
                        Navigation.PushAsync(new MainPage());
                        break;
                    case "التنزيلات":
                        Navigation.PushAsync(new downloadedshioukh());
                        break;
                    case "الاذكار":
                        Navigation.PushAsync(new AzkarPage());
                        break;
                    case "القبله":
                        Navigation.PushAsync(new qipla());
                        break;
                    case "السبحة":
                        Navigation.PushAsync(new Sebha());
                        break;
                    case "إعدادات الأذان":
                        Navigation.PushAsync(new About());
                        break;
                }
            });
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

            Application.Current.Properties["engdate"] = engtimeday.Text;
            Application.Current.Properties["ardate"] = timeday.Text;
            Application.Current.Properties["arhijri"] = hijriday.Text;
            Application.Current.Properties["enghijri"] = enghijriday.Text;

        }

        [Obsolete]
        protected override async void OnAppearing()
        {
            try
            {
                PermissionStatus status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        await DisplayAlert("Need location", "Gunna need that location", "OK");
                    }

                    Dictionary<Permission, PermissionStatus> results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    status = results[Permission.Location];
                }

                if (status == PermissionStatus.Granted)
                {

                    ///// Find Location /////
                    Plugin.Geolocator.Abstractions.IGeolocator locator = CrossGeolocator.Current;
                    Plugin.Geolocator.Abstractions.Position position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));
                    string positionLatitude = position.Latitude.ToString(); ;
                    string positionlongtude = position.Longitude.ToString(); ;
                    Debug.WriteLine("post" + positionLatitude);
                    Debug.WriteLine("post" + positionlongtude);
                    Debug.WriteLine("done");
                    string lat = positionLatitude.Replace(",", ".");
                    string long_ = positionlongtude.Replace(",", ".");
                    Application.Current.Properties["positionLatitude"] = lat;
                    await Application.Current.SavePropertiesAsync();
                    Application.Current.Properties["positionlongtude"] = long_;
                    await Application.Current.SavePropertiesAsync();

                    praytimespop();

                }
                else if (status != PermissionStatus.Unknown)
                {
                    await DisplayAlert("Location Denied", "Can not continue, try again.", "OK");
                }
            }
            catch {}
           
        }

        private string positionLatitude;
        private string positionlongtude;
        private string method;
        private int someInt;
        private int someInt2;
        private string prayjosn;

        private async void praytimeonline()
        {


            try
            {
                if (Application.Current.Properties.ContainsKey("positionLatitude"))
                {
                    positionLatitude = Application.Current.Properties["positionLatitude"].ToString();
                    positionlongtude = Application.Current.Properties["positionlongtude"].ToString();
                    Debug.WriteLine(positionLatitude);
                    Debug.WriteLine(positionlongtude);




                }

                Debug.WriteLine("start");


                /////// Find country ///

                string secondurl = "http://api.geonames.org/timezoneJSON?lat=" + positionLatitude + "&lng=" + positionlongtude + "&username=sayed321";


                HttpClient _clint2 = new HttpClient();


                Task<string> contact2 = _clint2.GetStringAsync(secondurl);
                string firstlocaljosn2 = await contact2;

                JObject json2 = JObject.Parse(firstlocaljosn2);
                string country = json2["countryCode"].ToString();
                string countryname = json2["countryName"].ToString();
                string city = countryname;
                Application.Current.Properties["city"] = city;
                await Application.Current.SavePropertiesAsync();
                Debug.WriteLine(countryname);
                double timezone2 = (double)json2["dstOffset"];


                double someDouble2 = timezone2;
                someInt2 = (int)someDouble2;
                Debug.WriteLine("timezone is  " + someInt2);
                /////// Find country /////

                Application.Current.Properties["countryname"] = countryname;
                await Application.Current.SavePropertiesAsync();



                /////// Find methods and timezone /////
                try
                {
                    string firsturl = "http://muslimsalat.com/" + countryname + "/daily.json?key=a08aeef74c7903c37852f2fa2acb5f88";
                    Debug.WriteLine(firsturl);

                    HttpClient _clint = new HttpClient();

                    Task<string> contact = _clint.GetStringAsync(firsturl);
                    string firstlocaljosn = await contact;


                    JObject json = JObject.Parse(firstlocaljosn);
                    method = json["method"].ToString();
                    JObject json3 = JObject.Parse(firstlocaljosn);
                    double timezone = (double)json["timezone"];

                    double someDouble = timezone;
                    someInt = (int)someDouble;
                    Debug.WriteLine(someInt);


                }
                catch
                {

                    try
                    {

                        string firsturl = "http://muslimsalat.com/" + country + "/daily.json?key=a08aeef74c7903c37852f2fa2acb5f88";
                        Debug.WriteLine(firsturl);

                        HttpClient _clint = new HttpClient();

                        Task<string> contact = _clint.GetStringAsync(firsturl);
                        string firstlocaljosn = await contact;


                        JObject json = JObject.Parse(firstlocaljosn);
                        method = json["method"].ToString();
                        JObject json3 = JObject.Parse(firstlocaljosn);
                        double timezone = (double)json["timezone"];

                        double someDouble = timezone;
                        someInt = (int)someDouble;
                        Debug.WriteLine(someInt);


                    }
                    catch {
                        await DisplayAlert("فشل تحديد الموقع", "فشل في تحديد الموقع تاكد من الاتصال بالانترنت وتفعيل خاصية تحديد الموقع", "OK");
                    }

                }



                if (method == "1")
                {

                    method = "Egypt";

                }
                if (method == "2")
                {

                    method = "Karachi";
                }
                if (method == "3")
                {

                    method = "Karachi";
                }
                if (method == "4")
                {

                    method = "ISNA";
                }
                if (method == "5")
                {

                    method = "MWL";
                }
                if (method == "6")
                {

                    method = "Makkah";
                }

                if (method == "7")
                {

                    method = "Makkah";
                }
                /////// Find methods and timezone /////

                /////// Find praytimes /////



                try
                {

                    string prayurl = "http://api.islamhouse.com/v1/VB2JyTShXIqIVgvf/services/praytime/get-times/" + DateTime.Now.Year + "/" + method + "/" + positionLatitude + "/" + positionlongtude + "/" + someInt2 + "/json";

                    Debug.WriteLine(prayurl);
                    Debug.WriteLine(positionLatitude);
                    Debug.WriteLine(positionlongtude);
                    Debug.WriteLine(someInt2);

                    HttpClient _prayhttp = new HttpClient();

                    Task<string> contactpray = _prayhttp.GetStringAsync(prayurl);

                    prayjosn = await contactpray;



                    JObject praytimes = JObject.Parse(prayjosn);
                    int day = DateTime.Now.DayOfYear - 1;
                    string fajr = praytimes["days"][day]["times"][0].ToString();
                    string sunrise = praytimes["days"][day]["times"][1].ToString();
                    string duhr = praytimes["days"][day]["times"][2].ToString();
                    string asr = praytimes["days"][day]["times"][3].ToString();
                    string maghreb = praytimes["days"][day]["times"][4].ToString();
                    string isha = praytimes["days"][day]["times"][6].ToString();

                    Debug.WriteLine("offline");


                    Application.Current.Properties["praytimes"] = prayjosn;
                    await Application.Current.SavePropertiesAsync();



                }
                catch
                {
                    string prayurl = "http://api.islamhouse.com/v1/VB2JyTShXIqIVgvf/services/praytime/get-times/" + DateTime.Now.Year + "/" + method + "/" + positionLatitude + "/" + positionlongtude + "/" + someInt + "/json";

                    Debug.WriteLine(prayurl);
                    Debug.WriteLine(prayurl);
                    Debug.WriteLine(positionLatitude);
                    Debug.WriteLine(positionlongtude);
                    Debug.WriteLine(someInt);

                    HttpClient _prayhttp = new HttpClient();

                    Task<string> contactpray = _prayhttp.GetStringAsync(prayurl);

                    prayjosn = await contactpray;



                    JObject praytimes = JObject.Parse(prayjosn);
                    int day = DateTime.Now.DayOfYear - 1;
                    string fajr = praytimes["days"][day]["times"][0].ToString();
                    string sunrise = praytimes["days"][day]["times"][1].ToString();
                    string duhr = praytimes["days"][day]["times"][2].ToString();
                    string asr = praytimes["days"][day]["times"][3].ToString();
                    string maghreb = praytimes["days"][day]["times"][4].ToString();
                    string isha = praytimes["days"][day]["times"][6].ToString();

                    Debug.WriteLine("offline");


                    Application.Current.Properties["praytimes"] = prayjosn;
                    await Application.Current.SavePropertiesAsync();



                }
                praytimespop();
                textforpraytimes();
                /////// Find praytimes /////

                //if (numbernotification == 0)
                //{

                //    switch (Device.RuntimePlatform)
                //    {

                //        case Device.iOS:
                //            Notificationios();
                //            break;
                //        case Device.Android:
                //            notificationandorid();
                //            break;

                //    }

                //}
                //else { Debug.WriteLine("alredy done"); }





            }
            catch
            {

                Debug.WriteLine("error");
                if (Application.Current.Properties.ContainsKey("praytimes"))
                {



                }
                else
                {



                }



            }

        }

        private void praytimespop()
        {

            try
            {
                if (Application.Current.Properties.ContainsKey("praytimes"))
                {
                    prayjosn = Application.Current.Properties["praytimes"].ToString();
                    JObject praytimes = JObject.Parse(prayjosn);
                    int day = DateTime.Now.DayOfYear - 1;

                    string fajr = praytimes["days"][day]["times"][0].ToString();
                    string sunrise = praytimes["days"][day]["times"][1].ToString();
                    string duhr = praytimes["days"][day]["times"][2].ToString();
                    string asr = praytimes["days"][day]["times"][3].ToString();
                    string maghreb = praytimes["days"][day]["times"][4].ToString();
                    string isha = praytimes["days"][day]["times"][6].ToString();

                    //// TEXT PRAY TIMES

                    string x0 = sunrise;

                    string h1_sun = x0.ToCharArray()[0].ToString();
                    string h2_sun = x0.ToCharArray()[1].ToString();
                    string m1_sun = x0.ToCharArray()[3].ToString();
                    string m2_sun = x0.ToCharArray()[4].ToString();

                    int hour_sun = Convert.ToInt32(h1_sun + "" + h2_sun);
                    string minute_sun = (m1_sun + "" + m2_sun);
                    string pm_am1_sun;
                    string fajr_hour_sun;

                    if (hour_sun > 12)
                    {

                        pm_am1_sun = "م";

                        fajr_hour_sun = (hour_sun - 12).ToString();


                    }
                    else
                    {

                        if (hour_sun == 12)
                        {
                            pm_am1_sun = "م";
                        }
                        else
                        {
                            pm_am1_sun = "ص";
                        }

                        fajr_hour_sun = (hour_sun).ToString();

                    }

                    string x = fajr;

                    string h1 = x.ToCharArray()[0].ToString();
                    string h2 = x.ToCharArray()[1].ToString();
                    string m1 = x.ToCharArray()[3].ToString();
                    string m2 = x.ToCharArray()[4].ToString();

                    int hour = Convert.ToInt32(h1 + "" + h2);
                    string minute = (m1 + "" + m2);
                    string pm_am1;
                    string fajr_hour;

                    if (hour > 12)
                    {

                        pm_am1 = "م";

                        fajr_hour = (hour - 12).ToString();


                    }
                    else
                    {

                        if (hour == 12)
                        {
                            pm_am1 = "م";
                        }
                        else
                        {
                            pm_am1 = "ص";
                        }

                        fajr_hour = (hour).ToString();

                    }


                    string x2 = duhr;

                    string duhrh1 = x2.ToCharArray()[0].ToString();
                    string duhrh2 = x2.ToCharArray()[1].ToString();
                    string duhrm1 = x2.ToCharArray()[3].ToString();
                    string duhrm2 = x2.ToCharArray()[4].ToString();

                    int d_hour = Convert.ToInt32(duhrh1 + "" + duhrh2);
                    string d_minute = (duhrm1 + "" + duhrm2);
                    string pm_am2;
                    string duhr_hour;
                    if (d_hour > 12)
                    {

                        pm_am2 = "م";
                        duhr_hour = (d_hour - 12).ToString();

                    }
                    else
                    {

                        if (d_hour == 12)
                        {
                            pm_am2 = "م";
                        }
                        else
                        {
                            pm_am2 = "ص";
                        }
                        duhr_hour = (d_hour).ToString();


                    }


                    //Debug.WriteLine(duhr_Text);

                    string x3 = asr;

                    string asrh1 = x3.ToCharArray()[0].ToString();
                    string asrh2 = x3.ToCharArray()[1].ToString();
                    string asrm1 = x3.ToCharArray()[3].ToString();
                    string asrm2 = x3.ToCharArray()[4].ToString();

                    int a_hour = Convert.ToInt32(asrh1 + "" + asrh2);

                    string a_minute = (asrm1 + "" + asrm2);
                    string pm_am3;
                    string asr_hour;
                    if (a_hour > 12)
                    {

                        pm_am3 = "م";
                        asr_hour = (a_hour - 12).ToString();
                    }
                    else
                    {
                        if (a_hour == 12)
                        {
                            pm_am3 = "م";
                        }
                        else
                        {
                            pm_am3 = "ص";
                        }
                        asr_hour = (a_hour).ToString();

                    }




                    //Debug.WriteLine(asr_Text);
                    string x4 = maghreb;

                    string maghh1 = x4.ToCharArray()[0].ToString();
                    string maghh2 = x4.ToCharArray()[1].ToString();
                    string maghm1 = x4.ToCharArray()[3].ToString();
                    string maghm2 = x4.ToCharArray()[4].ToString();

                    int m_hour = Convert.ToInt32(maghh1 + "" + maghh2);
                    string m_minute = (maghm1 + "" + maghm2);
                    string pm_am4;
                    string maghreb_hour;
                    if (m_hour > 12)
                    {

                        pm_am4 = "م";
                        maghreb_hour = (m_hour - 12).ToString();

                    }
                    else
                    {
                        if (m_hour == 12)
                        {
                            pm_am4 = "م";
                        }
                        else
                        {
                            pm_am4 = "ص";
                        }
                        maghreb_hour = (m_hour).ToString();

                    }


                    //ebug.WriteLine(maghreb_Text);
                    string x5 = isha;

                    string ishah1 = x5.ToCharArray()[0].ToString();
                    string ishah2 = x5.ToCharArray()[1].ToString();
                    string isham1 = x5.ToCharArray()[3].ToString();
                    string isham2 = x5.ToCharArray()[4].ToString();

                    int I_hour = Convert.ToInt32(ishah1 + "" + ishah2);
                    string I_minute = (isham1 + "" + isham2);

                    string pm_am5;
                    string isha_hour;
                    if (I_hour > 12)
                    {

                        pm_am5 = "م";
                        isha_hour = (I_hour - 12).ToString();

                    }
                    else
                    {

                        if (I_hour == 12)
                        {
                            pm_am5 = "م";
                        }
                        else
                        {
                            pm_am5 = "ص";
                        }
                        isha_hour = (I_hour).ToString();

                    }


                    loc0.Text = fajr_hour_sun + ":" + minute_sun + " " + pm_am1_sun;

                    loc2.Text = fajr_hour + ":" + minute + " " + pm_am1;

                    loc4.Text = duhr_hour + ":" + d_minute + " " + pm_am2;

                    loc5.Text = asr_hour + ":" + a_minute + " " + pm_am3;

                    loc6.Text = maghreb_hour + ":" + m_minute + " " + pm_am4;

                    loc7.Text = isha_hour + ":" + I_minute + " " + pm_am5;

                    if (notificationindex == 0)
                    {
                        switch (Device.RuntimePlatform)
                        {

                            case Device.iOS:
                                Notificatioios();
                                break;
                            case Device.Android:
                                notificatioandorid();
                                break;

                        }
                        notificationindex = 1;
                    }

                    textforpraytimes();
                    timingout();


                }
                else { praytimeonline(); }
            }
            catch { }
        }

        private string text_timeout;
        private int m_f;
        private int m_f1;
        private int m_f2;
        private int m_f3;
        private int m_f4;
        private int realtime;
        private int timeoutfajr;

        private void progresspar()
        {

            // Debug.WriteLine("progresspar()");
            try
            {
                if (m_f > realtime)
                {
                    totalsecond = m_f;
                    timebetween = m_f1 - m_f;
                    totalpastsecond = 0;

                }

                else
                {
                    if (m_f1 > realtime)
                    {
                        //Debug.WriteLine("duhr");
                        timebetween = m_f1 - m_f;
                        totalsecond = m_f1;
                        totalpastsecond = m_f;
                    }
                    else
                    {

                        if (m_f2 > realtime)
                        {

                            //Debug.WriteLine("asr");
                            timebetween = m_f2 - m_f1;
                            totalsecond = m_f2;
                            totalpastsecond = m_f1;
                        }
                        else
                        {

                            if (m_f3 > realtime)
                            {
                                //Debug.WriteLine("maghrib");
                                timebetween = m_f3 - m_f2;
                                totalsecond = m_f3;
                                totalpastsecond = m_f2;
                            }
                            else
                            {
                                if (m_f4 > realtime)
                                {
                                    //Debug.WriteLine("isha");
                                    timebetween = m_f4 - m_f3;
                                    totalsecond = m_f4;
                                    totalpastsecond = m_f3;
                                }
                                else
                                {
                                    if (m_f4 < realtime)
                                    {

                                        totalsecond = timeoutfajr;
                                        totalpastsecond = 0;


                                    }
                                }
                            }
                        }

                    }

                }









                circletime = totalsecond - totalpastsecond;
                timeoutcircle = circletime / 36;
                totalsecondsNOW = (DateTime.Now.Hour * 60 * 60) + (DateTime.Now.Minute * 60) + DateTime.Now.Second;

                int image = 0;
                int plus = 0;
                int nuberre = 0;





                for (; nuberre < 36; nuberre++)
                {
                    plus++;
                    image++;

                    if (totalsecondsNOW > totalpastsecond + (timeoutcircle * plus))
                    {



                    }

                }


            }
            catch { }

        }

        private void textforpraytimes()
        {

            try
            {
                Debug.WriteLine("start");



                //  Debug.WriteLine("void");
                prayjosn = Application.Current.Properties["praytimes"].ToString();
                JObject praytimes = JObject.Parse(prayjosn);

                int day = DateTime.Now.DayOfYear - 1;
                string fajr = praytimes["days"][day]["times"][0].ToString();
                string sunrise = praytimes["days"][day]["times"][1].ToString();
                string duhr = praytimes["days"][day]["times"][2].ToString();
                string asr = praytimes["days"][day]["times"][3].ToString();
                string maghreb = praytimes["days"][day]["times"][4].ToString();
                string isha = praytimes["days"][day]["times"][6].ToString();


                //// TEXT PRAY TIMES

                string x = fajr;

                string h1 = x.ToCharArray()[0].ToString();
                string h2 = x.ToCharArray()[1].ToString();
                string m1 = x.ToCharArray()[3].ToString();
                string m2 = x.ToCharArray()[4].ToString();

                int hour = Convert.ToInt32(h1 + "" + h2);
                string minute = (m1 + "" + m2);
                string pm_am1;
                string fajr_hour;

                if (hour > 12)
                {

                    pm_am1 = "م";

                    fajr_hour = (hour - 12).ToString();


                }
                else
                {

                    if (hour == 12)
                    {
                        pm_am1 = "م";
                    }
                    else
                    {
                        pm_am1 = "ص";
                    }

                    fajr_hour = (hour).ToString();

                }
                string praytime = fajr_hour + ":" + minute + " " + pm_am1;
                //Debug.WriteLine(praytime);

                string x2 = duhr;

                string duhrh1 = x2.ToCharArray()[0].ToString();
                string duhrh2 = x2.ToCharArray()[1].ToString();
                string duhrm1 = x2.ToCharArray()[3].ToString();
                string duhrm2 = x2.ToCharArray()[4].ToString();

                int d_hour = Convert.ToInt32(duhrh1 + "" + duhrh2);
                string d_minute = (duhrm1 + "" + duhrm2);
                string pm_am2;
                string duhr_hour;
                if (d_hour > 12)
                {

                    pm_am2 = "م";
                    duhr_hour = (d_hour - 12).ToString();

                }
                else
                {

                    if (d_hour == 12)
                    {
                        pm_am2 = "م";
                    }
                    else
                    {
                        pm_am2 = "ص";
                    }
                    duhr_hour = (d_hour).ToString();


                }
                string duhr_Text = duhr_hour + ":" + d_minute + " " + pm_am2;

                //Debug.WriteLine(duhr_Text);

                string x3 = asr;

                string asrh1 = x3.ToCharArray()[0].ToString();
                string asrh2 = x3.ToCharArray()[1].ToString();
                string asrm1 = x3.ToCharArray()[3].ToString();
                string asrm2 = x3.ToCharArray()[4].ToString();

                int a_hour = Convert.ToInt32(asrh1 + "" + asrh2);

                string a_minute = (asrm1 + "" + asrm2);
                string pm_am3;
                string asr_hour;
                if (a_hour > 12)
                {

                    pm_am3 = "م";
                    asr_hour = (a_hour - 12).ToString();
                }
                else
                {
                    if (a_hour == 12)
                    {
                        pm_am3 = "م";
                    }
                    else
                    {
                        pm_am3 = "ص";
                    }
                    asr_hour = (a_hour).ToString();

                }
                string asr_Text = asr_hour + ":" + a_minute + " " + pm_am3;



                //Debug.WriteLine(asr_Text);
                string x4 = maghreb;

                string maghh1 = x4.ToCharArray()[0].ToString();
                string maghh2 = x4.ToCharArray()[1].ToString();
                string maghm1 = x4.ToCharArray()[3].ToString();
                string maghm2 = x4.ToCharArray()[4].ToString();

                int m_hour = Convert.ToInt32(maghh1 + "" + maghh2);
                string m_minute = (maghm1 + "" + maghm2);
                string pm_am4;
                string maghreb_hour;
                if (m_hour > 12)
                {

                    pm_am4 = "م";
                    maghreb_hour = (m_hour - 12).ToString();

                }
                else
                {
                    if (m_hour == 12)
                    {
                        pm_am4 = "م";
                    }
                    else
                    {
                        pm_am4 = "ص";
                    }
                    maghreb_hour = (m_hour).ToString();

                }
                string maghreb_Text = maghreb_hour + ":" + m_minute + " " + pm_am4;



                //Debug.WriteLine(maghreb_Text);
                string x5 = isha;

                string ishah1 = x5.ToCharArray()[0].ToString();
                string ishah2 = x5.ToCharArray()[1].ToString();
                string isham1 = x5.ToCharArray()[3].ToString();
                string isham2 = x5.ToCharArray()[4].ToString();

                int I_hour = Convert.ToInt32(ishah1 + "" + ishah2);
                string I_minute = (isham1 + "" + isham2);

                string pm_am5;
                string isha_hour;
                if (I_hour > 12)
                {

                    pm_am5 = "م";
                    isha_hour = (I_hour - 12).ToString();

                }
                else
                {

                    if (I_hour == 12)
                    {
                        pm_am5 = "م";
                    }
                    else
                    {
                        pm_am5 = "ص";
                    }
                    isha_hour = (I_hour).ToString();

                }

                string isha_Text = isha_hour + ":" + I_minute + " " + pm_am5;

                realtime = (DateTime.Now.Hour * 60 * 60) + (DateTime.Now.Minute * 60) + DateTime.Now.Second;
                m_f = (hour * 60 * 60) + Convert.ToInt32(minute) * 60;
                m_f1 = (d_hour * 60 * 60) + Convert.ToInt32(d_minute) * 60;
                m_f2 = (a_hour * 60 * 60) + Convert.ToInt32(a_minute) * 60;
                m_f3 = (m_hour * 60 * 60) + Convert.ToInt32(m_minute) * 60;
                m_f4 = (I_hour * 60 * 60) + Convert.ToInt32(I_minute) * 60;
                if (m_f > realtime)
                {


                    timepray.Text = praytime;
                    text_timeout = fajr;

                    textpray.Text = "الفجر";



                }
                else
                {

                    if (m_f1 > realtime)
                    {
                        int intminte = Convert.ToInt32(d_minute);

                        timepray.Text = duhr_Text;
                        text_timeout = duhr;







                        textpray.Text = "الظهر";

                    }
                    else
                    {
                        if (m_f2 > realtime)
                        {
                            int intminte = Convert.ToInt32(a_minute);

                            timepray.Text = asr_Text;
                            text_timeout = asr;


                            textpray.Text = "العصر";


                        }
                        else
                        {
                            if (m_f3 > realtime)
                            {
                                int intminte = Convert.ToInt32(m_minute);

                                timepray.Text = maghreb_Text;
                                text_timeout = maghreb;


                                textpray.Text = "المغرب";


                            }
                            else
                            {
                                if (m_f4 > realtime)
                                {
                                    timepray.Text = isha_Text;
                                    text_timeout = isha;


                                    textpray.Text = "العشاء";

                                }
                                else
                                {
                                    if (m_f4 < realtime)
                                    {
                                        string fajr2;
                                        if (DateTime.Now.DayOfYear == DateTime.MaxValue.DayOfYear) { fajr2 = praytimes["days"][0]["times"][0].ToString(); }
                                        else { fajr2 = praytimes["days"][DateTime.Now.DayOfYear]["times"][0].ToString(); }

                                        int intminte = Convert.ToInt32(I_minute);
                                        string xt = fajr2;

                                        string h1t = xt.ToCharArray()[0].ToString();
                                        string h2t = xt.ToCharArray()[1].ToString();
                                        string m1t = xt.ToCharArray()[3].ToString();
                                        string m2t = xt.ToCharArray()[4].ToString();

                                        int hourt = Convert.ToInt32(h1t + "" + h2t);
                                        string minutet = (m1t + "" + m2t);
                                        string fajr_hourt;
                                        timeoutfajr = ((hourt * 60 * 60) + (Convert.ToInt32(minutet) * 60)) + (86400 - m_f4);
                                        text_timeout = Convert.ToString(timeoutfajr);

                                        if (hourt > 12)
                                        {
                                            fajr_hourt = (hour - 12).ToString();


                                        }
                                        else
                                        {

                                            if (hour == 12)
                                            {
                                                pm_am1 = "م";
                                            }
                                            else
                                            {
                                                pm_am1 = "ص";
                                            }

                                            fajr_hourt = (hour).ToString();

                                        }
                                        timepray.Text = fajr_hourt + ":" + minutet + " " + pm_am1;


                                        textpray.Text = "الفجر";





                                    }
                                }

                            }
                        }
                    }
                }









            }
            catch { }
        }

        private readonly int alert = 0;
        private int backtimer = 0;
        private int totalsecond;
        private int totalpastsecond;
        private int timebetween;
        private int circletime;
        private int timeoutcircle;
        private int totalsecondsNOW;

        private void timingout()
        {




            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {


                try
                {
                    if (textpray.Text == "الفجر")
                    {


                        AlFajer.BackgroundColor = Color.FromHex("#2EA15A");
                        loc2.TextColor = Color.White;
                        _loc2.TextColor = Color.White;
                        image1.Source = "speakerOn.png";
                    }
                    else
                    {
                        AlFajer.BackgroundColor = Color.Transparent;
                        loc2.TextColor = Color.Black;
                        _loc2.TextColor = Color.Black;
                        image1.Source = "speakerOf.png";

                    }
                    if (textpray.Text == "الظهر")
                    {


                        Al.BackgroundColor = Color.FromHex("#2EA15A");
                        loc4.TextColor = Color.White;
                        _loc4.TextColor = Color.White;
                        image2.Source = "speakerOn.png";
                    }
                    else
                    {
                        Al.BackgroundColor = Color.Transparent;
                        loc4.TextColor = Color.Black;
                        _loc4.TextColor = Color.Black;
                        image2.Source = "speakerOf.png";

                    }
                    if (textpray.Text == "العصر")
                    {


                        Aleasr.BackgroundColor = Color.FromHex("#2EA15A");
                        loc5.TextColor = Color.White;
                        _loc5.TextColor = Color.White;
                        image3.Source = "speakerOn.png";
                    }
                    else
                    {
                        Aleasr.BackgroundColor = Color.Transparent;
                        loc5.TextColor = Color.Black;
                        _loc5.TextColor = Color.Black;
                        image3.Source = "speakerOf.png";

                    }
                    if (textpray.Text == "المغرب")
                    {


                        Almaghrib.BackgroundColor = Color.FromHex("#2EA15A");
                        loc6.TextColor = Color.White;
                        _loc6.TextColor = Color.White;
                        image4.Source = "speakerOn.png";
                    }
                    else
                    {

                        Almaghrib.BackgroundColor = Color.Transparent;
                        loc6.TextColor = Color.Black;
                        _loc6.TextColor = Color.Black;
                        image4.Source = "speakerOf.png";
                    }
                    if (textpray.Text == "العشاء")
                    {


                        Al3saa.BackgroundColor = Color.FromHex("#2EA15A");
                        loc7.TextColor = Color.White;
                        _loc7.TextColor = Color.White;
                        image5.Source = "speakerOn.png";
                    }
                    else
                    {
                        Al3saa.BackgroundColor = Color.Transparent;
                        loc7.TextColor = Color.Black;
                        _loc7.TextColor = Color.Black;
                        image5.Source = "speakerOf.png";

                    }
                    //   azantime = DependencyService.Get<ios>().playorazan();
                    //   TimeSpan t = TimeSpan.FromSeconds(azantime);
                    // azantimeout.Text = string.Format("{0:D2}:{1:D2}",

                    //t.Minutes,
                    //t.Seconds);


                }
                catch { }
                try
                {

                    if (Application.Current.Properties.ContainsKey("playazan"))
                    {
                        if (Application.Current.Properties["playazan"] == "on")
                        {


                            Application.Current.Properties["playazan"] = "off";
                        }

                    }

                    int totalseconds = (DateTime.Now.Hour * 60 * 60) + (DateTime.Now.Minute * 60) + DateTime.Now.Second;


                    backtimer = DateTime.Now.Millisecond;

                    if (m_f4 < totalseconds)
                    {

                        if (textpray.Text == "Fajr" || textpray.Text == "الفجر") { }
                        else
                        {
                            textforpraytimes();
                            Debug.WriteLine("now");

                        }




                        TimeSpan time = TimeSpan.FromSeconds(timeoutfajr - (totalseconds - m_f4 + 60));

                        string str = time.ToString(@"hh\:mm\:ss", CultureInfo.InvariantCulture);
                        timeout.Text = str;


                    }
                    else
                    {
                        string timer = text_timeout;

                        string timer1 = timer.ToCharArray()[0].ToString();
                        string timer2 = timer.ToCharArray()[1].ToString();
                        string timer3 = timer.ToCharArray()[3].ToString();
                        string timer4 = timer.ToCharArray()[4].ToString();

                        int hour = Convert.ToInt32(timer1 + "" + timer2);
                        int minut = Convert.ToInt32(timer3 + "" + timer4);

                        int timeout_h = (DateTime.Now.Hour - hour);
                        int timeout_m = (DateTime.Now.Minute - minut);


                        int seconds = (timeout_h * 60 * 60) + (timeout_m * 60) + DateTime.Now.Second;
                        TimeSpan time = TimeSpan.FromSeconds(seconds);
                        string str = time.ToString(@"hh\:mm\:ss", CultureInfo.InvariantCulture);

                        if ((totalseconds - seconds) > timebetween + totalseconds)
                        {
                            Debug.WriteLine("duhr");
                            textforpraytimes();

                        }

                        int totalprayseconds = (hour * 60 * 60) + (minut * 60);

                        if (totalseconds > totalprayseconds)
                        {
                            textforpraytimes();

                            Debug.WriteLine("duhr2");

                        }
                        else { }



                        timeout.Text = str;




                    }



                }
                catch { }

                return true;



            });
            progresspar();
        }

        private void Notificatioios()
        {

            try
            {
                switch (Device.RuntimePlatform)
                {

                    case Device.iOS:
                        DependencyService.Get<InotiiosAndandorid>().cancellallnotification();

                        break;
                }
                string fajrtest = Application.Current.Properties["fajr_notification"].ToString();
                string duhrtest = Application.Current.Properties["duhr_notification"].ToString();
                string asrtest = Application.Current.Properties["asr_notification"].ToString();
                string maghrebtest = Application.Current.Properties["maghreb_notification"].ToString();
                string ishatest = Application.Current.Properties["isha_notification"].ToString();



                int fakeday = DateTime.Now.DayOfYear - 1;

                int x_id = 0;
                int minimumfakeday = fakeday + 11;





                for (; fakeday < DateTime.MaxValue.DayOfYear; fakeday++)
                {
                    if (fakeday <= minimumfakeday)
                    {





                        prayjosn = Application.Current.Properties["praytimes"].ToString();
                        JObject praytimes = JObject.Parse(prayjosn);
                        //find date

                        string date = praytimes["days"][fakeday]["date"].ToString();
                        string xdate = date;

                        string mounth1 = xdate.ToCharArray()[5].ToString();
                        string mounth2 = xdate.ToCharArray()[6].ToString();
                        string day1 = xdate.ToCharArray()[8].ToString();
                        string day2 = xdate.ToCharArray()[9].ToString();
                        int day = Convert.ToInt32(day1 + "" + day2);
                        int mounth = Convert.ToInt32(mounth1 + "" + mounth2);


                        //find date
                        if (fajrtest == "True")
                        {
                            //fajr time
                            string fajr = praytimes["days"][fakeday]["times"][0].ToString();

                            string x = fajr;

                            string h1 = x.ToCharArray()[0].ToString();
                            string h2 = x.ToCharArray()[1].ToString();
                            string m1 = x.ToCharArray()[3].ToString();
                            string m2 = x.ToCharArray()[4].ToString();

                            int hour = Convert.ToInt32(h1 + "" + h2);
                            int minute = Convert.ToInt32(m1 + "" + m2);

                            x_id++;


                            DependencyService.Get<InotiiosAndandorid>().Remindfajr("" + x_id, "صلاة الفجر", "حان الان موعد صلاة الفجر", hour, minute, day, mounth, DateTime.Now.Year);
                            //fajr time
                            Debug.WriteLine("الفجر يعمل");
                        }
                        if (duhrtest == "True")
                        {
                            //duhr time
                            string duhr = praytimes["days"][fakeday]["times"][2].ToString();


                            string x2 = duhr;

                            string duhrh1 = x2.ToCharArray()[0].ToString();
                            string duhrh2 = x2.ToCharArray()[1].ToString();
                            string duhrm1 = x2.ToCharArray()[3].ToString();
                            string duhrm2 = x2.ToCharArray()[4].ToString();

                            int d_hour = Convert.ToInt32(duhrh1 + "" + duhrh2);
                            int d_minute = Convert.ToInt32(duhrm1 + "" + duhrm2);

                            x_id++;

                            DependencyService.Get<InotiiosAndandorid>().Remindduhr("" + x_id, "صلاة الظهر", "حان الان موعد صلاة الظهر", d_hour, d_minute, day, mounth, DateTime.Now.Year);
                            //duhr time
                            Debug.WriteLine("الضهر يعمل");
                        }
                        if (asrtest == "True")
                        {
                            //asr time
                            string asr = praytimes["days"][fakeday]["times"][3].ToString();


                            string x3 = asr;

                            string asrh1 = x3.ToCharArray()[0].ToString();
                            string asrh2 = x3.ToCharArray()[1].ToString();
                            string asrm1 = x3.ToCharArray()[3].ToString();
                            string asrm2 = x3.ToCharArray()[4].ToString();

                            int a_hour = Convert.ToInt32(asrh1 + "" + asrh2);
                            int a_minute = Convert.ToInt32(asrm1 + "" + asrm2);

                            x_id++;

                            DependencyService.Get<InotiiosAndandorid>().Remindasr("" + x_id, "صلاة العصر", "حان الان موعد صلاة العصر", a_hour, a_minute, day, mounth, DateTime.Now.Year);
                            //asr time
                            Debug.WriteLine("العصر يعمل");
                        }
                        if (maghrebtest == "True")
                        {
                            //maghreb time
                            string maghreb = praytimes["days"][fakeday]["times"][4].ToString();


                            string x4 = maghreb;

                            string maghh1 = x4.ToCharArray()[0].ToString();
                            string maghh2 = x4.ToCharArray()[1].ToString();
                            string maghm1 = x4.ToCharArray()[3].ToString();
                            string maghm2 = x4.ToCharArray()[4].ToString();

                            int m_hour = Convert.ToInt32(maghh1 + "" + maghh2);
                            int m_minute = Convert.ToInt32(maghm1 + "" + maghm2);
                            x_id++;

                            DependencyService.Get<InotiiosAndandorid>().Remindmaghreb("" + x_id, "صلاة المغرب", "حان الان موعد صلاة المغرب", m_hour, m_minute, day, mounth, DateTime.Now.Year);
                            //maghreb time
                            Debug.WriteLine("المغرب يعمل");
                        }
                        if (ishatest == "True")
                        {
                            //isha time
                            string isha = praytimes["days"][fakeday]["times"][6].ToString();



                            string x5 = isha;

                            string ishah1 = x5.ToCharArray()[0].ToString();
                            string ishah2 = x5.ToCharArray()[1].ToString();
                            string isham1 = x5.ToCharArray()[3].ToString();
                            string isham2 = x5.ToCharArray()[4].ToString();

                            int I_hour = Convert.ToInt32(ishah1 + "" + ishah2);
                            int I_minute = Convert.ToInt32(isham1 + "" + isham2);
                            x_id++;

                            DependencyService.Get<InotiiosAndandorid>().Remindisha("" + x_id, "صلاة العشاء", "حان الان موعد صلاة العشاء", I_hour, I_minute, day, mounth, DateTime.Now.Year);
                            //isaha time
                            Debug.WriteLine("العشاء يعمل");
                        }













                    }
                    else { }





                }
            }
            catch { }

        }

        private async void notificatioandorid()
        {





            try
            {

                string fajrtest = Application.Current.Properties["fajr_notification"].ToString();
                string duhrtest = Application.Current.Properties["duhr_notification"].ToString();
                string asrtest = Application.Current.Properties["asr_notification"].ToString();
                string maghrebtest = Application.Current.Properties["maghreb_notification"].ToString();
                string ishatest = Application.Current.Properties["isha_notification"].ToString();

                string title_fajr = "صلاة الفجر";
                string body_fajr = "حان الان موعد صلاة الفجر";
                string title_duhr = "صلاة الظهر";
                string body_duhr = "حان الان موعد صلاة الظهر";
                string title_asr = "صلاة العصر";
                string body_asr = "حان الان موعد صلاة العصر";
                string title_maghreb = "صلاة المغرب";
                string body_maghreb = "حان الان موعد صلاة المغرب";
                string title_isha = "صلاة العشاء";
                string body_isha = "حان الان موعد صلاة العشاء";

                int fakeday = DateTime.Now.DayOfYear - 1;
                int realday = DateTime.Now.DayOfYear - 1;
                int x_id = 0;

                for (int daynotify = 0; daynotify < 30; fakeday++)
                {

                    daynotify++;
                    realday++;

                    prayjosn = Application.Current.Properties["praytimes"].ToString();
                    JObject praytimes = JObject.Parse(prayjosn);
                    if (fajrtest == "True")
                    {
                        string fajr = praytimes["days"][fakeday]["times"][0].ToString();

                        string x = fajr;

                        string h1 = x.ToCharArray()[0].ToString();
                        string h2 = x.ToCharArray()[1].ToString();
                        string m1 = x.ToCharArray()[3].ToString();
                        string m2 = x.ToCharArray()[4].ToString();

                        int hour = Convert.ToInt32(h1 + "" + h2);
                        int minute = Convert.ToInt32(m1 + "" + m2);
                        x_id++;

                        DependencyService.Get<Inoti>().Remindfajr(x_id, hour, minute, title_fajr, body_fajr, realday, DateTime.Now.Year);
                        Debug.WriteLine("الفجر يعمل");
                        //fajr time
                    }
                    if (duhrtest == "True")
                    {
                        //duhr time
                        string duhr = praytimes["days"][fakeday]["times"][2].ToString();

                        string x2 = duhr;

                        string duhrh1 = x2.ToCharArray()[0].ToString();
                        string duhrh2 = x2.ToCharArray()[1].ToString();
                        string duhrm1 = x2.ToCharArray()[3].ToString();
                        string duhrm2 = x2.ToCharArray()[4].ToString();

                        int d_hour = Convert.ToInt32(duhrh1 + "" + duhrh2);
                        int d_minute = Convert.ToInt32(duhrm1 + "" + duhrm2);
                        x_id++;
                        DependencyService.Get<Inoti>().Remindduhr(x_id, d_hour, d_minute, title_duhr, body_duhr, realday, DateTime.Now.Year);
                        //duhr time

                        Debug.WriteLine("الضهر يعمل");
                    }
                    if (asrtest == "True")
                    {
                        //asr time
                        string asr = praytimes["days"][fakeday]["times"][3].ToString();

                        string x3 = asr;

                        string asrh1 = x3.ToCharArray()[0].ToString();
                        string asrh2 = x3.ToCharArray()[1].ToString();
                        string asrm1 = x3.ToCharArray()[3].ToString();
                        string asrm2 = x3.ToCharArray()[4].ToString();

                        int a_hour = Convert.ToInt32(asrh1 + "" + asrh2);
                        int a_minute = Convert.ToInt32(asrm1 + "" + asrm2);
                        x_id++;
                        DependencyService.Get<Inoti>().Remindasr(x_id, a_hour, a_minute, title_asr, body_asr, realday, DateTime.Now.Year);
                        //asr time
                        Debug.WriteLine("العصر يعمل");
                    }
                    if (maghrebtest == "True")
                    {
                        //maghreb time
                        string maghreb = praytimes["days"][fakeday]["times"][4].ToString();

                        string x4 = maghreb;

                        string maghh1 = x4.ToCharArray()[0].ToString();
                        string maghh2 = x4.ToCharArray()[1].ToString();
                        string maghm1 = x4.ToCharArray()[3].ToString();
                        string maghm2 = x4.ToCharArray()[4].ToString();

                        int m_hour = Convert.ToInt32(maghh1 + "" + maghh2);
                        int m_minute = Convert.ToInt32(maghm1 + "" + maghm2);
                        x_id++;
                        DependencyService.Get<Inoti>().Remindmaghrb(x_id, m_hour, m_minute, title_maghreb, body_maghreb, realday, DateTime.Now.Year);
                        //maghreb time
                        Debug.WriteLine("المغرب يعمل");
                    }
                    if (ishatest == "True")
                    {
                        //isha time
                        string isha = praytimes["days"][fakeday]["times"][6].ToString();

                        string x5 = isha;

                        string ishah1 = x5.ToCharArray()[0].ToString();
                        string ishah2 = x5.ToCharArray()[1].ToString();
                        string isham1 = x5.ToCharArray()[3].ToString();
                        string isham2 = x5.ToCharArray()[4].ToString();

                        int I_hour = Convert.ToInt32(ishah1 + "" + ishah2);
                        int I_minute = Convert.ToInt32(isham1 + "" + isham2);
                        x_id++;
                        DependencyService.Get<Inoti>().Remindisha(x_id, I_hour, I_minute, title_isha, body_isha, realday, DateTime.Now.Year);
                        //isaha time

                        Debug.WriteLine("العشاء يعمل");
                    }





                    await Task.Delay(2000);





                }



            }
            catch { }
        }

        private int notificationindex = 0;
        public string GetMonthHigri(int month, string language)
        {
            switch (language)
            {
                case "ar":
                    {
                        switch (month)
                        {
                            case 1:
                                return "محرَّم";
                            case 2:
                                return "صفر";
                            case 3:
                                return "ربيع الأول";
                            case 4:
                                return "ربيع الآخر";
                            case 5:
                                return "جمادى الأولى";
                            case 6:
                                return "جمادى الآخرة";
                            case 7:
                                return "رجب";
                            case 8:
                                return "شعبان";
                            case 9:
                                return "رمضان";
                            case 10:
                                return "شوَّال";
                            case 11:
                                return "ذو القعدة";
                            case 12:
                                return "ذو الحجة";
                            default:
                                return "";
                        }
                    }
                case "en":
                    {
                        switch (month)
                        {
                            case 1:
                                return "Muharram ";
                            case 2:
                                return "Safar";
                            case 3:
                                return "Rabi Al Awwal";
                            case 4:
                                return "Rabi Al Akhar";
                            case 5:
                                return "Jamadil Awwal ";
                            case 6:
                                return "Jamadil Akhir";
                            case 7:
                                return "Rajab";
                            case 8:
                                return "Shaban";
                            case 9:
                                return "Ramadan ";
                            case 10:
                                return "Shawwal";
                            case 11:
                                return "Dhul al- Qa’dah";

                            case 12:
                                return "Dhul al -Hijjah";
                            default:
                                return "";
                        }
                    }
                default:
                    return "";

            }

        }
        public string GetMonthGregorian(int month, string language)
        {
            switch (language)
            {
                case "ar":
                    {
                        switch (month)
                        {
                            case 1:
                                return "يناير";
                            case 2:
                                return "فبراير";
                            case 3:
                                return "مارس";
                            case 4:
                                return "إبريل";
                            case 5:
                                return "مايو";
                            case 6:
                                return "يونيو";
                            case 7:
                                return "يوليو";
                            case 8:
                                return "أغسطس";
                            case 9:
                                return "سبتمبر";
                            case 10:
                                return "أكتوبر";
                            case 11:
                                return "نوفمبر";
                            case 12:
                                return "ديسمير";
                            default:
                                return "";
                        }
                    }
                case "en":
                    {
                        switch (month)
                        {
                            case 1:
                                return "January";
                            case 2:
                                return "February";
                            case 3:
                                return "March";
                            case 4:
                                return "April";
                            case 5:
                                return "May";
                            case 6:
                                return "June";
                            case 7:
                                return "July";
                            case 8:
                                return "August";
                            case 10:
                                return "September ";
                            case 11:
                                return "October";
                            case 12:
                                return "November";

                            default:
                                return "December";
                        }
                    }
                default:
                    return "";

            }

        }

        private readonly string realhijrijson;
        private string hijrijson1;
        private string hijrijson2;
        private string hijrijson3;
        private string hijrijson4;
        private string hijrijson5;
        private string hijrijson6;
        private string hijrijson7;
        private string hijrijson8;
        private string hijrijson9;
        private string hijrijson10;
        private string hijrijson11;
        private string hijrijson12;
        private readonly string hijrimounth;
        private readonly int x = 0;

        private async void hijri()
        {

            try
            { ///// Find hijiry ///

                int mounth = DateTime.Now.Month;
                for (; mounth <= 12; mounth++)
                {
                    string hijrylink = "http://api.aladhan.com/v1/calendar?latitude=51.508515&longitude=-0.1254872&method=2&month=" + mounth + "&year=" + DateTime.Now.Year;
                    HttpClient _clinthijri = new HttpClient();
                    Task<string> contacthijri = _clinthijri.GetStringAsync(hijrylink);

                    switch (mounth)
                    {

                        case 1:
                            hijrijson1 = await contacthijri;
                            Application.Current.Properties["hijrijson1"] = hijrijson1;
                            await Application.Current.SavePropertiesAsync();
                            if (mounth == DateTime.Now.Month)
                            {

                                string hijrimounth;
                                JObject jsonhijri = JObject.Parse(hijrijson1);
                                string hijridayx = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["day"].ToString();
                                hijrimounth = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["month"]["ar"].ToString();
                                string _hijrimounth = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["month"]["en"].ToString();
                                string hijriyear = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["year"].ToString();
                                hijriday.Text = hijridayx + " " + hijrimounth + " " + hijriyear + " ";
                                enghijriday.Text = hijridayx + " " + _hijrimounth + " " + hijriyear + " ";
                            }

                            break;
                        case 2:

                            hijrijson2 = await contacthijri;
                            Application.Current.Properties["hijrijson2"] = hijrijson2;
                            await Application.Current.SavePropertiesAsync();
                            if (mounth == DateTime.Now.Month)
                            {
                                JObject jsonhijri = JObject.Parse(hijrijson2);
                                string hijridayx = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["day"].ToString();
                                string _hijrimounth = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["month"]["en"].ToString();
                                string hijrimounth = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["month"]["ar"].ToString();

                                string hijriyear = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["year"].ToString();
                                hijriday.Text = hijridayx + " " + hijrimounth + " " + hijriyear + " ";
                                enghijriday.Text = hijridayx + " " + _hijrimounth + " " + hijriyear + " ";

                            }
                            break;
                        case 3:

                            hijrijson3 = await contacthijri;
                            Application.Current.Properties["hijrijson3"] = hijrijson3;
                            await Application.Current.SavePropertiesAsync();
                            if (mounth == DateTime.Now.Month)
                            {
                                JObject jsonhijri = JObject.Parse(hijrijson3);
                                string hijridayx = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["day"].ToString();
                                string _hijrimounth = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["month"]["en"].ToString();
                                string hijrimounth = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["month"]["ar"].ToString();

                                string hijriyear = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["year"].ToString();
                                hijriday.Text = hijridayx + " " + hijrimounth + " " + hijriyear + " ";
                                enghijriday.Text = hijridayx + " " + _hijrimounth + " " + hijriyear + " ";
                            }
                            break;
                        case 4:

                            hijrijson4 = await contacthijri;
                            Application.Current.Properties["hijrijson4"] = hijrijson4;

                            await Application.Current.SavePropertiesAsync();

                            if (mounth == DateTime.Now.Month)
                            {

                                JObject jsonhijri = JObject.Parse(hijrijson4);
                                string hijridayx = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["day"].ToString();
                                string _hijrimounth = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["month"]["en"].ToString();
                                string hijrimounth = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["month"]["ar"].ToString();

                                string hijriyear = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["year"].ToString();
                                hijriday.Text = hijridayx + " " + hijrimounth + " " + hijriyear + " ";
                                enghijriday.Text = hijridayx + " " + _hijrimounth + " " + hijriyear + " ";
                                //Debug.WriteLine(hijriday);
                            }
                            break;
                        case 5:

                            hijrijson5 = await contacthijri;
                            Application.Current.Properties["hijrijson5"] = hijrijson5;
                            await Application.Current.SavePropertiesAsync();
                            if (mounth == DateTime.Now.Month)
                            {
                                JObject jsonhijri = JObject.Parse(hijrijson5);
                                string hijridayx = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["day"].ToString();
                                string _hijrimounth = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["month"]["en"].ToString();
                                string hijrimounth = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["month"]["ar"].ToString();

                                string hijriyear = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["year"].ToString();
                                hijriday.Text = hijridayx + " " + hijrimounth + " " + hijriyear + " ";
                                enghijriday.Text = hijridayx + " " + _hijrimounth + " " + hijriyear + " ";
                            }
                            break;
                        case 6:

                            hijrijson6 = await contacthijri;
                            Application.Current.Properties["hijrijson6"] = hijrijson6;
                            await Application.Current.SavePropertiesAsync();
                            if (mounth == DateTime.Now.Month)
                            {
                                JObject jsonhijri = JObject.Parse(hijrijson6);
                                string hijridayx = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["day"].ToString();
                                string _hijrimounth = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["month"]["en"].ToString();
                                string hijrimounth = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["month"]["ar"].ToString();

                                string hijriyear = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["year"].ToString();
                                hijriday.Text = hijridayx + " " + hijrimounth + " " + hijriyear + " ";
                                enghijriday.Text = hijridayx + " " + _hijrimounth + " " + hijriyear + " ";

                            }
                            break;
                        case 7:

                            hijrijson7 = await contacthijri;
                            Application.Current.Properties["hijrijson7"] = hijrijson7;
                            await Application.Current.SavePropertiesAsync();
                            if (mounth == DateTime.Now.Month)
                            {
                                JObject jsonhijri = JObject.Parse(hijrijson7);
                                string hijridayx = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["day"].ToString();
                                string _hijrimounth = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["month"]["en"].ToString();
                                string hijrimounth = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["month"]["ar"].ToString();

                                string hijriyear = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["year"].ToString();
                                hijriday.Text = hijridayx + " " + hijrimounth + " " + hijriyear + " ";
                                enghijriday.Text = hijridayx + " " + _hijrimounth + " " + hijriyear + " ";
                            }
                            break;
                        case 8:

                            hijrijson8 = await contacthijri;
                            Application.Current.Properties["hijrijson8"] = hijrijson8;
                            await Application.Current.SavePropertiesAsync();
                            if (mounth == DateTime.Now.Month)
                            {
                                JObject jsonhijri = JObject.Parse(hijrijson8);
                                string hijridayx = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["day"].ToString();
                                string _hijrimounth = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["month"]["en"].ToString();
                                string hijrimounth = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["month"]["ar"].ToString();

                                string hijriyear = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["year"].ToString();
                                hijriday.Text = hijridayx + " " + hijrimounth + " " + hijriyear + " ";
                                enghijriday.Text = hijridayx + " " + _hijrimounth + " " + hijriyear + " ";
                            }
                            break;
                        case 9:

                            hijrijson9 = await contacthijri;
                            Application.Current.Properties["hijrijson9"] = hijrijson9;
                            await Application.Current.SavePropertiesAsync();
                            if (mounth == DateTime.Now.Month)
                            {
                                JObject jsonhijri = JObject.Parse(hijrijson9);
                                string hijridayx = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["day"].ToString();
                                string _hijrimounth = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["month"]["en"].ToString();
                                string hijrimounth = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["month"]["ar"].ToString();

                                string hijriyear = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["year"].ToString();
                                hijriday.Text = hijridayx + " " + hijrimounth + " " + hijriyear + " ";
                                enghijriday.Text = hijridayx + " " + _hijrimounth + " " + hijriyear + " ";
                            }
                            break;
                        case 10:

                            hijrijson10 = await contacthijri;
                            Application.Current.Properties["hijrijson10"] = hijrijson10;
                            await Application.Current.SavePropertiesAsync();
                            if (mounth == DateTime.Now.Month)
                            {
                                JObject jsonhijri = JObject.Parse(hijrijson10);
                                string hijridayx = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["day"].ToString();
                                string _hijrimounth = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["month"]["en"].ToString();
                                string hijrimounth = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["month"]["ar"].ToString();

                                string hijriyear = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["year"].ToString();
                                hijriday.Text = hijridayx + " " + hijrimounth + " " + hijriyear + " ";
                                enghijriday.Text = hijridayx + " " + _hijrimounth + " " + hijriyear + " ";
                            }
                            break;
                        case 11:

                            hijrijson11 = await contacthijri;
                            Application.Current.Properties["hijrijson11"] = hijrijson11;
                            await Application.Current.SavePropertiesAsync();
                            if (mounth == DateTime.Now.Month)
                            {
                                JObject jsonhijri = JObject.Parse(hijrijson11);
                                string hijridayx = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["day"].ToString();
                                string _hijrimounth = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["month"]["en"].ToString();
                                string hijrimounth = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["month"]["ar"].ToString();

                                string hijriyear = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["year"].ToString();
                                hijriday.Text = hijridayx + " " + hijrimounth + " " + hijriyear + " ";
                                enghijriday.Text = hijridayx + " " + _hijrimounth + " " + hijriyear + " ";
                            }
                            break;
                        case 12:

                            hijrijson12 = await contacthijri;
                            Application.Current.Properties["hijrijson12"] = hijrijson12;
                            await Application.Current.SavePropertiesAsync();
                            if (mounth == DateTime.Now.Month)
                            {
                                JObject jsonhijri = JObject.Parse(hijrijson12);
                                string hijridayx = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["day"].ToString();
                                string _hijrimounth = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["month"]["en"].ToString();
                                string hijrimounth = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["month"]["ar"].ToString();

                                string hijriyear = jsonhijri["data"][DateTime.Now.Day]["date"]["hijri"]["year"].ToString();
                                hijriday.Text = hijridayx + " " + hijrimounth + " " + hijriyear + " ";
                                enghijriday.Text = hijridayx + " " + _hijrimounth + " " + hijriyear + " ";
                            }
                            break;


                    }

                }
            }
            catch
            { // Debug.WriteLine("no internet");
            }

        }

    }
}
