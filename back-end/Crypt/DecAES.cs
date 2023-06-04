using System.Security.Cryptography;
using System.Text;
using back_end.Data.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using NuGet.Common;

namespace back_end.Crypt
{
    /// <summary>
    /// decryption for DBconnection string
    /// </summary>
    public class DecAES
    {
        /// <summary>
        /// connection string Dec method
        /// </summary>
        /// <param name="EncMessage"></param>
        /// <returns></returns>
        public static string Dec(string? EncMessage)
        {
            if (EncMessage == null)
                return "Fail";
            byte[] encryptedData = Convert.FromBase64String(EncMessage);

            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.PKCS7;

            rijndaelCipher.KeySize = 256;
            rijndaelCipher.BlockSize = 128;

            using (StreamReader file = File.OpenText("./secret.json"))
            {
                string json = file.ReadToEnd();
                var secret = JsonConvert.DeserializeObject<Secret>(json);
             
                if (secret == null)
                    return "Fail";

                if (secret.Password == null || secret.Iv_pass == null)
                    return "Fail";
                var key = new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(secret.Password));
                var iv = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(secret.Iv_pass));
                rijndaelCipher.Key = key;

                rijndaelCipher.IV = iv; ;

            }
            byte[] plainText = rijndaelCipher.CreateDecryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length);

            return Encoding.UTF8.GetString(plainText);
        }

    }
}