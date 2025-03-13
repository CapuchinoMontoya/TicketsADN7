using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIBRARY.COMMON.Formatos
{
    /// <summary>
    /// Clase que da formato a varias situaciones
    /// </summary>
    public class CaracteresEspeciales
    {
        /// <summary>
        /// Método que da formato a los caracteres de retorno de carro y salto de línea
        /// </summary>
        /// <param name="strNotas"></param>
        /// <returns></returns>
        public static string FormatoCaracteresEspeciales(string strNotas)
        {
            string strTemp;

            strTemp = strNotas.Replace("'", @"\'");
            strTemp = strTemp.Replace("\"", "\"");
            strTemp = strTemp.Replace("\r\r\n", @"\r\n");
            strTemp = strTemp.Replace("\r\n", @"\r\n");

            return strTemp;
        }
    }
}
