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
            //User newUser = new User();
            //string Usrname = username.Text;
            //string Pass = password.Text;
            //string Name = name.Text;
            //string sdt = phone_number.Text;
            //string Email = email.Text;
            //string DoB = DateOfBirth.Date.ToString();

            //HttpClient httpClient = new HttpClient();
            //var code =  await httpClient.GetStringAsync("http://appfood.somee.com/api/AppFoodController/CreateUser?username=" + Usrname.ToString() + "&pw=" + Pass.ToString() + "&name=" + Name.ToString() + "&SDT="+ sdt + "&EMAIL=" + Email.ToString()+ "&NGAYSINH="+ DoB);
            //code = code.ToString();
            //code=code.Replace("[{\"Code\":", string.Empty);
            //code = code.Replace("}]", string.Empty);
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
            HttpResponseMessage result = await http.PostAsync("http://192.168.202.11/webapi/api/UserController/CreateUser", httpContent);
            var code = await result.Content.ReadAsStringAsync();

            if (code == "0")
            {
                await DisplayAlert("Thông báo", "Username đã tồn tại.", "OK");
            }
            else if (Int32.Parse(code) > 0)
            {
                await DisplayAlert("Thông báo", "Tạo người dùng thành công.", "OK");
                await App.Current.MainPage.Navigation.PushAsync(new LoginPage(), false);
            }
            else
            {
                await DisplayAlert("Thông báo", "Tạo người dùng thất bại! Xin mời bạn tạo lại!", "OK");
            }
        }
    }
}