using FleetChatBotServer.UGProMiddleware.Dtos;

namespace FleetChatBotServer.ChatBot.Formatter.UGPro
{
    public class WorkplaceCurrentStateTimeFormatter : InferenceFormatter<WorkplaceRealTimeDto>
    {
        public WorkplaceCurrentStateTimeFormatter(string answer, string predicate, WorkplaceRealTimeDto dto) : base(answer, predicate, dto) { }

        public override string AnswerFormatted => string.Format(Answer, Predicate, Dto.Duration.ToString(@"hh\:mm\:ss"));
    }
}
