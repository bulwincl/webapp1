using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class BoomController : ControllerBase
    {
        IConfiguration _conf;
        IIncrementalService _incService;
        HttpClient _httpClient;

        public BoomController(IConfiguration conf, IIncrementalService iservice, HttpClient httpClient)
        {
            _conf = conf;
            _incService = iservice;
            _httpClient = httpClient;
        }

        [HttpGet("{dom:int}/{number:range(1,10)}")]
        public IActionResult Room(decimal number, int dom)
        {
            throw new Exception("New Exception!");

            return Ok(new { Method = "Room", Number = number, Dom = dom, Path = HttpContext.Request.Path });
        }

        [HttpGet("{dom:alpha:minlength(2)}/{number:int}")]
        public IActionResult Room2(int number, string dom, IIncrementalService iservice)
        {
            var c1 = _conf["conf1"];

            _incService.Increment();
            iservice.Increment();

            bool f = _incService.Equals(iservice);

            var fields = typeof(StatusCodes)
                .GetFields();

            var codes = fields.Select(f => new { Name = f.Name, Value = (int)f.GetValue(null) }).ToList();

            return base.StatusCode(number, new 
            { 
                StatusCode= number,
                StatusName = codes.FirstOrDefault(c => c.Value == number)?.Name ?? "No Code Name",
                Method = "Room2", 
                Number = number, 
                Dom = _incService.Number,
            });
          
        }

        [HttpGet("data")]
        public async Task<IActionResult> Data()
        {
            var data = await _httpClient.GetAsync("http://localhost:33333/data");

            return Ok(data);
        }


        [HttpGet("error")]
        public IActionResult Error()
        {
            var requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            var exceptionHandlerPathFeature =
                HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            return Problem(statusCode: 500);
        }
    }
}
