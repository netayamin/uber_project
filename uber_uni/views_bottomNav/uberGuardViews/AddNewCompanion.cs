using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Net;
using System.Linq;
using Xamarin.Forms.PlatformConfiguration;

namespace uber_uni
{


    public class AddNewCompanion : ContentPage
    {

        byte[] imgBytes;

        public AddNewCompanion()
          {
            Title = "Register a Companion";
            //view set up
            var btn = new Button { Text = "Add new companion", BackgroundColor = Color.Black, TextColor = Color.White };
            var label = new Label { Text = "User will have to confirm this link on their device", AnchorX = Bounds.Center.X, HorizontalTextAlignment = TextAlignment.Center };
            var second_name = new Entry { Placeholder = "Last Name", AnchorX = Bounds.Center.X, HeightRequest = 50 };
            var first_name = new Entry { Placeholder = "First Name", AnchorX = Bounds.Center.X, HeightRequest = 50 };
            var uber_account = new Entry { Placeholder = "Uber Account", AnchorX = Bounds.Center.X, HeightRequest = 50 };
            var myImage = new Image { Aspect = Aspect.AspectFill };
            var imageFrame = new Frame { HeightRequest = 100, Padding = 0, Margin = 0, CornerRadius = 50, HorizontalOptions = LayoutOptions.Center, WidthRequest = 100, IsClippedToBounds = true, Content = myImage, HasShadow=false, BackgroundColor = Color.Gray };

            //settings up the tap gesture for image frame
            var gest = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
            imageFrame.GestureRecognizers.Add(gest);

            //submit button handler
            btn.Clicked += Btn_Clicked;

            void Btn_Clicked(object sender, EventArgs e)
            {
                //add companion pic to local storage
                var filePath = DownloadImage(first_name.Text, imgBytes);

                //add new compnaion to local db
                var p = new Compnaion
                {
                    first_name = first_name.Text,
                    second_name = second_name.Text,
                    username = uber_account.Text,
                    image_path = filePath
                };

                App.Database.saveNewCompanion(p);
                companionsView.companionsList.Add(p);
                DisplayAlert("Success", "Companion was added successfully!", "OK");
            }

            //image frame tap handler
            gest.Tapped += Gest_Tapped;
            void Gest_Tapped(object sender, EventArgs e)
            {
                showMediaPicker();
            }

            //display photo gallery picker and get picked image
            async void showMediaPicker()
            {
                var res = await MediaPicker.PickPhotoAsync();
                try
                {
                    var stream = await res.OpenReadAsync();
                    var finalImage = ImageSource.FromStream(() => stream);
                    myImage.Source = finalImage;
                    var s = await res.OpenReadAsync();
                    imgBytes = GetImageStreamAsBytes(s);
                }
                catch (Exception e)
                {
                  Console.Write("error" + e.Message) ;
                }

            }

            //set content view
            Content = new StackLayout
            {
                Children = {
                    imageFrame,
                    label,
                    first_name,
                    second_name,
                    uber_account,
                    btn
                },
                AnchorX = Bounds.Center.X,
                Padding = 40,
                Spacing = 20,
                HorizontalOptions = LayoutOptions.Center,

            };
        }

        //change image to bytes to store in local storage
        public byte[] GetImageStreamAsBytes(Stream input)
        {
            var buffer = new byte[16 * 1024];
             MemoryStream ms = new MemoryStream();
             int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
        }

        //download image to local storage
            public String DownloadImage(string name ,byte[] t)
             {
                 //path to store img
                 string folderPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Images");
                 //name for image 
                 string filePath = System.IO.Path.Combine(folderPath, $"{name}.jpg");
                //create the directory and write the bytes
                  Directory.CreateDirectory(folderPath);
                  File.WriteAllBytes(filePath, t );

                   return filePath;
            }


    }


}


