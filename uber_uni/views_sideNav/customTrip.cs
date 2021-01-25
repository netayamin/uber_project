using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using uber_uni.helpers.models;
using uber_uni.view_assets;
using uber_uni.views;
using uber_uni.views_bottomNav.uberGuardViews;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace uber_uni.views_bottomNav
{
    public class customTrip : ContentPage
    {
        Button defaultBtn = new Button { BackgroundColor = Color.Black, Text = "Default", TextColor = Color.White};
        Button customBtn = new Button { BackgroundColor = Color.LightGray, Text = "Custom", TextColor = Color.White,};
        Label connectDeviceText = new Label { Text = "Connect with your device", HorizontalOptions = LayoutOptions.FillAndExpand };
        Switch connectDeviceSwitch = new Switch { HorizontalOptions = LayoutOptions.End };
        Picker languagePicker = new Picker { HorizontalOptions = LayoutOptions.FillAndExpand };
        Slider tempSlider = new Slider { Value = 16, Maximum = 27, Minimum = 16, HorizontalOptions = LayoutOptions.FillAndExpand };
        Label tempValueLabel = new Label {  };
        Label quietRideText = new Label { Text = "Enable driver talking" };
        Switch quietRideSwitch = new Switch { HorizontalOptions = LayoutOptions.EndAndExpand };
        Button connectWithALocalBtn = new Button { BackgroundColor = Color.DarkGray, Padding = 10, TextColor = Color.White, HeightRequest = 50, Text = "Connect with a local" };
        Button emergancyAlertBtn = new Button { BackgroundColor = Color.Red, Padding = 10, TextColor = Color.White, HeightRequest = 50, Text = "Emergency alert" };


        private void defaultSettings()
        {

            var index = languagePicker.Items.IndexOf("English");
            languagePicker.SelectedItem = languagePicker.Items[index];
            connectDeviceSwitch.IsToggled = false;
            connectDeviceSwitch.IsEnabled = false;
            tempSlider.IsEnabled = false;
            quietRideSwitch.IsEnabled = false;
            tempSlider.Value = 18;
            quietRideSwitch.IsToggled = false;
            languagePicker.IsEnabled = false;
        }


        private void CustomBtn_Clicked(object sender, EventArgs e)
        {
            var index = languagePicker.Items.IndexOf("English");
            languagePicker.SelectedItem = languagePicker.Items[index];

            connectDeviceSwitch.IsEnabled = true;

            tempSlider.IsEnabled = true;
            languagePicker.IsEnabled = true;
            quietRideSwitch.IsEnabled = true;
            defaultBtn.BackgroundColor = Color.LightGray;
            customBtn.BackgroundColor = Color.Black;
        }

        public customTrip()
        {
            var e = Task.Run(async () => await createLanguageList());
            languagePicker.ItemsSource = e.Result;
            defaultBtn.Clicked += DefaultBtn_Clicked;
            customBtn.Clicked += CustomBtn_Clicked;
            connectWithALocalBtn.Clicked += ConnectWithALocalBtn_Clicked;
            emergancyAlertBtn.Clicked += EmergancyAlertBtn_Clicked;
            Content = new StackLayout
            {
                Children = {
                   new uberHeaderView("Uber You", this),
                   createGridView()
                }
            };

            defaultSettings();
        }

        private void EmergancyAlertBtn_Clicked(object sender, EventArgs e)
        {
            PlacePhoneCall("999");
        }

        private void ConnectWithALocalBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(page: new MainChatView());
        }


        private void DefaultBtn_Clicked(object sender, EventArgs e)
        {
            defaultBtn.BackgroundColor = Color.Black;
            customBtn.BackgroundColor = Color.LightGray;
            defaultSettings();
        }


        public void PlacePhoneCall(string number)
        {
            try
            {
                PhoneDialer.Open(number);
            }
            catch (ArgumentNullException anEx)
            {
                // Number was null or white space
            }
            catch (FeatureNotSupportedException ex)
            {
                // Phone Dialer is not supported on this device.
            }
            catch (Exception ex)
            {
                // Other error has occurred.
            }
        }

        private void TempSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            var val = e.NewValue.ToString();
            var num = Convert.ToDouble(val);
            var text = Math.Round(num);
            tempValueLabel.Text = "Tempeture: " + text.ToString() + "°C";
        }

        private void QuietRideSwitch_Toggled(object sender, ToggledEventArgs e)
        {
           if(e.Value == true )
           {
                quietRideText.Text = "Disable driver talking";
           } else
            {
                quietRideText.Text = "Enable driver talking";
            }
        }

        private View createGridView()
        {
            tempSlider.ValueChanged += TempSlider_ValueChanged;
            quietRideSwitch.Toggled += QuietRideSwitch_Toggled;

            Grid grid = new Grid
            {             
                RowDefinitions =
                {
                     new RowDefinition{ Height = new GridLength(1.5, GridUnitType.Star) },
                     new RowDefinition { },
                     new RowDefinition{ Height = new GridLength(1.5, GridUnitType.Star) },
                     new RowDefinition(),
                     new RowDefinition{ },
                     new RowDefinition{ Height = new GridLength(1.5, GridUnitType.Star) },
                     new RowDefinition{ Height = new GridLength(1.5, GridUnitType.Star) }
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition{ Width = new GridLength( 1 ,GridUnitType.Auto) },
                }, Children = {

                    { defaultBtn , 0,0},
                    { customBtn, 1, 0 },
                    { connectDeviceText, 0, 1 },
                    { connectDeviceSwitch, 1, 1 },
                    { languagePicker, 0, 2},
                    { tempSlider, 1, 3 },
                    { tempValueLabel, 0, 3 },
                    { quietRideText, 0, 4 },
                    { quietRideSwitch, 1,4 },
                    { connectWithALocalBtn, 0, 5 },
                    { emergancyAlertBtn, 0, 6 },
                }, RowSpacing = 20, Padding = 20
            };

            Grid.SetColumnSpan(languagePicker, 2);
            Grid.SetColumnSpan(connectWithALocalBtn, 2);
            Grid.SetColumnSpan(emergancyAlertBtn, 2);
            return grid;
        }

    

        private async Task<List<String>> createLanguageList()
        {
            using (var stream = await FileSystem.OpenAppPackageFileAsync("LanguageList.json"))
            {
                using (var reader = new StreamReader(stream))
                {
                    var fileContents = await reader.ReadToEndAsync();
                    LanguageModel myobject  = JsonConvert.DeserializeObject<LanguageModel>(fileContents);
                    PropertyInfo[] props = myobject.GetType().GetProperties();
                    List<String> languageList = new List<String>();
                    for (var i = 0; i < props.Length; i++ )
                    {
                        String propValue = props[i].GetValue(myobject, null).ToString();
                        languageList.Add(propValue);
                    }

                    return languageList;
                }
            }

        }
    }
}

