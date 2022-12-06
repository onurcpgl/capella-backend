﻿using Application.DataTransferObject;
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

            var transaction = await _productWriteRepository.DbTransactional();
            try
            {
                var category = new HashSet<Category>();
                foreach (var item in productDto.Categories)
                {
                    var cat = _categoryReadRepository.GetWhere(x => x.Code == item.Code).FirstOrDefault();
                    category.Add(cat);
                }
                product.Categories = category;

                
                var classificationAttributeValueList = new HashSet<ClassificationAttributeValue>();
                ClassificationAttributeValue classificationAttributeValue = new();
                foreach (var item in productDto.ClassificationAttributeValue)
                {
                    classificationAttributeValue.ClassificationAttribute = await  _classificationAttributeReadRepository.GetAllWithInclude(true, x => x.Code == item.ClassificationAttribute.Code).FirstOrDefaultAsync();
                    classificationAttributeValue.Value = item.Value;
                    classificationAttributeValueList.Add(classificationAttributeValue);
                }

                product.ClassificationAttributeValues = classificationAttributeValueList;


                if (formFiles.Count > 0)
                {
                    var galleries = product.Galleries.ToList();
                    foreach (var item in formFiles)
                    {
                        var media = await _mediaService.saveGallery(item, true);
                        galleries.Add(media);
                    }
                    product.Galleries = galleries;
                }

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
