using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using uppgift_10.Models;

namespace uppgift_10
{
    public class Worker : BackgroundService
    {
        private HttpClient _client;
        private readonly ILogger<Worker> _logger;
        
       


      

        

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        
        

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _client = new HttpClient();
            _logger.LogInformation("The service has been started.");


            return base.StartAsync(cancellationToken);
        }


       


        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _client.Dispose();
            _logger.LogInformation("The service has been stopped.");

            return base.StopAsync(cancellationToken);
        }


       




        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {



           


            while (!stoppingToken.IsCancellationRequested)
            {
                var httpClient = HttpClientFactory.Create();
                var url = "https://api.openweathermap.org/data/2.5/onecall?lat=15&lon=49&%20&exclude=hourly,daily,minutely&appid=90820a9340d6e39c7e1b462ede90982d&units=metric";
                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(url);

                try
                {
                    if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var content = httpResponseMessage.Content;
                        var data = await content.ReadAsAsync<Rootobject>();
                        _logger.LogInformation($"The current temperature is: {data} Celsius.");

                        if (data.current.temp > 25)
                        {
                            _logger.LogInformation($"It's getting hot! Make sure to cooldown with a nice shower! ");

                        }

                    }


                }
                catch
                {
                    _logger.LogInformation($"an error ocurred: {httpResponseMessage.StatusCode}");
                }



                await Task.Delay(60 * 1000, stoppingToken);
            }
        }
    }
}
