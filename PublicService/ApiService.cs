
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PublicService
{
    public static class ApiService
    {
        #region public static string Fix(string text)
        /// <summary>
        /// Fix some text 
        /// </summary>
        /// <param name="text">Some Message Text For Fix Typing bug</param>
        /// <returns></returns>
        public static string Fix(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return string.Empty;
            }

            text = text.Trim();

            while (text.Contains("  "))
            {
                text = text.Replace("  ", " ");
            }

            return text;
        }
        #endregion public static string Fix(string text)

        #region public static string HashPasword(string password, out byte[] salt)
        /// <summary>
        /// This methos hashing all input string
        /// </summary>
        /// <param name="password">string for create hash</param>
        /// <param name="salt">salt for hash password</param>
        /// <returns>hash string</returns>
        public static string HashPasword(this string password, out string saltString)
        {
            var salt = RandomNumberGenerator.GetBytes(64);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                350000,
                HashAlgorithmName.SHA512,
            64);

            if (salt == null)
                saltString = "";
            else
                saltString = Convert.ToBase64String(salt);

            return Convert.ToHexString(hash);
        }
        #endregion /public static string HashPasword(string password, out byte[] salt)

        #region public static bool ConditionHash(string password, string hash, byte[] salt)
        /// <summary>
        /// This methos hashing all input string
        /// </summary>
        /// <param name="password">string for create hash</param>
        /// <param name="salt">salt for hash password</param>
        /// <returns>hash string</returns>
        public static bool ConditionHash(this string password, string hash, string saltString)
        {
            var salt = Encoding.ASCII.GetBytes(saltString);
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, 350000, HashAlgorithmName.SHA512, 64);
            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }
        #endregion /public static bool ConditionHash(string password, string hash, byte[] salt)
    }
}
