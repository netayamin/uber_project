using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Sharpnado.HorizontalListView.RenderedViews;
using uber_uni;
using uber_uni.helpers;
using uber_uni.views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace uber_uni
{
    public class companionsView : ContentPage
        {
        public static ObservableCollection<Compnaion> companionsList = new ObservableCollection<Compnaion> {
        new Compnaion { first_name = "Adam Smith", image_path = "https://res-5.cloudinary.com/crunchbase-production/image/upload/c_thumb,h_256,w_256,f_auto,g_faces,z_0.7,q_auto:eco/v1475740594/e0hctaqpwahdefwz4kbg.jpg" },
        new Compnaion { first_name = "Emily Cohen", image_path = "https://www.stylist.co.uk/images/app/uploads/2019/10/16143608/alyssa-carson-could-be-the-first-person-to-walk-on-mars-crop-1571233012-896x896.jpg?w=256&h=256&fit=max&auto=format%2Ccompress" },
        new Compnaion { first_name = "Elliot Levy", image_path = "https://www.gravatar.com/avatar/afab89c15ce27024be347ee57e820d5a?s=256" },
        new Compnaion { first_name = "Jessica Smith", image_path = "https://secure.gravatar.com/avatar/af7652457d957bb923ccdc31e2062119?s=256&d=mm&r=g" },
        new Compnaion { first_name = "Emily Cohen", image_path = "https://www.stylist.co.uk/images/app/uploads/2019/10/16143608/alyssa-carson-could-be-the-first-person-to-walk-on-mars-crop-1571233012-896x896.jpg?w=256&h=256&fit=max&auto=format%2Ccompress" },
        new Compnaion { first_name = "Adam Smith", image_path = "https://res-5.cloudinary.com/crunchbase-production/image/upload/c_thumb,h_256,w_256,f_auto,g_faces,z_0.7,q_auto:eco/v1475740594/e0hctaqpwahdefwz4kbg.jpg" },
        };

        Button btn = new Button { Text = "Add new companion", Margin = 20, Padding = 20, WidthRequest = 200, HorizontalOptions = LayoutOptions.Center, BackgroundColor = Color.Black, MinimumHeightRequest = 50, TextColor = Color.White };

        public companionsView()
        {
            getCompanions();
            Content = setUpView();
        }


        private void CompnaionsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Compnaion selectedComp = (Compnaion)e.Item;
            _ = Navigation.PushAsync(new uberGuardView(selectedComp));
        }

        private void OnTappedAsync(object obj)
        {
            Compnaion selectedComp = (Compnaion)obj;
            _ = Navigation.PushAsync(new uberGuardView(selectedComp));
        }

        //get companions from db
        void getCompanions()
        {
            var comp = App.Database.geAllCompanions().Result;
            foreach (var c in comp)
            {
                companionsList.Add(c);
            }
        }


        private View setUpView()
        {
            btn.Clicked += Btn_Clicked;
            var compnaionsListView = new ListView
            {
                ItemsSource = companionsList,
                ItemTemplate = datatemplate(),
                RowHeight = 70,
            };


            compnaionsListView.ItemTapped += CompnaionsListView_ItemTapped;


            return new StackLayout
            {
                Children =
                {
                    { new uberHeaderView("Uber Guard", this) },
                    compnaionsListView,
                    btn
                }, Spacing = 0, Padding = 0, Margin = 0,
            };
        }

 

        //add companion event handler
        void Btn_Clicked(object sender, EventArgs e)
        {
            _ = Navigation.PushAsync(new AddNewCompanion());
        }

        private DataTemplate datatemplate()
        {
            return new DataTemplate(() =>
             {
                 var stack = new StackLayout {  Margin = new Thickness(10,0,0,0) ,Orientation =StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.Start, VerticalOptions = LayoutOptions.Center };
                 var nameLabel = new Label { FontAttributes = FontAttributes.Bold, VerticalOptions = LayoutOptions.Center};
                 var img = new Image { Aspect = Aspect.AspectFill };
                 var imageFrame = new Frame {HasShadow = false ,Content = img, BackgroundColor = Color.Gray, HeightRequest = 50, WidthRequest = 50, Padding = 0, Margin = 0, CornerRadius = 25, IsClippedToBounds = true };

                //set binding from model
                 nameLabel.SetBinding(Label.TextProperty, "first_name");
                 img.SetBinding(Image.SourceProperty, "image_path");

                 stack.Children.Add(imageFrame);
                 stack.Children.Add(nameLabel);

                 return new ViewCell { View = stack };
             });
        }
   
    }
}

