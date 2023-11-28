
using System.Text;
using API.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Entities
{
   public static class IdentityServiceExtinsions
   {
      public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration config)
      {

         services.AddIdentityCore<AppUser>(opt =>
         {
            opt.Password.RequireNonAlphanumeric = false;
         })
          .AddRoles<AppRole>()
          .AddRoleManager<RoleManager<AppRole>>()
          .AddEntityFrameworkStores<DataContext>();
          
         services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
         .AddJwtBearer(option =>
         option.TokenValidationParameters = new TokenValidationParameters
         {

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
              Encoding.UTF8.GetBytes(config["TokenKey"])),
            ValidateIssuer = false,
            ValidateAudience = false
         }
         );

          services.AddAuthorization(opt =>
          {
            opt.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
            opt.AddPolicy("ModeratePhotoRole", policy => policy.RequireRole("Admin", "Moderator"));
          });

         return services;
      }
   }
}