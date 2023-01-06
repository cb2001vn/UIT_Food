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
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void btnRegister_Clicked(object sender, EventArgs e)
        {
            if (password.Text != password2.Text)
            {
                await DisplayAlert("Thông báo", "Tạo người dùng thất bại! Mật khẩu nhập lại không đúng", "OK");
                return;
            }
            User newUser = new User
            {
                USERNAME = username.Text,
                PASS = password.Text,
                HOTEN = name.Text,
                SDT = phone_number.Text,
                EMAIL = email.Text,
                NGAYSINH = DateOfBirth.Date
            };
            HttpClient http = new HttpClient();
            string jsonEnteredUser = JsonConvert.SerializeObject(newUser);
            StringContent httpContent = new StringContent(jsonEnteredUser, Encoding.UTF8, "application/json");
            HttpResponseMessage result = await http.PostAsync("http://appfood.somee.com/api/UserController/CreateUser", httpContent);
            var code = await result.Content.ReadAsStringAsync();

            if (code == "0")
            {
                await DisplayAlert("Thông báo", "tên đăng nhập đã tồn tại.", "OK");
            }
            else if (Int32.Parse(code) > 0)
            {
                await DisplayAlert("Thông báo", "Tạo tài khoản mới thành công.", "OK");
                await Navigation.PopAsync();
                await Navigation.PushAsync(new LoginPage(), false);
            }
            else
            {
                await DisplayAlert("Thông báo", "Tạo người dùng thất bại! Xin mời bạn tạo lại!", "OK");
            }
        }
    }
}