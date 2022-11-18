using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransferObject
{
    public class CategoryReorderDto
    {
        public string sourceCode { get; set; }

        public string? destinationCode { get; set; }

        public int level { get; set; }
    }
}
