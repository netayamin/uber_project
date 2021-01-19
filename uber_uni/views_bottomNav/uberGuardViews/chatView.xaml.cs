using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace uber_uni.views_bottomNav.uberGuardViews
{
    public partial class chatView : ContentPage
    {

        public ICommand ScrollListCommand { get; set; }
        Compnaion camp = new Compnaion();

        public chatView(Compnaion comp)
        {
            this.BindingContext = new ChatPageViewModel();
            camp = comp;
            Title = camp.first_name;
            InitializeComponent();
        }

        public void OnListTapped(object sender, ItemTappedEventArgs e)
        {
            chatInput.UnFocusEntry();
        }

        public void ScrollTap(object sender, System.EventArgs e)
        {
            lock (new object())
            {
                if (BindingContext != null)
                {
                    var vm = BindingContext as ChatPageViewModel;

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        while (vm.DelayedMessages.Count > 0)
                        {
                            vm.Messages.Insert(0, vm.DelayedMessages.Dequeue());
                        }
                        vm.ShowScrollTap = false;
                        vm.LastMessageVisible = true;
                        vm.PendingMessageCount = 0;
                        ChatList?.ScrollToFirst();
                    });
                }

            }
        }

    }
}
