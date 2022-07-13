using System.Security.Cryptography;
using System.Text;

namespace Hexa.Data.Utill
{
    public static class Helper
    {

        #region Helper functions

        public static string ComputeHash(string toHash, string salt)
        {
            var byteResult = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(toHash), Encoding.UTF8.GetBytes(salt), 10000);
            return Convert.ToBase64String(byteResult.GetBytes(24));
        }


        #endregion
    }
}
