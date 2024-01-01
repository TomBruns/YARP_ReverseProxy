using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using Asp.Versioning;
using Asp.Versioning.Builder;
using FluentValidation;
using FluentValidation.AspNetCore;

using Worldpay.US.Express.v4.Models;
using Worldpay.US.Express.Swagger;
using Worldpay.US.Swagger.Extensions;

namespace Worldpay.US.Express.v4.Routes;

/// <summary>
/// This class defines all the Payments Routes
/// </summary>
[SwaggerTagDisplayOrder(1)]
internal static class v4PaymentsAPIs
{
    const string ROUTE_GROUP_PREFIX = @"payments";

    public static RouteGroupBuilder MapV4PaymentEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet($"/{ROUTE_GROUP_PREFIX}/authorize", () =>
        {
            //return Results.Ok(@"response from EXPRESS");  // doing this wraps the response in double quotes
            return Results.Text(@"response from EXPRESS");
        })
        .RequireAuthorization()
        .WithName("authorizev4")
        .WithTags("payments")
        .Produces<string>(StatusCodes.Status200OK, @"text/plain")
        .WithOpenApi(operation => new(operation)
        {
            Summary = @"You want to process an Authorization and optionally Capture in one (1) step.",
            Description = """
            You want to process a Payment Authorization
              * this confirms the validity of the account and the transaction.
              * it also verifies that a customer has enough funds or credit to cover the amount of the transaction.
          
             Notes:  
              * This single endpoint supports both a wide range of Payment Methods for Card Present &amp; eCommerce(Card Not Present) requests.
              * You can optionally Capture(for settlement) in one(1) step using the ** AutoCapture** option
              * You can also use this method for **Card verification checks** with a $0 authorization amount
              * You can optionally perform an unreferenced incremenatal Auth by setting a processing option flag + ?
              * If you are sending PAN, you can request that the response contains a **WorldPay Security Token** useable in future transactions.
            """
        });

        return group;
    }
}
