using System;

using Xamarin.Forms;

namespace uber_uni.views
{
    public class RegisterAccountView : ContentPage
    {


        public RegisterAccountView()
        {
            var frame = new Frame { HeightRequest = 200, BackgroundColor = Color.Black, HasShadow = false, CornerRadius = 0, Padding =0, Margin= 0 };
            frame.Content = new Label { Text = "Welcome to Uber, have a safe ride!", TextColor = Color.White, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center };
            var submitBtn = new Button { Text = "Register", HeightRequest = 50, BackgroundColor = Color.Black, TextColor = Color.White };
            var nameField = new Entry { Placeholder = "Full Name", HeightRequest = 50 };
            var emailField = new Entry { Placeholder = "Email Address", HeightRequest = 50 };
            var phoneField = new Entry { Placeholder = "Phone Number", HeightRequest = 50 };
            var passwordField = new Entry { Placeholder = "Password" , HeightRequest = 50 };

            //collect data from all fields
            User getUserInfo()
            {
                return (new User()
                {
                    full_name = nameField.Text,
                    phone = int.Parse(phoneField.Text),
                    email = emailField.Text,
                    profile_pic = "",
                    password = passwordField.Text
                });
            }

           submitBtn.Clicked += SubmitBtn_Clicked;

           async void SubmitBtn_Clicked(object sender, EventArgs e)
            {
                User newUser = getUserInfo();
                //add user to db
                var res = await App.FireBase.addNewPerson(newUser);
                if (res == 1)
                {
                    await DisplayAlert("Successful", "Thanks for signing up " + newUser.full_name + ". Have a safe ride!", "OK");
                    App.Current.MainPage = new RootViewController();
                } else
                {
                    await DisplayAlert("Oops", "There was an error", "OK");
                }
            }


            var stack = new StackLayout
            {
                Children = {
                    nameField,
                    emailField,
                    passwordField,
                    phoneField,
                    submitBtn
                },
                Spacing = 20,
                Padding = 20
            };

            Content = new StackLayout
            {
                Children = {
                    frame,
                    stack
                }
            };

        }

    }
}








