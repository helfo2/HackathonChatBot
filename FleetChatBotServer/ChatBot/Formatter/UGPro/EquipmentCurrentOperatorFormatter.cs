using FleetChatBotServer.UGProMiddleware.Dtos;

namespace FleetChatBotServer.ChatBot.Formatter.UGPro
{
    public class EquipmentCurrentOperatorFormatter : InferenceFormatter<OperatorDto>
    {
        public EquipmentCurrentOperatorFormatter(string answer, string predicate, OperatorDto dto) : base(answer, predicate, dto) { }

        public override string AnswerFormatted
        {
            get
            {
                if (Dto.Id == 0)
                {
                    return $"There is no one operating {Predicate}";
                }
                return string.Format(Answer, Predicate, Dto.Name, Dto.Registration);
            }
        }
    }
}
