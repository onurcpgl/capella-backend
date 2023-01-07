using Application.DataTransferObject;
using Application.Repositories;
using Application.Repositories.ProductAbstract;
using Application.Services;
using AutoMapper;
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
        private readonly IOptionsService _optionsService;
        private readonly IMapper _mapper;

        public ClassificationService(IClassificationReadRepository classificationReadRepository,
            IClassificationWriteRepository classificationWriteRepository,
            IMapper mapper,
            IOptionsService optionsService)
        {
            _classificationReadRepository = classificationReadRepository;
            _classificationWriteRepository = classificationWriteRepository;   
            _mapper = mapper;
            _optionsService = optionsService;
        }
        public async Task Save(ClassificationDto classificationDto)
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

        }

        public async Task<List<Classification>> GetAllClassifications()
        {
            List<Classification> classifications = await _classificationReadRepository.GetAllWithInclude(true,x=> x.Options).ToListAsync();
            return classifications;
        }

        public async Task<ClassificationDto> GetClassificationByCode(string code)
        {
            var classification = await _classificationReadRepository.GetWhereWithInclude(x => x.Code == code, true, x => x.Options).FirstOrDefaultAsync();
            var classificationDto = _mapper.Map<ClassificationDto>(classification);
            return classificationDto;
        }
    }
}
