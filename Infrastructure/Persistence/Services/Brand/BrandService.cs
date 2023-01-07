﻿using Application.DataTransferObject;
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
        public async Task<bool> Save(BrandDto brandDto)
        {
            Brand brand = new();

            brand.Code = Guid.NewGuid().ToString();
            brand.Name = brandDto.Name;

            var result = await _brandWriteRepository.AddAsync(brand);
            if (!result)
            {
                return false;
            }
            return true;
        }
    }
}
