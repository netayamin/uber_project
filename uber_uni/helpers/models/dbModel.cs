using System;
using SQLite;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;

namespace uber_uni
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string full_name { get; set; }
        public int phone { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string profile_pic { get; set; }
        public DateTime Date { get; set; }
    }

    public  class Compnaion
    {
        public string first_name { get; set; } = "Emily";
        public string second_name { get; set; } = "Smith";
        public string username { get; set; } = "Emily123";
        public string image_path { get; set; } 
        public Boolean isLastRow { get; set; } 
    }

    public class Trip
    {
        public DateTime date { get; set; }
        public DateTime pickUpTime { get; set; }
        public double pickup_longt { get; set; }
        public double pickup_lat { get; set; }
        public double drop_off_longt { get; set; }
        public double drop_off_lat { get; set; }
        public string user_id { get; set; }
        public string duration { get; set; }
        public string price { get; set; }
        public string car { get; set; }
        public string driver_name { get; set; }
    }

    public class Trip_Position
    {
        public Position position { get; set; }
        public string date { get; set; } 
        public DateTime pickUpTime { get; set; }
        public string user_id { get; set; }
        public string duration { get; set; } 
        public string price { get; set; } 
        public string car { get; set; } 
        public string driver_name { get; set; }
        public int Index { get;  set; }
    }
}
