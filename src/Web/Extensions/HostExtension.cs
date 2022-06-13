using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Extensions
{
    public static class HostExtension
    {
        public async static Task SeedRolesAndUsersAsync(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                await AppIdentityDbContextSeed.SeedAsync(roleManager, userManager);
            }
        }

        public async static Task SeedProductsAsync(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var storeContext = scope.ServiceProvider.GetRequiredService<StoreContext>();
                await StoreContextSeed.SeedAsync(storeContext);
            }
        }
    }
}
