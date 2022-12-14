﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransferObject
{
    public class AddProductRequest
    {
        public List<IFormFile> Galleries { get; set; }

        public ProductDto ProductData { get; set; }
    }
}
