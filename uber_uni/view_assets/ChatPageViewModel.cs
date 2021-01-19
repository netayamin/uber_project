using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using uber_uni.models;
using Xamarin.Forms;

namespace uber_uni.views_bottomNav.uberGuardViews
{
    public class ChatPageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Message> Messages { get; set; } = new ObservableCollection<Message>();
        public string TextToSend { get; set; }
        public ICommand OnSendCommand { get; set; }

        public ChatPageViewModel()
        {
            MessageAppearingCommand = new Command<Message>(OnMessageAppearing);
            MessageDisappearingCommand = new Command<Message>(OnMessageDisappearing);
            //Messages.Add(new Message() { Text = "Hi" });
            //Messages.Add(new Message() { Text = "How are you?" });

            OnSendCommand = new Command(() =>
            {
                if (!string.IsNullOrEmpty(TextToSend))
                {
                    //  Messages.Insert(0, new Message() { Text = TextToSend, User = App.User });
                    Messages.Add(new Message() { Text = TextToSend, User = App.User });


                    if (TextToSend.ToLower() == "hey! i am now in the uber.")
                    {
                        Messages.Add(new Message() { Text = "okay great. is everthing okay?" });
                    }

                    else if (TextToSend.ToLower() == "all is good thank you! i am having a lovely conversation with the driver.")
                    {
                        Messages.Add(new Message() { Text = "Interesting. What are you talking about?" });
                    }

                    else if (TextToSend.ToLower() == "we were talking about restaurants actually! he has given me a list of really great places to eat in and around london, have you ever heard of bocconcino?")
                    {
                        Messages.Add(new Message() { Text = "OMG! Yes. I have heard that their Linguine Alle Vongole is to die for!!! We should go." });
                    }

                    else if (TextToSend.ToLower() == "yes yes yes, that sounds great! i need to buy a dress first.")
                    {
                        Messages.Add(new Message() { Text = "I see on Uber that you will be passing Bershka soon. If you ask the driver to let you out I will walk up and meet you." });
                    }

                    else if (TextToSend.ToLower() == "sounds like a plan! how long will it take you to walk up?")
                    {
                        Messages.Add(new Message() { Text = "Mmm no longer than five minutes." });
                    }

                    else if (TextToSend.ToLower() == "okay, see you then :)")
                    {
                        Messages.Add(new Message() { Text = "See you :P" });
                    }

                }
                TextToSend = string.Empty;

            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool ShowScrollTap { get; set; } = false; //Show the jump icon 
        public bool LastMessageVisible { get; set; } = true;
        public int PendingMessageCount { get; set; } = 0;
        public bool PendingMessageCountVisible { get { return PendingMessageCount > 0; } }
        public Queue<Message> DelayedMessages { get; set; } = new Queue<Message>();

        public ICommand MessageAppearingCommand { get; set; }
        public ICommand MessageDisappearingCommand { get; set; }

        void OnMessageAppearing(Message message)
        {
            var idx = Messages.IndexOf(message);
            if (idx <= 6)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    while (DelayedMessages.Count > 0)
                    {
                        // Messages.Insert(0, DelayedMessages.Dequeue());
                        var msg = DelayedMessages.Dequeue();
                        //Messages.Add(msg);
                        Messages.Insert(0, msg);

                    }

                    ShowScrollTap = false;
                    LastMessageVisible = true;
                    PendingMessageCount = 0;
                });
            }
        }

        void OnMessageDisappearing(Message message)
        {
            var idx = Messages.IndexOf(message);
            if (idx >= 6)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    ShowScrollTap = true;
                    LastMessageVisible = false;
                });

            }
        }
    }
}

