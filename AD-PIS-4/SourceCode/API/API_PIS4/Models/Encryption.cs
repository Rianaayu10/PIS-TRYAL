﻿using System.Text;
using System.Security.Cryptography;

namespace API_PIS4.Models
{

    public class Encryption
    {
        public string Encrypt(string clearText, string EncryptionKey)
        {
			EncryptionKey = EncryptionKey + "PIS42023";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                #pragma warning disable SYSLIB0041 // Type or member is obsolete
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                #pragma warning restore SYSLIB0041 // Type or member is obsolete
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = System.Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public string Decrypt(string cipherText, string EncryptionKey)
        {
            if (cipherText.Length < 5)
            {
                return cipherText;
            }
            EncryptionKey = EncryptionKey + "PIS42023";
            byte[] cipherBytes = System.Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                #pragma warning disable SYSLIB0041 // Type or member is obsolete
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                #pragma warning restore SYSLIB0041 // Type or member is obsolete
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
    }
}