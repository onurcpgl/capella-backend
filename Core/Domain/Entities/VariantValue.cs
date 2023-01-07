﻿using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class VariantValue: BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public Variant Variant { get; set; }
        public ICollection<VariantItem> VariantItems { get; set; }
    }
}