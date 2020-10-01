using Microsoft.Azure.Devices.Client;
using System;
using SharedServices.Services;

namespace Iot_HubApp
{
    class Program
    {
        private static readonly string _conn = "HostName=ec-win20iothub.azure-devices.net;DeviceId=consoleapp;SharedAccessKey=tUNezDX1xEO++CacyyeQfgqmeD2IwndyJEuwUHJoCqk=";

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
