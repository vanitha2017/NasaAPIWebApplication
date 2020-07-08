using Microsoft.Extensions.Configuration;
using NasaAPIWebApplication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NasaAPIWebApplication.Services
{
    public class NASAAPIService : INASAAPIService
    {
        private readonly IConfiguration _config;

        public NASAAPIService(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task<List<Images>> GetPhotos()
        {
            string[] lines = System.IO.File.ReadAllLines(@".\Files\SampleDates.txt");            
            List<Images> ImgList = new List<Images>();
            foreach (string line in lines)
            {
                try
                {
                    DateTime dt = DateTime.Parse(line);
                    var searchDate = dt.ToString("yyyy-MM-dd");

                    HttpClient http = new HttpClient();

                    var NasaApi = http.GetAsync(_config["Url"] + searchDate + "&api_key=DNBMtukhKD83Xec4BhdOmn8YFly5W2VNK7y8mJwV").Result;
                    var result = await NasaApi.Content.ReadAsStringAsync();
                    Images jsonObj = JsonConvert.DeserializeObject<Images>(result);

                    var imgdata = new Images();
                    imgdata.date = searchDate;
                    imgdata.photos = jsonObj.photos;

                    ImgList.Add(imgdata);                    
                }
                catch (Exception ex)
                {
                    return ImgList;
                }
            }
            return ImgList;

        }
    }
}
