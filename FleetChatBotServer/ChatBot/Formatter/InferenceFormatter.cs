namespace FleetChatBotServer.ChatBot.Formatter
{
    public abstract class InferenceFormatter<T> : IInferenceFormatter where T : class
    {
        public InferenceFormatter(string answer, string predicate, T dto)
        {
            Answer = answer;
            Predicate = predicate;
            Dto = dto;
        }
        public string Answer { get; }
        public string Predicate { get; }
        public T Dto { get; }
        public abstract string AnswerFormatted { get; }
    }
}
