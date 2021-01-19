using System;
using uber_uni.views;
using Xamarin.Forms;

namespace uber_uni.orderViews
{
    public class uberBusinessView : ContentPage
    {


        orderRideView mainView = new orderRideView(false, true, true);
        public uberBusinessView()
        {

            mainView.submitBtn.Clicked += SubmitBtn_Clicked;
            Content = new StackLayout
            {
                Children = {
                    new uberHeaderView("Uber Business", this),
                    mainView
                }
            };

        }

        private void SubmitBtn_Clicked(object sender, EventArgs e)
        {
            mainView.createTripsAndSaveInDB();
        }
    }
}

