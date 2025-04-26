using Microsoft.Extensions.Options;
using RestSharp;
using System.Text.Json;

namespace WHATSAPPSERVICES
{
    public class WhatsAppSender : IWhatsAppSender
    {
        private readonly WhatsAppConfiguration _whatsAppConfig;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="WhatsAppSender"/>.
        /// </summary>
        /// <param name="whatsAppConfig">Configuración de WhatsApp.</param>
        public WhatsAppSender(IOptions<WhatsAppConfiguration> whatsAppConfig)
        {
            _whatsAppConfig = whatsAppConfig.Value;
        }

        /// <summary>
        /// Envía un mensaje de WhatsApp con documento de manera síncrona.
        /// </summary>
        /// <param name="message">El mensaje de WhatsApp que se enviará.</param>
        /// <returns>True si el mensaje se envió correctamente, False en caso contrario.</returns>
        public bool SendWhatsAppMessageDocument(WhatsAppMessage message)
        {
            var url = $"{_whatsAppConfig.BaseUrl}/messages/document";
            var client = new RestClient(url);

            var request = new RestRequest(url, Method.Post);
            request.AddHeader("content-type", "application/json");

            var body = new
            {
                token = _whatsAppConfig.Token,
                to = message.To,
                filename = message.FileName,
                document = message.Document,
                caption = message.Caption
            };

            request.AddParameter("application/json", JsonSerializer.Serialize(body), ParameterType.RequestBody);
            RestResponse response = client.Execute(request);

            return ProcessResponse(response);
        }

        /// <summary>
        /// Envía un mensaje de WhatsApp con documento de manera asíncrona.
        /// </summary>
        /// <param name="message">El mensaje de WhatsApp que se enviará.</param>
        /// <returns>True si el mensaje se envió correctamente, False en caso contrario.</returns>
        public async Task<bool> SendWhatsAppMessageAsync(WhatsAppMessage message)
        {
            var url = $"{_whatsAppConfig.BaseUrl}/messages/document";
            var client = new RestClient(url);

            var request = new RestRequest(url, Method.Post);
            request.AddHeader("content-type", "application/json");

            var body = new
            {
                token = _whatsAppConfig.Token,
                to = message.To,
                filename = message.FileName,
                document = message.Document,
                caption = message.Caption
            };

            request.AddParameter("application/json", JsonSerializer.Serialize(body), ParameterType.RequestBody);
            RestResponse response = await client.ExecuteAsync(request);

            return ProcessResponse(response);
        }

        /// <summary>
        /// Envía un mensaje de WhatsApp de manera síncrona.
        /// </summary>
        /// <param name="message">El mensaje de WhatsApp que se enviará.</param>
        /// <returns>True si el mensaje se envió correctamente, False en caso contrario.</returns>
        public bool SendWhatsAppMessage(WhatsAppMessage message)
        {
            var url = $"{_whatsAppConfig.BaseUrl}/messages/chat";
            var client = new RestClient(url);

            var request = new RestRequest(url, Method.Post);
            request.AddHeader("content-type", "application/json");

            var body = new
            {
                token = _whatsAppConfig.Token,
                to = message.To,
                body = message.Body
            };

            request.AddParameter("application/json", JsonSerializer.Serialize(body), ParameterType.RequestBody);
            RestResponse response = client.Execute(request);

            return ProcessResponse(response);
        }

        /// <summary>
        /// Procesa la respuesta de la API de WhatsApp.
        /// </summary>
        /// <param name="response">Respuesta de la API.</param>
        /// <returns>True si el mensaje se envió correctamente, False en caso contrario.</returns>
        private bool ProcessResponse(RestResponse response)
        {
            if (response.IsSuccessful)
            {
                var jsonObject = JsonSerializer.Deserialize<JsonElement>(response.Content);
                string message = jsonObject.GetProperty("message").GetString();
                return message == "ok";
            }
            return false;
        }
    }
}
