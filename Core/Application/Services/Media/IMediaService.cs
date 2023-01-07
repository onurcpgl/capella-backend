using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IMediaService
    {
        Task<Media> Storage(IFormFile formFile, bool secure);

        Task<Gallery> SaveGallery(IFormFile formFile, bool secure);
    }
}
