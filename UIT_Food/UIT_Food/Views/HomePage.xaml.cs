using System;
using System.Collections.Generic;
using System.Linq;
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
        public HomePage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
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