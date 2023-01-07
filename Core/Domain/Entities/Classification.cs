using Domain.Entities.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Classification: CodeBaseEntity
    {
        public string Name { get; set; }
        public DataType DataType { get; set; }
        public ICollection<Options> Options { get; set; }   

    }
}
