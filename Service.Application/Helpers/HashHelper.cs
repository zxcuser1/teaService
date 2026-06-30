using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

namespace Service.Application.Helpers
{
    public static class HashHelper
    {
        public static string HashRefreshToken(string input)
        {
            return Convert.ToHexString(SHA512.HashData(Encoding.ASCII.GetBytes(input)));
        }

        public static PasswordDto HashPassword(string password)
        {
            var salt = GenerateSalt(16);
            
            return new PasswordDto {
                PasswordHash = GenerateHash(password, salt),
                Salt = salt,
            };
        }
    
        public static bool VerifyPassword(string password, byte[] salt, string storedHash)
        {
            var hashed = GenerateHash(password, salt);

            return CryptographicOperations.FixedTimeEquals(
                Convert.FromBase64String(hashed), 
                Convert.FromBase64String(storedHash)
            );
        }

        private static string GenerateHash(string password, byte[] salt)
        {
            using var hasher = new Argon2id(Encoding.UTF8.GetBytes(password));
            hasher.Salt = salt;
            hasher.DegreeOfParallelism = 1;
            hasher.MemorySize = 64 * 1024;
            hasher.Iterations = 3;

            return Convert.ToBase64String(hasher.GetBytes(32));
        }
        private static byte[] GenerateSalt(int length)
        {
            byte[] salt = new byte[length];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }
    }
}