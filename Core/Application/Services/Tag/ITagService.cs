using Application.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface ITagService
    {
        Task Save(TagDto tagDto);
        Task<TagDto> GetTagByCode(string code);
        Task<List<TagDto>> GetAllTags();
        Task Update(TagDto tagDto);
        Task Delete(string code);
    }
}
