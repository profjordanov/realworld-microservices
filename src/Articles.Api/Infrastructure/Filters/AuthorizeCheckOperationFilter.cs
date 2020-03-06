using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Articles.Api.Infrastructure.Filters
{
    public class AuthorizeCheckOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var hasAuthorize = context
                                   .MethodInfo
                                   .DeclaringType != null && 
                               (context
                                    .MethodInfo
                                    .DeclaringType
                                    .GetCustomAttributes(true)
                                    .OfType<AuthorizeAttribute>().Any() || 
                                context
                                    .MethodInfo
                                    .GetCustomAttributes(true)
                                    .OfType<AuthorizeAttribute>().Any());

            if (!hasAuthorize)
            {
                return;
            }

            operation.Responses.TryAdd("401", new OpenApiResponse { Description = "Unauthorized" });
            operation.Responses.TryAdd("403", new OpenApiResponse { Description = "Forbidden" });

            var oAuthScheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "oauth2",
                    Type = ReferenceType.SecurityScheme,
                }
            };

            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement
                {
                    [ oAuthScheme ] = new [] { "articlesapi" }
                }
            };
        }
    }
}