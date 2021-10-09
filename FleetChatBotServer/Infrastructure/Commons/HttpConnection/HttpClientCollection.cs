using System;
using System.Linq;
using Hxgn.Mop.UG.Simulator.Infrastructure.Commons.Configuration;

namespace FleetChatBotServer.Infrastructure.Commons.HttpConnection
{
    public class HttpClientCollection : IHttpClientCollection
    {
        private readonly IHttpConnection[] _httpClients =
        {
            new HttpConnection(InfraConfiguration.Instance().UGPro.WebApi),
            new HttpConnection(InfraConfiguration.Instance().ChatBot.WebApi)
        };

        public IHttpConnection this[HttpClients client] => FindHttpClient(client);

        private IHttpConnection FindHttpClient(HttpClients client)
        {
            var httpClient = _httpClients.FirstOrDefault(x => x.HttpConnectionConfig.HttpClient == client);
            if (httpClient is null)
            {
                throw new ArgumentOutOfRangeException(nameof(client), $"Http client {client} is not supported.");
            }          
            return httpClient;
        }
    }

    public enum HttpClients
    {
        UGPro,
        ChatBot
    }
}
