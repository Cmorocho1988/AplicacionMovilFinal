using AplicacionMovil;
using CristianMorocho.Controllers;
using CristianMorocho.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CristianMorocho.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : TabbedPage
    {
        static UserController userController = new UserController();
        static ProductController productController = new ProductController();

        ObservableCollection<ProductModel> productModels = new ObservableCollection<ProductModel>();
        public ObservableCollection<ProductModel> Products { get{ return productModels; } }

        
        public HomeView()
        {
            LoadProducts();
            LoadProfile();
            InitializeComponent();
        }
        
        void LogOut(object sender, EventArgs e)
        {
            Preferences.Set("token", "");
            App.Current.MainPage = new NavigationPage(new LoginPage());
        }
        
        async void UpdateProfile(object obj, EventArgs e)
        {
            ResponseModel responseModel = await userController.UpdateProfile(new UserModel
            {
                name = name.Text,
                email = email.Text,
                password = password.Text
            });

            if(responseModel.status == 201)
            {
                await DisplayAlert("Estado de Perfil", responseModel.message, "ACEPTAR");
            }
            
            if(responseModel.status == 200)
            {
                LoadProfile();
                await DisplayAlert("Estado de Perfil", responseModel.message, "ACEPTAR");
            }
        }
        
        public async void LoadProducts()
        {
            productModels.Clear();
            
            List<ProductModel> productModelsGet = await productController.GetProducts();

            foreach (ProductModel productModel in productModelsGet)
            {
                productModels.Add(productModel);
            }

            ProductosView.ItemsSource = productModels;
        }

        public async void LoadProfile()
        {
            UserModel userModel = await userController.GetUser();

            name.Text = userModel.name;
            email.Text = userModel.email;
        }
    }
}