﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AMDTServerModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrinterDataController : ControllerBase
    {
        private readonly IHubContext<SignalRTagController> _hubContext; // Add Hub context
        public PrinterDataController(IHubContext<SignalRTagController> hubContext)
        {
            _hubContext = hubContext;
        }
        [HttpPost("PrinterData")]
        public async Task<IActionResult> PostPrinterData([FromBody] PrinterData printerData)
        {
            if (printerData == null)
            {
                return BadRequest("Printer data is required.");
            }
            await _hubContext.Clients.All.SendAsync(printerData.PrinterId, Newtonsoft.Json.JsonConvert.SerializeObject(printerData));

            return Ok(new { message = "Printer data received successfully", data = printerData });
        }
        [HttpPost("PrinterGcode")]
        public IActionResult PostGCode([FromBody] PrinterGcode printerData)
        {
            if (printerData == null)
            {
                return BadRequest("Printer gcode data is required.");
            }

            // Here you can process the printer data, e.g., store it in a database, or perform validation
            // For example:
            // SavePrinterDataToDatabase(printerData);

            // Return a successful response with a message or the saved data
            Console.WriteLine(printerData);
            return Ok(new { message = "Printer gcode data received successfully", data = printerData });
        }
    }
    public class PrinterData
    {
        public string PrinterId { get; set; }
        public int? FanSpeed { get; set; }
        public double? BedTemp { get; set; }
        public double? ExtruderTemp { get; set; }
        public double? PositionX { get; set; }
        public double? PositionY { get; set; }
        public double? PositionZ { get; set; }
        public string Status { get; set; }
        public DateTime Timestamp { get; set; }
    }
    public class PrinterGcode
    {
        public string PrinterId { get; set; }
        public string gcode { get; set; }
     
    }
}
