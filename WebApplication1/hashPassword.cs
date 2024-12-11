using System.Security.Cryptography;
using System.Text;

namespace WebApplication1
{
    public class hashPassword
    {
        public string HashingPassword(string pass)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes=Encoding.UTF8.GetBytes(pass);
                byte[] bytePass=sha256Hash.ComputeHash(bytes);
                string hashpass=BitConverter.ToString(bytePass).Replace("-", string.Empty);
                return hashpass;
            }
        }
    }
}
