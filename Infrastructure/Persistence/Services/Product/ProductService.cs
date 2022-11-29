using Application.DataTransferObject;
using Application.Repositories;
using Application.Repositories.ProductAbstract;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IMediaService _mediaService;
        private readonly IProductReadRepository _productReadRepository;
        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IMapper mapper)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _mapper = mapper;
        }

       
        public async Task<bool> saveProduct(ProductDto productDto, List<IFormFile> formFiles)
        {
            Product product = new();
            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Active = productDto.Active;
            product.Code = Guid.NewGuid().ToString();

            var category = new HashSet<Category>();
            foreach (var item in productDto.Categories)
            {
                var cat = _categoryReadRepository.GetWhere(x => x.Code == item.Code).FirstOrDefault();
                category.Add(cat);
            }
            product.Categories = category;


            if (formFiles.Count > 0)
            {
                var medias = new HashSet<Media>();
                foreach (var item in formFiles)
                {
                    var media = await _mediaService.storage(item, true);
                    medias.Add(media);
                }
                product.Medias = medias;
                
            }

            return true;
            

        }
        public async Task<List<Product>> productList()
        {
            List<Product> products = await _productReadRepository.GetAll().ToListAsync();
            return products;
        }
        public async Task<Product> getProductById(int productId)
        {
            var product = await _productReadRepository.GetByIdAsync(productId);
            return product;

        }

    }
}
