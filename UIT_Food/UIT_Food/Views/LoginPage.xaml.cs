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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void BtnRegister_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            //User GlobalUser;
            //string Usrname = Username.Text;
            //string Pw = Pass.Text;

            ////goi api so sanh nguoiw dung hop le   
            ////var user = await httpclient.getstringasync("http://appfood.somee.com/api/appfoodcontroller/checkoneuser?username=" + usrname + "&pw=" + pw);
            ////user = user.replace("[", string.empty);
            ////user = user.replace("]", string.empty); // chuyen array thanh object

            //User enteredUser = new User { USERNAME = Username.Text, PASS = Pass.Text };
            //HttpClient http = new HttpClient();
            //string jsonEnteredUser = JsonConvert.SerializeObject(enteredUser);
            //StringContent httpContent = new StringContent(jsonEnteredUser, Encoding.UTF8, "application/json");
            //HttpResponseMessage result = await http.PostAsync("http://172.30.8.50/AppFoodApi/api/UserController/CheckOneUser", httpContent);
            //var user = await result.Content.ReadAsStringAsync();

            ////Kiem tra ket qua api
            //if (user == "")
            //{
            //    await DisplayAlert("Đăng nhập thất bại", "Mật khẩu hoặc tên người dùng không đúng", "OK");
            //}
            //else //neu dung truyen bien user qua mainpage
            //{
            //    user = user.Replace("[", string.Empty);
            //    user = user.Replace("]", string.Empty); // chuyen array thanh object
            //    var UserCV = JsonConvert.DeserializeObject<User>(user);
            //    GlobalUser = UserCV;
            //    await DisplayAlert("Đăng nhập thành công", "", "OK");
            //    await Navigation.PushAsync(new MainPage());
            //}
            await Navigation.PushAsync(new MainPage());
        }
    }
}