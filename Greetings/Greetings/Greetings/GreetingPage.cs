using System;
using Xamarin.Forms;

namespace Greetings
{
    public class GreetingPage : ContentPage
    {
        public GreetingPage()
        {
            this.Content = new Label
            {
                Text = "Greetings, Xamarin.Forms!",
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                //HorizontalOptions = LayoutOptions.Center,
                //VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.Yellow,
                TextColor = Color.Blue
            };

            //Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0);
            Device.OnPlatform(iOS: () =>
            {
                Padding = new Thickness(0, 20, 0, 0);
            });
        }
    }
}
