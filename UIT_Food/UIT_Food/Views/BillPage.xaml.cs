using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.Net.WebRequestMethods;

namespace UIT_Food.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BillPage : ContentPage
    {
        User User;

        public List<Cart> Cart { get; }

        List<Cart> Carts = new List<Cart>();
        float Ship;
        public BillPage(List<Cart> cart, User user)
        {
            InitializeComponent();
            User = user;
            Cart = cart;
            BillInit(cart, user);
        }
        private static double DegreesToRadians(double degrees) //ham doi do sang rad
        {
            return degrees * Math.PI / 180.0;
        }

        public static double CalculateDistance(Location location1, Location location2) //ham tinh khoang cach 2 toa do gps
        {
            double circumference = 40000.0; // Earth's circumference at the equator in km
            double distance = 0.0;

            double latitude1Rad = DegreesToRadians(location1.Latitude);
            double longitude1Rad = DegreesToRadians(location1.Longitude);
            double latititude2Rad = DegreesToRadians(location2.Latitude);
            double longitude2Rad = DegreesToRadians(location2.Longitude);
            double logitudeDiff = Math.Abs(longitude1Rad - longitude2Rad);

            if (logitudeDiff > Math.PI)
            {
                logitudeDiff = 2.0 * Math.PI - logitudeDiff;
            }

            double angleCalculation =
                Math.Acos(
                  Math.Sin(latititude2Rad) * Math.Sin(latitude1Rad) +
                  Math.Cos(latititude2Rad) * Math.Cos(latitude1Rad) * Math.Cos(logitudeDiff));

            distance = circumference * angleCalculation / (2.0 * Math.PI);

            distance = Math.Round(distance, 1);
            return distance;
        }
        async void BillInit(List<Cart> cart, User user)
        {
            LstBill.HeightRequest = (50 * Carts.Count);
            LstBill.ItemsSource = cart;

            //khoi tao cho UI
            USname.Text = "Người nhận: " + user.HOTEN;
            USSDT.Text = "SĐT: " + user.SDT;
            Time.Text = DateTime.Now.ToString();

            //Tinh tong gia tien
            float tien = 0;
            foreach (Cart x in cart)
            {
                tien = tien + x.TONGGIA;
            }
            Money.Text = tien.ToString();
            Ship = 25000;
            Total.Text = (tien + Ship).ToString();

            //lay vitri hien tai
            var location = await Geolocation.GetLastKnownLocationAsync();
            var location2 = await Geolocation.GetLastKnownLocationAsync();

            if (location == null)
            {
                location = await Geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(30)
                });
            }
            if (location == null)
                MyLocation.Text = "No GPS";
            else
                MyLocation.Text = $"{location.Latitude} {location.Longitude}";

            var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
            var placemark = placemarks?.FirstOrDefault();

            var Provine = placemark.AdminArea;
            var District = placemark.SubAdminArea;
            var SubAdd1 = placemark.FeatureName;
            var SubAdd2 = placemark.Thoroughfare;
            var tempaddress = SubAdd2 + ", " + SubAdd1 + ", " + District + ", " + Provine;
            MyLocation.Text = tempaddress;

        }

        private void BtnLater_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync(true);
        }

        private async void BtnOrder_Clicked(object sender, EventArgs e)
        {
            DateTime time1 = DateTime.Now;
            DateTime time2 = time1.AddMinutes(15);//15 phút sau
            HttpClient httpClient = new HttpClient();


            Bill bill = new Bill 
            { 
                MAND = User.MAND, 
                TGDAT = time1, 
                TGGIAO = time2, 
                TONGTIEN = float.Parse(Total.Text)
            };

            string jsonEnteredBill = JsonConvert.SerializeObject(bill);
            StringContent httpContent = new StringContent(jsonEnteredBill, Encoding.UTF8, "application/json");
            //goi api them vao hoa don
            var result = await httpClient.PostAsync("http://appfood.somee.com/api/BillController/InsertHoaDon", httpContent);
            var stringBill = await result.Content.ReadAsStringAsync();
            stringBill = stringBill.Replace("[", string.Empty);
            stringBill = stringBill.Replace("]", string.Empty);
            var BillCV = JsonConvert.DeserializeObject<int>(stringBill);

            //them vao trang chi tiet hoa don
            foreach (Cart x in Carts)
            {
                BillDetail billDetail = new BillDetail
                {
                    MAHD = BillCV,
                    MAMA = x.MAMA,
                    SHIP = 25000,
                    SOLUONG = x.SOLUONG,
                };
                string jsonEnteredBillDetail = JsonConvert.SerializeObject(billDetail);
                StringContent httpContent2 = new StringContent(jsonEnteredBillDetail, Encoding.UTF8, "application/json");
                var aaa = await httpClient.PostAsync("http://appfood.somee.com/api/AppFoodController/InsertCTHoaDon",httpContent2);
            }

            await DisplayAlert("Thông báo", "Đặt hàng thành công\nThời gian giao hàng dự kiến: " + time2.ToString() + "\nMã Hóa đơn: " + BillCV, "OK");
            await Navigation.PopAsync(true);
            await Navigation.PushAsync(new MainPage(User));

        }
    }
}