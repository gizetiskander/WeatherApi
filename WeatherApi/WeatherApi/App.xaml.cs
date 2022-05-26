using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WeatherApi.Views;

namespace WeatherApi
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new WeatheApiPage());
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
