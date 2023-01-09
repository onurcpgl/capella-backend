using Application.Repositories;
using Application.Repositories.ProductAbstract;
using Application.Services;
using Application.Services.Permission;
using Application.Services.Token;
using Application.Services.Variant;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories;
using Persistence.Services;
using Persistence.Services.Permission;
using Persistence.Services.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            #region Connection String
            services.AddDbContext<CapellaDbContext>(options => options.UseNpgsql(Configuration.ConnectionString));
            #endregion

            #region Repository Registration
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();
            services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
            services.AddScoped<IClassificationWriteRepository, ClassificationWriteRepository>();
            services.AddScoped<IClassificationReadRepository, ClassificationReadRepository>();
            services.AddScoped<IUnitReadRepository, UnitReadRepository>();
            services.AddScoped<IUnitWriteRepository, UnitWriteRepository>();  
            services.AddScoped<IClassificationAttributeValueReadRepository, ClassificationAttributeValueReadRepository>();  
            services.AddScoped<IClassificationAttributeValueWriteRepository, ClassificationAttributeValueWriteRepository>();
            services.AddScoped<IMediaWriteRepository, MediaWriteRepository>();
            services.AddScoped<IMediaReadRepository, MediaReadRepository>();
            services.AddScoped<IMediaFormatReadRepository, MediaFormatReadRepository>();
            services.AddScoped<IUserReadRepository, UserReadRepository>();
            services.AddScoped<IUserWriteRepository, UserWriteRepository>();    
            services.AddScoped<IAddressReadRepository, AddressReadRepository>();    
            services.AddScoped<IAddressWriteRepository, AddressWriteRepository>();  
            services.AddScoped<IRoleReadRepository, RoleReadRepository>();  
            services.AddScoped<IRoleWriteRepository, RoleWriteRepository>();    
            services.AddScoped<IPermissionReadRepository, PermissionReadRepository>();
            services.AddScoped<IPermissionWriteRepository, PermissionWriteRepository>();
            services.AddScoped<IOptionsReadRepository, OptionsReadRepository>();
            services.AddScoped<IOptionsWriteRepository, OptionsWriteRepository>();
            services.AddScoped<IVariantReadRepository, VariantReadRepository>();
            services.AddScoped<IVariantWriteRepository, VariantWriteRepository>();
            services.AddScoped<IVariantValueReadRepository, VariantValueReadRepository>();
            services.AddScoped<IVariantValueWriteRepository, VariantValueWriteRepository>();
            services.AddScoped<IVariantItemReadRepository, VariantItemReadRepository>();
            services.AddScoped<IVariantItemWriteRepository, VariantItemWriteRepository>();
            services.AddScoped<IBrandReadRepository, BrandReadRepository>();
            services.AddScoped<IBrandWriteRepository, BrandWriteRepository>();
            services.AddScoped<ITagReadRepository, TagReadRepository>();
            services.AddScoped<ITagWriteRepository, TagWriteRepository>();
            #endregion

            #region Service Registration
            services.AddScoped<IClassificationService, ClassificationService>();
            services.AddScoped<IMediaService, MediaService>();
            services.AddScoped<IUnitService, UnitService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IClassificationAttributeValueService, ClassificationAttributeValueService>();
            services.AddScoped<IOptionsService, OptionsService>();
            services.AddScoped<IVariantService, VariantService>();
            services.AddScoped<IVariantItemService, VariantItemService>();
            services.AddScoped<IVariantValueService, VariantValueService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<ITokenService, TokenService>();
            #endregion

            
        }
    }
}
