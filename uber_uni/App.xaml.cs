using System;
using System.IO;
using Xamarin.Forms;
using uber_uni.views;
using Xamarin.Essentials;
using uber_uni.views_bottomNav;
using GooglePlacesApi;
using GooglePlacesApi.Models;
using GoogleApi;
using Xamarin.Forms.PlatformConfiguration;
using System.Linq;
using uber_uni.helpers.models;
using System.Reflection;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace uber_uni
{
    public partial class App : Application
    {


        static Database database;
        static firebaseHelper firebase;
        public static String meterialIconsFamily;
        public static string User = "Rendy";
        public static GooglePlacesApiService googlePlacesService;
        public static double ScreenHeight { get; set; }
        public static double ScreenWidth { get; set; }
        GoogleApiSettings googleSettings = GoogleApiSettings.Builder
                            .WithApiKey(Keys.ApiKey)
                            .WithType(PlaceTypes.Address)
        .WithDetailLevel(DetailLevel.Basic)
                            .WithOrigin("lat,lon")
        .WithLocation("lat,lon")
        .Build();

        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notes.db3"));
                }
                return database;
            }
        }


        public static firebaseHelper FireBase
        {
            get
            {
                if (firebase == null)
                {
                    firebase = new firebaseHelper();
                }
                return firebase;
            }
        }


        public App()
        {
            googlePlacesService = new GooglePlacesApiService(GoogleApiSettings.Builder
                            .WithApiKey(Keys.ApiKey)
                            .WithType(PlaceTypes.Address)
        .WithDetailLevel(DetailLevel.Basic)
                            .WithOrigin("lat,lon")
        .WithLocation("lat,lon")
        .Build());
            ScreenHeight = DeviceDisplay.MainDisplayInfo.Height/2;
            ScreenWidth = DeviceDisplay.MainDisplayInfo.Width/2;
            InitializeComponent();
            var OnPlatformDic = (OnPlatform<string>)App.Current.Resources["MaterialIconFont"];

            meterialIconsFamily = OnPlatformDic.Platforms.FirstOrDefault((arg) => arg.Platform.FirstOrDefault() == Device.RuntimePlatform).Value.ToString();
            Sharpnado.HorizontalListView.Initializer.Initialize(true, false);
            MainPage = new mainPagView();
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }

    public static class Keys
    {
        public static string ApiKey = "AIzaSyD0WqCwh8vVTlBGNXz3YC55-fEjewHu3ws";
    }



}


