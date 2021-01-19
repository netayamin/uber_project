using System;
using System.Collections.Generic;
using uber_uni.views_bottomNav.uberGuardViews;
using Xamarin.Forms;

namespace uber_uni.view_assets
{
    public partial class inputBar : ContentView
    {
        public inputBar()
        {
            InitializeComponent();
        }


        public void Handle_Completed(object sender, EventArgs e)
        {
            // (this.Parent.Parent.BindingContext as ChatPageViewModel).OnSendCommand.Execute(null);


            (this.Parent.Parent.BindingContext as ChatPageViewModel).OnSendCommand.Execute(null);
            chatTextInput.Focus();
        }

        public void UnFocusEntry()
        {
            chatTextInput?.Unfocus();
        }

    }

}
