using System;
using uber_uni.views;
using Xamarin.Forms;

namespace uber_uni.view_assets
{
    public class MainChatView : ContentPage
    {
        Button question = new Button { BackgroundColor = Color.Gray, Text = "What are some good local restaurants?", Padding = 15, TextColor = Color.White };
        Button question1 = new Button { BackgroundColor = Color.Gray, Text = "What are some good places to see?", Padding = 15, TextColor = Color.White };
        Button question2 = new Button { BackgroundColor = Color.Gray, Text = "Whats around me?", Padding = 15, TextColor = Color.White };

        Button localBtn = new Button { BackgroundColor = Color.Black, Text = "Connect", Padding = 15, TextColor= Color.White };
        Frame chatFrame = new Frame { HeightRequest = 200, HasShadow = false, BackgroundColor = Color.LightCyan, WidthRequest = App.ScreenWidth };

        public MainChatView()
        {
            Content = setUpView();

        }

        public View setUpView()
        {
            var stack = new StackLayout
            {
                Children = {
                    new Label { Text = "Ask one of our preset questions or talk to an agent yourself", FontSize = 20 },
                    question,
                    question1,
                    question2,
                    new Label { Text = "Live chat powered by Genesys", FontSize = 18 },
                    chatFrame,
                    localBtn
                },
                Spacing = 10,
                Padding = 10
            };

            return new StackLayout
            { Children = { new uberHeaderView("Connect with a local", this), stack }
            };
        }
    }
}

