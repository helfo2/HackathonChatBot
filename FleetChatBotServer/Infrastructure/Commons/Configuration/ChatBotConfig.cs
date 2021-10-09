using FleetChatBotServer.Infrastructure.Commons.Configuration;
using FleetChatBotServer.Infrastructure.Commons.HttpConnection;

namespace Hxgn.Mop.UG.Simulator.Infrastructure.Commons.Configuration
{
    public class ChatBotConfig
    {
        public string DeepSpeechModelRelativePath { get; set; }
        public HttpConnectionConfig WebApi { get; set; }
    }
}