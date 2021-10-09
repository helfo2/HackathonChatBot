using FleetChatBotServer.ChatBot.Formatter;

namespace FleetChatBotServer.ChatBot.BotClient.Dtos
{
    public class InferenceResult
    {
        public InferenceTags Tag;
        public string Uri;
        public string Answer;
        public string Predicate;
    }
}
