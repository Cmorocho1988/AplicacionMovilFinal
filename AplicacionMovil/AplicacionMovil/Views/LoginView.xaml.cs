using AplicacionMovil;
using CristianMorocho.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CristianMorocho.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        async void LoginClick(object obj, EventArgs args)
        {
            if (string.IsNullOrEmpty(email.Text) || string.IsNullOrEmpty(password.Text))
            {
                await DisplayAlert("Inicio de Sesion", "Por favor ingrese nombre de usuario y contraseña", "ACEPTAR");
                return;
            }
            
            UserModel user = new UserModel
            {
                email = email.Text,
                password = password.Text
            };

            try
            {
                var client = new HttpClient();

                UserModel userModel = new UserModel()
                {
                    email = email.Text,
                    password = password.Text
                };

                HttpResponseMessage response = await client.PostAsync("https://webservice-1.sjbestudio.com/api/login", 
                    new StringContent(JsonConvert.SerializeObject(userModel), 
                    Encoding.UTF8, "application/json"));
                
                var result = await response.Content.ReadAsStringAsync();
                
                var res = JsonConvert.DeserializeObject<ResponseModel>(result);

                if (res.status ==  201)
                {
                    await DisplayAlert("Inicio de Sesion", res.message, "ACEPTAR");
                }

                if(res.status == 200)
                {
                    await DisplayAlert("Inicio de Sesion", res.message, "ACEPTAR");

                    Preferences.Set("token", res.token);

                    App.Current.MainPage = new NavigationPage(new HomeView());
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "OK");
            }
        }
    }
}