
using API.Repositores;
using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using API.SignalR;
using API.UnitOfWork;
using API.SignalR.HubServices;

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
           services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
           services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
           services.AddScoped<IPhotoService, PhotoService>();
           services.AddScoped<LogUserActivity>();
           services.AddSignalR();
           services.AddSingleton<PresenceTracker>();           
           services.AddScoped<IUnitOfWork,UnitOfWork.UnitOfWork>();
           return services;
       } 
    }
}