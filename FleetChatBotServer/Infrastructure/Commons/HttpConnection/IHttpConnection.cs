using System.Threading.Tasks;
using FleetChatBotServer.Infrastructure.Commons.Configuration;

namespace FleetChatBotServer.Infrastructure.Commons.HttpConnection
{
    public interface IHttpConnection
    {
        public HttpConnectionConfig HttpConnectionConfig { get; set; }
        Task<T> GetAsync<T>(HttpResquestBody requestBody);
        Task<T> PostAsync<T>(HttpPostBody postBody);
    }
}