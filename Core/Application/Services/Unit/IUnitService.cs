using Application.DataTransferObject;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IUnitService
    {
        Task<bool> Save(UnitDto unitDto);
        Task<List<Unit>> GetAllUnits();
        Task<UnitDto> GetUnitByCode(string code);
    }
}
