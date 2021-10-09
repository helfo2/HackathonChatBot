using System.Linq;
using FleetChatBotServer.ChatBot.Configuration;

namespace FleetChatBotServer.ChatBot.Utils
{
    public static class StringExtension
    {
        public static int ExtractIdentifier(this string predicate, ChatBotPredicates _chatBotEntities)
        {
            return _chatBotEntities[predicate].Value;
        }
    }
}
