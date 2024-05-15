using System.IO;
using System.Security.Principal;
using System.Text.Json;

namespace AuthenticationLibrary
{
    public class Authentication
    {
        private bool usernameSudahAda(string username, string path)
        {
            try
            {   
                string jsonData = File.ReadAllText(path);
                Account akun = JsonSerializer.Deserialize<Account>(jsonData);
                if(akun != null && akun.email == username)
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

        public void signUpAccount(Account akun, string path)
        {
            //Berperan sebagai sign up
            //Menulis file json
            //Pada bagian serialize sesuaikan dengan parameter input akun
            //Pastikan semua username berbeda

            if(usernameSudahAda(akun.email, path))
            {
                //username sudah ada
                return;
            }

            string jsonData = JsonSerializer.Serialize(akun);
            try
            {
                File.WriteAllText(path, jsonData);
            }
            catch (Exception ex)
            {
                return;
            }

        }

        public Account signInAccount(string username, string password, string file)
        {
            //Berperan sebagai sign in
            try
            {
                //Mencari file json yang menyimpan username dan password sesuai parameter
                string jsonData = File.ReadAllText(file);
                Account akun = JsonSerializer.Deserialize<Account>(jsonData);
                if (akun != null && akun.email == username && akun.password == password)
                {
                    //jika file ditemukan maka return object account sesuai informasi di file json
                    return akun;
                }
                else
                {
                    //username atau password tidak sesuai
                    return null;
                }
            }            
            //jika file tidak ditemukan return null
            catch (FileNotFoundException)
            {
                return null;
            }
        }

        public Account signOutAccount(string username, string password)
        {
            return null;
        }
    }
}
