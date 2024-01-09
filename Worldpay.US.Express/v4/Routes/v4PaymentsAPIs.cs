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
using Worldpay.US.IDP;
using Worldpay.US.Express.Utilities;
using Worldpay.US.Express.Entities;

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
        // ===================
        // POST /payments/authorize
        // ===================
        group.MapPost($"/{ROUTE_GROUP_PREFIX}/authorize", Results<Ok<AuthorizePaymentResponseDTO>, UnauthorizedHttpResult, BadRequest<ProblemDetails>> (HttpContext context, [FromServices] IdentityService identityService, [FromBody] AuthorizePaymentRequestDTO request) =>
        {
            // get the custom claims
            (bool IsWellFormedClaimsObject, ExpressClaimsBE expressClaims) = ClaimsHelpers.GetExpressClaims(context.User.Claims);

            #region === Authorization Checks ===
            // this really should be a relationship configured in IP CRM/, for a simple test we will jsut make sure it matches
            if (expressClaims.AcceptorId != request.MerchantData.MerchantId)
            {
                //return new UnauthorizedResult();
                // for testing return BadRequest error so we can include a ProblemDetails
                return TypedResults.BadRequest<ProblemDetails>(new ProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = $"Integrator Id is not valid for MerchantId.",
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Detail = $"AcceptorId => [{expressClaims.AcceptorId}] || MerchantId => [{request.MerchantData.MerchantId}]"
                });
            }
            #endregion

            return TypedResults.Ok(new AuthorizePaymentResponseDTO() 
            { 
                AuthorizeResult = @"response from Express",
                AcceptorId = expressClaims.AcceptorId,
                AccountToken = expressClaims.AccountToken
            });
        })
        .RequireAuthorization("ValidExpressAuthHeader")
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
