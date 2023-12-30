namespace Worldpay.US.Swagger.Extensions;

/// <summary>
/// Custom Attribute to control the order that OpenAPI Tags are displayed in Swagger
/// Note: This attribute is used with Minimal API (vs Controller Style) webAPIs
/// </summary>
/// <example>
///     Add this attribute to the class the defines the endpoints
///         [SwaggerTagDisplayOrder(2)]
/// </example>
public class SwaggerTagDisplayOrderAttribute : Attribute
{
    /// <summary>
    /// Gets the sorting order of the tag.
    /// </summary>
    public uint Order { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SwaggerTagDisplayOrderAttribute"/> class.
    /// </summary>
    /// <param name="order">Sets the sorting order of the tag.</param>
    public SwaggerTagDisplayOrderAttribute(uint order)
    {
        Order = order;
    }
}
