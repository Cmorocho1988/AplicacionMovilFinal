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
    public class UserController
    {
        public async Task<ResponseModel> UpdateProfile(UserModel userModel)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                HttpClient client = new HttpClient();

                var token = Preferences.Get("token", String.Empty);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                HttpResponseMessage response = await client.PostAsync("https://webservice-1.sjbestudio.com/api/user/update/profile", 
                    new StringContent(JsonConvert.SerializeObject(userModel), 
                    Encoding.UTF8, "application/json"));

                var result = await response.Content.ReadAsStringAsync();
                responseModel = JsonConvert.DeserializeObject<ResponseModel>(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return responseModel;
        }
            
        public async Task<UserModel> GetUser()
        {
            UserModel userModel = new UserModel();
            try
            {
                HttpClient client = new HttpClient();

                var token = Preferences.Get("token", String.Empty);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                HttpResponseMessage response = await client.GetAsync("https://webservice-1.sjbestudio.com/api/user");

                var result = await response.Content.ReadAsStringAsync();              
                var deserializer = JsonConvert.DeserializeObject<List<UserModel>>(result);

                foreach (var item in deserializer)
                {
                    userModel.id = item.id;
                    userModel.name = item.name;
                    userModel.email = item.email;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return userModel;
        }
    }
}
