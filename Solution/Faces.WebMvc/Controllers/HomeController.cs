using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Faces.WebMvc.Models;
using MassTransit;
using System.Threading.Tasks;
using System.IO;
using System;
using Messaging.InterfacesConstants.Constants;
using Messaging.InterfacesConstants.Commands;

namespace Faces.WebMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBusControl _busControl;

        public HomeController(ILogger<HomeController> logger,
                              IBusControl busControl
            )
        {
            this._logger = logger;
            this._busControl = busControl;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RegisterOrder()
            => View();

        [HttpPost]
        public async Task<IActionResult> RegisterOrder(OrderViewModel model)
        {
            using (MemoryStream ms = new())
            {
                using (var uploadFile = model.File.OpenReadStream())
                    await uploadFile.CopyToAsync(ms);
                
                model.ImageData = ms.ToArray();
                model.ImageUrl = model.File.FileName;
                model.OrderId = Guid.NewGuid();
                // Uri sendTo = new ($"{RabbitMqMassTransitConstants.RabbitMqUri}" +
                //     $"{RabbitMqMassTransitConstants.RegisterOrderCommandQueue}");

                var sendTo = new Uri($"{RabbitMqMassTransitConstants.RabbitMqUri}/{RabbitMqMassTransitConstants.RegisterOrderCommandQueue}");

                var endPoint = await _busControl.GetSendEndpoint(sendTo);
                await endPoint.Send<IRegisterOrderCommand>(
                    new 
                    {
                        model.OrderId,
                        model.UserEmail,
                        model.ImageData,
                        model.ImageUrl
                    });
            }
            ViewData["OrderId"] = model.OrderId;
            return View("Thanks");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
