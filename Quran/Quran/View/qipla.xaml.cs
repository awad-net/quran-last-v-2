
using SlideOverKit;
using System;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Quran
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class qipla : ContentPage, IMenuContainerPage
    {
        private SensorSpeed speed;

        public SlideMenuView SlideMenu { get; set; }
        public Action ShowMenuAction { get; set; }
        public Action HideMenuAction { get; set; }
        public qipla()
        {
            try
            {
              

                InitializeComponent();
                if (Application.Current.Properties.ContainsKey("imagesource"))
                {
                    Compass.ReadingChanged += Compass_ReadingChanged;
                    Compass.Start(Xamarin.Essentials.SensorSpeed.Fastest);
                    Compass.Start(SensorSpeed.UI, applyLowPassFilter: true);

                }

            }
            catch { return; }

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
        }

        public void ToggleCompass(SensorSpeed speed)
        {
            try
            {

                if (Compass.IsMonitoring)
                    Compass.Stop();
                else
                    Compass.Start(speed);
            }
            catch (FeatureNotSupportedException)
            {
                // Feature not supported on device
            }
            catch (Exception)
            {
                // Some other exception has occurred
            }
        }
        void Compass_ReadingChanged(object sender, CompassChangedEventArgs e)
        {
            var data = e.Reading;
            Console.WriteLine($"Reading: {data.HeadingMagneticNorth} degrees");


            switch (Device.RuntimePlatform)
            {

                case Device.iOS:
                    qiblaxx.Rotation = -e.Reading.HeadingMagneticNorth - 9;
                    break;
                case Device.Android:
                    qiblaxx.Rotation = -e.Reading.HeadingMagneticNorth - 9;
                    break;
            }
            switch (Device.RuntimePlatform)
            {

                case Device.iOS:
                    speed = SensorSpeed.Fastest;
                    break;
                case Device.Android:
                    speed = SensorSpeed.Fastest;
                    break;
            }
        }

    }
}















