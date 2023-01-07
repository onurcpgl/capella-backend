using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ClassificationAttributeValue : CodeBaseEntity
    {
        public string? Value { get; set; }
        public Classification Classification { get; set; }
        public ICollection<Options> Options { get; set; }
        public ICollection<VariantItem> VariantItems { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
