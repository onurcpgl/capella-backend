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

        public async Task Save(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            
            if (categoryDto.ParentCategory !=null)
            {
                var categoryParent = await _categoryReadRepository.GetWhere(x => x.Code == categoryDto.ParentCategory.Code).FirstOrDefaultAsync();
                category.ParentCategory = categoryParent;
            }

            int? maxLevel = await _categoryReadRepository.GetWhere(x => x.ParentCategory == category.ParentCategory).MaxAsync(x=> (int?) x.Level);

            category.Level = (maxLevel ?? 0) + 1;
            category.Code = Guid.NewGuid().ToString();
            await _categoryWriteRepository.AddAsync(category); 
        }

        public async Task<List<CategoryListDto>> GetAllCategories()
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

        public async Task ChangeLocationCategory(CategoryReorderDto categoryReorderDto)
        {
            List<Category> incCategories = new List<Category>();
            List<Category> decCategories = new List<Category>();

            var category = await _categoryReadRepository.GetWhereWithInclude(x => x.Code == categoryReorderDto.sourceCode,true, x => x.ParentCategory).FirstOrDefaultAsync();
            
            int oldLevel = category.Level;
            category.Level = categoryReorderDto.level;

            var categoryParent = await _categoryReadRepository.GetWhere(x => x.Code == categoryReorderDto.destinationCode).FirstOrDefaultAsync();
           
            //kendi ekseni
            if (category.ParentCategory == categoryParent)
            {
                await _categoryWriteRepository.UpdateAsync(category,category.Id);

                incCategories = await _categoryReadRepository.GetWhereWithInclude(x => x.Level >= categoryReorderDto.level && x.Level <= oldLevel
                    && x.Code != categoryReorderDto.sourceCode && x.ParentCategory.Code == categoryReorderDto.destinationCode, true, x => x.ParentCategory).ToListAsync();

                decCategories = await _categoryReadRepository.GetWhereWithInclude(x => x.Level >= oldLevel && x.Level <= categoryReorderDto.level
                   && x.Code != categoryReorderDto.sourceCode && x.ParentCategory.Code == categoryReorderDto.destinationCode, true, x => x.ParentCategory).ToListAsync();

            }
            //Parent dan root a yada root dan Parent a
            else if (category.ParentCategory != categoryParent)
            {

                incCategories = await _categoryReadRepository.GetWhereWithInclude(x => x.Level >= categoryReorderDto.level && x.Code != categoryReorderDto.sourceCode && x.ParentCategory == categoryParent, true, x => x.ParentCategory).ToListAsync();

                decCategories = await _categoryReadRepository.GetWhereWithInclude(x => x.Level >= oldLevel && x.Code != categoryReorderDto.sourceCode && x.ParentCategory == category.ParentCategory, true, x => x.ParentCategory).ToListAsync();

                category.ParentCategory = categoryParent;
                await _categoryWriteRepository.UpdateAsync(category,category.Id);

            }

            incCategories.ForEach(x => x.Level = (x.Level + 1));
            decCategories.ForEach(x => x.Level = (x.Level - 1));

            foreach (var item in incCategories.Concat(decCategories))
            {
                await _categoryWriteRepository.UpdateAsync(item,item.Id);
            }

        }

        public async Task<CategoryDto> GetCategoryByCode(string code)
        {
            var category = await _categoryReadRepository.GetWhereWithInclude(x => x.Code == code, true, x => x.ParentCategory).FirstOrDefaultAsync();
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return categoryDto;
        }
    }
}
