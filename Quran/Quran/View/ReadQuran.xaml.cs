using Newtonsoft.Json;
using Plugin.MediaManager;
using SlideOverKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Quran
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReadQuran : ContentPage, IMenuContainerPage
    {
        private readonly XmlDocument doc;
        private TimeSpan x;
        public string url;

        public SlideMenuView SlideMenu { get; set; }
        public Action ShowMenuAction { get; set; }
        public Action HideMenuAction { get; set; }
        public ReadQuran(string _url, string name, string sura, int suid)
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
            url = _url;
            suraname.Text = name;
            surat.Text = "بسم الله الرحمن الرحيم";
            //playsura();
            string jsonFileName = "quran.json";
            Quran q = new Quran();
            Assembly assembly = typeof(MainPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{jsonFileName}");
            using (StreamReader reader = new System.IO.StreamReader(stream))
            {
                string jsonString = reader.ReadToEnd();
                doc = JsonConvert.DeserializeXmlNode(jsonString);
            }
            XElement xe = XElement.Load(doc.CreateNavigator().ReadSubtree());
            IEnumerable<XElement> swar = xe.Elements();
            IEnumerable<XElement> result = from su in swar
                                           where su.Element("_index").Value == suid.ToString()
                                           select su;
            IEnumerable<XElement> ayat = result.Elements("aya");

            foreach (XElement aya in ayat)
            {
                Label x = new Label
                {
                    Text = "﴿" + aya.Element("_index").Value + "﴾" + aya.Element("_text").Value,
                    FontSize = 20,
                    HorizontalTextAlignment = TextAlignment.End,
                    Margin = 10,

                };


                StackLayoutMap.Children.Add(x);
            }

        }

        private async void playsura()
        {
            await CrossMediaManager.Current.Play(url);

        }

        private async void resumesura(object s, EventArgs e)
        {
            if (x != TimeSpan.Zero)
            {
                await CrossMediaManager.Current.Seek(x);

            }
            else
            {
                await CrossMediaManager.Current.Play(url);
            }
        }

        private async void Pause(object s, EventArgs ee)
        {
            x = CrossMediaManager.Current.Position;
            await CrossMediaManager.Current.Pause();


        }
    }
}