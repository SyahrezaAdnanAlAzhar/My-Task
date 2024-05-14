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

        [HttpPost(Name = "Create/Add Account")]
        public IActionResult Create(AuthenticationLibrary.Account account)
        {
            listAccount.Add(account);
            return Ok();
        }

        [HttpPost(Name = "Create/Add Task")]
        public IActionResult Create(CRUDTaskLibrary.Task task)
        {
            listTask.Add(task);
            return Ok();
        }

        [HttpPut(Name = "Update Account Name")]
        public IActionResult Update(string username, string newName)
        {
            foreach (var account in listAccount)
            {
                if (account.userName == username)
                {
                    account.nama = newName;
                    return Ok();
                }
            }
            return NotFound();
        }

        [HttpPut(Name = "Update Account Password")]
        public IActionResult Update(string username, string newPassword)
        {
            foreach (var account in listAccount)
            {
                if (account.userName == username)
                {
                    account.password = newPassword;
                    return Ok();
                }
            }
            return NotFound();
        }

        private readonly ILogger<MyTaskController> _logger;

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
