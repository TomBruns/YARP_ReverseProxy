using System.Reflection;

namespace Worldpay.US.Swagger.Extensions;

/// <summary>
/// The code allows you to control the order that OpenAPI Tag groups appear in Swagger.  
///     Note: By default they are alphabetical
/// </summary>
/// <example>
///     Add this to builder.Services.AddSwaggerGen 
///     
///         // create instance of class supporting custom "Order By Custom Attribute" 
///         var swaggerControllerOrder = new SwaggerTagDisplayOrder(Assembly.GetEntryAssembly());
///         
///         // sort the order that the tags are listed using the custom attribute: SwaggerTagDisplayOrder, by default they are alphabetical
///         //  Note: "controller" is key in this oob collection even for minimal apis
///         options.OrderActionsBy((apiDesc) => $"{swaggerControllerOrder.SortKey(apiDesc.ActionDescriptor.RouteValues["controller"])}")
/// </example>
public class SwaggerTagDisplayOrder
{
    private readonly Dictionary<string, uint> _displayOrder;   // Our lookup table which contains controllername -> sortorder pairs.

    /// <summary>
    /// Initializes a new instance of the <see cref="SwaggerTagDisplayOrder"/> class.
    /// </summary>
    /// <param name="assembly">The assembly to scan for for classes with custom attribute <see cref="SwaggerTagDisplayOrderAttribute"></see>.</param>
    public SwaggerTagDisplayOrder(Assembly assembly)
        : this(GetTypesWithCustomAttribute(assembly)) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="SwaggerTagDisplayOrder"/> class.
    /// </summary>
    /// <param name="controllers">
    /// The types to scan for a custom attribute <see cref="SwaggerTagDisplayOrderAttribute"/> to determine the sortorder.
    /// </param>
    public SwaggerTagDisplayOrder(IEnumerable<Type> controllers)
    {
        // Initialize our dictionary; scan the given types for our custom attribute, read the Order property
        // from the attribute and store it as typeName -> sorderorder pair in the (case-insensitive) dicationary.
        _displayOrder = new Dictionary<string, uint>(
            controllers.Where(c => c.GetCustomAttributes<SwaggerTagDisplayOrderAttribute>().Any())
            .Select(c => new { Name = c.Name, c.GetCustomAttribute<SwaggerTagDisplayOrderAttribute>().Order })
            .ToDictionary(v => v.Name, v => v.Order), StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Scans all types in the current assy and retuns any that have a specified attribute
    /// </summary>
    /// <param name="assembly"></param>
    /// <returns></returns>
    public static IEnumerable<Type> GetTypesWithCustomAttribute(Assembly assembly)
    {
        return assembly.GetTypes().Where(type => type.GetCustomAttributes(typeof(SwaggerTagDisplayOrderAttribute), true).Length > 0);
    }

    /// <summary>
    /// Returns the unsigned integer sort order value.  
    /// </summary>
    /// <param name="controller">The controller name.</param>
    /// <returns>The unsigned integer sort order value.</returns>
    private uint Order(string controller)
    {
        // Try to get the sort order value from our lookup; if none is found, assume uint.MaxValue.
        if (!_displayOrder.TryGetValue(controller, out uint order))
            order = uint.MaxValue;

        return order;
    }

    /// <summary>
    /// Returns an order key based on a the SwaggerTagDisplayOrderAttribute for use with OrderActionsBy.
    /// </summary>
    /// <param name="typeName">The type name.</param>
    /// <returns>A zero padded 32-bit unsigned integer.</returns>
    public string OrderKey(string typeName)
    {
        return Order(typeName).ToString("D10");
    }

    /// <summary>
    /// Returns a sort key based on a the SwaggerTagDisplayOrderAttribute for use with OrderActionsBy.
    /// </summary>
    /// <param name="typeName">The type name.</param>
    /// <returns>A zero padded 32-bit unsigned integer combined with the type's name.</returns>
    public string SortKey(string typeName)
    {
        return $"{OrderKey(typeName)}_{typeName}";
    }
}
