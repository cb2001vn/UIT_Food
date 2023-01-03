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
    public partial class UpdatePage : ContentPage
    {
        User user;
        public UpdatePage(User user)
        {
            this.user = user;
            InitializeComponent();
            InfoInit(user);
        }
        void InfoInit(User user)
        {
            InputName.Text = user.HOTEN;
            if (user.SDT == null)
            {
                InputPhone.Text = "";
            }
            else
            {
                InputPhone.Text = user.SDT;
            }

            if (user.EMAIL == null)
            {
                InputEmail.Text = "";
            }
            else
            {
                InputEmail.Text = user.EMAIL;
            }


            if (user.NGAYSINH == null)
            {
                DateTime a = new DateTime(2001, 01, 01, 0, 0, 0);
                InputDOB.Date = (DateTime)a;
            }
            else
            {
                InputDOB.Date = (DateTime)user.NGAYSINH;
            }

        }

        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            if (InputOldPass.Text == user.PASS)
            {
                string DoB = InputDOB.Date.ToString();
                DoB = DoB.Replace("SA", "AM");
                DoB = DoB.Replace("CH", "PM");
                HttpClient httpClient = new HttpClient();
                var code = await httpClient.GetStringAsync("http://appfood.somee.com/api/AppFoodController/UpdateUser?mand=" + user.MAND.ToString() + "&hoten=" + InputName.Text.ToString() + "&SDT=" + InputPhone.Text + "&EMAIL=" + InputEmail.Text.ToString() + "&NGAYSINH=" + DoB);
                code = code.ToString();
                code = code.Replace("[{\"Code\":", string.Empty);
                code = code.Replace("}]", string.Empty);
                if (code == "1")
                {
                    user.HOTEN = InputName.Text;
                    user.SDT = InputPhone.Text;
                    user.EMAIL = InputEmail.Text;
                    user.NGAYSINH = InputDOB.Date;

                    await DisplayAlert("Thông báo", "Cập nhật thành công.", "OK");
                    await App.Current.MainPage.Navigation.PushAsync(new MainPage(user), false);
                }
                else
                {
                    await DisplayAlert("Thông báo", "Tạo người dùng thất bại! Xin mời bạn tạo lại!", "OK");
                }
            }
            else
            {
                await DisplayAlert("Thông báo", "Cập nhật thất bại!\nSai mật khẩu!", "OK");
            }
        }
    }
}