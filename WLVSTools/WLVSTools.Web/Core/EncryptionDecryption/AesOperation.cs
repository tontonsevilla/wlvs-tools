using System.Security.Cryptography;
using System.Text;

namespace WLVSTools.Web.Core.EncryptionDecryption
{
    /// <summary>
    /// https://www.c-sharpcorner.com/article/encryption-and-decryption-using-a-symmetric-key-in-c-sharp/
    /// </summary>
    public static class AesOperation
    {
        public static string EncryptString(string key, string plainText)
        {
            byte[] rawPlaintext = System.Text.Encoding.Unicode.GetBytes("This is all clear now!");

            using (Aes aes = new AesManaged())
            {
                aes.Padding = PaddingMode.PKCS7;
                aes.KeySize = 128;          // in bits
                aes.Key = new byte[128 / 8];  // 16 bytes for 128 bit encryption
                aes.IV = new byte[128 / 8];   // AES needs a 16-byte IV
                                              // Should set Key and IV here.  Good approach: derive them from 
                                              // a password via Cryptography.Rfc2898DeriveBytes 
                byte[] cipherText = null;
                byte[] plainText = null;

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(rawPlaintext, 0, rawPlaintext.Length);
                    }

                    cipherText = ms.ToArray();
                }


                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherText, 0, cipherText.Length);
                    }

                    plainText = ms.ToArray();
                }
                string s = System.Text.Encoding.Unicode.GetString(plainText);
                Console.WriteLine(s);
            }
        }

        public static string DecryptString(string key, string encrypted)
        {
            
        }
    }
}
