using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace AuthenticationLibrary
{
    public static class Authentication
    {
        public class Authentication
        {
        private string _path;
        }
        public Authentication(string path)
        {
            _path = path;
        }

        public void signUpAccount(Account akun)
        {
            if (!IsUsernameUnique(akun.userName))
            {
                throw new Exception("Username sudah digunakan.");
            }

            var accounts = GetAllAccounts();
            accounts.Add(akun);
            SaveAccounts(accounts);
        }

        public Account signInAccount(string username, string password)
        {
            var account = GetAllAccounts().FirstOrDefault(a => a.userName == username && a.password == password);
            return account;
        }

        public Account signOutAccount(string username, string password)
        {
            return null;
        }

        private List<Account> GetAllAccounts()
        {
            if (!File.Exists(_path))
            {
                File.WriteAllText(_path, "[]");
            }

            var json = File.ReadAllText(_path);
            var accounts = JsonConvert.DeserializeObject<List<Account>>(json);
            return accounts ?? new List<Account>();
        }

        private void SaveAccounts(List<Account> accounts)
        {
            var json = JsonConvert.SerializeObject(accounts);
            File.WriteAllText(_path, json);
        }

        private bool IsUsernameUnique(string username)
        {
            var accounts = GetAllAccounts();
            return !accounts.Any(a => a.userName == username);
        }
    }
    String Authentication = "Users/ahmadfadliakbar/Projects/My-Task/AuthenticationLibrary/Authentication.json";
    Authentication auth = new Authentication(Authentication.json);
        
        private static bool usernameSudahAda(string username, string path)
        {
            try
            {
                string jsonData = File.ReadAllText(path);
                Account akun = JsonSerializer.Deserialize<Account>(jsonData);
                if (akun != null && akun.email == username)
                {
                    //username sudah ada
                    return true;
                }
                else
                {
                    //username belum ada
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //private string _path;

        //public static Account FindAccount(string userName)
        //{
        //    string filePath = $"{userName}.json";
        //    if (File.Exists(filePath))
        //    {
        //        try
        //        {
        //            string jsonString = File.ReadAllText(filePath);
        //            Account account = JsonSerializer.Deserialize<Account>(jsonString);
        //            return account;
        //        }
        //        catch (JsonException e)
        //        {
        //            Console.WriteLine($"Error deserializing JSON: {e.Message}");
        //            return null;
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine($"{userName} not found.");
        //        return null;
        //    }
        //}
        

        //public static Authentication(string path)
        //{
        //    _path = path;
        //}

        //public static void signUpAccount(string userName, string nama, string email, string password)
        //{
        //    Account account = new Account();
        //    account.userName = userName;
        //    account.nama = nama;
        //    account.email = email;
        //    account.password = password;
        //    account.state = AccountState.SignedOut;

        //    if (!IsUsernameUnique(akun.userName))
        //    {
        //        throw new Exception("Username sudah digunakan.");
        //    }

        //    var accounts = GetAllAccounts();
        //    accounts.Add(akun);
        //    SaveAccounts(accounts);
        //}

        //public static Account signInAccount(string username, string password)
        //{
        //    var account = GetAllAccounts().FirstOrDefault(a => a.userName == username && a.password == password);
        //    return account;
        //}

        //public static Account signOutAccount(string username, string password)
        //{
        //    // Implementasi signOutAccount sesuai kebutuhan aplikasi
        //    return null;
        //}

        //private List<Account> GetAllAccounts()
        //{
        //    if (!File.Exists(_path))
        //    {
        //        File.WriteAllText(_path, "[]");
        //    }

        //    var json = File.ReadAllText(_path);
        //    var accounts = JsonConvert.DeserializeObject<List<Account>>(json);
        //    return accounts ?? new List<Account>();
        //}

        //private static void SaveAccounts(List<Account> accounts)
        //{
        //    var json = JsonConvert.SerializeObject(accounts);
        //    File.WriteAllText(_path, json);
        //}

        //private static bool IsUsernameUnique(string username)
        //{
        //    var accounts = GetAllAccounts();
        //    return !accounts.Any(a => a.userName == username);
        //}
    }
}
