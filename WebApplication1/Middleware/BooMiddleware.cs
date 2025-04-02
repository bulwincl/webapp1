using WebApplication1.Services;

namespace WebApplication1.Middleware
{
    public class BooMiddleware
    {
        int _i;

        RequestDelegate _next;

        public BooMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context, IIncrementalService incService)
        {
            Console.WriteLine("Start Boo Middlware");

            _i++;

            incService.Increment();

            await _next(context);

            Console.WriteLine("End Boo Middlware");
        }
    }
}
