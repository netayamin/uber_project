using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using GooglePlacesApi;
using GooglePlacesApi.Models;
using Xamarin.Forms;

namespace uber_uni.view_assets
{
    public class AutoCompleteEntry : ContentView
    {
        ObservableCollection<string> list = new ObservableCollection<string>();
        public List<Prediction> predictions = new List<Prediction>();
        public Entry entry;
        public ListView AutoCompleteList = new ListView { HeightRequest = 200, IsVisible = false };
        static readonly CancellationTokenSource s_cts = new CancellationTokenSource();

        public AutoCompleteEntry(string title)
        {

            HorizontalOptions = LayoutOptions.FillAndExpand;
            entry = new Entry { Placeholder = title, HeightRequest = 50 };
            AutoCompleteList.ItemsSource = list;
            entry.TextChanged += PickupEntry_TextChangedAsync;

            Content = new StackLayout
            {
                Children =
                {
                    entry,
                    AutoCompleteList
                }
            };
        }


        public async void PickupEntry_TextChangedAsync(object sender, TextChangedEventArgs e)
        {
            s_cts.Cancel();
            list.Clear();
            AutoCompleteList.IsVisible = true;
            AutoCompleteList.Focus();

            if ( e.NewTextValue.Length > 1 )
            {
                var result = await App.googlePlacesService.GetPredictionsAsync(e.NewTextValue);
                if (result.Items != null)
                {
                    predictions = result.Items;
                    foreach (var res in result.Items.ToArray())
                    {
                        list.Add(res.Description);
                    }
                }
            }

            else
            {
                AutoCompleteList.IsVisible = false;
            }
        }


    }
}

