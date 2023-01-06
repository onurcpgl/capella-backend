using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransferObject
{
    public class ClassificationAttributeValueDto
    {
        public string? Value { get; set; }
        public ClassificationDto? Classification { get; set; }
        public ICollection<OptionsDto>? Options { get; set; }
    }
}
