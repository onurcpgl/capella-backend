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
        private readonly IClassificationAttributeValueService _classificationAttributeValueService;
        private readonly IVariantItemService _variantItemService;
        private readonly IMapper _mapper;

        public ProductService(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository,ICategoryReadRepository categoryReadRepository,IMediaService mediaService, IClassificationAttributeValueService classificationAttributeValueService, IVariantItemService variantItemService, IMapper mapper)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _categoryReadRepository = categoryReadRepository;
            _classificationAttributeValueService = classificationAttributeValueService;
            _mediaService = mediaService;
            _variantItemService = variantItemService;
            _mapper = mapper;
        }

        #region SaveProduct
        //public async Task<bool> saveProduct(ProductDto productDto, List<IFormFile> formFiles)
        public async Task<bool> saveProduct(ProductDto productDto)
        {
            var transaction = await _productWriteRepository.DbTransactional();
            Product product = new();
                product.Name = productDto.Name;
                product.Description = productDto.Description;
                product.Active = productDto.Active;
                product.Code = Guid.NewGuid().ToString();
            
            try
            {

                //var category = new HashSet<Category>();
                //foreach (var item in productDto.Categories)
                //{
                //    var cat = _categoryReadRepository.GetWhere(x => x.Code == item.Code).FirstOrDefault();
                //    category.Add(cat);
                //}
                //product.Categories = category;

                var classificationAttributeValues = new HashSet<ClassificationAttributeValue>();
                foreach (var item in productDto.ClassificationAttributeValues)
                {
                   var classificationAttributeValue = await _classificationAttributeValueService.Save(item);
                   classificationAttributeValues.Add(classificationAttributeValue);
                   
                }

                product.ClassificationAttributeValues = classificationAttributeValues;

                var variantItems = new HashSet<VariantItem>();
                foreach (var item in productDto.VariantItems)
                {
                    var variantItem = await _variantItemService.Save(item);
                    variantItems.Add(variantItem);
                }

                product.VariantItems = variantItems;

                //if (formFiles.Count > 0)
                //{
                //    var galleries = new HashSet<Gallery>();
                //    foreach (var item in formFiles)
                //    {
                //        var media = await _mediaService.saveGallery(item, true);
                //        galleries.Add(media);
                //    }
                //    product.Galleries = galleries;
                //}
                await _productWriteRepository.AddAsync(product);
                transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            return true;

        }
        #endregion
        
        public async Task<List<Product>> productList()
        {
            List<Product> products = await _productReadRepository.GetAll().ToListAsync();
            return products;
        }

        public async Task<ProductDto> GetProductByCode(string code)
        {
            var product = await _productReadRepository.GetWhere(x => x.Code == code)
                .Include(x=> x.Categories)
                .ThenInclude(x=> x.Classifications)
                .Include(x=>x.ClassificationAttributeValues)
                .Include(x=>x.Galleries)
                .ThenInclude(x=>x.Medias)
                .FirstOrDefaultAsync();


            var productDto = _mapper.Map<ProductDto>(product);

            return productDto;

        }

    }
}
