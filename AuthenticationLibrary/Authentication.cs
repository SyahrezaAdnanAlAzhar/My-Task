using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace AuthenticationLibrary
{
    public class Authentication
    {
        private string _path;

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
            // Implementasi signOutAccount sesuai kebutuhan aplikasi
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
}
