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
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = this;
            this.Children.Add(new HomePage() { IconImageSource = "home.png"});
            this.Children.Add(new SearchPage() { IconImageSource = "search.png" });
            this.Children.Add(new ProductPage() { IconImageSource = "list.png" });
            this.Children.Add(new PersonalPage() { IconImageSource = "user.png" });
            this.Children.Add(new RestaurantPage() { IconImageSource = "home.png" });
        }
    }
}