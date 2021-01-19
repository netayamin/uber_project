using System;
using uber_uni.models;
using Xamarin.Forms;

namespace uber_uni.view_assets
{
    class ChatTemplateSelector : DataTemplateSelector
    {
        DataTemplate incomingDataTemplate;
        DataTemplate outgoingDataTemplate;

        public ChatTemplateSelector()
        {
            this.incomingDataTemplate = new DataTemplate(typeof(IncomeViewCell));
            this.outgoingDataTemplate = new DataTemplate(typeof(outgoingViewCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var messageVm = item as Message;
            if (messageVm == null)
                return null;


            return (messageVm.User == App.User) ? outgoingDataTemplate : incomingDataTemplate;
        }

    }
}

