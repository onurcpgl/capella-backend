using Application.DataTransferObject;
using Application.Repositories;
using Application.Services;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class OptionsService: IOptionsService
    {
        private readonly IOptionsReadRepository _optionsReadRepository;
        private readonly IOptionsWriteRepository _optionsWriteRepository;
        private readonly IClassificationReadRepository _classificationReadRepository;
        public OptionsService(IOptionsReadRepository optionsReadRepository, IOptionsWriteRepository optionsWriteRepository, IClassificationReadRepository classificationReadRepository)
        {
            _optionsReadRepository = optionsReadRepository;
            _optionsWriteRepository = optionsWriteRepository;
            _classificationReadRepository = classificationReadRepository;
        }


        public async Task<Options> Save(OptionsDto optionsDto, string Code)
        {
            Options options = new();

            options.Code = Guid.NewGuid().ToString();
            options.Name = optionsDto.Name;

            var classification = _classificationReadRepository.GetWhere(x => x.Code == Code).FirstOrDefault();
            options.Classification = classification;

            var result = await _optionsWriteRepository.AddAsync(options);
            if (!result)
            {
                return null;
            }
            return options;
        }
    }
}
