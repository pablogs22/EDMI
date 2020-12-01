using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.IO;
using System.Text;
using System.Net;

namespace EDMI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public class AddCustomerArgs
        {
            public int Id { get; set; }
            public string SerialNumber { get; set; }
            public int FirmwareVersion { get; set; }
            public string State { get; set; }
        }

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost]
        public void Post([FromBody] string[] data)
        {
            saveFile(data);         
        }

        private StatusCodeResult saveFile(string[] data)
        {
            try
            {
                SaveFileDialog file = new SaveFileDialog();
                file.FileName = "electricMeters.txt";
                file.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                //file.ShowDialog();

                using (StreamWriter filewrite = new StreamWriter(file.FileName, true))
                {
                    filewrite.WriteLine("Id: {0}, Serial Number: {1}, Firmware Version: {2}, State: {3} "
                        , data[0], data[1], data[2], data[3]);
                }
                return StatusCode((int)HttpStatusCode.OK);
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            
        }
    }
}

