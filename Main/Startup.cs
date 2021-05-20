using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using DAL;

namespace Main
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<AllDbContext>();
            services.AddTransient<LoginService>();
            services.AddTransient<MapperService>();
            services.AddSingleton<UserService>();
            services.AddTransient<DbContextLoader>();
            services.AddSingleton<IAutoListService, AutoListService>();
            services.AddTransient<IPhoneConfirmService, PhoneConfirmService>();
            services.AddSingleton<UserRegisterService>();
            services.AddSingleton<DeclarationService>();
            services.AddSingleton<ClientPipeHanlder>();
            services.AddTransient<Services.UpdateHandlerService>();
        }
    }
}
