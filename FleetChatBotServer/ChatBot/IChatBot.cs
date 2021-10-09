using System.Threading.Tasks;
using FleetChatBotServer.ChatBot.BotClient;

namespace FleetChatBotServer.ChatBot
{
    public interface IChatBot
    {
        public IChatBotAI ChatBotAIClient { get; }
        public Task<string> Answer(string question);
    }
}