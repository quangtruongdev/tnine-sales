using AutoMapper;
using System.Linq;
using tnine.Application.Shared.Authorization.IPermissionService.Dto;
using tnine.Application.Shared.Authorization.IUserService.Dto;
using tnine.Application.Shared.ICategoryService.Dto;
using tnine.Application.Shared.ICustomerService.Dto;
using tnine.Application.Shared.IInvoiceService.Dto;
using tnine.Application.Shared.IOrderService.Dto;
using tnine.Application.Shared.IPaymentMethodsService.Dto;
using tnine.Application.Shared.IPaymentStatusService.Dto;
using tnine.Application.Shared.IProductService.Dto;
using tnine.Application.Shared.IProductVariationService.Dto;
using tnine.Application.Shared.IRoleService.Dto;
using tnine.Application.Shared.IShopService.Dto;
using tnine.Application.Shared.ISizeService.Dto;
using tnine.Application.Shared.ISupplierService.Dto;
using tnine.Application.Shared.ITodoService.Dto;
using tnine.Core;
using tnine.Core.Shared.IColorService.Dto;
using tnine.Web.Host.Models;


namespace tnine.Web.Host.App_Start
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ApplicationUser, GetUserForViewDto>().ReverseMap();
            CreateMap<ApplicationUser, CreateOrEditUserDto>().ReverseMap();
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
            CreateMap<ProductVariations, CreateOrEditProductVariaionDto>().ReverseMap();
            CreateMap<PaymentStatus, CreateOrEditPaymentStatusDto>().ReverseMap();
            CreateMap<PaymentMethods, CreateOrEditPaymentMethodsDto>().ReverseMap();
            CreateMap<Invoice, CreateOrEditInvoiceDto>().ReverseMap();
            CreateMap<Shop, CreateOrEditShopDto>().ReverseMap();
            CreateMap<Categories, CreateOrEditCategoryDto>().ReverseMap();
            CreateMap<Orders, CreateOrEditOrderDto>().ReverseMap();

            CreateMap<GetColorForViewDto, ColorViewModel>();
            CreateMap<GetProductForViewDto, ProductViewModel>();
            CreateMap<GetProductVariationForViewDto, ProductVariationViewModel>();

            //CreateMap<GetSizeForViewDto, SizeViewModel>();
            //CreateMap<CreateOrEditProductDto, ProductViewModel>();
            CreateMap<Suppliers, CreateOrEditSupplierDto>().ReverseMap();
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
