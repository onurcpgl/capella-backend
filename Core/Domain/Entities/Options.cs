using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Options: CodeBaseEntity
    {
        public string Name { get; set; }
        public Classification Classification { get; set; }
        public ICollection<ClassificationAttributeValue> ClassificationAttributeValues { get; set; }
    }
}
