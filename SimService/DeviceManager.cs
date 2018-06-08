using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimService
{
    interface IDeviceManager
    {
        Task TickAsync();
    }

    public class DeviceManager : IDeviceManager
    {
        private readonly List<IDevice> _devices;

        public DeviceManager( IConfigService configService )
        {
            _devices = configService
                .ConfigFile
                .Devices
                .Select( deviceConfig => new Device( deviceConfig.DeviceId, configService.ConfigFile.IotHubUri, deviceConfig.DeviceKey ) )
                .ToList<IDevice>();
        }

        public async Task TickAsync()
        {
            var tasks = _devices
                .Select( d => d.TickAsync() )
                .ToArray();

            await Task.WhenAll( tasks );
        }
    }
}
