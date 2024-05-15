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
        public static void updateTanggalSelesai(string judulTask, DateTime perubahanTanggalSelesai, string username)
        {
            //merubah tanggal mulai pada file json
            //pastikan perubahan tanggal selesai yaitu pada tanggal setelah tanggal mulai
            //pastikan state ikut menyesuaikan

            // Design By Contract: Memastikan judulTask dan username tidak null
            if (judulTask == null || username == null)
            {
                throw new ArgumentNullException("Judul atau Username tidak boleh null.");
            }

            Task desTask = readTask(judulTask, username);

            // Programming Defensive: Memastikan perubahanTanggalSelesai adalah objek DateTime yang valid
            if (!(perubahanTanggalSelesai is DateTime))
            {
                throw new ArgumentException("Tanggal Selesai Baru harus berupa objek DateTime.");
            }

            if (perubahanTanggalSelesai.CompareTo(desTask.tanggalSelesai) < 0)
            {
                throw new ArgumentException("Tanggal Selesai Baru harus lebih dari tanggal mulai");
            }

            desTask.tanggalSelesai = perubahanTanggalSelesai;

            createTask(desTask);
            Console.WriteLine("Tanggal selesai sudah menjadi: " + perubahanTanggalSelesai);
        }
        public static void updateJenisTugas(string judulTask, int perubahanJenisTugas, string username)
        {
            //merubah tanggal mulai pada file json
            //pastikan bahwa jenis tugas yang ingin dirubah terdapat di enum
            //Kalo perlu lokasi: string path = @"JSON\tasks.json";

            // Design By Contract: Memastikan judulTask dan username tidak null
            if (judulTask == null || username == null)
            {
                throw new ArgumentNullException("Judul Task atau Username tidak boleh null.");
            }

            // Programming Defensive: Memastikan perubahanJenisTugas adalah angka integer antara 1 dan 9
            if (!(perubahanJenisTugas is int) || perubahanJenisTugas < 1 || perubahanJenisTugas > 9)
            {
                throw new ArgumentException("perubahanJenisTugas harus berupa angka antara 1 dan 9.");
            }
            Task desTask = readTask(judulTask, username);
            if (perubahanJenisTugas.Equals(1))
            {
                desTask.jenisTugas = Task.JenisTugas.Video;
            } else if (perubahanJenisTugas.Equals(2)){
                desTask.jenisTugas = Task.JenisTugas.Laporan;
            } else if (perubahanJenisTugas.Equals(3))
            {
                desTask.jenisTugas = Task.JenisTugas.Project;
            } else if (perubahanJenisTugas.Equals(4))
            {
                desTask.jenisTugas = Task.JenisTugas.Desain;
            } else if (perubahanJenisTugas.Equals(5))
            {
                desTask.jenisTugas = Task.JenisTugas.Proposal;
            }  else if (perubahanJenisTugas.Equals(6))
            {
                desTask.jenisTugas = Task.JenisTugas.SlidePresentasi;
            }  else if (perubahanJenisTugas.Equals(7))
            {
                desTask.jenisTugas = Task.JenisTugas.Observasi;
            } else if (perubahanJenisTugas.Equals(8))
            {
                desTask.jenisTugas = Task.JenisTugas.Quiz;
            } else if (perubahanJenisTugas.Equals(9))
            {
                desTask.jenisTugas = Task.JenisTugas.ForumDiskusi;
            }

            createTask(desTask);
            //Bisa pake JArray kalo gabisa buat 1-1

            Console.WriteLine("Jenis Tugas Terdupdate menjadi Tipe: " + Task.getKodeJenisTugas(desTask.jenisTugas));
         
        }
        public static void updatePrioritas(string judulTask, int perubahanPrioritas, string username)
        {
            //merubah tanggal mulai pada file json
            //pastikan bahwa tingkat prioritas yang ingin dirubah terdapat di enum
            //Kalo perlu lokasi: string path = @"JSON\tasks.json";

            // Design By Contract: Memastikan judulTask dan username tidak null
            if (judulTask == null || username == null)
            {
                throw new ArgumentNullException("Judul Task atau Username tidak boleh null.");
            }

            // Programming Defensive: Memastikan perubahanPrioritas adalah angka integer antara 1 dan 5
            if (perubahanPrioritas < 1 || perubahanPrioritas > 5)
            {
                throw new ArgumentException("perubahanPrioritas harus berupa angka antara 1 dan 5.");
            }

            Task desTask = readTask(judulTask, username);
            if (perubahanPrioritas.Equals(1))
            {
                desTask.namaPrioritas = Task.Prioritas.Highest;
            } else if (perubahanPrioritas.Equals (2))
            {
                desTask.namaPrioritas = Task.Prioritas.High;
            } else if (perubahanPrioritas.Equals(3))
            {
                desTask.namaPrioritas = Task.Prioritas.Medium;
            } else if (perubahanPrioritas.Equals(4))
            {
                desTask.namaPrioritas = Task.Prioritas.Low;
            } else if (perubahanPrioritas.Equals(5))
            {
                desTask.namaPrioritas = Task.Prioritas.Lowest;
            }

            createTask(desTask);

            Console.WriteLine("Tignkat Prioritas Tugas Sekarang Bernilai: " + Task.getUrutanPrioritas(desTask.namaPrioritas));
        }
    }
}
