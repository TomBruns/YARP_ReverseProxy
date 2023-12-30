namespace Worldpay.US.Swagger.Extensions;

/// <summary>
/// Custom Attribute to control the order that OpenAPI Tags are displayed in Swagger
/// Note: This attribute is used with Controller (vs Minimal API Style) webAPIs
/// </summary>
/// <example>
///     Add this attribute to the class the defines the endpoints
///         [SwaggerControllerDisplayOrder(2)]
/// </example>
public class SwaggerControllerDisplayOrderAttribute : Attribute
{
    /// <summary>
    /// Gets the sorting order of the controller.
    /// </summary>
    public uint Order { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SwaggerControllerDisplayOrderAttribute"/> class.
    /// </summary>
    /// <param name="order">Sets the sorting order of the controller.</param>
    public SwaggerControllerDisplayOrderAttribute(uint order)
    {
        Order = order;
    }
}
