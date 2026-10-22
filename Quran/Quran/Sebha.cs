using System;

using Xamarin.Forms;

namespace Quran
{
    public class Sebha : ContentPage
    {
        public Sebha()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

