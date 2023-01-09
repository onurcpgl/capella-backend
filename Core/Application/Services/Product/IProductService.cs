using Application.DataTransferObject;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IProductService
    {
        Task Save(ProductDto productDto, List<IFormFile> formFiles);
        Task<List<ProductDto>> GetAllProducts();
        Task Delete(string code);
        Task Update(ProductDto productDto, List<IFormFile> formFiles);
        Task<ProductDto> GetProductByCode(string code);
    }
}
