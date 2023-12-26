using System.Security.Cryptography;
using System.Text;

namespace WLVSTools.Web.Core.EncryptionDecryption
{
    /// <summary>
    /// https://www.c-sharpcorner.com/article/encryption-and-decryption-using-a-symmetric-key-in-c-sharp/
    /// </summary>
    public static class AesOperation
    {
        public static string EncryptString(string key, string toEncrypt)
        {
            var keyArray = Convert.FromBase64String(key);
            var info = Encoding.ASCII.GetBytes(toEncrypt);

            var encrypted = Encrypt(keyArray, info);

            return Convert.ToBase64String(encrypted);
        }

        public static string DecryptString(string key, string cipherString)
        {
            var keyArray = Convert.FromBase64String(key);
            var cipherText = Convert.FromBase64String(cipherString);

            var decrypted = Decrypt(keyArray, cipherText);

            return Encoding.ASCII.GetString(decrypted);
        }

        private static byte[] Encrypt(byte[] key, byte[] info)
        {
            using (var cipher = Aes.Create())
            {
                cipher.Key = key;
                cipher.Mode = CipherMode.CBC;
                cipher.Padding = PaddingMode.ISO10126;

                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, cipher.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(info, 0, info.Length);
                    }

                    var ciphertext = ms.ToArray();

                    var message = new byte[cipher.IV.Length + ciphertext.Length];
                    cipher.IV.CopyTo(message, 0);
                    ciphertext.CopyTo(message, cipher.IV.Length);
                    return message;
                }
            }
        }

        private static byte[] Decrypt(byte[] key, byte[] ciphertext)
        {
            using (var cipher = Aes.Create())
            {
                cipher.Key = key;
                cipher.Mode = CipherMode.CBC;
                cipher.Padding = PaddingMode.ISO10126;

                var ivSize = cipher.IV.Length;
                var iv = new byte[ivSize];
                Array.Copy(ciphertext, iv, ivSize);
                cipher.IV = iv;

                var data = new byte[ciphertext.Length - ivSize];
                Array.Copy(ciphertext, ivSize, data, 0, data.Length);

                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, cipher.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(data, 0, data.Length);
                    }

                    return ms.ToArray();
                }
            }
        }

    }
}
