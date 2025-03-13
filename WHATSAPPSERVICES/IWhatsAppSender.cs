namespace WHATSAPPSERVICES
{
    public interface IWhatsAppSender
    {
        /// <summary>
        /// Envía un mensaje de WhatsApp de manera síncrona.
        /// </summary>
        /// <param name="message">El mensaje de WhatsApp que se enviará.</param>
        /// <returns>True si el mensaje se envió correctamente, False en caso contrario.</returns>
        bool SendWhatsAppMessage(WhatsAppMessage message);

        /// <summary>
        /// Envía un mensaje de WhatsApp de manera asíncrona.
        /// </summary>
        /// <param name="message">El mensaje de WhatsApp que se enviará.</param>
        /// <returns>True si el mensaje se envió correctamente, False en caso contrario.</returns>
        Task<bool> SendWhatsAppMessageAsync(WhatsAppMessage message);
    }
}
