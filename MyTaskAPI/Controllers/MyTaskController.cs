using Microsoft.AspNetCore.Mvc;
using AuthenticationLibrary;
using CRUDTaskLibrary;
using Newtonsoft.Json;
using FluentValidation.Results;
using Task = CRUDTaskLibrary.Task;


namespace MyTaskController.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class APIController : ControllerBase
    {
        private static List<AuthenticationLibrary.Account> listAccount;
        private static List<CRUDTaskLibrary.Task> listTask;

        // Constructor APIController untuk membaca file json yang disimpan di listAccount dan listTask
        public APIController()
        {
            listAccount = LoadAccountsFromJson("Account.json");
            listTask = LoadTasksFromJson("main.json");
        }

        // function untuk membaca file json untuk inisiasi listAccount
        private List<AuthenticationLibrary.Account> LoadAccountsFromJson(string jsonFileName)
        {
            List<AuthenticationLibrary.Account> accounts = new List<AuthenticationLibrary.Account>();
            string jsonString = System.IO.File.ReadAllText(jsonFileName);
            accounts = JsonConvert.DeserializeObject<List<AuthenticationLibrary.Account>>(jsonString);
            return accounts;
        }

        // function untuk membaca file json untuk inisiasi listTask
        private List<CRUDTaskLibrary.Task> LoadTasksFromJson(string jsonFileName)
        {
            List<CRUDTaskLibrary.Task> tasks = new List<CRUDTaskLibrary.Task>();
            string jsonString = System.IO.File.ReadAllText(jsonFileName);
            tasks = JsonConvert.DeserializeObject<List<CRUDTaskLibrary.Task>>(jsonString);
            return tasks;
        }


        // Create Account dengan menerima object account, kemudian ditambahkan pada json
        [HttpPost]
        [Route("CreateAccount")]
        public ActionResult CreateAccount(AuthenticationLibrary.Account accountInput)
        {
            AccountValidator validator = new AccountValidator();
            ValidationResult validationResult = validator.Validate(accountInput);

            if (!validationResult.IsValid)
            {
                return BadRequest("Data Account yang dimasukkan tidak valid");
            }

            listAccount.Add(accountInput);

            string jsonContent = JsonConvert.SerializeObject(listAccount, Formatting.Indented);
            System.IO.File.WriteAllText("Account.json", jsonContent);

            return Ok($"Account dengan username {accountInput.userName} telah ditambahkan");
        }

        // Create Task dengan menerima object task, kemudian ditambahkan pada json
        [HttpPost]
        [Route("CreateTask")]
        public ActionResult CreateTask(CRUDTaskLibrary.Task taskInput)
        {
            TaskValidator validator = new TaskValidator();
            ValidationResult validatorResult = validator.Validate(taskInput);

            if (!validatorResult.IsValid)
            {
                return BadRequest("Data Task yang dimasukkan tidak valid");
            }

            listTask.Add(taskInput);

            string jsonContent = JsonConvert.SerializeObject(listTask, Formatting.Indented);
            System.IO.File.WriteAllText("main.json", jsonContent);
            return Ok($"Task dengan judul {taskInput.judul} milik {taskInput.username} telah ditambahkan");
        }

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

        [HttpPut]
        [Route("Update Task Deskripsi")]
        public ActionResult UpdateTaskDeskripsi(string username, string judulTask, string password, string newDeskripsi)
        {
            foreach (Account A in listAccount)
            {
                if (A.userName.Equals(username) && A.password.Equals(password))
                {
                    foreach (Task T in listTask)
                    {
                        if (T.username.Equals(username) && T.judul.Equals(judulTask))
                        {
                            T.deskripsi = newDeskripsi;
                            string jsonContent = JsonConvert.SerializeObject(listTask, Formatting.Indented);
                            System.IO.File.WriteAllText("main.json", jsonContent);
                            return Ok($"Deskripsi dari task {judulTask} milik {username} berhasil dirubah");
                        }
                    }
                    return BadRequest($"Task {judulTask} tidak ditemukan");
                }
                else if (A.userName.Equals(username) && !A.password.Equals(password))
                {
                    return BadRequest("Password yang anda masukkan salah");
                }
            }
            return BadRequest($"Username {username} tidak ditemukan");
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

        public IActionResult GetById<T>(int id, T listObject) where T : List<object>
        {
            if ( id < 0 || id >= listObject.Count)
            {
                return NotFound();
            }
            return Ok(listObject[id]);
        }

    }
}