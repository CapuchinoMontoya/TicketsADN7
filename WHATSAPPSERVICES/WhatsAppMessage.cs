namespace WHATSAPPSERVICES
{
    public class WhatsAppMessage
    {
        /// <summary>
        /// Número de teléfono del destinatario (incluyendo el código de país).
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// Nombre del archivo adjunto (si se envía un documento).
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Archivo adjunto en formato base64 (si se envía un documento).
        /// </summary>
        public string Document { get; set; }

        /// <summary>
        /// Mensaje de texto que acompaña al archivo adjunto.
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="WhatsAppMessage"/>.
        /// </summary>
        /// <param name="to">Número de teléfono del destinatario.</param>
        /// <param name="fileName">Nombre del archivo adjunto.</param>
        /// <param name="document">Archivo adjunto en base64.</param>
        /// <param name="caption">Mensaje de texto.</param>
        public WhatsAppMessage(string to, string fileName, string document, string caption)
        {
            To = to;
            FileName = fileName;
            Document = document;
            Caption = caption;
        }
    }
}
