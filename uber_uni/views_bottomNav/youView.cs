using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using GoogleApi;
using GoogleApi.Entities.Common;
using GoogleApi.Entities.Maps.Directions.Request;
using uber_uni.helpers;
using uber_uni.view_assets;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace uber_uni.views_bottomNav
{

    public class youView : ContentPage
    {
        public static Map mapView = new Map { WidthRequest = App.ScreenWidth, VerticalOptions = LayoutOptions.FillAndExpand, IsVisible = true };
        Frame frame = new Frame { WidthRequest = 50, HeightRequest = 50, CornerRadius = 25, HasShadow = false, BackgroundColor = Color.Black, Margin =0 ,Padding = 0 };
        Image chatImg = new Image { VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
        TapGestureRecognizer tapGest = new TapGestureRecognizer { NumberOfTapsRequired = 1 };

        public youView()
        {
            Content = setUpView();
            frame.GestureRecognizers.Add(tapGest);
            tapGest.Tapped += TapGest_Tapped;
        }

        private void TapGest_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainChatView());
        }

        public static async Task drawRouteOnMap(Position pickup, Position dropOff)
        {
            var origin = new Location { Latitude = pickup.Latitude, Longitude = pickup.Longitude };
            var Destination = new Location { Latitude = dropOff.Latitude, Longitude = dropOff.Longitude };
            var request = new DirectionsRequest
            {
                Key = "AIzaSyD0WqCwh8vVTlBGNXz3YC55-fEjewHu3ws",
                Origin = origin,
                Destination = Destination,
            };

            var result = await GoogleMaps.Directions.QueryAsync(request);
            var overview = result.Routes.First().OverviewPath.Line;
            var poly = new Polyline {  StrokeColor = Color.Black, StrokeWidth = 7};

            foreach (var line in overview )
            {
                var pos = new Position(latitude: line.Latitude, longitude: line.Longitude);
                poly.Geopath.Add(pos);
            }

            mapView.MapElements.Add(poly);

            var startPin = new Position(latitude: overview.First().Latitude, overview.First().Longitude);
            var endPin = new Position(latitude: overview.Last().Latitude, overview.Last().Longitude);

            var pin = new Pin { Position = startPin, Label = "Pick Up" };
            var pin1 = new Pin { Position = endPin, Label = "Drop Off" };

            var test =  Math.Acos(Math.Sin(pickup.Latitude * Math.PI / 180) * Math.Sin(dropOff.Latitude * Math.PI / 180) + Math.Cos(pickup.Latitude * Math.PI / 180) * Math.Cos(dropOff.Latitude * Math.PI / 180) * Math.Cos(dropOff.Longitude * Math.PI / 180 - pickup.Longitude * Math.PI / 180)) * 6371000;

            mapView.MoveToRegion(MapSpan.FromCenterAndRadius(startPin, new Distance(test)));
            mapView.Pins.Clear();
            mapView.Pins.Add(pin);
            mapView.Pins.Add(pin1);
        }



        private View setUpView()
        {
            chatImg.Source = new FontImageSource { FontFamily = App.meterialIconsFamily, Size = 25, Glyph = IconFont.Chat, Color = Color.White };
            frame.Content = chatImg;

            return new AbsoluteLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    { mapView, new Rectangle(0,0,1,1), AbsoluteLayoutFlags.All },
                    { frame , new Point(x: 10 , y: 10) },
                }
            };
      
        }
    }


    public class myTabHeight : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int height;

        public myTabHeight(int value)
        {
            this.height = value;
        }

        public int MyTabHeight
        {
            get { return height; }
            set
            {
                height = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged();
            }
        }

        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged(string height = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(height));
        }
    }

}

