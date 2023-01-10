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
    public partial class SearchPage : ContentPage
    {
        List<Restaurant> restaurants;
        User User;
        public SearchPage()
        {
            InitializeComponent();
            InitRestaurant();
        }
        public SearchPage(User user)
        {
            InitializeComponent();
            InitRestaurant();
            User=user;
        }
        async void InitRestaurant()
        {
            restaurants = new List<Restaurant>();
            HttpClient httpClient = new HttpClient();
            var ResList = await httpClient.GetStringAsync("http://appfood.somee.com/api/RestaurantController/GetNhaHang");
            var ResListCV = JsonConvert.DeserializeObject<List<Restaurant>>(ResList);
            
            restaurants = ResListCV;
            ListRestaurant.ItemsSource = restaurants;
        }
        public static string RemoveUnicode(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
                        "đ",
                        "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
                        "í","ì","ỉ","ĩ","ị",
                        "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
                        "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
                        "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
                        "d",
                        "e","e","e","e","e","e","e","e","e","e","e",
                        "i","i","i","i","i",
                        "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
                        "u","u","u","u","u","u","u","u","u","u","u",
                        "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }
        private void SearchRestaurant_TextChanged(object sender, TextChangedEventArgs e)
        {
            var result = restaurants.Where(c => RemoveUnicode(c.TEN.ToLower()).Contains(RemoveUnicode(SearchRestaurant.Text.ToLower())) || RemoveUnicode(c.DIADIEM.ToLower()).Contains(RemoveUnicode(SearchRestaurant.Text.ToLower())) || RemoveUnicode(c.LOAI.ToLower()).Contains(RemoveUnicode(SearchRestaurant.Text.ToLower())));
            if (result.Count() == 0)
            {
                NotFound.IsVisible = true;
                ListRestaurant.IsVisible = false;
            }
            else
            {
                NotFound.IsVisible = false;
                ListRestaurant.IsVisible = true;
                ListRestaurant.ItemsSource = result;
            }
        }

        private void ListRestaurant_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Restaurant slRes = (Restaurant)ListRestaurant.SelectedItem;
            Navigation.PushAsync(new RestaurantPage(slRes,User), false);
        }
    }
}