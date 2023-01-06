using Domain.Entities;
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
        public string Code { get; set; }
        public string? Name { get; set; }
        public DataType? DataType { get; set; }
        public ICollection<OptionsDto>? Options { get; set; }
    }
}
