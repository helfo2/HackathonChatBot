using FleetChatBotServer.ChatBot.BotClient;
using FleetChatBotServer.ChatBot.Configuration;
using FleetChatBotServer.ChatBot.Formatter.UGPro;
using FleetChatBotServer.ChatBot.Utils;
using FleetChatBotServer.Infraestructure.Libraries.Utils;
using FleetChatBotServer.Infrastructure.Commons.HttpConnection;
using System.IO;
using System.Threading.Tasks;

namespace FleetChatBotServer.ChatBot
{
    public class ChatBot : IChatBot
    {
        private readonly IHttpClientCollection _httpClients;
        private readonly ChatBotPredicates _chatBotPredicates;

        public static string PredicateseRelativePath => @"ChatBot\Configuration\ChatBotPredicates.json";

        public IChatBotAI ChatBotAIClient { get; }

        public ChatBot(IHttpClientCollection httpClients)
        {
            _httpClients = httpClients;

            var chatBotPredicatesJson = File.ReadAllText(Path.Combine(PredicateseRelativePath));
            _chatBotPredicates = Helpers.JsonSerializer.Deserialize<ChatBotPredicates>(chatBotPredicatesJson);

            ChatBotAIClient = new ChatBotAI(httpClients[HttpClients.ChatBot]);
        }

        public async Task<string> Answer(string question)
        {
            var inferenceResult = await ChatBotAIClient.Answer(question);

            InferenceAnswerFormatter formatter = new(inferenceResult.Tag,
                inferenceResult.Answer,
                inferenceResult.Predicate,
                string.Format(inferenceResult.Uri, inferenceResult.Predicate.ExtractIdentifier(_chatBotPredicates)),
                _httpClients[HttpClients.UGPro]);

            return await formatter.GetAnswer();
        }
    }
}
