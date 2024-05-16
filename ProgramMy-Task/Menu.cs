using AuthenticationLibrary;
using CRUDTaskLibrary;
using Task = CRUDTaskLibrary.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace ProgramMy_Task
{
    public class Menu
    {
        public void printAllMenu()
        {
            Console.WriteLine("----- MENU -----");
            Console.WriteLine("1. Create Account");
            Console.WriteLine("2. Sign in");
            Console.WriteLine("3. Sign out");
            Console.WriteLine("4. Create Task");
            Console.WriteLine("5. Search Task");
            Console.WriteLine("6. Change Judul");
            Console.WriteLine("7. Change Deskripsi");
            Console.WriteLine("8. Change Tanggal Mulai");
            Console.WriteLine("9. Change Tanggal Selesai");
            Console.WriteLine("10. Change Jenis Tugas");
            Console.WriteLine("11. Change Prioritas");
            Console.WriteLine("0. Exit");
            Console.Write("Select menu: ");
        }

        public void run()
        {
            int selectedMenu = 1;
            Account inputAccount = new Account();
            Task inputTask = new Task();
            AccountValidator accountValidator = new AccountValidator();
            while (selectedMenu != 0)
            {
                printAllMenu();
                selectedMenu = int.Parse(Console.ReadLine());
                switch (selectedMenu)
                {
                    case 1:
                        Console.WriteLine("Silahkan masukkan data data yang diperlukan");
                        inputAccount = Authentication.getInputAccountData(accountValidator);
                        ValidationResult validationResult = accountValidator.Validate(inputAccount);
                        if (validationResult.IsValid)
                        {
                            
                        }
                        else
                        {
                            // Tampilkan pesan kesalahan jika validasi gagal
                            foreach (var error in validationResult.Errors)
                            {
                                Console.WriteLine(error.ErrorMessage);
                            }
                        }
                        break;
                    case 2:

                        break;
                    case 3:

                        break;
                    case 4:

                        break;
                    case 5:

                        break;
                    case 6:
                        Console.Write("Masukkan Judul Tugas Lama: ");
                        string judulLama = Console.ReadLine();
                        Console.Write("Masukkan Username: ");
                        string username = Console.ReadLine();
                        Console.Write("Masukkan Judul Baru: ");
                        string judulBaru = Console.ReadLine();

                        CRUDTask.updateJudul(judulLama, judulBaru, username);
                        break;
                    case 7:
                        Console.Write("Masukkan Judul Tugas: ");
                        judulLama = Console.ReadLine();
                        Console.Write("Masukkan Username: ");
                        username = Console.ReadLine();
                        Console.Write("Masukkan Deskripsi Baru: ");
                        string deskripsiBaru = Console.ReadLine();

                        CRUDTask.updateDeskripsi(judulLama, deskripsiBaru, username);
                        break;
                    case 8:
                        Console.Write("Masukkan Judul Tugas: ");
                        judulLama = Console.ReadLine();
                        Console.Write("Masukkan Username: ");
                        username = Console.ReadLine();
                        Console.Write("Masukkan Tanggal Mulai Baru (YYYY-MM-DD): ");
                        string tanggalMulaiBaru = Console.ReadLine();

                        CRUDTask.updateTanggalMulai(judulLama, DateTime.Parse(tanggalMulaiBaru), username);
                        break;
                    case 9:
                        Console.Write("Masukkan Judul Tugas: ");
                        judulLama = Console.ReadLine();
                        Console.Write("Masukkan Username: ");
                        username = Console.ReadLine();
                        Console.Write("Masukkan Tanggal Selesai Baru (YYYY-MM-DD): ");
                        string tanggalSelesaiBaru = Console.ReadLine();

                        CRUDTask.updateTanggalSelesai(judulLama, DateTime.Parse(tanggalSelesaiBaru), username);
                        break;
                    case 10:
                        Console.Write("Masukkan Judul Tugas: ");
                        judulLama = Console.ReadLine();
                        Console.Write("Masukkan Username: ");
                        username = Console.ReadLine();
                        Console.WriteLine("1. Video");
                        Console.WriteLine("2. Laporan");
                        Console.WriteLine("3. Project");
                        Console.WriteLine("4. Desain");
                        Console.WriteLine("5. Proposal");
                        Console.WriteLine("6. Slide Presentasi");
                        Console.WriteLine("7. Observasi");
                        Console.WriteLine("8. Quiz");
                        Console.WriteLine("9. Forum Diskusi");
                        Console.Write("Masukkan Nomor Pilihan Jenis Tugas: ");

                        int nomorJenisTugas = int.Parse(Console.ReadLine());

                        CRUDTask.updateJenisTugas(judulLama, nomorJenisTugas, username);
                        break;
                    case 11:
                        Console.Write("Masukkan Judul Tugas: ");
                        judulLama = Console.ReadLine();
                        Console.Write("Masukkan Username: ");
                        username = Console.ReadLine();
                        Console.WriteLine("1. Highest");
                        Console.WriteLine("2. High");
                        Console.WriteLine("3. Medium");
                        Console.WriteLine("4. Low");
                        Console.WriteLine("5. Lowest");

                        int nomorPrioritas = int.Parse(Console.ReadLine());

                        CRUDTask.updatePrioritas(judulLama, nomorPrioritas, username);
                        break;
                }
            }
        }

        
    }
}
