using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.Services;

namespace Store.Bootstrap
{
    internal class DependencyInjectionBootstraper
    {
        internal void Setup(IServiceCollection services, IConfiguration configuration)
        {
            SetupCommonModule(services, configuration);
            SetupDatabase(services, configuration);
        }

        private void SetupCommonModule(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IUserService, UserService>();
        }

        private void SetupDatabase(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
