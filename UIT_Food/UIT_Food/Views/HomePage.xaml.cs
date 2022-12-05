using Newtonsoft.Json;
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
        public HomePage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            InitRestaurant();
        }

        async void InitRestaurant()
        {
            restaurants = new List<Restaurant>();
            HttpClient httpClient = new HttpClient();
            var ResList = await httpClient.GetStringAsync("http://192.168.1.148/webapi/api/RestaurantController/GetNhaHang");
            var RestListCV = JsonConvert.DeserializeObject<List<Restaurant>>(ResList);
            restaurants = RestListCV;
 

            Name1.Text = restaurants[5].TEN;
            Name2.Text = restaurants[6].TEN;
            Name3.Text = restaurants[7].TEN;
            Name4.Text = restaurants[8].TEN;
            Name5.Text = restaurants[9].TEN;

        }

        private async void GetLocation_Tapped(object sender, EventArgs e)
        {
            try
            {
                var location = await Geolocation.GetLocationAsync();
                if (location == null)
                {
                    location = await Geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Medium,
                        Timeout = TimeSpan.FromSeconds(30)
                    });
                }
                if (location == null)
                    LabelLocation.Text = "No GPS";
                else
                {
                    var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
                    var placemark = placemarks?.FirstOrDefault();
                    var Provine = placemark.AdminArea;
                    var District = placemark.SubAdminArea;
                    var SubAdd1 = placemark.FeatureName;
                    var SubAdd2 = placemark.Thoroughfare;
                    var tempaddress = SubAdd2 + ", " + SubAdd1 + ", " + District + ", " + Provine;
                    LabelLocation.Text = tempaddress;
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Error !!!", "Something's wrong", "Try again!");
            }
        }
    }
}