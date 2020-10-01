using Microsoft.Azure.Devices.Client;
using System;
using SharedServices.Services;

namespace Iot_HubApp
{
    class Program
    {
        private static readonly string _conn = "HostName=ec-win20iothub.azure-devices.net;DeviceId=DeviceApp;SharedAccessKey=sJGB59/d4EwPyNVxsX/VXWzxQoZkivpeYQUN+Bu7j+k=";

        private static readonly DeviceClient deviceClient =
            DeviceClient.CreateFromConnectionString(_conn, TransportType.Mqtt);

        static void Main(string[] args)
        {
            DeviceServices.SendMessageAsync(deviceClient).GetAwaiter();
            DeviceServices.ReceiveMessageAsync(deviceClient).GetAwaiter();
            Console.ReadKey();
        }
    }
}
