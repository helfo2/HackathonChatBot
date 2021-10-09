using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FleetChatBotServer.Infraestructure.Libraries.Utils.Serialization
{
    public class JsonSerializerHelper : IBaseSerializerHelper
    {
        /// <summary>
        /// Default Json settings with UTC dates in ISO format and CamelCase
        /// </summary>
        private readonly JsonSerializerSettings _defaultJsonSettings;

        /// <summary>
        /// Default instance is provided by the static class Hxgn.Min.Platform.Libraries.Helpers
        /// </summary>
        public JsonSerializerHelper()
        {
            _defaultJsonSettings = new JsonSerializerSettings()
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };
            _defaultJsonSettings.Converters.Add(new StringEnumConverter());
        }

        public string Serialize<T>(T obj) => JsonConvert.SerializeObject(obj, _defaultJsonSettings);
        public T Deserialize<T>(string value) => JsonConvert.DeserializeObject<T>(value);


    }
}
