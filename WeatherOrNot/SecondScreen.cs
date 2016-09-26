using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace WeatherOrNot
{
    [Activity(Label = "SecondScreen")]
    public class SecondScreen : Activity
    {
        //private List<string> mItems;
        //private ListView mListView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.second_layout);
            double currLongitude = Convert.ToDouble(Intent.GetStringExtra("longitude"));
            double currLatitude = Convert.ToDouble(Intent.GetStringExtra("latitude"));

            string key = getLocationKey(currLongitude, currLatitude);
            var listForecast = getForecast("10day", key);



            TextView button1 = (TextView)FindViewById(Resource.Id.btnDay1);
            string time = DateTime.Now.ToString();
            button1.Text = time + "\n" + listForecast[0]["maxVal"] + "\n" + listForecast[0]["descriptionString"];
            ImageView img1 = (ImageView)FindViewById(Resource.Id.imgDay1);
            img1.SetImageResource(2130837504 + Convert.ToInt16(listForecast[0]["iconVal"]));

            TextView button2 = (TextView)FindViewById(Resource.Id.btnDay2);
            time = DateTime.Now.AddDays(1).ToString();
            button2.Text = time + "\n" + listForecast[1]["maxVal"] + "\n" + listForecast[1]["descriptionString"];
            ImageView img2 = (ImageView)FindViewById(Resource.Id.imgDay2);
            img1.SetImageResource(2130837504 + Convert.ToInt16(listForecast[0]["iconVal"]));


            TextView button3 = (TextView)FindViewById(Resource.Id.btnDay3);
            time = DateTime.Now.AddDays(2).ToString();
            button3.Text = time + "\n" + listForecast[2]["maxVal"] + "\n" + listForecast[2]["descriptionString"];
            ImageView img3 = (ImageView)FindViewById(Resource.Id.imgDay3);
            img1.SetImageResource(2130837504 + Convert.ToInt16(listForecast[0]["iconVal"]));

            TextView button4 = (TextView)FindViewById(Resource.Id.btnDay4);
            time = DateTime.Now.AddDays(3).ToString();
            button4.Text = time + "\n" + listForecast[3]["maxVal"] + "\n" + listForecast[3]["descriptionString"];
            ImageView img4 = (ImageView)FindViewById(Resource.Id.imgDay4);
            img1.SetImageResource(2130837504 + Convert.ToInt16(listForecast[0]["iconVal"]));

            TextView button5 = (TextView)FindViewById(Resource.Id.btnDay5);
            time = DateTime.Now.AddDays(4).ToString();
            button5.Text = time + "\n" + listForecast[4]["maxVal"] + "\n" + listForecast[4]["descriptionString"];
            ImageView img5 = (ImageView)FindViewById(Resource.Id.imgDay5);
            img1.SetImageResource(2130837504 + Convert.ToInt16(listForecast[0]["iconVal"]));

            TextView button6 = (TextView)FindViewById(Resource.Id.btnDay6);
            time = DateTime.Now.AddDays(5).ToString();
            button6.Text = time + "\n" + listForecast[5]["maxVal"] + "\n" + listForecast[5]["descriptionString"];
            ImageView img6 = (ImageView)FindViewById(Resource.Id.imgDay6);
            img1.SetImageResource(2130837504 + Convert.ToInt16(listForecast[0]["iconVal"]));

            TextView button7 = (TextView)FindViewById(Resource.Id.btnDay7);
            time = DateTime.Now.AddDays(6).ToString();
            button7.Text = time + "\n" + listForecast[6]["maxVal"] + "\n" + listForecast[6]["descriptionString"];
            ImageView img7 = (ImageView)FindViewById(Resource.Id.imgDay7);
            img1.SetImageResource(2130837504 + Convert.ToInt16(listForecast[0]["iconVal"]));
        }

        public string getLocationKey(double longitude, double latitude)
        {
            string apiKey = "HackuWeather2016";
            string url = "http://apidev.accuweather.com/locations/v1/cities/geoposition/search.json?q=" + latitude.ToString() + "," + longitude.ToString() + "&apikey=" + apiKey;

            using (var webClient = new System.Net.WebClient())
            {
                var json = webClient.DownloadString(url);
                var currLocation = Newtonsoft.Json.Linq.JObject.Parse(json);

                return (string)currLocation["Key"];//account for null case!?!??!
            }
        }

        public List<Dictionary<string, string>> getForecast(string timeString, string locationKey)
        {
            string apiKey = "HackuWeather2016";
            string url = "http://apidev.accuweather.com/forecasts/v1/daily/" + timeString + "/" + locationKey + "?apikey=" + apiKey;

            using (var webClient = new System.Net.WebClient())
            {
                var json = webClient.DownloadString(url);
                var locForecast = Newtonsoft.Json.Linq.JObject.Parse(json);

                var i = 0;
                var listForecast = new List<Dictionary<string, string>>();

                foreach (var item in locForecast["DailyForecasts"])
                {
                    Dictionary<string, string> dict = new Dictionary<string, string>();

                    dict.Add("minVal", (string)locForecast["DailyForecasts"][i]["Temperature"]["Minimum"]["Value"]);
                    dict.Add("maxVal", (string)locForecast["DailyForecasts"][i]["Temperature"]["Maximum"]["Value"]);
                    dict.Add("iconVal", (string)locForecast["DailyForecasts"][i]["Day"]["Icon"]);
                    dict.Add("descriptionString", (string)locForecast["DailyForecasts"][i]["Day"]["IconPhrase"]);
                    listForecast.Add(dict);
                    i++;
                }
                return listForecast;
            }
        }
    }

    

}