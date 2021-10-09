namespace FleetChatBotServer.ChatBot.Formatter
{
    public interface IInferenceFormatter
    {
        public abstract string AnswerFormatted { get; }
    }
}