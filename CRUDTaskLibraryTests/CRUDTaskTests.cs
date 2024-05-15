using Microsoft.VisualStudio.TestTools.UnitTesting;
using CRUDTaskLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDTaskLibrary.Tests
{
    [TestClass()]
    public class CRUDTaskTests
    {
        [TestMethod()]
        public void updatePrioritasTest_judul()
        {
            /*Task taskTes = new Task();

            taskTes.judul = null;
            taskTes.username = "Bagas";
            taskTes.deskripsi = "blabla";
            taskTes.tanggalMulai = new DateTime(2023, 10, 10);
            taskTes.tanggalSelesai = new DateTime(2023, 12, 10);
            taskTes.jenisTugas = Task.JenisTugas.ForumDiskusi;
            taskTes.namaPrioritas = Task.Prioritas.Highest;
            taskTes.taskState = TaskState.InProgress;*/

            try
            {
                CRUDTask.updatePrioritas(null, 5, "Bagas");
            } catch (Exception ex)
            {
                Console.WriteLine("Ada Null Judul");
            }
        }

        [TestMethod()]
        public void updatePrioritasTest_username()
        {
            try
            {
                CRUDTask.updatePrioritas("Tubes", 5, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ada Null Username");
            }
        }
        [TestMethod()]
        public void updatePrioritasTest_prioritas1()
        {
            try
            {
                CRUDTask.updatePrioritas("Tubes", 0, "Bagas");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Angka < 0");
            }
        }
        [TestMethod()]
        public void updatePrioritasTest_prioritas2()
        {
            try
            {
                CRUDTask.updatePrioritas("Tubes", 6, "Bagas");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Angka < 0");
            }
        }
    }
}