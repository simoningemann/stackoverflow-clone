using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace WebService
{
    public class PasswordService
    {
        private static RNGCryptoServiceProvider _rng = new RNGCryptoServiceProvider();

        public static string GenerateSalt(int size)
        {
            var buffer = new byte[size];
            
            _rng.GetBytes(buffer); // Helper to generate strong salt
            return Convert.ToBase64String(buffer);
        }

        public static string HashPassword(string password, string salt, int size)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password, // password string given by the user
                Encoding.UTF8.GetBytes(salt), //just convert the salt to bytes array
                KeyDerivationPrf.HMACSHA256, // which hash function to use
                10000, // how many times to re-hash
                size // how many bytes should the hash be
                ));
        }
    }
}