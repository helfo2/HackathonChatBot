using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace FleetChatBotServer.Infraestructure.Libraries.Utils.File
{
    public class JsonFileLoader : IFileLoader
    {
        private readonly string _filePath;

        public JsonFileLoader(string filePath)
        {
            _filePath = filePath;
        }

        public T LoadFile<T>(string[] hierarchierarchicalSections)
        {
            try
            {
                using StreamReader readStream = new StreamReader(_filePath);
                string jsonText = readStream.ReadToEnd();
                JObject jsonObj = JObject.Parse(jsonText);

                JToken result = jsonObj;
                foreach (string section in hierarchierarchicalSections)
                {
                    result = result[section];
                }

                return result.ToObject<T>();
            }
            catch (Exception ex)
            {
                string msg = "Unable to load the json file";
                throw new Exception(msg, ex);
            }
        }
    }
}