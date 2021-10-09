using System.Threading.Tasks;
using FleetChatBotServer.ChatBot.BotClient.Dtos;
using FleetChatBotServer.Infrastructure.Commons.HttpConnection;

namespace FleetChatBotServer.ChatBot.BotClient
{
    public interface IChatBotAI
    {
        public IHttpConnection ClientWebApi { get; }
        public Task<InferenceResult> Answer(string question);
    }
}