
using API.Classes;
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extinsions
{
    public static class ApplicationServiceExtinsions
    {
       public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration config)
       {
          services.AddDbContext<DataContext>(op =>{

            op.UseSqlite(config.GetConnectionString("DefaultConnection"));
           });


           services.AddCors();
           services.AddScoped<ITokenService, TokenService>();
           services.AddScoped<IUserRepository, UserRepository>();
           services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
           return services;
       } 
    }
}