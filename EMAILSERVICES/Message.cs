using MimeKit;
using Microsoft.AspNetCore.Http;

namespace EmailService
{
    /// <summary>
    /// Clase que representa un mensaje de correo electrónico, incluyendo destinatarios, asunto, contenido y archivos adjuntos.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Obtiene o establece la lista de destinatarios del correo electrónico.
        /// </summary>
        public List<MailboxAddress> To { get; set; }

        /// <summary>
        /// Obtiene o establece el asunto del correo electrónico.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Obtiene o establece el contenido del correo electrónico.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Obtiene o establece la colección de archivos adjuntos del correo electrónico.
        /// </summary>
        public IFormFileCollection Attachments { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Message"/> con destinatarios, asunto, contenido y archivos adjuntos.
        /// </summary>
        /// <param name="to">La lista de direcciones de correo electrónico de los destinatarios.</param>
        /// <param name="subject">El asunto del correo electrónico.</param>
        /// <param name="content">El contenido del correo electrónico.</param>
        /// <param name="attachments">La colección de archivos adjuntos.</param>
        public Message(IEnumerable<string> to, string subject, string content, IFormFileCollection attachments)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress("", x))); // Convierte las direcciones de correo en objetos MailboxAddress.
            Subject = subject;
            Content = content;
            Attachments = attachments;
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Message"/> con destinatarios, asunto y contenido.
        /// </summary>
        /// <param name="to">La lista de direcciones de correo electrónico de los destinatarios.</param>
        /// <param name="subject">El asunto del correo electrónico.</param>
        /// <param name="content">El contenido del correo electrónico.</param>
        public Message(IEnumerable<string> to, string subject, string content)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress("", x))); // Convierte las direcciones de correo en objetos MailboxAddress.
            Subject = subject;
            Content = content;
        }
    }
}