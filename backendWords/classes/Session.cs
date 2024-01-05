using System.Security.Cryptography;
using System.Text;

namespace backendWords.classes
{
    public static class Session
    {

        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                return hash;
            }
        }

        public static bool VerifyPassword(string enteredPassword, string storedHash)
        {
            string enteredHash = HashPassword(enteredPassword);
            return enteredHash == storedHash;
        }

        public static string GenerateRememberToken()
        {
            // Token length in bytes (e.g., 32 bytes will give 64 chars after Base64 encoding)
            int tokenLength = 32;

            // Create a byte array with the specified length
            byte[] tokenBytes = new byte[tokenLength];

            using (var rng = new RNGCryptoServiceProvider())
            {
                // Fill the array with cryptographically strong random bytes
                rng.GetBytes(tokenBytes);
            }

            // Convert to a Base64 string which can be safely stored and transmitted
            return Convert.ToBase64String(tokenBytes);
        }
    }
}
