﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UIT_Food.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        List<Restaurant> restaurants;
        User HomeUser;
        public HomePage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            InitRestaurant();
        }
        public HomePage(User user)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            InitRestaurant();
            HomeUser = user;
        }

        async void InitRestaurant()
        {
            restaurants = new List<Restaurant>();
            HttpClient httpClient = new HttpClient();
            var ResList = await httpClient.GetStringAsync("http://appfood.somee.com/api/RestaurantController/GetNhaHang");
            var RestListCV = JsonConvert.DeserializeObject<List<Restaurant>>(ResList);
            restaurants = RestListCV;

            Name1.Text = restaurants[5].TEN;
            Name2.Text = restaurants[6].TEN;
            Name3.Text = restaurants[7].TEN;
            Name4.Text = restaurants[8].TEN;
            Name5.Text = restaurants[9].TEN;

            Picture1.Source = restaurants[5].IMG;
            Picture2.Source = restaurants[6].IMG;
            Picture3.Source = restaurants[7].IMG;
            Picture4.Source = restaurants[8].IMG;
            Picture5.Source = restaurants[9].IMG;

        }

        private async void GetLocation_Tapped(object sender, EventArgs e)
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location == null)
                {
                    location = await Geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Medium,
                        Timeout = TimeSpan.FromSeconds(30)
                    });
                }
                Console.WriteLine("Kết quả: " + location);
                if (location == null)
                    LabelLocation.Text = "No GPS";
                else
                {
                    var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
                    var placemark = placemarks?.FirstOrDefault();
                    var City = placemark.AdminArea;
                    var District = placemark.SubAdminArea;
                    var Number = placemark.FeatureName;
                    var Street = placemark.Thoroughfare;
                    Console.WriteLine("Kết quả: " + Street);
                    var tempaddress = Number  + ", " + Street + ", " + District + ", " + City;
                    
                    LabelLocation.Text = tempaddress;
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Error !!!", "Something's wrong", "Try again!");
            }
        }

        private void Restaurant1_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RestaurantPage(restaurants[5],HomeUser));
        }

        private void Restaurant2_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RestaurantPage(restaurants[6], HomeUser));
        }

        private void Restaurant3_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RestaurantPage(restaurants[7], HomeUser));
        }

        private void Restaurant4_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RestaurantPage(restaurants[8], HomeUser));
        }

        private void Restaurant5_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RestaurantPage(restaurants[9], HomeUser));
        }
    }
}