using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<DAL.AllDbContext>();
            services.AddTransient<BL.DbContextLoader>();
            services.AddTransient<Services.CloneItemsSerivce>();
            services.AddSingleton<Services.FieldsGenerator>();
        }
    }
}
