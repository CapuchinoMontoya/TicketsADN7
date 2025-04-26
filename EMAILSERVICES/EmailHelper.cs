using EmailService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMAILSERVICES
{
    public static class EmailHelper
    {
        public static bool EnviarCorreo(EmailConfiguration config, string toCorreo, string asunto, string mensaje)
        {
            try
            {
                var emailSender = new EmailSender(config);
                var message = new Message(new List<string> { toCorreo }, asunto, mensaje);

                emailSender.SendEmailAsync(message).GetAwaiter().GetResult();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
