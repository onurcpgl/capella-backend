using Application.DataTransferObject;
using Application.Repositories;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class TagService : ITagService
    {
        private readonly ITagReadRepository _tagReadRepository;
        private readonly ITagWriteRepository _tagWriteRepository;
        private readonly IMapper _mapper;

        public TagService(ITagReadRepository tagReadRepository, ITagWriteRepository tagWriteRepository, IMapper mapper)
        {
            _tagReadRepository = tagReadRepository;
            _tagWriteRepository = tagWriteRepository;
            _mapper = mapper;
        }

        public async Task<List<TagDto>> GetAllTags()
        {
            var tags = _tagReadRepository.GetAll().ToList();
            var tagsDto = _mapper.Map<List<TagDto>>(tags);
            return tagsDto;
        }

        public async Task<TagDto> GetTagByCode(string code)
        {
            var tag = _tagReadRepository.GetWhere(x => x.Code == code).FirstOrDefault();
            var tagDto = _mapper.Map<TagDto>(tag);
            return tagDto;
        }

        public async Task Save(TagDto tagDto)
        {
            Tag tag = new();

            tag.Code = Guid.NewGuid().ToString();
            tag.Name = tagDto.Name;

            await _tagWriteRepository.AddAsync(tag);
          
        }
    }
}
