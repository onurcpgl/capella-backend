using Application.DataTransferObject;
using Application.Repositories;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryWriteRepository _categoryWriteRepository;
        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryReadRepository categoryReadRepository, ICategoryWriteRepository categoryWriteRepository, IMapper mapper)
        {
            _categoryReadRepository = categoryReadRepository;
            _categoryWriteRepository = categoryWriteRepository;
            _mapper = mapper;
        }

        public async Task<bool> saveCategory(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            if (categoryDto.ParentCategory !=null)
            {
                var categoryParent = await _categoryReadRepository.GetWhere(x => x.Code == categoryDto.ParentCategory.Code).FirstOrDefaultAsync();
                category.ParentCategory = categoryParent;
            }
           
            category.Code = Guid.NewGuid().ToString();
            var result = await _categoryWriteRepository.AddAsync(category);
            if (!result)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public async Task<Category> getCategoryById(int id)
        {
            var category = await _categoryReadRepository.GetWhereWithInclude(x => x.Id == id, true, x => x.SubCategories, x => x.ParentCategory).FirstOrDefaultAsync();
            return category;
        }

        public async Task<List<CategoryListDto>> categoryList()
        {
            List<Category> categories = await _categoryReadRepository.GetAll().ToListAsync();

            //Sorting for categories level

            List<CategoryListDto> categoryListDtos = new List<CategoryListDto>();

            categoryListDtos=
                    categories
                     .Where(c => c.ParentCategoryId == null)
                     .OrderBy(x => x.Level)
                     .Select(c => new CategoryListDto
                     {
                         key = (c.Level - 1).ToString(),
                         label=c.Name,
                         data = new CategoryDto
                         {
                             Code = c.Code,
                             Name = c.Name,
                             Level = c.Level,
                             Description = c.Description,
                         },
                         children = GetChildren(c.SubCategories, c.Id, (c.Level - 1).ToString())
                     })
                     .ToList();

            return categoryListDtos;
        }

        private List<CategoryListDto> GetChildren(ICollection<Category>? category, int parentId,string key)
        {

            return category
                    .Where(c => c.ParentCategoryId == parentId)
                    .OrderBy(x => x.Level)
                    .Select(c => new CategoryListDto
                    {
                        label=c.Name,
                        key = key + "-" + (c.Level-1).ToString(),
                        data = new CategoryDto
                        {
                            Code = c.Code,
                            Name = c.Name,
                            Level = c.Level,
                            Description = c.Description,
                        },
                        children = GetChildren(c.SubCategories, c.Id,key+"-"+ (c.Level - 1).ToString())
                    })
                    .ToList();
        }

        public async Task<bool> changeLocationCategory(CategoryReorderDto categoryReorderDto)
        {
           
            var category = await _categoryReadRepository.GetWhere(x => x.Code == categoryReorderDto.sourceCode).FirstOrDefaultAsync();
            category.Level = categoryReorderDto.level;
            var categoryParent = await _categoryReadRepository.GetWhere(x => x.Code == categoryReorderDto.destinationCode).FirstOrDefaultAsync();
            category.ParentCategory = categoryParent;
            var status = await _categoryWriteRepository.Update(category);
            List<Category> categories = await _categoryReadRepository.GetWhereWithInclude(x => categoryReorderDto.level >= x.Level && x.Code != categoryReorderDto.sourceCode &&x.ParentCategory.Code == categoryReorderDto.destinationCode, true, x => x.SubCategories, x => x.ParentCategory).ToListAsync();
            foreach (var item in categories)
            {
                    
                item.Level = item.Level + 1;

                bool updateItem = await _categoryWriteRepository.Update(item);
                if (!updateItem)
                {
                    return false;
                }
            }
            
            return true;

        }
    }
}
