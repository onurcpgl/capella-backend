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
            CreateMap<User, UserDto>().ReverseMap();
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
            CreateMap<ClassificationAttributeValue, ClassificationAttributeValueDto>().ReverseMap();
            #endregion

            #region Options Mapper
            CreateMap<Options, OptionsDto>().ReverseMap();
            #endregion

            #region Variant Mapper
            CreateMap<Variant, VariantDto>().ReverseMap();
            #endregion

            #region VariantValue Mapper
            CreateMap<VariantValue, VariantValueDto>().ReverseMap();
            #endregion

            #region VariantItem Mapper
            CreateMap<VariantItem, VariantItemDto>().ReverseMap();
            #endregion

            #region Brand Mapper
            CreateMap<Brand, BrandDto>().ReverseMap();
            #endregion

            #region Tag Mapper
            CreateMap<Tag, TagDto>().ReverseMap();
            #endregion

        }
    }
}
