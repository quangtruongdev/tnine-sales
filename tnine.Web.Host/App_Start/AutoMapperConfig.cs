using AutoMapper;
using System.Linq;
using tnine.Application.Shared.Authorization.IPermissionService.Dto;
using tnine.Application.Shared.IRoleService.Dto;
using tnine.Application.Shared.IUserService.Dto;
using tnine.Application.Shared.ICustomerService.Dto;
using tnine.Application.Shared.ISizeService.Dto;
using tnine.Application.Shared.IProductService.Dto;
using tnine.Application.Shared.IProductVariationDto.Dto;
using tnine.Application.Shared.IInvoiceService.Dto;
using tnine.Application.Shared.ITodoService.Dto;
using tnine.Core;
using tnine.Core.Shared.IColorService.Dto;
using tnine.Application.Shared.IShopService.Dto;
using tnine.Application.Shared.IPaymentMethodsService.Dto;
using tnine.Application.Shared.IPaymentStatusService.Dto;

namespace tnine.Web.Host.App_Start
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ApplicationUser, GetApplicationUserForViewDto>().ReverseMap();
            CreateMap<ApplicationUser, CreateOrEditApplicationUserDto>().ReverseMap();
            CreateMap<ApplicationRole, CreateOrEditRoleDto>()
                .ForMember(dest => dest.PermissionIds, opt => opt.MapFrom(src => src.RolePermissions.Select(p => p.PermissionId)))
                .ReverseMap()
                .ForMember(dest => dest.RolePermissions, opt => opt.Ignore());
            CreateMap<Permission, CreateOrEditPermissionDto>().ReverseMap();

            CreateMap<Customer, CreateOrEditCustomerDto>().ReverseMap();
            CreateMap<Todo, CreateOrEditTodoDto>().ReverseMap();
            CreateMap<Colors, CreateOrEditColorDto>().ReverseMap();
            CreateMap<Sizes, CreateOrEditSizeDto>().ReverseMap();
            CreateMap<Product, CreateOrEditProductDto>().ReverseMap();
            CreateMap<ProductVariations, CreateOrEditProductVariayionDto>().ReverseMap();
            CreateMap<PaymentStatus, CreateOrEditPaymentStatusDto>().ReverseMap();
            CreateMap<PaymentMethods, CreateOrEditPaymentMethodsDto>().ReverseMap();
            CreateMap<Invoice, CreateOrEditInvoiceDto>().ReverseMap();
            CreateMap<Shop, CreateOrEditShopDto>().ReverseMap();
        }
    }

    public static class AutoMapperConfig
    {
        // Cấu hình singleton cho AutoMapper
        private static MapperConfiguration _mapperConfiguration;

        public static void Configure()
        {
            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
        }

        // Tạo phương thức để lấy IMapper
        public static IMapper GetMapper()
        {
            if (_mapperConfiguration == null)
            {
                Configure();
            }
            return _mapperConfiguration.CreateMapper();
        }

        // Cấu hình MapperConfiguration cho DI
        public static MapperConfiguration GetConfiguration()
        {
            if (_mapperConfiguration == null)
            {
                Configure();
            }
            return _mapperConfiguration;
        }
    }
}
