﻿using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Permission : CodeBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; } 
        public ICollection<Role> Roles { get; set; }
        
    }
}
