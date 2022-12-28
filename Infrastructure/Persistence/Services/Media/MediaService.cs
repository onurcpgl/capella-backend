using Application.Repositories;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Tiff;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace Persistence.Services
{

    public class MediaService : IMediaService
    {
        Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        private readonly IConfiguration _config;
        private readonly IMediaWriteRepository _mediaWriteRepository;
        private readonly IMediaFormatReadRepository _mediaFormatReadRepository;
        public MediaService(IConfiguration config, IMediaWriteRepository mediaWriteRepository, IMediaFormatReadRepository mediaFormatReadRepository)
        {
            _config = config;
            _mediaWriteRepository = mediaWriteRepository;
            _mediaFormatReadRepository = mediaFormatReadRepository;
        }
        public async Task<Media> storage(IFormFile formFile, bool secure)
        { 
            
            try
            {
                var todayDate = DateTime.Now.ToString("yyyyMMdd");
                var todayTime = DateTime.Now.ToString("HHmmss");
                var rootPath = _config["MediaStorage:FileRootPath"];
                var isSecure = secure ? _config["MediaStorage:SecurePath"] : _config["MediaStorage:PublicPath"];
                var filePath = $"{isSecure}capella/{todayDate}/{todayTime}";
                var fullPath = $"{rootPath}/{filePath}";        
                var filenamehash =  new string(Enumerable.Repeat(chars, 20).Select(s => s[random.Next(s.Length)]).ToArray());

                Directory.CreateDirectory(fullPath);
                using (var stream = new FileStream(Path.Combine(fullPath, formFile.FileName), FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }

                Media media = new();
                media.RealFilename = formFile.FileName;
                media.EncodedFilename = filenamehash;
                media.Extension = Path.GetExtension(formFile.FileName);
                media.FilePath = filePath;
                media.RootPath = rootPath;
                media.Size = formFile.Length;
                media.Mime = formFile.ContentType;
                media.Secure = secure;
                var absolutePath = $"{filePath}/{filenamehash}{Path.GetExtension(formFile.FileName)}";
                media.AbsolutePath = absolutePath;
                media.ServePath = absolutePath;
                media.Code = Guid.NewGuid().ToString();
                await _mediaWriteRepository.AddAsync(media);
                return media;
                
            }
            catch (IOException exception)
            {

                throw exception;
            }
        }

        public async Task<Gallery> saveGallery(IFormFile formFile, bool secure)
        {
            try
            {
                Gallery gallery = new Gallery();
                var todayDate = DateTime.Now.ToString("yyyyMMdd");
                var todayTime = DateTime.Now.ToString("HHmmss");
                var isSecure = secure ? _config["MediaStorage:SecurePath"] : _config["MediaStorage:PublicPath"];
                var rootPath = _config["MediaStorage:FileRootPath"];
                var filePath = $"{isSecure}capella/{todayDate}/{todayTime}";
                var fullPath = $"{rootPath}/{filePath}";
                Directory.CreateDirectory(fullPath);
                gallery.Code = Guid.NewGuid().ToString();
                gallery.Name = formFile.FileName;

                var mediaFormats = _mediaFormatReadRepository.GetAll().ToList();
                var medias = new HashSet<Media>();
                foreach (var mediaFormat in mediaFormats)
                {
                    
                    var filenamehash = new string(Enumerable.Repeat(chars, 20).Select(s => s[random.Next(s.Length)]).ToArray());
                   
                    using (var stream = new MemoryStream())
                    {
                        await formFile.CopyToAsync(stream);
                        stream.Position = 0;
                        string fileName = Path.Combine(fullPath, filenamehash +"-"+mediaFormat.Name+Path.GetExtension(formFile.FileName));
                        using (var image = Image.Load(stream))
                        {
                            var options = new ResizeOptions
                            {
                                Size = new Size(mediaFormat.Height, mediaFormat.Width),
                                Mode = ResizeMode.Max
                            };

                            image.Mutate(x => x.Resize(options));

                            using (var output = File.OpenWrite(fileName))
                            {
                                image.Save(output, GetEncoder(fileName));
                            }
                        }


                    }

                    Media media = new();
                    media.RealFilename = formFile.FileName;
                    media.EncodedFilename = filenamehash;
                    media.Extension = Path.GetExtension(formFile.FileName);
                    media.FilePath = filePath;
                    media.RootPath = rootPath;
                    media.Size = formFile.Length;
                    media.Mime = formFile.ContentType;
                    media.Secure = secure;
                    var absolutePath = $"{filePath}/{filenamehash}{Path.GetExtension(formFile.FileName)}";
                    media.AbsolutePath = absolutePath;
                    media.ServePath = absolutePath;
                    media.Code = Guid.NewGuid().ToString();
                    await _mediaWriteRepository.AddAsync(media);
                    medias.Add(media);

                }
                gallery.Medias = medias;
                return gallery;
            }
            catch (IOException exception)
            {
                throw exception;
            }
        }


        private static IImageEncoder GetEncoder(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            switch (extension)
            {
                case ".bmp":
                    return new BmpEncoder();
                case ".gif":
                    return new GifEncoder();
                case ".jpg":
                case ".jpeg":
                    return new JpegEncoder();
                case ".png":
                    return new PngEncoder();
                case ".tiff":
                    return new TiffEncoder();
                default:
                    throw new NotSupportedException($"The specified file extension is not supported: {extension}");
            }
        }
    }


}
