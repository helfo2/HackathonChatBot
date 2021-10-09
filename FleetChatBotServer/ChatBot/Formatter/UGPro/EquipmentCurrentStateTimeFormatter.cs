using FleetChatBotServer.UGProMiddleware.Dtos;

namespace FleetChatBotServer.ChatBot.Formatter.UGPro
{
    public class EquipmentCurrentStateTimeFormatter : InferenceFormatter<EquipmentRealTimeDto>
    {
        public EquipmentCurrentStateTimeFormatter(string answer, string predicate, EquipmentRealTimeDto dto) : base(answer, predicate, dto) { }

        public override string AnswerFormatted => string.Format(Answer, Dto.StateName, Dto.Duration.ToString(@"hh\:mm\:ss"));
    }
}
