using System;
using System.Collections.Generic;
using GooglePlacesApi.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace uber_uni.views
{
    public class RootViewController : MasterDetailPage
    {
        private sideNavViewPage master = new sideNavViewPage();
        public bottomTabsView tabsView;
        public NavigationPage mynav; 

        public RootViewController(Location dropOffPoint)
        {

            tabsView = new bottomTabsView(dropOffPoint);
            mynav = new NavigationPage(tabsView);
            Master = master;
            mynav.BarBackgroundColor = Color.Black;
            mynav.BarTextColor = Color.White;
            Detail = mynav;
            IsPresented = false;
            master.ListView.ItemSelected += OnItemSelected;
        }
        
        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            Page page = new Page();

            if (item != null && item.TargetType != null)
            {
                if (item.TargetType == typeof(bottomTabsView))
                {
                    page = (Page)Activator.CreateInstance(item.TargetType, new Location());
                } else
                {
                    page = (Page)Activator.CreateInstance(item.TargetType);
                }
                var nav = new NavigationPage(page);
                nav.BarBackgroundColor = Color.Black;
                nav.BarTextColor = Color.White;
                Detail = nav;
                IsPresented = false;
            }
        }      
    }
}

