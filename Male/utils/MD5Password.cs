using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Male.utils
{
    public static class MD5Password
    {
        public static string HashPass(string password, string salt)
        {
            var bytes = new UTF8Encoding().GetBytes(password + salt);
            var hashBytes = System.Security.Cryptography.MD5.Create().ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }

        public static bool ComparePassword(string password, string salt, string passHash) => passHash.Equals(HashPass(password, salt));
    }
}