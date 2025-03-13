using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace LIBRARY.COMMON.Crypto
{
    /// <summary>
    /// Clase de ayuda para realizar operaciones de encriptación y desencriptación utilizando el algoritmo AES.
    /// </summary>
    public class CryptionHelper
    {
        /// <summary>
        /// Encripta un valor utilizando el algoritmo AES.
        /// </summary>
        /// <param name="value">El valor a encriptar. Puede ser de cualquier tipo, pero se convertirá a cadena.</param>
        /// <param name="key">La clave de encriptación. Debe tener al menos 32 caracteres.</param>
        /// <returns>Una cadena encriptada en formato hexadecimal.</returns>
        /// <exception cref="Exception">Se lanza si el valor a encriptar es nulo o vacío, o si ocurre un error durante la encriptación.</exception>
        public static string Encrypt(object value, string key)
        {
            try
            {
                string plainText = value == null || value == DBNull.Value ? string.Empty : value.ToString();

                if (string.IsNullOrEmpty(plainText))
                    throw new Exception("El valor a encriptar no puede ser nulo o vacío.");

                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = Encoding.UTF8.GetBytes(key.PadRight(32));
                    aesAlg.GenerateIV();
                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(plainText);
                            }
                        }
                        byte[] iv = aesAlg.IV;
                        byte[] encrypted = msEncrypt.ToArray();
                        byte[] result = new byte[iv.Length + encrypted.Length];
                        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                        Buffer.BlockCopy(encrypted, 0, result, iv.Length, encrypted.Length);

                        return BitConverter.ToString(result).Replace("-", "");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al encriptar la cadena. {ex.Message}", ex.InnerException);
            }
        }

        /// <summary>
        /// Desencripta una cadena encriptada utilizando el algoritmo AES.
        /// </summary>
        /// <param name="cipherText">La cadena encriptada en formato hexadecimal.</param>
        /// <param name="key">La clave de desencriptación. Debe ser la misma que se usó para encriptar.</param>
        /// <returns>La cadena desencriptada.</returns>
        /// <exception cref="Exception">Se lanza si ocurre un error durante la desencriptación.</exception>
        public static string Decrypt(string cipherText, string key)
        {
            try
            {
                byte[] fullCipher = new byte[cipherText.Length / 2];
                for (int i = 0; i < fullCipher.Length; i++)
                {
                    fullCipher[i] = Convert.ToByte(cipherText.Substring(i * 2, 2), 16);
                }
                byte[] iv = new byte[16];
                Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);

                byte[] cipher = new byte[fullCipher.Length - iv.Length];
                Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, cipher.Length);

                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = Encoding.UTF8.GetBytes(key.PadRight(32).Substring(0, 32));
                    aesAlg.IV = iv;

                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    using (MemoryStream msDecrypt = new MemoryStream(cipher))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                return srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while decrypting the string. {ex.Message}", ex.InnerException);
            }
        }
    }
}