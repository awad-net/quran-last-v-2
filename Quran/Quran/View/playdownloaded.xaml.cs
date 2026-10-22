
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
    public partial class playdownloaded : ContentPage, IMenuContainerPage
    {
        private readonly XmlDocument doc;
        public SlideMenuView SlideMenu { get; set; }
        public Action ShowMenuAction { get; set; }
        public Action HideMenuAction { get; set; }
        public int shiekhid;
        public int current;
        public string url;
        public playdownloaded(string _url, string name, int id)
        {
            InitializeComponent();


            shiekhid = id;
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
                                           where su.Element("_index").Value == id.ToString()
                                           select su;
            IEnumerable<XElement> ayat = result.Elements("aya");

            foreach (XElement aya in ayat)
            {
                Label x = new Label
                {
                    Text = "﴿" + aya.Element("_index").Value + "﴾" + aya.Element("_text").Value,
                    FontSize = 18,
                    HorizontalTextAlignment = TextAlignment.End,
                    Margin = 10,

                };


                StackLayoutMap.Children.Add(x);
            }
            Play();
        }

        private void playsura(object s, EventArgs ee)
        {
            //  await CrossMediaManager.Current.Play(new MediaFile(url, MediaFileType.Audio, ResourceAvailability.Local));

        }

        private void Play()
        {
            //  await CrossMediaManager.Current.Play(new MediaFile(url, MediaFileType.Audio, ResourceAvailability.Local));

        }

        private async void stopsura(object s, EventArgs ee)
        {
            //if (CrossMediaManager.Current.IsPlaying() == true)
            await CrossMediaManager.Current.Stop();
        }

    }
}


