namespace EmailService
{
    /// <summary>
    /// Interfaz que define los métodos para enviar correos electrónicos de manera síncrona y asíncrona.
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Envía un correo electrónico de manera síncrona.
        /// </summary>
        /// <param name="message">El mensaje de correo electrónico que se enviará.</param>
        void SendEmail(Message message);

        /// <summary>
        /// Envía un correo electrónico de manera asíncrona.
        /// </summary>
        /// <param name="message">El mensaje de correo electrónico que se enviará.</param>
        /// <returns>Una tarea que representa la operación asíncrona.</returns>
        Task SendEmailAsync(Message message);
    }
}