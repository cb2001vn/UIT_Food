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
            this.Children.Add(new SearchPage() { IconImageSource = "list.png" });
            this.Children.Add(new ProductPage() { IconImageSource = "basket.png" });
            this.Children.Add(new PersonalPage() { IconImageSource = "user.png" });
        }


        public MainPage(User user)
        {
            InitializeComponent();
            this.BindingContext = this;
            this.Children.Add(new HomePage(user) { IconImageSource = "home.png" });
            this.Children.Add(new SearchPage(user) { IconImageSource = "list.png" });
            this.Children.Add(new ProductPage(user) { IconImageSource = "basket.png" });
            this.Children.Add(new PersonalPage(user) { IconImageSource = "user.png" });

        }
    }
}