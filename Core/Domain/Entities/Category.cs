using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Category: CodeBaseEntity
    {
        public Category()
        {
            SubCategories=new List<Category>();
        }
        public string Name { get; set; }

        public string Description { get; set; }

        public Category? ParentCategory { get; set; }

        public int? ParentCategoryId { get; set; }

        public int Level { get; set; }

        public ICollection<Category>? SubCategories { get; set; }

        public ICollection<Product> Products { get; set; }

    }
}
