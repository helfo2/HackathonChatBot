using System;
using FleetChatBotServer.Infrastructure.Commons.HttpConnection;

namespace FleetChatBotServer.Infrastructure.Commons.Configuration
{
    public class HttpConnectionConfig
    {
        public HttpClients HttpClient { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ServiceKey { get; set; }
        public Uri ServiceUri { get; set; }
        public string ServiceVersion { get; set; }
    }
}