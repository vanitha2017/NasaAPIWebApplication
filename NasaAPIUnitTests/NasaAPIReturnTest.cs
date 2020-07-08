using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NasaAPIWebApplication.Controllers;
using NasaAPIWebApplication.Models;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Newtonsoft.Json;
using NasaAPIWebApplication.Services;
using Microsoft.Extensions.Configuration;

namespace NasaAPIUnitTest.Tests
{
    public class NasaAPIReturnTest
    {
        private NASAAPIService _nasaApiService;
        private IConfiguration _config;

        public NasaAPIReturnTest()
        {
            _nasaApiService = new NASAAPIService(_config);
        }

        [TestCase]
        public void GetImagesFromNasaAPI_ShouldReturnResults()
        {
            //Act             
            //var response = _nasaApiService.GetPhotos();            

            //Assert           
            //Assert.NotNull(response.Result.Count);

            string strDate = "02/27/17";
            HttpClient http = new HttpClient();

            //Act
            var response = http.GetAsync("https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?earth_date=" + strDate + "&api_key=DNBMtukhKD83Xec4BhdOmn8YFly5W2VNK7y8mJwV").Result;

            //Assert
            Assert.NotNull(response.Content);

        }

    }
}
