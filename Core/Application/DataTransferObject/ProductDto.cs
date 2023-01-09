using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransferObject
{
    public class ProductDto
    {
        public string Code { get; set; }
        public string? Name { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public DateTime? Startdate { get; set; }
        public DateTime? Enddate { get; set; }
        public string? Description { get; set; }
        public ICollection<CategoryDto>? Categories { get; set; }
        public ICollection<ClassificationAttributeValueDto>? ClassificationAttributeValues { get; set; }
        public ICollection<VariantItemDto>? VariantItems { get; set; }
        public ICollection<GalleryDto>? Galleries { get; set; }
        public BrandDto? Brand  { get; set; }
        public ICollection<TagDto>? Tags { get; set; }
    }
}
