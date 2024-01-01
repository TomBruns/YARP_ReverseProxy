using Worldpay.US.ReverseProxy;
using Worldpay.US.ReverseProxy.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
                    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddHttpLogging(o => { });
builder.Services.AddTelemetryConsumer<ForwarderTelemetry>();

var app = builder.Build();

app.UseHttpLogging();

// Register the reverse proxy routes
app.MapReverseProxy(proxyPipeline =>
{
    // I would register this above most or all of the the other middleware
    // to avoid unnecessary processing when attempting to short circuit
    // routes you don't want to proxy. This really depends on what other
    // middleware you have and what it does. Just be careful here.
    proxyPipeline.UseMiddleware<BlockRouteMiddleware>();
});

app.Run();
