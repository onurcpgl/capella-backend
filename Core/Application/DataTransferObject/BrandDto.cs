﻿using Application.DataTransferObject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransferObject
{
    public class BrandDto: BaseDto
    {
        public string? Code { get; set; }

        public string? Name { get; set; }
    }
}
