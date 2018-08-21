using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Repository;
using WebApplication.Repository.Entities;

namespace WebApplication.Api
{
    public partial class Startup
    {
        public void InitializeDb(IApplicationBuilder app, IHostingEnvironment env)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<WebApplicationDbContext>();
                    WebApplicationInitializer.Initialize(context);
                    if (!context.Users.Any(x => x.Username == "admin"))
                    {
                        var passwordHasher = services.GetRequiredService<IPasswordHasher<User>>();
                        var user = new User
                        {
                            Username = "admin",
                        };
                        user.Password = passwordHasher.HashPassword(user, "admin");
                        context.Users.Add(user);
                        context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    
                }
            }
        }
    }
}
