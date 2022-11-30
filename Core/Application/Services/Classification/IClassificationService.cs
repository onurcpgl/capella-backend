using Application.DataTransferObject;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IClassificationService
    {
        Task<bool> saveClassification(ClassificationDto classificationDto);

        Task<bool> saveClassificationAttribute(Classification classification, Domain.Entities.Unit unit);

        Task<bool> saveClassificationAttributeValue(ClassificationAttributeValueDto classificationAttributeValueDto);

        Task<List<Classification>> getAll();

        Task<List<Classification>> getClassificationByCategory(List<CategoryDto> categoryDtos);
    }
}
