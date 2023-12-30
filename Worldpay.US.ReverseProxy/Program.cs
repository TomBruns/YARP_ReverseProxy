using Worldpay.US.ReverseProxy;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
                    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

//builder.Services.AddHttpLogging(o => { });
//builder.Services.AddTelemetryConsumer<ForwarderTelemetry>();

var app = builder.Build();

//app.UseHttpLogging();

// Register the reverse proxy routes
app.MapReverseProxy();

app.Run();
