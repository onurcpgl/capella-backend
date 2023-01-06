using Application.DataTransferObject;
using Application.Repositories;
using Application.Repositories.ProductAbstract;
using Application.Services;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class ClassificationService : IClassificationService
    {
        private readonly IClassificationReadRepository _classificationReadRepository;
        private readonly IClassificationWriteRepository _classificationWriteRepository;
      
        private readonly IClassificationAttributeValueWriteRepository _classificationAttributeValueWriteRepository;
        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly IOptionsService _optionsService;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IUnitReadRepository _unitReadRepository;

        public ClassificationService(IClassificationReadRepository classificationReadRepository, IClassificationWriteRepository classificationWriteRepository,
             ICategoryReadRepository categoryReadRepository,
            IUnitReadRepository unitReadRepository, IProductReadRepository productReadRepository, IClassificationAttributeValueWriteRepository classificationAttributeValueWriteRepository, IOptionsService optionsService)
        {
            _classificationReadRepository = classificationReadRepository;
            _classificationWriteRepository = classificationWriteRepository;
   
            _classificationAttributeValueWriteRepository = classificationAttributeValueWriteRepository;
            _categoryReadRepository = categoryReadRepository;
            _productReadRepository = productReadRepository;
            _unitReadRepository = unitReadRepository;
            _optionsService = optionsService;
        }
        public async Task<bool> saveClassification(ClassificationDto classificationDto)
        {
            
            Classification classification = new();

            classification.Name = classificationDto.Name;
            classification.Code = Guid.NewGuid().ToString();
            classification.DataType = (Domain.Enums.DataType)classificationDto.DataType;
            var transaction = await _classificationWriteRepository.DbTransactional();
          
                try
                {

                    await _classificationWriteRepository.AddAsync(classification);
                    var options = new HashSet<Options>();
                        foreach (var item in classificationDto.Options)
                        {
                            var option = await _optionsService.Save(item,classification.Code);
                            options.Add(option);
                        
                        }

                    

                    transaction.CommitAsync();


                }catch (Exception ex)
                {
                    transaction.Rollback();
                }

            return true;

        }



        public async Task<List<Classification>> getAll()
        {
            List<Classification> classifications = await _classificationReadRepository.GetAllWithInclude(true,x=> x.Options).ToListAsync();
            return classifications;
        }

        
    }
}
