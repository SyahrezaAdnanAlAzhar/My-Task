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
         public static void updateJenisTugas<T, U, V>(T judulTask, U perubahanJenisTugas, V username)
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
     dynamic dynamicJenisTugas = perubahanJenisTugas;
     if (!(dynamicJenisTugas is int) || dynamicJenisTugas < 1 || dynamicJenisTugas > 9)
     {
         throw new ArgumentException("perubahanJenisTugas harus berupa angka antara 1 dan 9.");
     }
     Task desTask = readTask(judulTask, username);

     createTask(desTask);

     if (perubahanJenisTugas.Equals(1))
     {
         desTask.jenisTugas = Task.JenisTugas.Video;
     }
     else if (perubahanJenisTugas.Equals(2))
     {
         desTask.jenisTugas = Task.JenisTugas.Laporan;
     }
     else if (perubahanJenisTugas.Equals(3))
     {
         desTask.jenisTugas = Task.JenisTugas.Project;
     }
     else if (perubahanJenisTugas.Equals(4))
     {
         desTask.jenisTugas = Task.JenisTugas.Desain;
     }
     else if (perubahanJenisTugas.Equals(5))
     {
         desTask.jenisTugas = Task.JenisTugas.Proposal;
     }
     else if (perubahanJenisTugas.Equals(6))
     {
         desTask.jenisTugas = Task.JenisTugas.SlidePresentasi;
     }
     else if (perubahanJenisTugas.Equals(7))
     {
         desTask.jenisTugas = Task.JenisTugas.Observasi;
     }
     else if (perubahanJenisTugas.Equals(8))
     {
         desTask.jenisTugas = Task.JenisTugas.Quiz;
     }
     else if (perubahanJenisTugas.Equals(9))
     {
         desTask.jenisTugas = Task.JenisTugas.ForumDiskusi;
     }
     createTask(desTask);
     //Bisa pake JArray kalo gabisa buat 1-1

     Console.WriteLine("Jenis Tugas Terdupdate menjadi Tipe: " + Task.getKodeJenisTugas(desTask.jenisTugas));

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
