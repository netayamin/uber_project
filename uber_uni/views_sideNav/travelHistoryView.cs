using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace uber_uni.views
{
    public class travelHistoryView : ContentPage
    {

        ListView tripsListView;
        public static ObservableCollection<Trip_Position> tripsCollection = new ObservableCollection<Trip_Position>();
        ActivityIndicator loader = new ActivityIndicator { IsRunning = true, IsVisible = true, Color = Color.Black, HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center };

        DataTemplate dt = new DataTemplate(() =>
        {
            var date = new Label();
            var car = new Label { FontSize = 12 };

            date.SetBinding(Label.TextProperty, nameof(Trip_Position.date));
            car.SetBinding(Label.TextProperty, nameof(Trip_Position.car));

            var mapView = new myMap { WidthRequest = 100, HeightRequest = 200, InputTransparent = true };
            mapView.SetBinding(mapView.locationProperty, nameof(Trip_Position.position));

            var stack = new StackLayout();
            stack.Children.Add(date);
            stack.Children.Add(car);
            stack.Children.Add(mapView);

            var frame = new Frame { BackgroundColor = Color.White, Padding = 20, Margin = 10, Content = stack, HasShadow = false, CornerRadius = 10 };

            return new ViewCell { View = frame };
        });


        public travelHistoryView()
        {
              _  = getUserTrips();

          
            tripsListView = new ListView
            {
                ItemTemplate = dt, HasUnevenRows = true,  BackgroundColor = Color.FromHex("#F5F5F5"),
                SeparatorVisibility = SeparatorVisibility.None
            };

            Content = new AbsoluteLayout {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = {
                    tripsListView,
                    { loader , new Rectangle(0.5, 0.5, -1, -1),
                      AbsoluteLayoutFlags.PositionProportional },
                } };
        }



        private async Task getUserTrips() {
            tripsCollection.Clear();
            var trips =  await App.Database.geAllTrips();

            trips.ForEach((t) => {
                var trip = new Trip_Position
                {
                    position = new Position(latitude: t.drop_off_lat, longitude: t.pickup_longt),
                    date = t.date.ToLongDateString(),
                    car = t.car
                };
                tripsCollection.Add(trip);
            });

            tripsListView.ItemsSource = tripsCollection;
            loader.IsVisible = false;
            loader.IsRunning = false;
        }

    }

    //custom map view - binding
    public class myMap : ContentView
    {
        Map map = new Map();
        public  BindableProperty locationProperty = BindableProperty.Create("position", typeof(Position), typeof(travelHistoryView), null, propertyChanged: MapMoveChangeValue);

        private static void MapMoveChangeValue(BindableObject bindable, object oldValue, object newValue)
        {
            myMap view = bindable as myMap;
            if (view != null)
            {
                Position position = (Position)newValue;
                view.map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Distance.FromMiles(2)));
            }
        }

        public myMap()
        {
            Content = map;
        }
    }

};




