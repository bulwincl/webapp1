using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class BoomController : ControllerBase
    {
        IConfiguration _conf;
        IIncrementalService _incService;

        public BoomController(IConfiguration conf, IIncrementalService iservice)
        {
            _conf = conf;
            _incService = iservice;
        }

        [HttpGet("{dom:int}/{number:range(1,10)}")]
        public IActionResult Room(decimal number, int dom)
        {
            return Ok(new { Method = "Room", Number = number, Dom = dom });
        }

        [HttpGet("{dom:alpha:minlength(2)}/{number}")]
        public IActionResult Room2(string number, string dom, IIncrementalService iservice)
        {
             var c1= _conf["conf1"];

            _incService.Increment();
            iservice.Increment();

            bool f = _incService.Equals(iservice);

            return Ok(new { Method = "Room2", Number = number, Dom = _incService.Number });
        }

    }
}
