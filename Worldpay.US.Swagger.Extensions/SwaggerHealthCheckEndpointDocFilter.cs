using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

using static System.Text.Json.JsonNamingPolicy;

namespace Worldpay.US.Swagger.Extensions;

/// <summary>
/// This Swashbuckle Doc Filter injects an "un-versioned" endpoint into all swagger versioned pages exposing the OOB Health Check functionality
/// </summary>
/// <remarks>
/// Normally this endpoint is not visible in swagger
/// 
/// Add this line to the .AddSwaggerGen() method
///     options.DocumentFilter<SwaggerHealthCheckEndpointDocFilter>();
/// </remarks>
public class SwaggerHealthCheckEndpointDocFilter : IDocumentFilter
{
    #region === obs stuff ===
    /// <summary>
    /// 
    /// </summary>
    //private readonly global::HealthChecks.UI.Configuration.Options Options;

    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="Options"></param>
    //public SwaggerHealthCheckEndpointDocFilter(IOptions<global::HealthChecks.UI.Configuration.Options> Options)
    //{
    //    this.Options = Options?.Value ?? throw new ArgumentNullException(nameof(Options));
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="SwaggerDoc"></param>
    ///// <param name="Context"></param>
    //public void Apply(OpenApiDocument SwaggerDoc, DocumentFilterContext Context)
    //{
    //    var PathItem = new OpenApiPathItem
    //    {
    //        Operations = new Dictionary<OperationType, OpenApiOperation>
    //        {
    //            [OperationType.Get] = new OpenApiOperation
    //            {
    //                Description = "Returns all the health states used by this Microservice",
    //                Tags =
    //                {
    //                    new OpenApiTag
    //                    {
    //                        Name = "HealthCheck"
    //                    }
    //                },
    //                Responses =
    //                {
    //                    [StatusCodes.Status200OK.ToString()] = new OpenApiResponse
    //                    {
    //                        Description = "API is healthy",
    //                        Content =
    //                        {
    //                            ["application/json"] = new OpenApiMediaType
    //                            {
    //                                Schema = new OpenApiSchema
    //                                {
    //                                    Reference = new OpenApiReference
    //                                    {
    //                                        Id = nameof(HealthCheckExecution),
    //                                        Type = ReferenceType.Schema,
    //                                    }
    //                                }
    //                            }
    //                        }
    //                    },
    //                    [StatusCodes.Status503ServiceUnavailable.ToString()] = new OpenApiResponse
    //                    {
    //                        Description = "API is not healthy"
    //                    }
    //                }
    //            }
    //        }
    //    };

    //    var HealthCheckSchema = new OpenApiSchema
    //    {
    //        Type = "object",
    //        Properties =
    //        {
    //            [CamelCase.ConvertName(nameof(HealthCheckExecution.Id))] = new OpenApiSchema
    //            {
    //                Type = "integer",
    //                Format = "int32"
    //            },
    //            [CamelCase.ConvertName(nameof(HealthCheckExecution.Status))] = new OpenApiSchema
    //            {
    //                Type = "string"
    //            },
    //            [CamelCase.ConvertName(nameof(HealthCheckExecution.OnStateFrom))] = new OpenApiSchema
    //            {
    //                Type = "string",
    //                Format = "date-time"
    //            },
    //            [CamelCase.ConvertName(nameof(HealthCheckExecution.LastExecuted))] = new OpenApiSchema
    //            {
    //                Type = "string",
    //                Format = "date-time"
    //            },
    //            [CamelCase.ConvertName(nameof(HealthCheckExecution.Uri))] = new OpenApiSchema
    //            {
    //                Type = "string"
    //            },
    //            [CamelCase.ConvertName(nameof(HealthCheckExecution.Name))] = new OpenApiSchema
    //            {
    //                Type = "string"
    //            },
    //            [CamelCase.ConvertName(nameof(HealthCheckExecution.DiscoveryService))] = new OpenApiSchema
    //            {
    //                Type = "string",
    //                Nullable = true
    //            },
    //            [CamelCase.ConvertName(nameof(HealthCheckExecution.Entries))] = new OpenApiSchema
    //            {
    //                Type = "array",
    //                Items = new OpenApiSchema
    //                {
    //                    Reference = new OpenApiReference
    //                    {
    //                        Id = nameof(HealthCheckExecutionEntry),
    //                        Type = ReferenceType.Schema,
    //                    }
    //                }
    //            },
    //            [CamelCase.ConvertName(nameof(HealthCheckExecution.History))] = new OpenApiSchema
    //            {
    //                Type = "array",
    //                Items = new OpenApiSchema
    //                {
    //                    Reference = new OpenApiReference
    //                    {
    //                        Id = nameof(HealthCheckExecutionHistory),
    //                        Type = ReferenceType.Schema,
    //                    }
    //                }
    //            }
    //        }
    //    };

    //    var HealthCheckEntrySchema = new OpenApiSchema
    //    {
    //        Type = "object",

