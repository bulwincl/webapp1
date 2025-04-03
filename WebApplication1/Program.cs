using System.Diagnostics;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Routing.Patterns;
using Serilog;
using WebApplication1.Checks;
using WebApplication1.Middleware;
using WebApplication1.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();



        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddHttpContextAccessor();
        builder.Configuration["conf1"] = "boo";


        builder.Services.AddHealthChecks()
            .AddCheck<RequestTimeHealthCheck>("RequestTimeCheck");

        builder.Services.AddHttpClient();


        builder.Services.AddSingleton<IIncrementalService, FooService>();
        //builder.Services.AddTransient<IIncrementalService, FooService>();
        //builder.Services.AddScoped(typeof(FooService));

        //ServiceDescriptor d = new ServiceDescriptor(typeof(IIncrementalService), typeof(FooService), ServiceLifetime.Scoped);
        //builder.Services.Add(d);

        builder.Services.AddControllers();

        // Add services to the container.

        var app = builder.Build();

        app.UseExceptionHandler("/api/boom/error");


        app.UseMiddleware<BooMiddleware>();
        // app.UseMiddleware<BooMiddleware>();


        app.MapControllers();
        app.MapHealthChecks("/health");


        app.Run();
    }
}
