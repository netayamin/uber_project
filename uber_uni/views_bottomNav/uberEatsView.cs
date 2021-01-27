using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using GoogleApi.Entities.Places.Search.NearBy.Request;
using GoogleApi.Entities.Places.Search.Common.Enums;
using GoogleApi;
using GoogleApi.Entities.Places.Search.NearBy.Response;
using System.Collections.Generic;
using Xamarin.Forms.Maps;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;
using uber_uni.view_assets;
using System.Linq;
using GoogleApi.Entities.Places.Common;

namespace uber_uni.views
{
    public class uberEatsView : ContentPage
    {

        ObservableCollection<PlaceInfo> collection = new ObservableCollection<PlaceInfo>();
        static ActivityIndicator loader = new ActivityIndicator { IsRunning = true, IsVisible = true, IsEnabled = true };
        DataTemplate dt = new DataTemplate(() => {         
            var text1 = new Label { };
            var text2 = new Label { FontSize = 14};
            var image = new Image { WidthRequest = 50, HeightRequest = 50, Aspect = Aspect.AspectFill,  };

            text1.SetBinding(Label.TextProperty, nameof(PlaceInfo.name));
            text2.SetBinding(Label.TextProperty, nameof(PlaceInfo.address));
            image.SetBinding(Image.SourceProperty, nameof(PlaceInfo.picUrl));

            var column1 = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = {
                    text1,
                    text2,
                }
            };

            var row = new StackLayout
            {
                Padding = 20,
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.Center,
                Children = {
                    column1,
                    loader,
                    image
                }
               
            };
            return new ViewCell { View = row };

        });

        Map mapView = new Map { Margin  = 0 };
        ListView list;

        public uberEatsView(Position dropOff)
        {
            setLocation(dropOff);
            Content = setUpView();
        }

        public async Task findPlacesNearAsync(Position position)
        {
            var location = new Location(latitude: position.Latitude, longitude: position.Longitude);
            var request =   new PlacesNearBySearchRequest
            {
                Key = Keys.ApiKey,
                Location = location,
                Radius = 1000,
                Type = SearchPlaceType.Restaurant
            };

            var response = await GooglePlaces.NearBySearch.QueryAsync(request);
           
            foreach (var r in response.Results)
            {
                if ( response.Results != null)
                {
                    try {

                        var photoRef = r.Photos.ElementAt<Photo>(0).PhotoReference;
                        if (photoRef != null)
                        {
                            var imageUrl = $"https://maps.googleapis.com/maps/api/place/photo?maxwidth=200&photoreference={photoRef}&key={Keys.ApiKey}";
                            var placeInfo = new PlaceInfo { address = r.Vicinity, name = r.Name, picUrl = imageUrl };
                            collection.Add(placeInfo);
                        }
                        var restruantPosition = new Position(latitude: r.Geometry.Location.Latitude, longitude: r.Geometry.Location.Longitude);
                        var pin = new Pin
                        {
                            Label = r.Name,
                            Position = restruantPosition,
                            Address = r.Vicinity,
                            Type = PinType.Place,
                        };
                        loader.IsEnabled = false;
                        loader.IsRunning = false;
                        loader.IsVisible = false;
                        pin.InfoWindowClicked += Pin_InfoWindowClicked;
                        mapView.Pins.Add(pin);

                    } catch
                    {
                        Console.WriteLine("err");
                    }

           
                }
            
            }
        }

        private void Pin_InfoWindowClicked(object sender, PinClickedEventArgs e)
        {
            Navigation.PushAsync(new MainChatView());
        }


        private View setUpView()
        {
            list = new ListView { ItemsSource = collection, HeightRequest = Application.Current.MainPage.Height/2 - 100, ItemTemplate = dt, HasUnevenRows = true};
            list.ItemSelected += List_ItemSelected;

            return new StackLayout
            {
                Children = {
                    new uberHeaderView("Uber Eats", this),
                    mapView,
                    list
                }, Spacing = 0
            };
        }

        private void List_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Navigation.PushAsync(new MainChatView());
        }

        public void setLocation(Position pos)
        {
            mapView.MoveToRegion(MapSpan.FromCenterAndRadius(pos, new Distance(5000)));
            _ = findPlacesNearAsync(pos);

        }

    }
}




public class PlaceInfo
{
    public string name { get; set; }
    public string address { get; set; }
    public double rating { get; set; }
    public string picUrl { get; set; }
}