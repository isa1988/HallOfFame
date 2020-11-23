using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace HallOfFame.WebAPI.AppStart.AutoMapper
{
    public static class AutoMapperServiceExtension
    {
        public static void AddAutoMapperCustom(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new HallOfFame.Service.MappingProfile());
                mc.AddProfile(new HallOfFame.WebAPI.AppStart.AutoMapper.MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
