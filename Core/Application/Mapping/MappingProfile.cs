﻿using Application.DataTransferObject;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Product Mapper

            CreateMap<Product, ProductDto>().ReverseMap();


            #endregion


            #region Category Mapper

            CreateMap<Category, CategoryDto>().ReverseMap();

            #endregion
        }
    }
}