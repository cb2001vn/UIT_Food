using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UIT_Food.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        List<Restaurant> restaurants;
        public SearchPage()
        {
            InitializeComponent();
            InitRestaurant();
        }
        public SearchPage(User user)
        {
            InitializeComponent();
            InitRestaurant();
        }
        async void InitRestaurant()
        {
            restaurants = new List<Restaurant>();
            HttpClient httpClient = new HttpClient();
            var ResList = await httpClient.GetStringAsync("http://appfood.somee.com/api/RestaurantController/GetNhaHang");
            var ResListCV = JsonConvert.DeserializeObject<List<Restaurant>>(ResList);
            
            restaurants = ResListCV;
            ListRestaurant.ItemsSource = restaurants;
        }

        private void SearchRestaurant_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ListRestaurant_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}