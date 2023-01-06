using Application.DataTransferObject;
using Application.Repositories;
using Application.Services;
using Application.Services.Variant;
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

        public VariantService(IVariantWriteRepository variantWriteRepository, IVariantReadRepository variantReadRepository, IVariantValueService variantValueService)
        {
            _variantReadRepository = variantReadRepository;
            _variantWriteRepository = variantWriteRepository;
            _variantValueService = variantValueService;
        }
        public async Task<bool> save(VariantDto variantDto)
        {
            Variant variant = new();

            variant.Name = variantDto.Name;
            variant.Code = Guid.NewGuid().ToString();
            variant.ChooseType = (Domain.Enums.DataType)variantDto.ChooseType;
            var transaction = await _variantWriteRepository.DbTransactional();

            try
            {
                await _variantWriteRepository.AddAsync(variant);
                var variantValues = new HashSet<VariantValue>();
                foreach (var item in variantDto.VariantValues)
                {
                    var variantValue = await _variantValueService.Save(item, variant.Code);
                    variantValues.Add(variantValue);

                }
                transaction.CommitAsync();


            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }

            return true;

        }
    }
}
