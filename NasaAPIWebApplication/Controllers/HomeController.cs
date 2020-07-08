using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NasaAPIWebApplication.Models;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Extensions.FileProviders;
using System.Net;
using System.Drawing;
using System.Drawing.Imaging;
using NasaAPIWebApplication.Services;

namespace NasaAPIWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;
        private readonly INASAAPIService _nASAAPIService;

        public HomeController(ILogger<HomeController> logger ,IConfiguration configuration, INASAAPIService nASAAPIService)
        {
            _logger = logger;
            _config = configuration;
            _nASAAPIService = nASAAPIService;

        }

        public async Task<IActionResult> Index()
        {
            //string[] lines =System.IO.File.ReadAllLines(@".\Files\SampleDates.txt");
            //List<Images> ImgList = new List<Images>();
            //foreach (string line in lines)
            //{ 
            //    try
            //    {
            //        DateTime dt = DateTime.Parse(line);
            //        var searchDate = dt.ToString("yyyy-MM-dd");

            //        HttpClient http = new HttpClient();

            //        var NasaApi = http.GetAsync(_config["Url"] + searchDate + "&api_key=VIWf7sXbilMiuFaPAjHKyDM8jnAkirCkNIe0EWiK").Result;
            //        var result = await NasaApi.Content.ReadAsStringAsync();
            //        Images jsonObj = JsonConvert.DeserializeObject<Images>(result);

            //        var imgdata = new Images();
            //        imgdata.date = searchDate;
            //        imgdata.photos = jsonObj.photos;

            //        ImgList.Add(imgdata);

            //    }
            //    catch(Exception ex)
            //    {
            //        return View(ImgList);
            //    }
            //}

            //return View(ImgList);

            dynamic img = await _nASAAPIService.GetPhotos();
            return View(img);
        }

        public System.Drawing.Image DownloadImageFromUrl(string imageUrl)
        {
            System.Drawing.Image image = null;

            try
            {
                System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(imageUrl);
                webRequest.AllowWriteStreamBuffering = true;
                webRequest.Timeout = 30000;

                System.Net.WebResponse webResponse = webRequest.GetResponse();

                System.IO.Stream stream = webResponse.GetResponseStream();

                image = System.Drawing.Image.FromStream(stream);

                webResponse.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return image;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //don't need to download all the files. only the one's user selects.
        //foreach (Photo photo in imgdata.photos)
        //{
        //    var fileName = Path.GetFileName(photo.img_src);
        //    var format = Path.GetExtension(photo.img_src);
        //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Images\\"+photo.earth_date);
        //    if (!Directory.Exists(filePath))
        //    {
        //        Directory.CreateDirectory(filePath);
        //    }
        //    Image image = DownloadImageFromUrl(photo.img_src);

        //    string rootPath = filePath;
        //    string fileName1 = System.IO.Path.Combine(rootPath, fileName);
        //    if (!System.IO.File.Exists(fileName1))
        //    {
        //        image.Save(fileName1);
        //    }

        //}
    }
}
