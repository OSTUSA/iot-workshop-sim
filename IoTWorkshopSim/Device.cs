using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace IoTWorkshopSim
{
    interface IDevice
    {
        Task TickAsync();
    }

    class Device : IDevice
    {
        private readonly string _deviceId;
        private readonly Random _rnd;
        private DeviceClient _deviceClient;

        public Device( string deviceId, string iotHubUri, string deviceKey )
        {
            _rnd = new Random();
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

        private double GetRandomValue( int lower, int upper, int places = 0 )
        {
            var randomVal = _rnd.NextDouble() * (upper - lower) + lower;
            return Math.Round( randomVal, places );
        }

        public async Task TickAsync()
        {
            var temperature = GetRandomValue( 60, 90, 2 );
            var humidity = GetRandomValue( 30, 35, 2 );
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
