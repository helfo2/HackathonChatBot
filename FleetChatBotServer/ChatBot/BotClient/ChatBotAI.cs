using System.Threading.Tasks;
using FleetChatBotServer.ChatBot.BotClient.Dtos;
using FleetChatBotServer.Infrastructure.Commons.HttpConnection;

namespace FleetChatBotServer.ChatBot.BotClient
{
    public class ChatBotAI : IChatBotAI
    {
        public ChatBotAI(IHttpConnection clientWebApi)
        {
            ClientWebApi = clientWebApi;
        }

        public IHttpConnection ClientWebApi { get; }

        public async Task<InferenceResult> Answer(string question)
        {
            var body = new HttpPostBody()
            {
                ControllerPath = "inference",
                Content = new { phrase = question }
            };

            return await ClientWebApi.PostAsync<InferenceResult>(body);
        }
    }
}