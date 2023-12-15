using CRM.Services.Abstract;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Http.Features;

namespace CRM.Services.Filter
{
    public class CustomAuthorizeFilter : IAuthorizationFilter
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;

        public CustomAuthorizeFilter(ITokenService tokenService, IUserService userService)
        {
            _tokenService = tokenService;
            _userService = userService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var allowedPathStarts = new[] { "/Login" };

            var httpRequestFeature = context.HttpContext.Features.Get<IHttpRequestFeature>();
            var currentPath = httpRequestFeature?.Path ?? string.Empty;

            if(currentPath=="/")
            {
                return;
            }

            // Mevcut yol izin verilen yollar listesindeyse, yetkilendirme yapma
            if (allowedPathStarts.Any(allowedPath => currentPath.StartsWith(allowedPath, StringComparison.OrdinalIgnoreCase)))
            {
                // İzin verilen yollardan biriyse yetkilendirme kontrolü yapma
                return;
            }

            bool isAuthorized = CheckUserAuthorization(context);
             
            if (!isAuthorized)
            {
                context.Result = new ForbidResult(); // Yetkisiz erişimler için
            }
        }

        private bool CheckUserAuthorization(AuthorizationFilterContext context)
        {
            var authHeader = context.HttpContext.Request.Headers["Authorization"];

            if (!StringValues.IsNullOrEmpty(authHeader))
            {
                var authHeaderString = authHeader.ToString();

                var guid = authHeaderString.Replace("Bearer ", "");
                var userParameter = _userService.GetUserParameters(guid);
                if (userParameter != null)
                {
                    var validated = _tokenService.ValidateToken(userParameter.Token, userParameter.SecretKey);
                    if (validated)
                    {
                        return true;
                    }
                }

                return false;
            }
            else
            {
                var userParameter = _userService.GetUserParameters("UserParameters");
                if (userParameter != null)
                {
                    var validated = _tokenService.ValidateToken(userParameter.Token, userParameter.SecretKey);
                    if (validated)
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}
