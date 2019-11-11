using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using rawdata_portfolioproject_2;
using Xunit;

namespace UnitTests
{
    
    public class WebServiceTests
    {
        private const string ProfilesApi = "http://localhost:5000/api/profiles";
        private const string PostsApi = "http://localhost:5000/api/posts";
        
        // make tests for the webservice like in the example below

        [Fact]
        public void CreateProfileReturnStatusCodeCreated()
        {
            var newProfile = new
            {
                Email = "test7@email.com",
                Password = "testpassword"
            };
            
            var (profile, statusCode) = PostData(ProfilesApi, newProfile);

            Assert.Equal(HttpStatusCode.Created, statusCode);
            Assert.Equal(newProfile.Email, profile["email"]);

            PostData($"{ProfilesApi}/delete", newProfile);
        }

        [Fact]
        public void ProfileLoginReturnOk()
        {
            var newProfile = new
            {
                Email = "test@email.com",
                Password = "testpassword"
            };
            
            PostData(ProfilesApi, newProfile);
            var (_, statusCode) = PostData($"{ProfilesApi}/login", newProfile);

            Assert.Equal(HttpStatusCode.OK, statusCode);

            PostData($"{ProfilesApi}/delete", newProfile);
        }
        
        [Fact]
        public void GetProfileWithoutLoginReturnUnauthorized()
        {
            var newProfile = new
            {
                Email = "test@email.com",
                Password = "testpassword"
            };
            
            PostData(ProfilesApi, newProfile);
            var (_, statusCode) = GetObject($"{ProfilesApi}/email/{newProfile.Email}");

            Assert.Equal(HttpStatusCode.Unauthorized, statusCode);

            PostData($"{ProfilesApi}/delete", newProfile);
        }
        
        [Fact]
        public void GetProfileWithLoginReturnOk()
        {
            // works in postman but dont know how to add header in C#
            var newProfile = new
            {
                Email = "test@email.com",
                Password = "testpassword"
            };
            
            PostData(ProfilesApi, newProfile);
            PostData($"{ProfilesApi}/login", newProfile);
            var (_, statusCode) = GetObject($"{ProfilesApi}/email/{newProfile.Email}");

            Assert.Equal(HttpStatusCode.OK, statusCode);

            PostData($"{ProfilesApi}/delete", newProfile);
        }

        // Helpers

        (JArray, HttpStatusCode) GetArray(string url)
        {
            var client = new HttpClient();
            var response = client.GetAsync(url).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            return ((JArray)JsonConvert.DeserializeObject(data), response.StatusCode);
        }

        (JObject, HttpStatusCode) GetObject(string url)
        {
            var client = new HttpClient();
            var response = client.GetAsync(url).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            return ((JObject)JsonConvert.DeserializeObject(data), response.StatusCode);
        }

        (JObject, HttpStatusCode) PostData(string url, object content)
        {
            var client = new HttpClient();
            var requestContent = new StringContent(
                JsonConvert.SerializeObject(content),
                Encoding.UTF8,
                "application/json");
            var response = client.PostAsync(url, requestContent).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            return ((JObject)JsonConvert.DeserializeObject(data), response.StatusCode);
        }

        HttpStatusCode PutData(string url, object content)
        {
            var client = new HttpClient();
            var response = client.PutAsync(
                url,
                new StringContent(
                    JsonConvert.SerializeObject(content),
                    Encoding.UTF8,
                    "application/json")).Result;
            return response.StatusCode;
        }

        HttpStatusCode DeleteData(string url)
        {
            var client = new HttpClient();
            var response = client.DeleteAsync(url).Result;
            return response.StatusCode;
        }
    }
}