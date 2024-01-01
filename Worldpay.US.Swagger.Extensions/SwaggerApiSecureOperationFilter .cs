using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Worldpay.US.Swagger.Extensions;

/// <summary>
/// Only show the lock icon on Operations that do not have the AllowAnonymousAttribute
/// </summary>
public class SwaggerApiSecureOperationFilter : IOperationFilter
{

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.GetControllerAndActionAttributes<AllowAnonymousAttribute>().Any())
        {
            return;
        }

        operation.Security = new List<OpenApiSecurityRequirement>()
        {
            new OpenApiSecurityRequirement{
                {
                    new OpenApiSecurityScheme{
                        Reference = new OpenApiReference{
                            Id = JwtBearerDefaults.AuthenticationScheme,
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    new List<string>()
                }
            }
        };
    }
}