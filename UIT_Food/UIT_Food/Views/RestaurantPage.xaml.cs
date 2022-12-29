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
    public partial class RestaurantPage : ContentPage
    {
        public RestaurantPage(Restaurant restaurant)
        {
            InitializeComponent();
            InforInit(restaurant);
        }
        async void InforInit(Restaurant restaurant)
        {
            ResName.Text = restaurant.TEN;
            ResImg.Source = restaurant.IMG;
            ResPlace.Text = restaurant.DIADIEM;
            string maNH = restaurant.MANH;
            HttpClient httpClient = new HttpClient();
            var FoodList = await httpClient.GetStringAsync("http://appfood.somee.com/api/FoodController/GetMonAnNhaHang?manh=" + maNH);
            var FoodListCV = JsonConvert.DeserializeObject<List<Food>>(FoodList);
            FoodLst.ItemsSource = FoodListCV;
        }
    }
}