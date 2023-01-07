using Application.DataTransferObject;
using Application.Repositories;
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
    public class UnitService : IUnitService
    {
        private readonly IUnitReadRepository _unitReadRepository;
        private readonly IUnitWriteRepository _unitWriteRepository;
        private readonly IMapper _mapper;

        public UnitService(IUnitReadRepository unitReadRepository, IUnitWriteRepository unitWriteRepository, IMapper mapper)
        {
            _unitReadRepository = unitReadRepository;
            _unitWriteRepository = unitWriteRepository;
            _mapper = mapper;
        }

        public async Task<UnitDto> GetUnitByCode(string code)
        {
            var unit = _unitReadRepository.GetWhere(x => x.Code == code).FirstOrDefault();
            var unitDto = _mapper.Map<UnitDto>(unit);
            return unitDto;
        }

        public async Task<bool> Save(UnitDto unitDto)
        {
            var unit = _mapper.Map<Unit>(unitDto);
            var result = await _unitWriteRepository.AddAsync(unit);
            if (!result)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<List<Unit>> GetAllUnits()
        {
            List<Unit> units = await _unitReadRepository.GetAll().ToListAsync();
            return units;
        }
    }
}
