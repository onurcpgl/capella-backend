using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransferObject
{
    public class VariantItemDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public ICollection<ClassificationAttributeValueDto>? ClassificationAttributeValues { get; set; }
        public ICollection<GalleryDto>? Galleries { get; set; }
        public ICollection<VariantValueDto>? VariantValues { get; set; }

    }
}
