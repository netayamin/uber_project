using System;
using System.Linq;
using uber_uni.helpers;
using uber_uni.orderViews;
using uber_uni.views_bottomNav;
using Xamarin.Forms;
using GooglePlacesApi.Models;
using Xamarin.Forms.Maps;
using System.Runtime.InteropServices;

namespace uber_uni.views
{
    public class bottomTabsView : TabbedPage
    {
        
        public bottomTabsView([Optional] Location dropOff)
        {
            var pos = new Position(latitude: dropOff.Latitude, longitude: dropOff.Longitude);
            Children.Add(new uberEatsView(pos) { Title = "Eats", IconImageSource = new FontImageSource { FontFamily = App.meterialIconsFamily, Size = 30, Glyph = IconFont.Food } });
            Children.Add(new youView { Title = "You", IconImageSource = IconImageSource = new FontImageSource { FontFamily = App.meterialIconsFamily, Size = 30, Glyph = IconFont.Map } }); //this display map with users location/trip
            Children.Add(new companionsView { Title = "Guard" , IconImageSource = new FontImageSource { FontFamily = App.meterialIconsFamily, Size = 30, Glyph = IconFont.Shield } }); 
            Children.Add(new uberBusinessView { Title = "Business", IconImageSource = new FontImageSource { FontFamily = App.meterialIconsFamily, Size = 30, Glyph = IconFont.OfficeBuilding } });
            BarBackgroundColor = Color.Black;
            BarTextColor = Color.White;
            SelectedItem = this.Children[1];
        }



    }

}






            