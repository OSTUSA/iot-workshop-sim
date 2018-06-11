# IoT Workshop Sim(ulation) Service

Generate simulated telemetry against an Azure IoT Hub, for assistance developing Streaming Analytics jobs, and Power BI dashboards.

Uses the (TopShelf)[http://topshelf-project.com/] library for generating either a command-line app, or a Windows Service.

## Notes

Expects all simulated devices to already be registered in the IoT Hub Device Registry.

Looks for a `deviceConfig.json` file in the root of the `/bin` directory when starting up, which should specify the devices to act as.

Example schema:

```
{
    "Devices": [
        {
            "DeviceId": "<IoT Hub Device ID>",
            "DeviceKey": "<IoT Hub Device Key>"
        }
    ],
    "IotHubUri": "<IoT Hub URI>"
}
```