using AuthenticationLibrary;
using CRUDTaskLibrary;
using Task = CRUDTaskLibrary.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;
using System.ComponentModel;

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
                        inputAccount = getInputAccountData(accountValidator);
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

                        CRUDTask.updatePrioritas(judulLama, nomorPrioritas,username);
                        break;
                }
            }
        }

        private Account getInputAccountData(AccountValidator validator)
        {
            Account newAccount = new Account();
            ValidationResult validationResult;

            // Looping hingga semua atribut valid
            do
            {
                // Input username
                do
                {
                    Console.Write("Username: ");
                    newAccount.userName = Console.ReadLine();
                    validationResult = validator.Validate(newAccount, uleSet: "Username");

                    if (!validationResult.IsValid)
                    {
                        // Tampilkan pesan kesalahan jika validasi gagal
                        foreach (var error in validationResult.Errors)
                        {
                            Console.WriteLine(error.ErrorMessage);
                        }
                    }

                } while (!validationResult.IsValid);

                // Input nama
                do
                {
                    Console.Write("Nama: ");
                    newAccount.nama = Console.ReadLine();
                    validationResult = validator.Validate(newAccount, ruleSet: "Nama");

                    if (!validationResult.IsValid)
                    {
                        // Tampilkan pesan kesalahan jika validasi gagal
                        foreach (var error in validationResult.Errors)
                        {
                            Console.WriteLine(error.ErrorMessage);
                        }
                    }

                } while (!validationResult.IsValid);

                // Input email
                do
                {
                    Console.Write("Email: ");
                    newAccount.email = Console.ReadLine();
                    validationResult = validator.Validate(newAccount, ruleSet: "Email");

                    if (!validationResult.IsValid)
                    {
                        // Tampilkan pesan kesalahan jika validasi gagal
                        foreach (var error in validationResult.Errors)
                        {
                            Console.WriteLine(error.ErrorMessage);
                        }
                    }

                } while (!validationResult.IsValid);

                // Input password
                do
                {
                    Console.Write("Password: ");
                    newAccount.password = Console.ReadLine();
                    validationResult = validator.Validate(newAccount, ruleSet: "Password");

                    if (!validationResult.IsValid)
                    {
                        // Tampilkan pesan kesalahan jika validasi gagal
                        foreach (var error in validationResult.Errors)
                        {
                            Console.WriteLine(error.ErrorMessage);
                        }
                    }

                } while (!validationResult.IsValid);

            } while (!validationResult.IsValid);

            return newAccount;
        }

        private Account getInputAccountData()
        {
            Account newAccount = new Account();
            Console.Write("Username: ");
            newAccount.userName = Console.ReadLine();
            while (!Authentication.FindAccount(newAccount.userName).Equals(null))
            {
                Console.WriteLine("Username sudah digunakan, buat username yang unik!");
                Console.Write("Username: ");
                newAccount.userName = Console.ReadLine();
            }
            Console.Write("Nama: ");
            newAccount.nama = Console.ReadLine();
            Console.Write("Email: ");
            newAccount.email = Console.ReadLine();
            Console.Write("Password: ");
            newAccount.password = Console.ReadLine();
            return newAccount;
        }
    }
}
