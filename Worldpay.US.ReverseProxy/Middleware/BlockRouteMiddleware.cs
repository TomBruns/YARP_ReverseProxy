namespace Worldpay.US.ReverseProxy.Middleware;

public class BlockRouteMiddleware
{
    private readonly RequestDelegate _next;

    public BlockRouteMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Assign your nullable meta data from the config for the
        // current requested route to a variable
        var metaData = context.GetReverseProxyFeature().Route.Config.Metadata;

        // If metaData is not null and a string value, this includes null 
        // and whitespace, exists for the key "ReturnResponseStatusCode",
        // then short circuit the middleware
        if (metaData?.TryGetValue("ReturnResponseStatusCode", 
            out var unsuccessfulResponseStatusCode) ?? false)
        {
            // Adding a switch case here allows our 
            // ReturnResponseStatusCode key to be robust enough to 
            // handle multiple different unsuccessful response status 
            // codes if you have different cases for different routes.
            switch (unsuccessfulResponseStatusCode)
            {
                case "404":
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    break;
                case "500":
                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }
        }
        // Otherwise, invoke the next middleware delegate
        else
        {
            await _next(context);
        }
    }
}
