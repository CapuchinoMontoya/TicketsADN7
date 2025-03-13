using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LIBRARY.COMMON.Crypto
{
    public static class DESCipher
    {
        /// <summary>
        /// Encripción TrupleDES de una cadena enviada
        /// </summary>
        /// <param name="toEncrypt">Cadena a encriptar</param>
        /// <param name="key">Llave o password de la cadena a utilizar para cifrar</param>
        /// <param name="useHashing">Indica si la cadena a crifrar tiene HASH</param>
        /// <returns>Cadena convertida a su valor encriptado</returns>
        public static string Encrypt(string toEncrypt, string key, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            //If hashing use get hashcode regards to your key
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

                hashmd5.Clear();
            }
            else
            {
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();

            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// Decripción TrupleDES de una cadena enviada
        /// </summary>
        /// <param name="cipherString">Cadena a descifrar</param>
        /// <param name="key">Llave o password de la cadena a utilizar para descifrar</param>
        /// <param name="useHashing">Indica si la cadena a descrifrar tiene HASH</param>
        /// <returns>Cadena convertida a su valor no encriptado</returns>
        public static string Decrypt(string cipherString, string key, bool useHashing)
        {
            byte[] keyArray;

            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
            {
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

            tdes.Key = keyArray;

            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);          
            tdes.Clear();

            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }
}
