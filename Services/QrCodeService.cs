using QRCoder;
using System;

namespace TicketsADN7.Services
{
    public interface IQrCodeService
    {
        /// <summary>
        /// Genera un código QR a partir del contenido proporcionado y lo devuelve como una cadena en Base64.
        /// </summary>
        /// <param name="content">Contenido textual que se codificará en el código QR.</param>
        /// <returns>Una cadena Base64 que representa la imagen PNG del código QR generado.</returns>
        string GenerateQrCodeBase64(string content);
    }

    public class QrCodeService : IQrCodeService
    {
        /// <summary>
        /// Genera un código QR con el contenido proporcionado y lo convierte en una imagen PNG codificada en Base64.
        /// </summary>
        /// <param name="content">El texto que se desea convertir en un código QR.</param>
        /// <returns>Una cadena en Base64 que representa el código QR como una imagen PNG.</returns>
        /// <exception cref="ArgumentException">Se lanza si el contenido es nulo o está vacío.</exception>
        public string GenerateQrCodeBase64(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("El contenido para generar el QR no puede estar vacío.");

            using (var qrGenerator = new QRCodeGenerator())
            {
                var qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
                using (var qrCode = new PngByteQRCode(qrCodeData))
                {
                    byte[] qrCodeImage = qrCode.GetGraphic(20);
                    return Convert.ToBase64String(qrCodeImage);
                }
            }
        }
    }
}
