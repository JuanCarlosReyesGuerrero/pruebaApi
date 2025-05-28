using Commun.Helpers;
using Commun.Logger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Data;
using RepositoryLayer.IRepository;
using RepositoryLayer.Repository;
using ServiceLayer.IService;
using ServiceLayer.Service;
using System.Text;

namespace DependencyInjection
{
    public static class InyectarDependencia
    {
        public static void ConexionDataBases(this IServiceCollection services, IConfiguration Configuration)
        {
            string dbConnectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(dbConnectionString));

            services.AddScoped<DbInitializer>();
        }

        public static void AutenticacionJwt(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddControllers();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,

                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,

                    ValidAudience = JwtSettings.Jwt_Audience,
                    ValidIssuer = JwtSettings.Jwt_Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.Jwt_SecretPassword))
                };
            });
        }

        public static void InyeccionServicios(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddScoped(typeof(ICreateLogger), typeof(CreateLogger));

            services.AddScoped(typeof(IBackOfficeRepository), typeof(BackOfficeRepository));
            services.AddScoped(typeof(IPermisoRepository), typeof(PermisoRepository));
            services.AddScoped(typeof(IModuloRepository), typeof(ModuloRepository));
            services.AddScoped(typeof(IPerfilRepository), typeof(PerfilRepository));
            services.AddScoped(typeof(IUsuarioRepository), typeof(UsuarioRepository));
            services.AddScoped(typeof(ITipoDocumentoRepository), typeof(TipoDocumentoRepository));

            services.AddScoped(typeof(IBackOfficeService), typeof(BackOfficeService));
            services.AddScoped(typeof(IPermisoService), typeof(PermisoService));
            services.AddScoped(typeof(IModuloService), typeof(ModuloService));
            services.AddScoped(typeof(IPerfilService), typeof(PerfilService));
            services.AddScoped(typeof(IConvertPdfService), typeof(ConvertPdfService));
            services.AddScoped(typeof(IUsuarioService), typeof(UsuarioService));
            services.AddScoped(typeof(ITipoDocumentoService), typeof(TipoDocumentoService));
            services.AddScoped(typeof(IHelperService), typeof(HelperService));

            services.AddTransient<IAuthToken, AuthToken>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        public static void UserIdentity(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}