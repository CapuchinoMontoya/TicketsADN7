using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIBRARY.COMMON.Crypto
{
    public class HexBuilder
    {
        /// <summary>
        /// Conversión de arreglo de bytes a string
        /// </summary>
        /// <param name="ba">Arreglo de bytes a convertir</param>
        /// <returns>Cadena que contiene valores HEX</returns>
        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        /// <summary>
        /// Converrsión de arreglo de string a arreglo de bytes
        /// </summary>
        /// <param name="hex">Cadena que contiene valores HEX a convertir</param>
        /// <returns>Arreglo de bytes</returns>
        public static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
    }
}
