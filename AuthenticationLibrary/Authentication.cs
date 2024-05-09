namespace AuthenticationLibrary
{
    public class Authentication
    {
        public void signUpAccount(Account akun, string path)
        {
            //Berperan sebagai sign up
            //Menulis file json
            //Pada bagian serialize sesuaikan dengan parameter input akun
            //Pastikan semua username berbeda
        }
        public Account signInAccount(string username, string password)
        {
            //Berperan sebagai sign in
            //Mencari file json yang menyimpan username dan password sesuai parameter
            //jika file ditemukan maka return object account sesuai informasi di file json
            //jika file tidak ditemukan return null
            return null;
        }
        public Account signOutAccount(string username, string password)
        {
            return null;
        }
    }
}
