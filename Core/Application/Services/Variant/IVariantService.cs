using Application.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Variant
{
    public interface IVariantService
    {
        Task<bool> save(VariantDto variantDto);
    }
}
