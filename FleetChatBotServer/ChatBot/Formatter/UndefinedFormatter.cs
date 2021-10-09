namespace FleetChatBotServer.ChatBot.Formatter.UGPro
{
    internal class UndefinedFormatter : IInferenceFormatter
    {
        private readonly string _answer;

        public UndefinedFormatter(string answer)
        {
            this._answer = answer;
        }

        public string AnswerFormatted => _answer;
    }
}