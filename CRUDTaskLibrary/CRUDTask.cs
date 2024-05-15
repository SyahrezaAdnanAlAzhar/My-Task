using System.IO;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using FluentValidation;

namespace CRUDTaskLibrary
{
    public static class CRUDTask
    {
        public static void createTask(Task taskInput)
        {
            string jsonText = JsonSerializer.Serialize(taskInput);
            string path = taskInput.judul + "_" + taskInput.username + ".json";
            File.WriteAllText(path, jsonText);
            Console.WriteLine($"Tugas dengan judul {taskInput.judul} telah berhasil ditambahkan");

        }
        public static Task readTask(string judulTask, string username)
        {
            string path = judulTask + "_" + username + ".json";
            if (File.Exists(path))
            {
                string jsonText = File.ReadAllText(path);
                Task dataTask = JsonSerializer.Deserialize<Task>(jsonText);
                return dataTask;
            }
            else
            {
                return null;
            }
        }
        public static void deleteTask<T, U>(T judulTask, U username)
        {
            //delete file json
        }
        public static void updateJudul(String judulAwalTask, String judulPerubahanTask, String username)
        {
             var taskData = System.Text.Json.JsonSerializer.Deserialize<List<Task>>(File.ReadAllText("task_data.json"));

             // Find the task with the matching title
             var task = taskData.Find(t => t.judul == judulAwalTask);

             // Ensure the task is found
             if (task == null)
            {
                 throw new Exception($"Task with title '{judulAwalTask}' not found.");
             }

             // Update the task title
             task.judul = judulPerubahanTask;

             // Record the change
             var changeLog = new ChangeLog
            {
                 Username = username.ToString(),
                 Message = $"Task title '{task.judul}' updated from '{judulAwalTask}' to '{judulPerubahanTask}'."
             };

             taskData.Add(changeLog);

             // Save the data back to the JSON file
             File.WriteAllText("task_data.json", System.Text.Json.JsonSerializer.Serialize(taskData));
        }
        public static void updateTanggalMulai(string judulTask, DateTime perubahanTanggalMulai, string username)
        {
         if (judulTask == null || username == null)
         {
             throw new ArgumentNullException("Judul atau Username tidak boleh null.");
         }

         Task desTask = readTask(judulTask, username);

         // Programming Defensive: Memastikan perubahanTanggalMulai adalah objek DateTime yang valid
         if (!(perubahanTanggalMulai is DateTime))
         {
             throw new ArgumentException("Tanggal Mulai Baru harus berupa objek DateTime.");
         }

         if (perubahanTanggalMulai.CompareTo(desTask.tanggalMulai) < 0)
         {
             throw new ArgumentException("Tanggal Mulai Baru harus lebih dari tanggal mulai");
         }

         desTask.tanggalMulai = perubahanTanggalMulai;

         createTask(desTask);
         Console.WriteLine("Tanggal mulai sudah menjadi: " + perubahanTanggalMulai);
        }
        public static void updateTanggalSelesai<T, U, V>(T judulTask, U perubahanTanggalSelesai, V username)
        {
            //merubah tanggal mulai pada file json
            //pastikan perubahan tanggal selesai yaitu pada tanggal setelah tanggal mulai
            //pastikan state ikut menyesuaikan
        }
        public static void updateJenisTugas<T, U, V>(T judulTask, U perubahanJenisTugas, V username)
        {
            //merubah tanggal mulai pada file json
            //pastikan bahwa jenis tugas yang ingin dirubah terdapat di enum
        }
        public static void updatePrioritas<T, U, V>(T judulTask, U perubahanPrioritas, V username)
        {
            //merubah tanggal mulai pada file json
            //pastikan bahwa tingkat prioritas yang ingin dirubah terdapat di enum
        }
    }
}
