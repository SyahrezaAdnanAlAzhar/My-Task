using Microsoft.AspNetCore.Mvc;
using AuthenticationLibrary;
using CRUDTaskLibrary;

namespace MyTaskAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MyTaskController : ControllerBase
    {
        private static readonly List<AuthenticationLibrary.Account> listAccount = new List<AuthenticationLibrary.Account>();
        private static readonly List<CRUDTaskLibrary.Task> listTask = new List<CRUDTaskLibrary.Task>();

        public MyTaskController(ILogger<MyTaskController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<MyTask> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new MyTask
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
