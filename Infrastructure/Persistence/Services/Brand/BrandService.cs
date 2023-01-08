using Application.DataTransferObject;
using Application.Repositories;
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
    public class BrandService : IBrandService
    {
        private readonly IBrandReadRepository _brandReadRepository;
        private readonly IBrandWriteRepository _brandWriteRepository;
        private readonly IMapper _mapper;

        public BrandService(IBrandReadRepository brandReadRepository, IBrandWriteRepository brandWriteRepository, IMapper mapper)
        {
            _brandReadRepository = brandReadRepository;
            _brandWriteRepository = brandWriteRepository;
            _mapper = mapper;
        }

        public async Task Delete(string code)
        {
            var brand = _brandReadRepository.GetWhere(x => x.Code == code).FirstOrDefault();
            await _brandWriteRepository.RemoveAsync(brand);
        }

        public async Task<List<BrandDto>> GetAllBrands()
        {
            var brands = _brandReadRepository.GetAll().ToList();
            var brandDto = _mapper.Map<List<BrandDto>>(brands);
            return brandDto;
        }

        public async Task<BrandDto> GetBrandByCode(string code)
        {
            var brand = _brandReadRepository.GetWhere(x => x.Code == code).FirstOrDefault();
            var brandDto = _mapper.Map<BrandDto>(brand);
            return brandDto;
        }

        public async Task Save(BrandDto brandDto)
        {
            Brand brand = new();
            brand.Code = Guid.NewGuid().ToString();
            brand.Name = brandDto.Name;
            await _brandWriteRepository.AddAsync(brand);
           
        }

        public async Task Update(BrandDto brandDto)
        {
            var brand = _mapper.Map<Brand>(brandDto);
            await _brandWriteRepository.UpdateAsync(brand, brandDto.Id);
        }
    }
}
