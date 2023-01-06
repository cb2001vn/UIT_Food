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
    public partial class ProductPage : ContentPage
    {
        List<Cart> carts = new List<Cart>();
        User User;
        public ProductPage()
        {
            InitializeComponent();
        }
        public ProductPage(User user)
        {
            InitializeComponent();
            ListCartInit(user);
            User = user;
        }
        async void ListCartInit(User user)
        {
            if (user != null)
            {
                HttpClient httpClient = new HttpClient();
                var CartList = await httpClient.GetStringAsync("http://appfood.somee.com/api/CartController/GetCartFood?mand=" + user.MAND.ToString());
                var CartListCV = JsonConvert.DeserializeObject<List<Cart>>(CartList);
                carts = CartListCV;
                if (carts.Count == 0)
                    btnThanhToan.IsVisible = false;
                float tien = 0;
                float TongTien;
                foreach (Cart x in carts)
                {
                    tien = tien + x.TONGGIA;
                };
                TongTien = tien;
                LbTongTien.Text = TongTien.ToString();
                LstCart.HeightRequest = (100 * carts.Count);
                LstCart.ItemsSource = carts;
            }
        }

        private void btnThanhToan_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync(true);
            Navigation.PushAsync(new BillPage(carts,User));
        }

        private void BtnRefresh_Clicked(object sender, EventArgs e)
        {
            ListCartInit(User);
        }
        private async void stepper_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int MAGH = int.Parse(button.CommandParameter.ToString());
            string text = button.Text.ToString();
            if (text == "+")
            {

                foreach (Cart x in carts)
                {
                    if (x.MAGH == MAGH)
                    {

                        User_Food user_food = new User_Food { MAND = User.MAND, MAMA = x.MAMA };
                        HttpClient http = new HttpClient();
                        string jsonUserFood = JsonConvert.SerializeObject(user_food);
                        StringContent httpContent = new StringContent(jsonUserFood, Encoding.UTF8, "application/json");
                        HttpResponseMessage result = await http.PostAsync("http://appfood.somee.com/api/CartController/InsertGioHang", httpContent);
                        var code = await result.Content.ReadAsStringAsync();

                        if (Int32.Parse(code) > 0)
                        {
                            ListCartInit(User);
                        }
                        else
                        {
                            await DisplayAlert("Thông báo", "Có lỗi xảy ra, thêm món ăn KHÔNG thành công", "OK");
                        }
                        break;
                    }
                }
            }
            else
            {
                foreach (Cart x in carts)
                {
                    if (x.MAGH == MAGH)
                    {
                        if (x.SOLUONG > 0)
                        {

                            User_Food user_food = new User_Food { MAND = User.MAND, MAMA = x.MAMA };
                            HttpClient http = new HttpClient();
                            string jsonUserFood = JsonConvert.SerializeObject(user_food);
                            StringContent httpContent = new StringContent(jsonUserFood, Encoding.UTF8, "application/json");
                            HttpResponseMessage result = await http.PostAsync("http://appfood.somee.com/api/CartController/DeleteGioHang", httpContent);
                            var code = await result.Content.ReadAsStringAsync();
                            if (Int32.Parse(code) > 0)
                            {
                                //await DisplayAlert("Thông báo", "Thêm món ăn vào giỏ hàng thành công", "OK");
                                ListCartInit(User);
                            }
                            else if (Int32.Parse(code) == 0)
                            {
                                await DisplayAlert("Thông báo", "Xóa sản phẩm thành công", "OK");
                                ListCartInit(User);
                            }
                            else
                            {
                                await DisplayAlert("Thông báo", "Có lỗi xảy ra", "OK");
                            }
                            break;
                        }

                    }
                }
            }
            ListCartInit(User);
        }
    }
}