using FleetChatBotServer.UGProMiddleware.Dtos;

namespace FleetChatBotServer.ChatBot.Formatter.UGPro
{
    public class EquipmentCurrentStateFormatter : InferenceFormatter<EquipmentRealTimeDto>
    {
        public EquipmentCurrentStateFormatter(string answer, string predicate, EquipmentRealTimeDto dto) : base(answer, predicate, dto) { }

        public override string AnswerFormatted => string.Format(Answer, Predicate, Dto.StateName);
    }
}