    //        Properties =
    //        {
    //            [CamelCase.ConvertName(nameof(HealthCheckExecutionEntry.Id))] = new OpenApiSchema
    //            {
    //                Type = "integer",
    //                Format = "int32"
    //            },
    //            [CamelCase.ConvertName(nameof(HealthCheckExecutionEntry.Name))] = new OpenApiSchema
    //            {
    //                Type = "string"
    //            },
    //            [CamelCase.ConvertName(nameof(HealthCheckExecutionEntry.Status))] = new OpenApiSchema
    //            {
    //                Reference = new OpenApiReference
    //                {
    //                    Id = nameof(UIHealthStatus),
    //                    Type = ReferenceType.Schema,
    //                }
    //            },
    //            [CamelCase.ConvertName(nameof(HealthCheckExecutionEntry.Description))] = new OpenApiSchema
    //            {
    //                Type = "string"
    //            },
    //            [CamelCase.ConvertName(nameof(HealthCheckExecutionEntry.Duration))] = new OpenApiSchema
    //            {
    //                Type = "string",
    //                Format = "[-][d'.']hh':'mm':'ss['.'fffffff]"
    //            },
    //            [CamelCase.ConvertName(nameof(HealthCheckExecutionEntry.Tags))] = new OpenApiSchema
    //            {
    //                Type = "array",
    //                Items = new OpenApiSchema
    //                {
    //                    Type = "string"
    //                }
    //            },
    //        }
    //    };

    //    var HealthCheckHistorySchema = new OpenApiSchema
    //    {
    //        Type = "object",

    //        Properties =
    //        {
    //            [CamelCase.ConvertName(nameof(HealthCheckExecutionHistory.Id))] = new OpenApiSchema
    //            {
    //                Type = "integer",
    //                Format = "int32"
    //            },
    //            [CamelCase.ConvertName(nameof(HealthCheckExecutionHistory.Name))] = new OpenApiSchema
    //            {
    //                Type = "string"
    //            },
    //            [CamelCase.ConvertName(nameof(HealthCheckExecutionHistory.Description))] = new OpenApiSchema
    //            {
    //                Type = "string"
    //            },
    //            [CamelCase.ConvertName(nameof(HealthCheckExecutionHistory.Status))] = new OpenApiSchema
    //            {
    //                Reference = new OpenApiReference
    //                {
    //                    Id = nameof(UIHealthStatus),
    //                    Type = ReferenceType.Schema,
    //                }
    //            },
    //            [CamelCase.ConvertName(nameof(HealthCheckExecutionHistory.On))] = new OpenApiSchema
    //            {
    //                Type = "string",
    //                Format = "date-time"
    //            },
    //        }
    //    };

    //    var UIHealthStatusSchema = new OpenApiSchema
    //    {
    //        Type = "string",

    //        Enum =
    //        {
    //            new OpenApiString(UIHealthStatus.Healthy.ToString()),
    //            new OpenApiString(UIHealthStatus.Unhealthy.ToString()),
    //            new OpenApiString(UIHealthStatus.Degraded.ToString())
    //        }
    //    };

    //    SwaggerDoc.Paths.Add(Options.ApiPath, PathItem);
    //    SwaggerDoc.Components.Schemas.Add(nameof(HealthCheckExecution), HealthCheckSchema);
    //    SwaggerDoc.Components.Schemas.Add(nameof(HealthCheckExecutionEntry), HealthCheckEntrySchema);
    //    SwaggerDoc.Components.Schemas.Add(nameof(HealthCheckExecutionHistory), HealthCheckHistorySchema);
    //    SwaggerDoc.Components.Schemas.Add(nameof(UIHealthStatus), UIHealthStatusSchema);
    #endregion

    /// <summary>
    /// Execute this Doc Filter
    /// </summary>
    /// <param name="SwaggerDoc"></param>
    /// <param name="Context"></param>
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var pathItem = new OpenApiPathItem
        {
            Operations = new Dictionary<OperationType, OpenApiOperation>
            {
                [OperationType.Get] = new OpenApiOperation
                {
                    Summary = "Returns all the health status of this Microservice",
                    //Description = "Returns all the health states used by this Microservice",
                    Tags =
                    {
                        new OpenApiTag
                        {
                            Name = @"health",
                            Description = @"Health Check endpoints"
                        }
                    },
                    Responses =
                    {
                        [StatusCodes.Status200OK.ToString()] = new OpenApiResponse
                        {
                            Description = "API is healthy",
                            Content =
                            {
                                ["application/json"] = new OpenApiMediaType
                                {
                                    //Schema = new OpenApiSchema
                                    //{
                                    //    Reference = new OpenApiReference
                                    //    {
                                    //        Id = @"DUMMY", //nameof(HealthCheckExecution),
                                    //        Type = ReferenceType.Schema,
                                    //    }
                                    //}
                                }
                            }
                        },
                        [StatusCodes.Status503ServiceUnavailable.ToString()] = new OpenApiResponse
                        {
                            Description = "API is not healthy"
                        }
                    }
                }
            }
        };

        swaggerDoc.Paths.Add(@"/health", pathItem);
    }
}