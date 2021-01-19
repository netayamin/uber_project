using System;
using System.Windows.Input;
using uber_uni.views;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace uber_uni.views_bottomNav.uberGuardViews
{
    public class trackCompnaionView : ContentPage
    {

        Map mapView = new Map { VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand } ;
        Button chatBtn = new Button { BackgroundColor = Color.Black, HeightRequest = 50, TextColor = Color.White, };
        Button emgBtn = new Button { Text = "Emergency alert", BackgroundColor = Color.Red, HeightRequest = 50, TextColor = Color.White, };
        Compnaion comp = new Compnaion();
        TapGestureRecognizer gest = new TapGestureRecognizer { NumberOfTapsRequired = 1 };

    public trackCompnaionView(Compnaion compnaion, Position pickupPoint)
        {
            comp = compnaion;
            mapView.MoveToRegion(MapSpan.FromCenterAndRadius(pickupPoint , new Distance(5000)));
            mapView.Pins.Add(new Pin { Position = pickupPoint, Label = $"{comp.first_name}'s pick up point" } );
            Content = new StackLayout { Children = { new uberHeaderView("Uber Guard", this), mainStack() } };
        }

        private StackLayout mainStack()
        {
            gest.Tapped += Gest_Tapped;
            chatBtn.GestureRecognizers.Add(gest);
            chatBtn.Text = $"Chat to {comp.first_name}";

            return new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = {
                    new Label { Text =  $"Track {comp.first_name} ", FontSize = 20, FontAttributes = FontAttributes.Bold  },
                    mapView,
                    chatBtn,
                    emgBtn
                },
                    Padding = 15,
                    Spacing = 10
                };
        }

        private void Gest_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new chatView(comp));
        }
    }
}

