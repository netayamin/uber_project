using System;
using System.Collections.Generic;
using uber_uni.helpers;
using uber_uni.orderViews;
using uber_uni.views_bottomNav;
using Xamarin.Forms;

namespace uber_uni.views
{
    public class sideNavViewPage : ContentPage
    {
        public ListView ListView { get { return listView; } }

        Frame stackFrame = new Frame {  BackgroundColor = Color.Black, HeightRequest = 160, Margin = 0, Padding = 0, CornerRadius = 0 };
        StackLayout userInfoStack = new StackLayout { Orientation = StackOrientation.Horizontal, BackgroundColor = Color.Black, Padding = 20};
        Label username = new Label { Text = "John Smith", TextColor = Color.White, VerticalOptions = LayoutOptions.Center, FontSize = 18 };
        Frame imageFrame = new Frame { WidthRequest = 60, HeightRequest = 60, CornerRadius = 30, HasShadow = false, Margin = 0, Padding = 0, VerticalOptions = LayoutOptions.Center, IsClippedToBounds = true };
        Image UserImage = new Image {  Aspect = Aspect.Fill, Source = "https://images.unsplash.com/profile-fb-1527368999-01bec71421e9.jpg?ixlib=rb-1.2.1&q=80&fm=jpg&crop=faces&cs=tinysrgb&fit=crop&h=128&w=128" };
        ListView listView;
        List<MasterPageItem> masterPageItems;

        public sideNavViewPage()
        {
            Title = "Nav";
            IconImageSource = new FontImageSource { FontFamily = App.meterialIconsFamily, Size = 30, Glyph = IconFont.Menu };
            populateList();
            createListView();
            imageFrame.Content = UserImage;
            Extensions.SetIPhoneSafeArea(this, userInfoStack);
            ListView.SelectedItem = masterPageItems[0];
            userInfoStack.Children.Add(imageFrame);
            userInfoStack.Children.Add(username);
            stackFrame.Content = userInfoStack;
            Content = getStackLayout();
        }

        private View getStackLayout()
        {
            var stack = new StackLayout
            {
                Children = {
                    stackFrame,
                    listView
                }
            };

            return stack;
        }

        private void createListView()
        {
            listView = new ListView
            {
                BackgroundColor = Color.Black,
                ItemsSource = masterPageItems,
                ItemTemplate = new DataTemplate(() => {
                    var grid = new Grid { Padding = new Thickness(5, 10) };
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30) });
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                    var image = new Image();
                    var iconImage = new FontImageSource { FontFamily = App.meterialIconsFamily, Size = 30, Color = Color.White };
                    iconImage.SetBinding(FontImageSource.GlyphProperty, "IconSource");
                    image.Source = iconImage;
                    var label = new Label { VerticalOptions = LayoutOptions.FillAndExpand, TextColor = Color.White };
                    label.SetBinding(Label.TextProperty, "Title");

                    grid.Children.Add(image);
                    grid.Children.Add(label, 1, 0);

                    return new ViewCell { View = grid };
                }),
                SeparatorVisibility = SeparatorVisibility.None
            };
        }


        private void populateList()
        {
            masterPageItems = new List<MasterPageItem>();
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Uber You",
                IconSource = IconFont.FaceProfile ,
                TargetType = typeof(bottomTabsView)
            });


            masterPageItems.Add(new MasterPageItem
            {
                Title = "Uber Guard",
                IconSource = IconFont.Shield,
                TargetType = typeof(companionsView)
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Uber Business",
                IconSource = IconFont.GoogleMyBusiness,
                TargetType = typeof(uberBusinessView)

            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Your Trips",
                IconSource = IconFont.Car,
                TargetType = typeof(travelHistoryView)
            });

            masterPageItems.Add(new MasterPageItem
            {
                Title = "Payment",
                IconSource = IconFont.Cash,
            });

            masterPageItems.Add(new MasterPageItem
            {
                Title = "Help",
                IconSource = IconFont.AccountQuestion,
            });

            masterPageItems.Add(new MasterPageItem
            {
                Title = "Get Discount",
                IconSource = IconFont.CashMinus,
            });


            masterPageItems.Add(new MasterPageItem
            {
                Title = "Custom Trip",
                IconSource = IconFont.AccountSettings,
                TargetType = typeof(customTrip)
            }) ;
        }
    }


}

public class MasterPageItem
{
    public string Title { get; set; }
    public string IconSource { get; set; } = "chat-icon-white.png";
    public Type TargetType { get; set; }
}