using System.IO;
using System.Text.Json;

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
                Task dataTask = JsonSerializer.Deserialize<Task>(path);
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
        public static void updateJudul<T, U, V>(T judulAwalTask, U judulPerubahanTask, V username)
        {
            //merubah judul pada file json
        }
        public static void updateDeskripsi<T, U, V>(T judulTask, U perubahanDeskripsi, V username)
        {
            //merubah deskrispi pada file json
        }
        public static void updateTanggalMulai<T, U, V>(T judulTask, U perubahanTanggalMulai, V username)
        {
            //merubah tanggal mulai pada file json
            //pastikan perubahan tanggal mulai yaitu pada tanggal sebelum tanggalSelesai
            //Pastikan statenya ikut menyesuaikan
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
