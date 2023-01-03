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
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage(User user)
        {
            InitializeComponent();
            ListHistoryInit(user);
        }
        async void ListHistoryInit(User user)
        {
            HttpClient httpClient = new HttpClient();
            var HistoryList = await httpClient.GetStringAsync("http://appfood.somee.com/api/AppFoodController/GetHoaDonByUser?mand=" + user.MAND.ToString());
            var HistoryListCV = JsonConvert.DeserializeObject<List<Receipt>>(HistoryList);

            LstHistory.ItemsSource = HistoryListCV;
        }
    }
}