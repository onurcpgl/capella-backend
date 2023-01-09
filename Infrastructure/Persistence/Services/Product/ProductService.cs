using Application.DataTransferObject;
using Application.Repositories;
using Application.Repositories.ProductAbstract;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IMediaService _mediaService;
        private readonly IProductReadRepository _productReadRepository;
        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly IBrandReadRepository _brandReadRepository;
        private readonly ITagReadRepository _tagReadRepository;
        private readonly IClassificationAttributeValueService _classificationAttributeValueService;
        private readonly IVariantItemService _variantItemService;
        private readonly IMapper _mapper;

        public ProductService(IProductWriteRepository productWriteRepository,
            IProductReadRepository productReadRepository,
            ICategoryReadRepository categoryReadRepository,
            IMediaService mediaService,
            IClassificationAttributeValueService classificationAttributeValueService,
            IVariantItemService variantItemService,
            IBrandReadRepository brandReadRepository,
            ITagReadRepository tagReadRepository,
            IMapper mapper)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _categoryReadRepository = categoryReadRepository;
            _classificationAttributeValueService = classificationAttributeValueService;
            _mediaService = mediaService;
            _variantItemService = variantItemService;
            _tagReadRepository = tagReadRepository;
            _brandReadRepository = brandReadRepository;
            _mapper = mapper;
        }

        #region SaveProduct
        public async Task Save(ProductDto productDto, List<IFormFile> formFiles)
        {
            var transaction = await _productWriteRepository.DbTransactional();
            Product product = new();
            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Active = productDto.Active;
            product.Code = Guid.NewGuid().ToString();

            try
            {
                var categories = new HashSet<Category>();
                foreach (var item in productDto.Categories)
                {
                    var category = _categoryReadRepository.GetWhere(x => x.Code == item.Code).FirstOrDefault();
                    categories.Add(category);
                }
                product.Categories = categories;

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

                var brand = await _brandReadRepository.GetWhere(x => x.Code == productDto.Brand.Code).FirstOrDefaultAsync();
                product.Brand = brand;

                var tags = new HashSet<Tag>();
                foreach (var item in productDto.Tags)
                {
                    var tag = await _tagReadRepository.GetWhere(x => x.Code == item.Code).FirstOrDefaultAsync();
                    tags.Add(tag);

                }
                product.Tags = tags;

                var galleries = new HashSet<Gallery>();
                foreach (var item in formFiles)
                {
                    var media = await _mediaService.SaveGallery(item, true);
                    galleries.Add(media);
                }
                product.Galleries = galleries;

                await _productWriteRepository.AddAsync(product);

                transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }

        }
        #endregion

        public async Task<List<ProductDto>> GetAllProducts()
        {
            var products = await _productReadRepository.GetAll().ToListAsync();
            var productsDto = _mapper.Map<List<ProductDto>>(products);
            return productsDto;
        }

        public async Task<ProductDto> GetProductByCode(string code)
        {
            var product = await _productReadRepository.GetWhere(x => x.Code == code)
                .Include(x => x.Categories)
                .Include(x => x.ClassificationAttributeValues)
                .ThenInclude(x => x.Classification)
                .ThenInclude(x => x.Options)
                .Include(x => x.VariantItems)
                .ThenInclude(x => x.VariantValues)
                .Include(x => x.Brand)
                .Include(x => x.Tags)
                .Include(x => x.Galleries)
                .FirstOrDefaultAsync();

            var productDto = _mapper.Map<ProductDto>(product);

            return productDto;

        }

        public async Task Delete(string code)
        {
            var product = _productReadRepository.GetWhere(x => x.Code == code).FirstOrDefault();
            await _productWriteRepository.RemoveAsync(product);
        }

        #region UpdateProduct
        public async Task Update(ProductDto productDto, List<IFormFile> formFiles)
        {
            var product = await _productReadRepository.GetWhere(x => x.Code == productDto.Code)
                .Include(x => x.Categories)
                .Include(x=> x.ClassificationAttributeValues)
                .ThenInclude(x=> x.Classification)
                .ThenInclude(x=>x.Options)
                .Include(x=>x.VariantItems)
                .ThenInclude(x=>x.VariantValues)
                .Include(x=> x.Brand)
                .Include(x=> x.Tags)
                .Include(x=> x.Galleries)
                .FirstOrDefaultAsync();

            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Active = productDto.Active;
            product.Code = productDto.Code;
            var categories = new HashSet<Category>();
            foreach (var item in productDto.Categories)
            {
                var category = _categoryReadRepository.GetWhere(x => x.Code == item.Code).FirstOrDefault();
                categories.Add(category);
            }
            product.Categories = categories;

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

            var brand = await _brandReadRepository.GetWhere(x => x.Code == productDto.Brand.Code).FirstOrDefaultAsync();
            product.Brand = brand;

            var tags = new HashSet<Tag>();
            foreach (var item in productDto.Tags)
            {
                var tag = await _tagReadRepository.GetWhere(x => x.Code == item.Code).FirstOrDefaultAsync();
                tags.Add(tag);

            }
            product.Tags = tags;

            var galleries = new HashSet<Gallery>();
            foreach (var item in formFiles)
            {
                var media = await _mediaService.SaveGallery(item, true);
                galleries.Add(media);
            }
            product.Galleries = galleries;

            await _productWriteRepository.UpdateAsync(product,product.Id);
        }
        #endregion
    }
}
