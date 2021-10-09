namespace FleetChatBotServer.Infrastructure.Commons.HttpConnection
{
    public interface IHttpClientCollection
    {
        public IHttpConnection this[HttpClients client] { get; }
    }
}