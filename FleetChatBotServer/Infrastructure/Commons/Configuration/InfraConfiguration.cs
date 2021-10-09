using FleetChatBotServer.Infraestructure.Libraries.Utils;
using FleetChatBotServer.Infrastructure.Commons.Configuration;
using System.IO;

namespace Hxgn.Mop.UG.Simulator.Infrastructure.Commons.Configuration
{
    public class InfraConfiguration
    {
        private static InfraConfiguration _internalReference;

        public static string ConfigFileRelativePath => "Infrastructure\\GlobalConfig\\InfraConfiguration.json";
        public string LogRelativePath => "Log\\FleetChatBot.log";
        public UGProConfig UGPro { get; set; } = new();
        public ChatBotConfig ChatBot { get; set; } = new();

        public static InfraConfiguration Instance()
        {
            if (_internalReference is null)
            {
                _internalReference = Build();
            }
            return _internalReference;
        }

        private static InfraConfiguration Build()
        {
            var content = File.ReadAllText(Path.Combine(ConfigFileRelativePath));
            var config = Helpers.JsonSerializer.Deserialize<InfraConfigurationWrapper>(content);
            return config.InfraConfiguration;
        }
    }

    public class InfraConfigurationWrapper
    {
        public InfraConfiguration InfraConfiguration { get; set; }
    }
}