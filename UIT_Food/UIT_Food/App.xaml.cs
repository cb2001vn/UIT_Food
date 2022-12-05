using System;
using UIT_Food.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("UTMAvo.ttf", Alias = "UTMAvo")]
[assembly: ExportFont("UTMAvoBold.ttf", Alias = "UTMAvoBold")]
namespace UIT_Food
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage())
            {
                BarBackgroundColor = Color.FromRgb(107, 78, 60)
            };
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
