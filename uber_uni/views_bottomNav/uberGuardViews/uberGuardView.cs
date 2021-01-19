using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GooglePlacesApi;
using GooglePlacesApi.Models;
using uber_uni.view_assets;
using uber_uni.views_bottomNav.uberGuardViews;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace uber_uni.views
{
    public class uberGuardView : ContentPage
    {
        orderRideView myview = new orderRideView(false, false, true);
        Compnaion comp = new Compnaion();

        public uberGuardView(Compnaion compnaion)
        {
            comp = compnaion;
            myview.submitBtn.Clicked += SubmitBtn_Clicked;
            myview.title.Text = $"Schedule a ride for {compnaion.first_name}";
            Content = new StackLayout
            {
                Children = {
                    new uberHeaderView("Uber Guard", this),
                    myview
             }
            };
        }

        private void SubmitBtn_Clicked(object sender, EventArgs e)
        {
            var pos = new Position(latitude: myview.PickUpPoint.Latitude, longitude: myview.PickUpPoint.Longitude);
            Navigation.PushAsync(new trackCompnaionView(compnaion: comp, pickupPoint: pos));
        }
    }
}

