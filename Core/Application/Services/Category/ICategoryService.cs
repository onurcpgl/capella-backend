using Application.DataTransferObject;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface ICategoryService
    {
        Task<bool> Save(CategoryDto categoryDto);
        Task<List<CategoryListDto>> GetAllCategories();
        Task<bool> ChangeLocationCategory(CategoryReorderDto categoryReorderDto);
        Task<CategoryDto> GetCategoryByCode(string code);
    }
}
