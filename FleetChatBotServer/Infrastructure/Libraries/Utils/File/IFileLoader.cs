namespace FleetChatBotServer.Infraestructure.Libraries.Utils.File
{
    public interface IFileLoader
    {
        T LoadFile<T>(params string[] sections);
    }
}
