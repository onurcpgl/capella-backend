using Application.DataTransferObject;
using Application.Repositories;
using Application.Repositories.ProductAbstract;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class VariantItemService : IVariantItemService
    {
        private readonly IVariantItemReadRepository _variantItemReadRepository;
        private readonly IVariantValueReadRepository _variantValueReadRepository;
        private readonly IVariantItemWriteRepository _variantItemWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IClassificationAttributeValueService _classificationAttributeValueService;
        private readonly IMapper _mapper;
        public VariantItemService(IVariantItemReadRepository variantItemReadRepository, IClassificationAttributeValueService classificationAttributeValueService,IVariantValueReadRepository variantValueReadRepository, IVariantItemWriteRepository variantItemWriteRepository, IMapper mapper, IProductReadRepository productReadRepository)
        {
            _variantItemReadRepository = variantItemReadRepository;
            _classificationAttributeValueService = classificationAttributeValueService;
            _variantValueReadRepository = variantValueReadRepository;
            _variantItemWriteRepository = variantItemWriteRepository;
            _productReadRepository = productReadRepository;
            _mapper = mapper;
        }
        public async Task<VariantItem> Save(VariantItemDto variantItemDto)
        {
            VariantItem variantItem = new();
            variantItem.Name = variantItemDto.Name;
            variantItem.Code = Guid.NewGuid().ToString();

            var variantValues = new HashSet<VariantValue>();
            foreach (var item in variantItemDto.VariantValues)
            {
                var variantValue = _variantValueReadRepository.GetWhere(x => x.Code == item.Code).FirstOrDefault();
                variantValues.Add(variantValue);
            }
            variantItem.VariantValues = variantValues;

            var classificationAttributeValues = new HashSet<ClassificationAttributeValue>();
            foreach (var item in variantItemDto.ClassificationAttributeValues)
            {
                var classificationAttributeValue = await _classificationAttributeValueService.Save(item);
                classificationAttributeValues.Add(classificationAttributeValue);
            }
            variantItem.ClassificationAttributeValues = classificationAttributeValues;        

            var result = await _variantItemWriteRepository.AddAsync(variantItem);
            return result ? variantItem : null;
        }
    }
}
