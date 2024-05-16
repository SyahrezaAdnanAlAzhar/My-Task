using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AuthenticationLibrary;
using CRUDTaskLibrary;
using System.Threading.Tasks;
using Task = CRUDTaskLibrary.Task;


namespace MyTaskController.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class APIController : ControllerBase
    {
        private static readonly List<AuthenticationLibrary.Account> listAccount = new List<AuthenticationLibrary.Account>();
        private static readonly List<CRUDTaskLibrary.Task> listTask = new List<CRUDTaskLibrary.Task>();

        static APIController()
        {
            listTask.Add(new CRUDTaskLibrary.Task
            {
                judul = "Task1",
                username = "User1",
                deskripsi = "This is task 1",
                tanggalMulai = DateTime.Now,
                tanggalSelesai = DateTime.Now.AddDays(2),
                jenisTugas = CRUDTaskLibrary.Task.JenisTugas.Video,
                namaPrioritas = CRUDTaskLibrary.Task.Prioritas.Highest,
                taskState = TaskState.InProgress
            });
            listTask.Add(new CRUDTaskLibrary.Task
            {
                judul = "Task 2",
                username = "User2",
                deskripsi = "This is task 2",
                tanggalMulai = DateTime.Now,
                tanggalSelesai = DateTime.Now.AddDays(3),
                jenisTugas = CRUDTaskLibrary.Task.JenisTugas.Laporan,
                namaPrioritas = CRUDTaskLibrary.Task.Prioritas.Medium,
                taskState = TaskState.PostPone
            });

            listTask.Add(new CRUDTaskLibrary.Task
            {
                judul = "Task 3",
                username = "User3",
                deskripsi = "This is task 3",
                tanggalMulai = DateTime.Now,
                tanggalSelesai = DateTime.Now.AddDays(4),
                jenisTugas = CRUDTaskLibrary.Task.JenisTugas.Project,
                namaPrioritas = CRUDTaskLibrary.Task.Prioritas.Low,
                taskState = TaskState.ToDo
            });
        }

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
        public IActionResult UpdateUsername(string username, string newName)
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
        public IActionResult UpdatPassworde(string username, string newPassword)
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

        [HttpGet("task/detail/{username}/{judul}")]
        public ActionResult<object> GetTask(string username, string judul)
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
        public IActionResult UpdatPassworde(string username, string newPassword)
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


        // GET: api/API
        [HttpGet]
        public ActionResult<IEnumerable<CRUDTaskLibrary.Task>> GetTasks()
        {
            var tasksFix = listTask.Select(task => new
            {
                task.judul,
                task.username,
                task.deskripsi,
                task.tanggalMulai,
                task.tanggalSelesai,
                jenisTugas = task.jenisTugas.ToString(),
                namaPrioritas = task.namaPrioritas.ToString(),
                taskState = task.taskState.ToString()
            });

            return Ok(tasksFix);
        }

        // GET using username and judul
        [HttpGet("{username}/{judul}")]
        public ActionResult<object> GetTask(string username, string judul)
        {
            var task = listTask.FirstOrDefault(t =>
                    string.Equals(t.username, username, StringComparison.OrdinalIgnoreCase) &&
                    t.judul == judul 
                );

            if (task == null)
            {
                return NotFound();
            }

            var tasksFix = new
            {
                task.judul,
                task.username,
                task.deskripsi,
                task.tanggalMulai,
                task.tanggalSelesai,
                jenisTugas = task.jenisTugas.ToString(),
                namaPrioritas = task.namaPrioritas.ToString(),
                taskState = task.taskState.ToString()
            };

            return Ok(tasksFix);
        }

        // DELETE: api/API/5
        [HttpDelete("{judul}")]
        public IActionResult DeleteTask(string judul)
        {
            
            var task = listTask.FirstOrDefault(t => string.Equals(t.judul, judul, StringComparison.OrdinalIgnoreCase));

            if (task == null)
            {
                return NotFound();
            }

            listTask.Remove(task);

            return NoContent();
        }
    }
}
