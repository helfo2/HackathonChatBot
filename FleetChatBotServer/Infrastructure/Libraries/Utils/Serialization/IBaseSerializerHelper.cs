namespace FleetChatBotServer.Infraestructure.Libraries.Utils.Serialization
{
    public interface IBaseSerializerHelper
    {
        T Deserialize<T>(string value);

        string Serialize<T>(T obj);
    }
}