using System;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

using Xamarin.Forms;

namespace uber_uni.views
{
    public class uberHeaderView : ContentView
    {
        public uberHeaderView(string title, ContentPage page)
        {
            var text = new Label { TextColor = Color.White, Padding = new Thickness( 20,0,0,0), MaxLines = 3,  FontSize = 30, FontAttributes = FontAttributes.Bold, Text = title, VerticalOptions = LayoutOptions.Center};
            var frame = new Frame {Content = text, Margin = 0, Padding = new Thickness(0, 20, 0, 20) , HasShadow = false, BackgroundColor = Color.Black, CornerRadius = 0 };
            Content = frame;
        }
    }
}

