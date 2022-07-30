using Microsoft.Extensions.DependencyInjection;

namespace FDX.Services
{
    public static class AutoMapperSetup
    {
        public static void AddAutoMappingSetup(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddAutoMapper(typeof(MappingProfile).Assembly);
        }
    }
}
