using System.Collections.Generic;

namespace FleetChatBotServer.Infrastructure.Commons.HttpConnection
{
    public class HttpBaseBody
    {
        public string ControllerPath { get; set; }

        /// <summary>
        /// If the dictionary value is a collection, convert it to a string array
        /// </summary>
        public IDictionary<string, object> QueryOptions = new Dictionary<string, object>();
    }
}