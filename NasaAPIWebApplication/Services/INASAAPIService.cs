using NasaAPIWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NasaAPIWebApplication.Services
{
    public interface INASAAPIService
    {

        Task<List<Images>> GetPhotos();
    }
}
