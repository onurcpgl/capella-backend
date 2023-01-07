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
    public class ClassificationAttributeValueService : IClassificationAttributeValueService
    {
        private readonly IClassificationAttributeValueReadRepository _classificationAttributeValueReadRepository;
        private readonly IClassificationAttributeValueWriteRepository _classificationAttributeValueWriteRepository;
        private readonly IClassificationReadRepository _classificationReadRepository;
        private readonly IOptionsReadRepository _optionsReadRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IMapper _mapper;
        public ClassificationAttributeValueService(IClassificationAttributeValueReadRepository classificationAttributeValueReadRepository, IClassificationAttributeValueWriteRepository classificationAttributeValueWriteRepository, IMapper mapper, IClassificationReadRepository classificationReadRepository, IOptionsReadRepository optionsReadRepository, IProductReadRepository productReadRepository)
        {
            _classificationAttributeValueReadRepository = classificationAttributeValueReadRepository;
            _classificationAttributeValueWriteRepository = classificationAttributeValueWriteRepository;
            _classificationReadRepository = classificationReadRepository;
            _optionsReadRepository = optionsReadRepository;
            _productReadRepository = productReadRepository;
            _mapper = mapper;
        }
        public async Task Save(ClassificationAttributeValueDto classificationAttributeValueDto)
        {
            ClassificationAttributeValue classificationAttributeValue = new();
            classificationAttributeValue.Value = classificationAttributeValueDto.Value;
            classificationAttributeValue.Code = Guid.NewGuid().ToString();
            
            var classification = _classificationReadRepository.GetWhere(x=> x.Code == classificationAttributeValueDto.Classification.Code).FirstOrDefault();
            classificationAttributeValue.Classification = classification;

            var options = new HashSet<Options>();
            foreach (var item in classificationAttributeValueDto.Options)
            {
                var option = _optionsReadRepository.GetWhere(x => x.Code == item.Code).FirstOrDefault();
                options.Add(option);
            }
            classificationAttributeValue.Options = options;

            //var product = _productReadRepository.GetWhere(x => x.Code == ProductCode).FirstOrDefault();
            //classificationAttributeValue.Product = product;

            await _classificationAttributeValueWriteRepository.AddAsync(classificationAttributeValue);
           

        }
    }
}
