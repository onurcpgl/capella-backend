using Application.DataTransferObject;
using Application.Repositories;
using Application.Services;
using Application.Services.Variant;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class VariantService : IVariantService
    {
        private readonly IVariantWriteRepository _variantWriteRepository;
        private readonly IVariantReadRepository _variantReadRepository;
        private readonly IVariantValueService _variantValueService;
        private readonly IMapper _mapper;

        public VariantService(IVariantWriteRepository variantWriteRepository, IVariantReadRepository variantReadRepository, IVariantValueService variantValueService, IMapper mapper)
        {
            _variantReadRepository = variantReadRepository;
            _variantWriteRepository = variantWriteRepository;
            _variantValueService = variantValueService;
            _mapper = mapper;
        }

        public async Task Delete(string code)
        {
            var variant = _variantReadRepository.GetWhere(x => x.Code == code).FirstOrDefault();
            await _variantWriteRepository.RemoveAsync(variant);
        }

        public async Task<List<VariantDto>> GetAllVariants()
        {
            var variants = _variantReadRepository.GetAllWithInclude(true, x => x.VariantValues).ToList();
            var variantsDto = _mapper.Map<List<VariantDto>>(variants);
            return variantsDto;
        }

        public async Task<VariantDto> GetVariantByCode(string code)
        {
            var variant = _variantReadRepository.GetWhereWithInclude(x => x.Code == code, true, x => x.VariantValues).FirstOrDefault();
            var variantDto = _mapper.Map<VariantDto>(variant);
            return variantDto;
        }

        public async Task Save(VariantDto variantDto)
        {
            Variant variant = new();

            variant.Name = variantDto.Name;
            variant.Code = Guid.NewGuid().ToString();
            variant.ChooseType = (Domain.Enums.DataType)variantDto.ChooseType;
            var transaction = await _variantWriteRepository.DbTransactional();

            try
            {
                var variantValues = new HashSet<VariantValue>();
                foreach (var item in variantDto.VariantValues)
                {
                    var variantValue = await _variantValueService.Save(item, variant.Code);
                    variantValues.Add(variantValue);

                }
                await _variantWriteRepository.AddAsync(variant);

                transaction.CommitAsync();


            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
        }

        public async Task Update(VariantDto variantDto)
        {
            var variant = _variantReadRepository.GetWhereWithInclude(x => x.Code == variantDto.Code, true, x => x.VariantValues).FirstOrDefault();
            variant.Name = variantDto.Name;
            variant.Code = variantDto.Code;
            variant.ChooseType = (Domain.Enums.DataType)variantDto.ChooseType;
            var variantValues = new HashSet<VariantValue>();
            foreach (var item in variantDto.VariantValues)
            {
                var variantValue = await _variantValueService.Save(item, variant.Code);
                variantValues.Add(variantValue);

            }
            variant.VariantValues = variantValues;
            await _variantWriteRepository.UpdateAsync(variant,variant.Id);
        }
    }
}
