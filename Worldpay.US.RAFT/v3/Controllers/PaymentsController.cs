﻿using System;
using System.ComponentModel;
using System.Net;
using System.Runtime.Intrinsics.X86;
using System.Transactions;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Asp.Versioning;
using Swashbuckle.AspNetCore.Annotations;

using Worldpay.US.RAFT.Swagger;
using Worldpay.US.Swagger.Extensions;
using Microsoft.AspNetCore.Authorization;
using Worldpay.US.RAFT.v3.Models;

namespace Worldpay.US.RAFT.v3.Controllers;

/// <summary>
/// This class implements the Payments Service endpoints
/// </summary>
[ApiVersion(3.0)]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[SwaggerControllerDisplayOrder(1)]
[Authorize]
public class PaymentsController : ControllerBase
{
    private readonly ILogger<PaymentsController> _logger;

    /// <summary>
    /// Create an instance of the Payments Controller
    /// </summary>
    /// <param name="logger"></param>
    public PaymentsController(ILogger<PaymentsController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// You want to process an Authorization and optionally Capture in one (1) step.
    /// </summary>
    /// <remarks>
    /// You want to process a Payment Authorization
    ///  * this confirms the validity of the account and the transaction.
    ///  * it also verifies that a customer has enough funds or credit to cover the amount of the transaction.
    ///  
    /// Notes:  
    ///  * This single endpoint supports both a wide range of Payment Methods for Card Present &amp; eCommerce(Card Not Present) requests.
    ///  * You can optionally Capture(for settlement) in one(1) step using the ** AutoCapture** option
    ///  * You can also use this method for **Card verification checks** with a $0 authorization amount
    ///  * You can optionally perform an unreferenced incremental Auth by setting a processing option flag + ?
    ///  * If you are sending PAN, you can request that the response contains a **WorldPay Security Token** useable in future transactions.
    /// </remarks>
    /// <returns></returns>
    [HttpGet(template: "authorize", Name = "authorize")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(AuthorizePaymentResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [SwaggerOperation(Tags = new[] { "payments" })]
    public ActionResult<AuthorizePaymentResponseDTO> Authorize(AuthorizePaymentRequestDTO request)
    {
        return Ok(new AuthorizePaymentResponseDTO() { AuthorizeResult = @"response from RAFT" });
    }

}
