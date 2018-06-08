using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace SimService
{
    interface IDevice
    {
        Task TickAsync();
    }

    class Device : IDevice
    {
        private readonly string _deviceId;
        private DeviceClient _deviceClient;

        public Device( string deviceId, string iotHubUri, string deviceKey )
        {
            _deviceId = deviceId;
            Connect( iotHubUri, deviceKey );
        }

        private void Connect( string iotHubUri, string deviceKey )
        {
            _deviceClient = DeviceClient.Create( 
                iotHubUri, 
                new DeviceAuthenticationWithRegistrySymmetricKey( _deviceId, deviceKey ), 
                TransportType.Mqtt );
        }

        public async Task TickAsync()
        {
            var temperature = 0.0;
            var humidity = 0.0;
            var telemetryDataPoint = new
            {
                deviceId = _deviceId,
                temperature,
                humidity
            };

            var messageString = JsonConvert.SerializeObject( telemetryDataPoint );
            var message = new Message( Encoding.ASCII.GetBytes( messageString ) );

            await _deviceClient.SendEventAsync( message );
        }
    }
}
