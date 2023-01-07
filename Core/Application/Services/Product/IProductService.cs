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
        //Task<bool> saveProduct(ProductDto productDto, List<IFormFile> formFiles);
        Task<bool> Save(ProductDto productDto);
        Task<List<Product>> GetAllProducts();
        Task<ProductDto> GetProductByCode(string code);
    }
}
