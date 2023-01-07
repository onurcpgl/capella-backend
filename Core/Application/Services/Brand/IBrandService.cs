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
        Task<bool> Save(BrandDto brandDto);
        Task<BrandDto> GetBrandByCode(string code);
        Task<bool> Update(BrandDto brandDto);
        Task<bool> Delete(string code);
    }
}
