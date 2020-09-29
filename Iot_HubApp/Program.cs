using Microsoft.Azure.Devices.Client;
using System;
using SharedServices.Services;

namespace Iot_HubApp
{
    class Program
    {
        private static readonly string _conn = "HostName=ec-win20iothub.azure-devices.net;DeviceId=consoleapp;SharedAccessKey=s5bq+AsW6yo+00GMDTgvNVWUUgNd+Mye35x/6wbktmo=";

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
