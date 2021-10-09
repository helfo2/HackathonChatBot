using System;
using FleetChatBotServer.Infrastructure.Commons.HttpConnection;

namespace FleetChatBotServer.UGProMiddleware.Dtos
{
    public class WorkplaceRealTimeDto
    {
        public int Id { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public TimeSpan Duration { get; set; }
        public int WorkplaceId { get; set; }
    }
}