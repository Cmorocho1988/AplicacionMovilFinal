using CristianMorocho.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace CristianMorocho.Controllers
{
    public class ProductController
    {
        static List<ProductModel> productModels = new List<ProductModel>();
        
        public async Task<List<ProductModel>> GetProducts()
        {
            try
            {
                HttpClient client = new HttpClient();

                var token = Preferences.Get("token", String.Empty);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                HttpResponseMessage response = await client.GetAsync("https://webservice-1.sjbestudio.com/api/products");

                var result = await response.Content.ReadAsStringAsync();
                var deserializer = JsonConvert.DeserializeObject<List<ProductModel>>(result);

                foreach(var item in deserializer)
                {
                    ProductModel product = new ProductModel();

                    product.id = item.id;
                    product.name = item.name;
                    product.description = item.description;
                    product.image = item.image;
                    product.price = item.price;
                    product.stock = item.stock;
                    product.category = item.category;

                    productModels.Add(product);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Products:" + ex.Message);
            }

            return productModels;
        }
    }
}
