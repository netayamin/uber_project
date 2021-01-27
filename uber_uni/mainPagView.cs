using System;
using uber_uni.views;
using Xamarin.Forms;

namespace uber_uni
{
    public class mainPagView : ContentPage
    {
        orderRideView myview = new orderRideView(true, false, false);
        ActivityIndicator loader = new ActivityIndicator { IsRunning = true, IsVisible = false, Color = Color.Black, HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center };
                      
        public mainPagView()
        {
            myview.submitBtn.Clicked += SubmitBtn_Clicked;
           

            var main = new StackLayout
            {
                Spacing = 0,
                Margin =0, 
                Children = {
                    new uberHeaderView("Uber, your ride your way", this),
                    loader,
                    myview,
                }
            };

            Content = new StackLayout
            {
                Spacing = 0,
                Margin = 0,
                Padding = 0,
                Children = {new BoxView { Margin = 0 ,HeightRequest = 45, BackgroundColor = Color.Black }, main
            }
            };
        }

        private void SubmitBtn_Clicked(object sender, System.EventArgs e)
        {
            App.Current.MainPage = new RootViewController(myview.DropOffPoint);
            myview.createTripsAndSaveInDB();
        }
    }
}