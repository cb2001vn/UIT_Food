using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UIT_Food.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonalPage : ContentPage
    {
        public User Globaluser;
        public PersonalPage()
        {
            InitializeComponent();
        }
        public PersonalPage(User user)
        {
            Globaluser = user;
            InitializeComponent();
            PersonInit(Globaluser);

        }

        void PersonInit(User user)
        {
            if (user.SDT == null)
            {
                if (user.EMAIL == null)
                {
                    lblName.Text = user.HOTEN;
                }
                else
                {
                    lblName.Text = user.HOTEN;
                    lblEmail.Text = user.EMAIL;
                }
            }
            else
            {
                if (user.EMAIL == null)
                {
                    lblName.Text = user.HOTEN;
                    lblName.Text = user.SDT;
                }
                else
                {
                    lblName.Text = user.HOTEN;
                    lblPhone.Text = user.SDT;
                    lblEmail.Text = user.EMAIL;
                }
            }
        }

        private void History_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HistoryPage(Globaluser),true);
        }

        private void ChangeInfo_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UpdatePage(Globaluser), true);
        }

        private void BtnLogout_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
            Navigation.PushAsync(new LoginPage(),true);
        }
    }
}