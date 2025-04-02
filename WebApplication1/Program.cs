using System.Diagnostics;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Routing.Patterns;
using WebApplication1.Middleware;
using WebApplication1.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        string? f = null;
        string? v = "sdfds" + f;

        string a = "abc";
        string b = "ab" + "c";

        bool g = a.Equals(b);

        var builder = WebApplication.CreateBuilder(args);
        builder.Configuration["conf1"] = "boo";

        builder.Services.AddSingleton<IIncrementalService, FooService>();
        builder.Services.AddTransient<IIncrementalService, FooService>();
        builder.Services.AddScoped(typeof(FooService));

        //ServiceDescriptor d = new ServiceDescriptor(typeof(IIncrementalService), typeof(FooService), ServiceLifetime.Scoped);
        //builder.Services.Add(d);

        builder.Services.AddControllers();

        // Add services to the container.

        var app = builder.Build();


        // Configure the HTTP request pipeline.

        var summaries = new[]
        {
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};


        app.UseMiddleware<BooMiddleware>();
        // app.UseMiddleware<BooMiddleware>();

        app.MapGet("/", (IConfiguration conf) => { return $"Index Page. Age = {conf["age"]}"; });

        app.MapGet("/inc", () => 
        {
            Console.WriteLine("inc");
            return new { path = "dsfdsf" }; 
        });

        app.MapGet("/routes", (IEnumerable<EndpointDataSource> endpointSources) =>
            string.Join("\n", endpointSources.SelectMany(source => source.Endpoints)));

        app.MapGet("/i/{id}", (int id) => $"USer id: {id}");


        app.Map("/boo/", (int? boo) =>
        {
            return $"{boo ?? 0}";
        });

        app.MapControllers();


        app.Run();
    }
}

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
