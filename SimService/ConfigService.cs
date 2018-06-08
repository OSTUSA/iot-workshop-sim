using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace SimService
{
    public interface IConfigService
    {
        ConfigFile ConfigFile { get; }
    }

    public class ConfigService : IConfigService
    {
        public ConfigFile ConfigFile { get; }

        public ConfigService()
        {
            using ( StreamReader file = File.OpenText( @"deviceConfig.json" ) )
            {
                using ( JsonTextReader reader = new JsonTextReader( file ) )
                {
                    ConfigFile = new JsonSerializer().Deserialize<ConfigFile>( reader );
                }
            }
        }
    }

    public class ConfigFile
    {
        public List<ConfigFileDevice> Devices { get; set; }
        public string IotHubUri { get; set; }
    }

    public class ConfigFileDevice
    {
        public string DeviceId { get; set; }
        public string DeviceKey { get; set; }
    }
}
