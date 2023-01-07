using Application.DataTransferObject;
using Application.Repositories;
using Application.Services;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class VariantValueService : IVariantValueService
    {
        private readonly IVariantValueReadRepository _variantValueReadRepository;
        private readonly IVariantValueWriteRepository _variantValueWriteRepository;
        private readonly IVariantReadRepository _variantReadRepository;
        public VariantValueService(IVariantValueReadRepository variantValueReadRepository, IVariantValueWriteRepository variantValueWriteRepository, IVariantReadRepository variantReadRepository)
        {
            _variantValueReadRepository = variantValueReadRepository;
            _variantValueWriteRepository = variantValueWriteRepository;
            _variantReadRepository = variantReadRepository;
        }

        public async Task<VariantValue> Save(VariantValueDto variantValueDto, string VariantCode)
        {
            VariantValue variantValue = new();

            variantValue.Code = Guid.NewGuid().ToString();
            variantValue.Name = variantValueDto.Name;

            var variant = _variantReadRepository.GetWhere(x => x.Code == VariantCode).FirstOrDefault();
            variantValue.Variant = variant;

            var model = await _variantValueWriteRepository.AddAsyncWithModel(variantValue);
            return model;
        }
    }
}
