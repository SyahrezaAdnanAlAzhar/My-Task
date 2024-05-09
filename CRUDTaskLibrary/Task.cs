using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDTaskLibrary
{

    internal class Task
    {
        public enum JenisTugas { Video, Laporan, Project, Desain, Proposal, SlidePresentasi, Observasi, Quiz, ForumDiskusi };

        public static string getKodeJenisTugas(JenisTugas jenisTugas)
        {
            string[] KodeJenisTugas = { "F-1V", "F-2L", "F-3P", "F-4D", "F-5P", "F-6S", "NF-1O", "NF-2Q", "NF-3F" };
            return KodeJenisTugas[(int)jenisTugas];
        }
        public string judul { get; set; }
        public string deskripsi { get; set; }
        public DateTime tanggalMulai { get; set; }
        public DateTime tanggalSelesai { get; set; }
        public JenisTugas jenisTUgas { get; set; }
        public int noPrioritas { get; set; }


    }
}
