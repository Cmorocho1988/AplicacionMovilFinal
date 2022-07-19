using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CristianMorocho.Views;
using Xamarin.Essentials;

namespace AplicacionMovil
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var token = Preferences.Get("token", string.Empty);

            if (token != string.Empty)
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            
            //MainPage = new LoginPage();
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
