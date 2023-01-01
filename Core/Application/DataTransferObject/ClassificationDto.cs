using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DataTransferObject
{
    public class ClassificationDto
    {
        public string? Name { get; set; }

        public DataType? DataType { get; set; }
        public ICollection<CategoryDto>? Categories { get; set; }
        public ICollection<ClassificationAttributeDto> ClassificationAttributes { get; set; }
    }
}
