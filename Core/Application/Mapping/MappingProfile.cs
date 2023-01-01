using Application.DataTransferObject;
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

            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Classifications, opt => opt.MapFrom(src => src.Categories.SelectMany(c => c.Classifications)))
                .ReverseMap();
            #endregion

            #region Category Mapper

            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category[], CategoryDto[]>().ReverseMap();

            #endregion

            #region Classification Mapper

            CreateMap<Classification, ClassificationDto>()
                .ForMember(dest => dest.Categories, opt => opt.Ignore()).ReverseMap();
           

            #endregion

            #region Unit Mapper

            CreateMap<Unit, UnitDto>().ReverseMap();

            #endregion

            #region User Mapper
            CreateMap<User, UserDto>()
            .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.Roles.SelectMany(r => r.Permissions)));
            #endregion

            #region Role Mapper
            CreateMap<Role, RoleDto>().ReverseMap();
            #endregion

            #region Permission Mapper
            CreateMap<Permission, PermissionDto>().ReverseMap();
            #endregion

            #region Media Mapper
            CreateMap<Media, MediaDto>().ReverseMap();
            #endregion

            #region Gallery Mapper
            CreateMap<Gallery, GalleryDto>().ReverseMap();
            #endregion

            #region ClassificationAttributeValue Mapper
            CreateMap<ClassificationAttributeValue, ClassificationAttributeValueDto>()
                 .ForMember(dto => dto.Classification, opt => opt.MapFrom(src => src.ClassificationAttribute.Classifications))
                .ReverseMap();
            #endregion

            #region ClassificationAttribute Mapper
            CreateMap<ClassificationAttribute, ClassificationAttributeDto>().ReverseMap();
            #endregion
        }
    }
}
