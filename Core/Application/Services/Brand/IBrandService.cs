using Application.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IBrandService
    {
        Task Save(BrandDto brandDto);
        Task<BrandDto> GetBrandByCode(string code);
        Task Update(BrandDto brandDto);
        Task Delete(string code);
        Task<List<BrandDto>> GetAllBrands();
    }
}
