using Domain.Entities.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Variant: BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public DataType ChooseType { get; set; }
        public ICollection<VariantValue> VariantValues { get; set; }
    }
}
