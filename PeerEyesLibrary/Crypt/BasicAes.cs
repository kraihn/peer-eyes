using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace PeerEyesLibrary.Crypt
{
    public static class BasicAes
    {
        private static RijndaelManaged handler;

        static BasicAes()
        {
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes("awefaqr23@QR@Q#FRhqdo232q43213rsadFSDsafd", Encoding.ASCII.GetBytes("sASDFASdfsadfhasf23428039fhna923rAQRW"));
            handler = new RijndaelManaged();
            handler.KeySize = 256;
            handler.BlockSize = 128;
            handler.Padding = PaddingMode.Zeros;
            handler.Key = key.GetBytes(handler.KeySize / 8);
            handler.IV = key.GetBytes(handler.BlockSize / 8);
        }

        public static byte[] EncryptBytes(byte[] value)
        {
            byte[] encrypted;

            ICryptoTransform encryptor = handler.CreateEncryptor(handler.Key, handler.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    csEncrypt.Write(value, 0, value.Length);
                }
                encrypted = msEncrypt.ToArray();
            }
            return encrypted;
        }

        public static byte[] DecryptBytes(byte[] value)
        {
            byte[] decrypted;

            ICryptoTransform decryptor = handler.CreateDecryptor(handler.Key, handler.IV);

            using (MemoryStream msDecrypt = new MemoryStream())
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Write))
                {
                    csDecrypt.Write(value, 0, value.Length);
                }
                decrypted = msDecrypt.ToArray();
            }
            return decrypted;
        }
    }
}
