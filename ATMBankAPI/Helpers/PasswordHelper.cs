using BCrypt.Net;

namespace ATMBankAPI.Helpers
{
    public class PasswordHelper
    {
        public static string Hashpassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerifyPassword(string password,string HashPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, HashPassword);
        }
    }
}
