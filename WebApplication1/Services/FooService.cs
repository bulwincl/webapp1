using Serilog;

namespace WebApplication1.Services
{
    public class FooService : IIncrementalService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private int _number;

        public int Number { get { return _number; } }

        public FooService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void Increment()
        {
            _number = _number + 1;

            Log.Information("FooService - Increment Method");
            Log.Information($"httpContextAccessor.HttpContext.Request {_httpContextAccessor.HttpContext.Request}");
        }
    }


}
