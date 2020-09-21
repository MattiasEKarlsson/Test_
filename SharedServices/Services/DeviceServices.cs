using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using SharedServices.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MAD = Microsoft.Azure.Devices;

namespace SharedServices.Services
{
    public class DeviceServices
    {
        public static async Task SendMessageAsync(DeviceClient deviceClient)
        {
            var httpClient = HttpClientFactory.Create();

            while (true)
            {
                double temp = 0;
                int humidity = 0;
                TemperatureModel senddata = new TemperatureModel();

                try
                {
                    var url = "https://api.openweathermap.org/data/2.5/onecall?lat=59.27412&units=metric&lon=15.2066&exclude=hourly,daily,minutely&appid=5bf919005c4c20e778ba98f74c7f2e33";
                    HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(url);

                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        var content = httpResponseMessage.Content;                                    //Hämtar Api
                        var datatemp = await content.ReadAsAsync<Rootobject>();

                        temp = datatemp.current.temp;                                                // Sätter Temp och Humidity
                        humidity = datatemp.current.humidity;

                        senddata = new TemperatureModel                                              //Lägger Temp och Humidity i samma objekt.
                        {
                            Temerature = temp,
                            Humidity = humidity
                        };

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }


                try
                {
                    var json = JsonConvert.SerializeObject(senddata);                                        // Converterar till Json format.

                    var payload = new Message(Encoding.UTF8.GetBytes(json));                             // Packeterar meddelandena

                    await deviceClient.SendEventAsync(payload);                                         // Skickar

                    Console.WriteLine($"Message Sent: {json}");
                }
                catch (Exception exx)
                {
                    Console.WriteLine(exx.Message);
                }

                await Task.Delay(60 * 1000);

            }

        }

        public static async Task ReceiveMessageAsync(DeviceClient deviceClient)
        {
            while (true)
            {
                var payload = await deviceClient.ReceiveAsync();

                if (payload == null)
                {
                    continue;
                }

                Console.WriteLine($"Message recived {Encoding.UTF8.GetString(payload.GetBytes())}");
                await deviceClient.CompleteAsync(payload);
            }

        }

        public static async Task SendMessageToDeviceAsync(MAD.ServiceClient serviceClient, string targetDeviceId, string message)
        {
            var payload = new MAD.Message(Encoding.UTF8.GetBytes(message));
            await serviceClient.SendAsync(targetDeviceId, payload);



        }


    }

    

   

}
