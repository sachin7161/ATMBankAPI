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

        public static string HashPin(string pin)
        {
            return BCrypt.Net.BCrypt.HashPassword(pin);
        }
        public static bool VerifyPin(string pin,string HashPin)
        {
            return BCrypt.Net.BCrypt.Verify(pin, HashPin);
        }

        public static string NewHashPin(string pin)
        {
            return BCrypt.Net.BCrypt.HashPassword(pin);
        }
       
    }
}
