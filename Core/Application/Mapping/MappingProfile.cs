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
            CreateMap<Category[], CategoryDto[]>().ReverseMap();

            #endregion

            #region Classification Mapper

            CreateMap<Classification, ClassificationDto>().ReverseMap();
           

            #endregion

            #region Unit Mapper

            CreateMap<Unit, UnitDto>().ReverseMap();

            #endregion

            #region User Mapper
            CreateMap<User, UserDto>()
            .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.Roles.SelectMany(r => r.Permissions)));
            #endregion

            #region Role Mapper
            CreateMap<Role, RoleDto>();
            #endregion

            #region Permission Mapper
            CreateMap<Permission, PermissionDto>();
            #endregion
        }
    }
}
