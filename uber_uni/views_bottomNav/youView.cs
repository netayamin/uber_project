using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
        public static Map mapView = new Map { WidthRequest = App.ScreenWidth, HeightRequest = App.ScreenHeight - 170, IsVisible = false };
        Frame frame = new Frame { WidthRequest = 50, HeightRequest = 50, CornerRadius = 25, HasShadow = false, BackgroundColor = Color.Black, Margin =0 ,Padding = 0 };
        Image chatImg = new Image { VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
        static ActivityIndicator loader = new ActivityIndicator { IsRunning = true, IsVisible = true, HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center };
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

        public static void drawRouteOnMap(Position pickup, Position dropOff)
        {
            var origin = new Location { Latitude = pickup.Latitude, Longitude = pickup.Longitude };
            var Destination = new Location { Latitude = dropOff.Latitude, Longitude = dropOff.Longitude };

            var request = new DirectionsRequest
            {
                Key = "AIzaSyD0WqCwh8vVTlBGNXz3YC55-fEjewHu3ws",
                Origin = origin,
                Destination = Destination
            };

            var result = GoogleMaps.Directions.Query(request);
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

            mapView.MoveToRegion(MapSpan.FromCenterAndRadius(startPin, new Distance(5000)));
            mapView.Pins.Clear();
            mapView.Pins.Add(pin);
            mapView.Pins.Add(pin1);
            mapView.IsVisible = true;
            loader.IsVisible = false;
        }

        public async void GetCurrentLocation()
        {
            var request = new Xamarin.Essentials.GeolocationRequest(Xamarin.Essentials.GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            var location = await Xamarin.Essentials.Geolocation.GetLocationAsync(request);
            Position position = new Position ( location.Latitude, location.Longitude );
            mapView.MoveToRegion(MapSpan.FromCenterAndRadius(position, new Distance(1000)));
            mapView.Pins.Add(new Pin { Position = position, Label = "Current Location", Type = PinType.Generic, });
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
                    mapView,
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

