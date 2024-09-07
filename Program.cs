using Yarp.ReverseProxy.Transforms;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .AddTransforms(transforms =>
    {
        transforms.AddRequestTransform(async context =>
        {
            context.ProxyRequest.Version = new Version(1, 1);
            context.ProxyRequest.VersionPolicy = HttpVersionPolicy.RequestVersionExact;
        });
    });

var app = builder.Build();

app.MapReverseProxy();

app.Run();