using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace UserWebClient.Logic
{
    public static class AuthenticationUtil
    {
        public static string HashPasswordString(string email, string password)
        {
            return GetHashString(password + email);
        }

        public static byte[] HashPasswordByte(string email, string password)
        {
            return GetHash(password + email);
        }

        private static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = MD5.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        private static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
    }
}