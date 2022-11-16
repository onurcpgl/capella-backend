using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransferObject
{
    public class CategoryListDto
    {
        public string? key { get; set; }
        public string? label { get; set; }
        public CategoryDto data { get; set; }
        public List<CategoryListDto> children { get; set; }

    }
}
