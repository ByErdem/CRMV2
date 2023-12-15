using CRM.Data.Abstract;
using CRM.Data.Concrete;
using CRM.Data.Concrete.EntityFramework.Contexts;
using CRM.Services.Abstract;
using CRM.Services.Automapper.Profiles;
using CRM.Services.Concrete;
using CRM.Services.Filter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CRM.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadServices(this IServiceCollection sc, IConfiguration configuration)
        {
            sc.AddJwt(configuration);
            sc.AddDbContext<CRMContext>();
            sc.AddScoped<CustomAuthorizeFilter>();
            sc.AddSingleton<IEncryptionService, EncryptionManager>();
            sc.AddSingleton<IHttpService, HttpManager>();
            sc.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            sc.AddSingleton<ISessionService, SessionManager>();
            sc.AddSingleton<ITokenService, TokenManager>();
            sc.AddScoped<IMenuService,MenuManager>();
            sc.AddScoped<IRoleService, RoleManager>();
            sc.AddScoped<IHomeService, HomeManager>();
            sc.AddScoped<IUserService, UserManager>();
            sc.AddScoped<IUnitOfWork, UnitOfWork>();
            sc.AddAutoMapper(typeof(MappingProfile));

            sc.AddControllersWithViews(opt =>
            {
                opt.Filters.Add(new ServiceFilterAttribute(typeof(CustomAuthorizeFilter)));
            }).AddRazorRuntimeCompilation().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                
            });

            sc.AddSession(options =>
            {
                // Session ayarlarınızı burada yapabilirsiniz, örneğin:
                options.IdleTimeout = TimeSpan.FromDays(1); // Session süresini 1 gün olarak ayarla
                options.Cookie.HttpOnly = true; // Güvenlik için önemlidir, JavaScript'in cookie'ye erişmesini önler
                options.Cookie.IsEssential = true; // GDPR kapsamında zorunlu cookie olarak işaretler
            });

            sc.AddHttpContextAccessor();

            return sc;
        }
    }
}
