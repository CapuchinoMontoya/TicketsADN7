using MailKit.Net.Smtp;
using MimeKit;

namespace EmailService
{
    /// <summary>
    /// Clase que implementa la interfaz <see cref="IEmailSender"/> para enviar correos electrónicos utilizando el protocolo SMTP.
    /// </summary>
    public class EmailSender : IEmailSender
    {
        /// <summary>
        /// Configuración del servidor de correo electrónico.
        /// </summary>
        public readonly EmailConfiguration _emailConfig;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="EmailSender"/> con la configuración proporcionada.
        /// </summary>
        /// <param name="emailConfig">La configuración del servidor de correo electrónico.</param>
        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        /// <summary>
        /// Envía un correo electrónico de manera síncrona.
        /// </summary>
        /// <param name="message">El mensaje de correo electrónico que se enviará.</param>
        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
        }

        /// <summary>
        /// Envía un correo electrónico de manera asíncrona.
        /// </summary>
        /// <param name="message">El mensaje de correo electrónico que se enviará.</param>
        /// <returns>Una tarea que representa la operación asíncrona.</returns>
        public async Task SendEmailAsync(Message message)
        {
            var mailMessage = CreateEmailMessage(message);
            await SendAsync(mailMessage);
        }

        /// <summary>
        /// Crea un mensaje de correo electrónico a partir de un objeto <see cref="Message"/>.
        /// </summary>
        /// <param name="message">El mensaje de correo electrónico que se convertirá en un objeto <see cref="MimeMessage"/>.</param>
        /// <returns>Un objeto <see cref="MimeMessage"/> listo para ser enviado.</returns>
        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.FromName, _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;

            // Personaliza el formato del mensaje con HTML.
            var bodyBuilder = new BodyBuilder { HtmlBody = string.Format("<h2 style='color:red;'>{0}</h2>", message.Content) };

            // Agrega archivos adjuntos si existen.
            if (message.Attachments != null && message.Attachments.Any())
            {
                byte[] fileBytes;
                foreach (var attachment in message.Attachments)
                {
                    using (var ms = new MemoryStream())
                    {
                        attachment.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }
                    bodyBuilder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
                }
            }

            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }

        /// <summary>
        /// Envía un correo electrónico de manera síncrona utilizando el protocolo SMTP.
        /// </summary>
        /// <param name="mailMessage">El mensaje de correo electrónico que se enviará.</param>
        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    // Conecta al servidor SMTP.
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, MailKit.Security.SecureSocketOptions.StartTls);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    // Autentica al usuario.
                    client.Authenticate(_emailConfig.UserName, _emailConfig.Password);
                    // Envía el mensaje.
                    client.Send(mailMessage);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    // Desconecta del servidor SMTP.
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        /// <summary>
        /// Envía un correo electrónico de manera asíncrona utilizando el protocolo SMTP.
        /// </summary>
        /// <param name="mailMessage">El mensaje de correo electrónico que se enviará.</param>
        /// <returns>Una tarea que representa la operación asíncrona.</returns>
        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    // Conecta al servidor SMTP de manera asíncrona.
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, MailKit.Security.SecureSocketOptions.StartTls);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    // Autentica al usuario de manera asíncrona.
                    await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);
                    // Envía el mensaje de manera asíncrona.
                    await client.SendAsync(mailMessage);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    // Desconecta del servidor SMTP de manera asíncrona.
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
    }
}