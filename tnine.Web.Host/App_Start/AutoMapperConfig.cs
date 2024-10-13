using AutoMapper;
using tnine.Application.Shared.ITodoService.Dto;
using tnine.Core;

namespace tnine.Web.Host.App_Start
{
    public class AutoMapperConfig
    {
        public AutoMapperConfig() { }

        public static AutoMapperConfig Instance { get { return new AutoMapperConfig(); } }

        public void Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateOrEditTodoDto, Todo>().ReverseMap();
            });

            IMapper mapper = config.CreateMapper();
        }
    }
}