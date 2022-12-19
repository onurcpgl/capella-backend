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
        private readonly IClassificationAttributeReadRepository _classificationAttributeReadRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository,ICategoryReadRepository categoryReadRepository, IClassificationAttributeReadRepository classificationAttributeReadRepository,IMediaService mediaService, IMapper mapper)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _categoryReadRepository = categoryReadRepository;
            _classificationAttributeReadRepository = classificationAttributeReadRepository;
            _mediaService = mediaService;
            _mapper = mapper;
        }


        public async Task<bool> saveProduct(ProductDto productDto, List<IFormFile> formFiles)
        {
            var transaction = await _productWriteRepository.DbTransactional();
            try
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

                
                var classificationAttributeValueList = new HashSet<ClassificationAttributeValue>();
                foreach (var item in productDto.ClassificationAttributeValue)
                {
                    if (!Object.Equals(item.ClassificationAttribute, null))
                    {
                        ClassificationAttributeValue classificationAttributeValue = new();
                        classificationAttributeValue.Code = Guid.NewGuid().ToString();
                        classificationAttributeValue.ClassificationAttribute = await _classificationAttributeReadRepository.GetWhere(x => x.Code == item.ClassificationAttribute.AttributeCode).FirstOrDefaultAsync();
                        classificationAttributeValue.Value = item.Value;
                        classificationAttributeValueList.Add(classificationAttributeValue);
                    }
                    
                }

                product.ClassificationAttributeValues = classificationAttributeValueList;


                if (formFiles.Count > 0)
                {
                    var galleries = new HashSet<Gallery>();
                    foreach (var item in formFiles)
                    {
                        var media = await _mediaService.saveGallery(item, true);
                        galleries.Add(media);
                    }
                    product.Galleries = galleries;
                }

                await _productWriteRepository.AddAsync(product);

                transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
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
