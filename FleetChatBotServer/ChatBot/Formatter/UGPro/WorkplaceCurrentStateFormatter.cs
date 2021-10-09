using FleetChatBotServer.UGProMiddleware.Dtos;

namespace FleetChatBotServer.ChatBot.Formatter.UGPro
{
    public class WorkplaceCurrentStateFormatter : InferenceFormatter<WorkplaceRealTimeDto>
    {
        public WorkplaceCurrentStateFormatter(string answer, string predicate, WorkplaceRealTimeDto dto) : base(answer, predicate, dto) { }

        public override string AnswerFormatted => string.Format(Answer, Predicate, Dto.StateName);
    }
}
