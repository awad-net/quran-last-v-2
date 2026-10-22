using Quran.Model;
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
    public partial class downloadedshioukh : ContentPage, IMenuContainerPage
    {
        private readonly List<Mshaiekh> msh = new List<Mshaiekh>();
        private readonly List<int> id = new List<int>();


        public SlideMenuView SlideMenu { get; set; }
        public Action ShowMenuAction { get; set; }
        public Action HideMenuAction { get; set; }
        public downloadedshioukh()
        {
            InitializeComponent();
            getshioukh();


            SlideMenu = new right();

        }

        private async void getshioukh()
        {
            downloadDB qurandb = new downloadDB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "download2.db3"));
            shak shak = new shak();

            List<Download> download = await qurandb.GetItemsAsync();
            foreach (Download d in download)
            {
                if (id.Contains(d.sid))
                {
                    continue;
                }
                else
                {
                    id.Add(d.sid);
                }
                Mshaiekh mshaiekh = shak.getshak(d.sid);
                msh.Add(mshaiekh);
            }
            CshaiekhDownlst.ItemsSource = msh;
        }


        private async void CshaiekhDownlst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                Mshaiekh shie = e.CurrentSelection.FirstOrDefault() as Mshaiekh;
                if (shie != null)
                {
                    if (App.ShaiekNameLocal != shie.name)
                    {
                        App.ShaiekNameLocal = shie.name;
                        App.player.Stop();

                    }

                    await Navigation.PushAsync(new downloaded(shie.id, shie.img, shie.name));
                    CshaiekhDownlst.SelectedItem = null;
                }
            }
        }
    }
}