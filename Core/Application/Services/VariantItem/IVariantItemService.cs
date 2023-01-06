using Application.DataTransferObject;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IVariantItemService
    {
        Task<VariantItem> Save(VariantItemDto variantItemDto);
    }
}
