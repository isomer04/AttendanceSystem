using System.Security.Cryptography;
using System.Text;

namespace AttendanceSystem.Services
{
    internal class PasswordHasher
    {

        private const string Pepper = "StudentPepperValue";
        private const int Iteration = 10;

        public static string ComputeHash(string password, string salt)
        {
            if (Iteration <= 0)
            {
            }

            using (var sha256 = SHA256.Create())
            {
                var passwordSaltPepper = $"{password}{salt}{Pepper}";
                var byteValue = Encoding.UTF8.GetBytes(passwordSaltPepper);
                var byteHash = sha256.ComputeHash(byteValue);
                var hash = Convert.ToBase64String(byteHash);
                return ComputeHash(hash, salt, Iteration - 1);
            }
        }

        private static string ComputeHash(string password, string salt, int iteration)
        {
            if (iteration <= 0) return password;

            using (var sha256 = SHA256.Create())
            {
                var passwordSaltPepper = $"{password}{salt}{Pepper}";
                var byteValue = Encoding.UTF8.GetBytes(passwordSaltPepper);
                var byteHash = sha256.ComputeHash(byteValue);
                var hash = Convert.ToBase64String(byteHash);
                return ComputeHash(hash, salt, iteration - 1);
            }
        }

        public static string GenerateSalt()
        {
            using var rng = RandomNumberGenerator.Create();
            var byteSalt = new byte[16];
            rng.GetBytes(byteSalt);
            var salt = Convert.ToBase64String(byteSalt);
            return salt;
        }
    }
}
