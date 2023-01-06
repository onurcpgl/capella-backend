using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransferObject
{
    public class VariantDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public DataType? ChooseType { get; set; }
        public ICollection<VariantValueDto>? VariantValues { get; set; }
    }
}
