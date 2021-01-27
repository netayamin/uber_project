using System;
using System.Collections.ObjectModel;
using GooglePlacesApi.Models;
using uber_uni.view_assets;
using uber_uni.views_bottomNav;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.PlatformConfiguration;


namespace uber_uni.views
{
    public class orderRideView : ContentView
    {
       public Label title = new Label { Text = "Schedule your ride", FontAttributes = FontAttributes.Bold };
        AutoCompleteEntry pickupPoint = new AutoCompleteEntry("Pick Up Point");
        AutoCompleteEntry dropOffPoint = new AutoCompleteEntry("Drop Off Point");
        Entry comment_field = new Entry { Placeholder = "Additional Requirements", HeightRequest = 150, };
       TimePicker time_field = new TimePicker { VerticalOptions = LayoutOptions.Start, HeightRequest = 50 };
       ObservableCollection<String> generesList = new ObservableCollection<String>();
       Picker musicPicker = new Picker { HeightRequest = 50 };
       Button connectWithALocal = new Button {  BackgroundColor = Color.DarkGray, Padding = 10, TextColor = Color.White, HeightRequest = 50, Text = "Connect with a local"};
       public Button submitBtn = new Button { WidthRequest = 200,   BackgroundColor = Color.Black, TextColor = Color.White, HeightRequest = 50, Text = "Order"};
       public const string ColumnID = "ID";
       public Location PickUpPoint;
       public Location DropOffPoint;

        private void populateMusicList()
        {
            generesList.Add("Pick music genre");
            generesList.Add("Rock/Metal");
            generesList.Add("Hip-Hop/Rap");
            generesList.Add("Blues");
            generesList.Add("R&B");
            generesList.Add("Opera");
            generesList.Add("Soul");
            generesList.Add("Classical");
            generesList.Add("Reggae/Ska");
            generesList.Add("Dubstep");
            generesList.Add("K-Pop/J-Pop");
            generesList.Add("Electronic");
        }

        //init order view with users choices
        public orderRideView(Boolean showMusicPicker, Boolean showConnectWithLocal, Boolean showCommentField )
        {
            Content = viewSetUp(showMusicPicker, showConnectWithLocal, showCommentField);
            pickupPoint.AutoCompleteList.ItemTapped += pickupPoint_ItemTapped;
            dropOffPoint.AutoCompleteList.ItemTapped += dropoffPoint_ItemTapped;
        }

        private async void dropoffPoint_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            dropOffPoint.AutoCompleteList.IsVisible = false;
            var index = e.ItemIndex;
            Prediction p = dropOffPoint.predictions[index];
            dropOffPoint.entry.Text = p.Description.ToString();
            dropOffPoint.AutoCompleteList.IsVisible = false;
            DropOffPoint = getPosition(p.PlaceId);
        }

        private Location getPosition(string placeID)
        {
            var res =  App.googlePlacesService.GetDetailsAsync(placeID, "", DetailLevel.Basic).Result;
            var location = res.Place.Geometry.Location;
            return location;
        }

        private async void pickupPoint_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            pickupPoint.AutoCompleteList.IsVisible = false;
            var index = e.ItemIndex;
            Prediction p = pickupPoint.predictions[index];
            pickupPoint.entry.Text = p.Description.ToString();
            pickupPoint.AutoCompleteList.IsVisible = false;
            PickUpPoint = getPosition(p.PlaceId);
        }

        //create trips table
        public void createTripsAndSaveInDB()
        {
            string[] cars = { "Audi", "Volvo", "BMW" };
            Random random = new Random();
            var t = new Trip { drop_off_lat = DropOffPoint.Latitude, drop_off_longt = DropOffPoint.Longitude, date = DateTime.Now, car = cars[random.Next(cars.Length)],  };
            App.Database.saveNewTrip(t);
            var postion = new Position(latitude: DropOffPoint.Latitude, longitude: DropOffPoint.Longitude);
            var pickup_postion = new Position(latitude: PickUpPoint.Latitude, longitude: PickUpPoint.Longitude);
            youView.drawRouteOnMap(pickup_postion , postion);
        } 

        private StackLayout viewSetUp(Boolean showMusicPicker, Boolean showConnectWithLocal, Boolean showCommentField)
        {
            populateMusicList();
            musicPicker.ItemsSource = generesList;
            musicPicker.SelectedItem = generesList[0];

            musicPicker.IsVisible = showMusicPicker;
            comment_field.IsVisible = showCommentField;
            connectWithALocal.IsVisible = showConnectWithLocal;

            connectWithALocal.Clicked += ConnectWithALocal_Clicked;
            var topStack = new StackLayout { Children = { pickupPoint, time_field }, Orientation = StackOrientation.Horizontal};

            return new StackLayout
            {
                Children = {
                            title,
                            topStack,
                            dropOffPoint,
                            comment_field,
                            musicPicker,
                            connectWithALocal,
                            submitBtn
                        },
                Padding = 20,
                Spacing = 10, VerticalOptions = LayoutOptions.FillAndExpand
            };
          
           
        }

        private void ConnectWithALocal_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainChatView());
        }
    }
}

