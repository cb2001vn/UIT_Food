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
        public PersonalPage()
        {
            InitializeComponent();
        }

        private void History_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HistoryPage(),true);
        }

        private void ChangeInfo_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UpdatePage(), true);
        }
    }
}