using System;

namespace FleetChatBotServer.UGProMiddleware.Dtos
{
    public class EquipmentRealTimeDto
    {
        public int Id { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int OperatorId { get; set; }
        public TimeSpan Duration => DateTime.UtcNow.Subtract(StateStart);
        public DateTime StateStart { get; set; }
        public int EquipmentId { get; set; }
    }
}