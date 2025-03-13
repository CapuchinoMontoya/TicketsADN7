using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace LIBRARY.COMMON.Crypto
{
    /// <summary>
    /// Clase estática que proporciona métodos para encriptar y desencriptar cadenas utilizando el algoritmo Rijndael (AES).
    /// </summary>
    public static class StringCipher
    {
        // Este constante define el tamaño de la clave de encriptación en bits.
        private const int Keysize = 128;

        // Este constante define el número de iteraciones para la generación de bytes de la contraseña.
        private const int DerivationIterations = 1000;

        /// <summary>
        /// Encripta una cadena de texto utilizando una frase de contraseña.
        /// </summary>
        /// <param name="plainText">La cadena de texto a encriptar.</param>
        /// <param name="passPhrase">La frase de contraseña utilizada para la encriptación.</param>
        /// <returns>La cadena encriptada en formato Base64.</returns>
        public static string Encrypt(string plainText, string passPhrase)
        {
            // Genera bytes aleatorios para el salt y el vector de inicialización (IV).
            var saltStringBytes = Generate128BitsOfRandomEntropy();
            var ivStringBytes = Generate128BitsOfRandomEntropy();
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            // Genera la clave a partir de la frase de contraseña y el salt.
            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);

                // Configura el algoritmo de encriptación Rijndael.
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 128;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;

                    // Crea un encriptador usando la clave y el IV.
                    using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                // Escribe los bytes del texto plano en el CryptoStream.
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();

                                // Combina el salt, el IV y los bytes encriptados en un solo arreglo.
                                var cipherTextBytes = saltStringBytes;
                                cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();

                                // Convierte el arreglo de bytes a una cadena Base64.
                                return Convert.ToBase64String(cipherTextBytes).Replace("/", "*");
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Desencripta una cadena encriptada utilizando una frase de contraseña.
        /// </summary>
        /// <param name="cipherText">La cadena encriptada en formato Base64.</param>
        /// <param name="passPhrase">La frase de contraseña utilizada para la desencriptación.</param>
        /// <returns>La cadena desencriptada.</returns>
        public static string Decrypt(string cipherText, string passPhrase)
        {
            // Convierte la cadena Base64 en un arreglo de bytes.
            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText.Replace("*", "/"));

            // Extrae el salt y el IV del arreglo de bytes.
            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
            var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

            // Genera la clave a partir de la frase de contraseña y el salt.
            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);

                // Configura el algoritmo de desencriptación Rijndael.
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 128;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;

                    // Crea un desencriptador usando la clave y el IV.
                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                // Lee los bytes desencriptados y los convierte a una cadena.
                                var plainTextBytes = new byte[cipherTextBytes.Length];
                                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Genera 128 bits de entropía aleatoria utilizando un generador de números aleatorios criptográficamente seguro.
        /// </summary>
        /// <returns>Un arreglo de 16 bytes con valores aleatorios.</returns>
        private static byte[] Generate128BitsOfRandomEntropy()
        {
            var randomBytes = new byte[16]; // 16 bytes = 128 bits.
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                // Llena el arreglo con bytes aleatorios seguros.
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
        }
    }
}