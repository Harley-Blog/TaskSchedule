using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BC.TS.Api.Filters
{
    /// <summary>
    /// Swagger custome filter
    /// </summary>
    public class AddRequiredHeaderParameterFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var filterPipeline = context.ApiDescription.ActionDescriptor.FilterDescriptors;
            var allowAnonymous = filterPipeline.Select(filterInfo => filterInfo.Filter).Any(filter => filter is IAllowAnonymousFilter);

            var typeFilterAttribute = filterPipeline.Select(filterInfo => filterInfo.Filter).FirstOrDefault(filter => filter is TypeFilterAttribute) as TypeFilterAttribute;
            var isAuthorized = typeFilterAttribute != null && typeFilterAttribute.ImplementationType == typeof(AppCodeAuthorizeFilter);
            if (isAuthorized && !allowAnonymous)
            {
                if (operation.Parameters == null)
                    operation.Parameters = new List<OpenApiParameter>();

                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "AppCode",
                    In = ParameterLocation.Header,
                    Description = "Authorization",
                    Required = true,
                    Schema = new OpenApiSchema
                    {
                        Type = "string",
                        Default = new OpenApiString("")
                    }
                });
            }
        }
    }
}
