using AutoMapper;
using tnine.Application.Shared.Authorization.IPermissionService.Dto;
using tnine.Application.Shared.IApplicationRoleService.Dto;
using tnine.Application.Shared.IApplicationUserService.Dto;
using tnine.Application.Shared.ITodoService.Dto;
using tnine.Core;

namespace tnine.Web.Host.App_Start
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ApplicationUser, GetApplicationUserForViewDto>().ReverseMap();
            CreateMap<ApplicationUser, CreateOrEditApplicationUserDto>().ReverseMap();
            CreateMap<ApplicationRole, CreateOrEditRoleDto>().ReverseMap();
            CreateMap<Permission, CreateOrEditPermissionDto>().ReverseMap();

            CreateMap<Todo, CreateOrEditTodoDto>().ReverseMap();
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
