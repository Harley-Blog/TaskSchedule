using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BC.TS.Api.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class AppCodeAuthorizeFilter : Attribute, IAuthorizationFilter
    {
        private readonly IConfiguration _configuration;
        public AppCodeAuthorizeFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            context.HttpContext.Request.Headers.TryGetValue("AppCode", out var value);
            if (value.ToString() != _configuration["AppSettings:Authentication:AppCode"].ToString())
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
