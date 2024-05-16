using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AuthenticationLibrary;
using CRUDTaskLibrary;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System;
using FluentValidation;
using System.ComponentModel.DataAnnotations;


namespace MyTaskController.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class APIController : ControllerBase
    {
        private static readonly List<AuthenticationLibrary.Account> listAccount = new List<AuthenticationLibrary.Account>();
        private static readonly List<CRUDTaskLibrary.Task> listTask = new List<CRUDTaskLibrary.Task>();
        
        [HttpGet]
        public ActionResult<IEnumerable<CRUDTaskLibrary.Task>> GetTasks()
        {
            var tasksFix = new List<object>();
            try
            {
                string content = System.IO.File.ReadAllText("main.json");
                var tasks = JsonConvert.DeserializeObject<List<CRUDTaskLibrary.Task>>(content);

                if (tasks == null)
                {
                    return NotFound("Isinya mana, kek manaaa");
                }

                foreach (var task in tasks)
                {
                    tasksFix.Add(new
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
                }
                return Ok(tasksFix);
            }
            catch (FileNotFoundException ex)
            {
                return NotFound("file ga ada nih : " + ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, " errornya : " + ex.Message);
            }
        }

        [HttpPost(Name = "Create/Add Account")]
        public IActionResult CreateAccount(AuthenticationLibrary.Account account)
        {
            listAccount.Add(account);
            return Ok();
        }

        [HttpPost(Name = "Create/Add Task")]
        public IActionResult CreateTask(CRUDTaskLibrary.Task task)
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
            string path = judul + "_" + username + ".json";
            if (!System.IO.File.Exists(path))
            {
                return NotFound("File tidak ada");
            }

            try
            {
                var json = System.IO.File.ReadAllText(path);
                var listTask = JsonConvert.DeserializeObject<List<CRUDTaskLibrary.Task>>(json);

                if (listTask == null)
                {
                    return NotFound("No tasks found");
                }

                var task = listTask.FirstOrDefault(t => t.username == username && t.judul == judul);

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
            catch (Exception ex)
            {
                return StatusCode(500, " errornya : " + ex.Message);
            }
        }

        [HttpGet("account/{username}/{password}")]
        public ActionResult<AuthenticationLibrary.Account> GetAccount(string username, string password)
        {
            try
            {
                string content = System.IO.File.ReadAllText("Account.json");
                var accounts = JsonConvert.DeserializeObject<List<AuthenticationLibrary.Account>>(content);

                if (accounts == null)
                {
                    return NotFound("No accounts found");
                }

                AuthenticationLibrary.Account account = null;
                foreach (var fAccount in accounts)
                {
                    if (fAccount.userName == username && fAccount.password == password)
                    {
                        account = fAccount;
                        break;
                    }
                }

                if (account == null)
                {
                    return NotFound("Account not found");
                }

                return Ok(account);
            }
            catch (FileNotFoundException ex)
            {
                return NotFound("File tidak ada : " + ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ada error : " + ex.Message);
            }
        }

        [HttpDelete("{username}/{judul}")]
        public IActionResult DeleteTask(string username, string judul)
        {
            string path = judul + "_" + username + ".json";
            if (!System.IO.File.Exists(path))
            {
                return NotFound("File jangan ngarang");
            }

            try
            {
                System.IO.File.Delete(path);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }

        [HttpDelete("account/{username}/{password}")]
        public IActionResult DeleteAccount(string username, string password)
        {
            try
            {
                string content = System.IO.File.ReadAllText("Account.json");
                var accounts = JsonConvert.DeserializeObject<List<AuthenticationLibrary.Account>>(content);

                if (accounts == null)
                {
                    return NotFound("No accounts found");
                }

                var account = accounts.FirstOrDefault(a => a.userName == username && a.password == password);

                if (account == null)
                {
                    return NotFound("Account not found");
                }
                accounts.Remove(account);
                System.IO.File.WriteAllText("Account.json", JsonConvert.SerializeObject(accounts));

                return NoContent();
            }
            catch (FileNotFoundException ex)
            {
                return NotFound("file ga ada ngab : " + ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ada error di : " + ex.Message);
            }
        }
    }
}