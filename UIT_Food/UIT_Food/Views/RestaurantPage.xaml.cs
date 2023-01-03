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
        User User;
        List<Cart> carts;
        public RestaurantPage(Restaurant restaurant, User user)
        {
            InitializeComponent();
            InforInit(restaurant);
            User = user;
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
            var CartList = await httpClient.GetStringAsync("http://appfood.somee.com/api/CartController/GetCartFood?mand=" + User.MAND.ToString());
            var CartListCV = JsonConvert.DeserializeObject<List<Cart>>(CartList);
            carts = CartListCV;
            FoodLst.ItemsSource = FoodListCV;
        }

        private void Bill_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BillPage(carts,User));
        }

        private void Basket_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ProductPage(User));
        }

        private async void BtnPlus_Clicked(object sender, EventArgs e)
        {
            ImageButton button = (ImageButton)sender;
            int MAMA = int.Parse(button.CommandParameter.ToString());
            User_Food user_food = new User_Food { MAND = User.MAND, MAMA = MAMA };
            HttpClient http = new HttpClient();
            string jsonUserFood = JsonConvert.SerializeObject(user_food);
            StringContent httpContent = new StringContent(jsonUserFood, Encoding.UTF8, "application/json");
            HttpResponseMessage result = await http.PostAsync("http://appfood.somee.com/api/CartController/InsertGioHang", httpContent);
            var code = await result.Content.ReadAsStringAsync();

            if (Int32.Parse(code) > 0)
            {
                await DisplayAlert("Thông báo", "Thêm món ăn vào giỏ hàng thành công", "OK");
            }
            else
            {
                await DisplayAlert("Thông báo", "Có lỗi xảy ra, thêm món ăn KHÔNG thành công", "OK");
            }
        }
    }
}