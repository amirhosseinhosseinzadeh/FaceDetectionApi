using Faces.WebMvc.RestClients;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Faces.WebMvc.Controllers
{
    public class OrderManagementController : Controller
    {
        private readonly IORderManagementApi _orderManagementApi;

        public OrderManagementController(IORderManagementApi orderManagementApi)
        {
            _orderManagementApi = orderManagementApi;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _orderManagementApi.GetOrdersAsync();
            orders.ForEach(order =>
            {
                order.ImageString = ConvertAndFormatToString(order.ImageData);
            });
            return View(orders);
        }

        [Route("/Details/{orderId}")]
        public async Task<IActionResult> Details(string orderId)
        {
            var order = await _orderManagementApi.GetOrderByIdAsync(orderId);
            if (order == null)
                throw new Exception();
            order.ImageString = ConvertAndFormatToString(order.ImageData);
            order.OrderDetails.ForEach(detail =>
            {
                detail.ImageString = ConvertAndFormatToString(detail.FaceData);
            });
            return View(order);
        }

        private string ConvertAndFormatToString(byte[] imageData)
        {
            var base64Str = Convert.ToBase64String(imageData);
            return string.Format("data:image/png;base64, {0}",base64Str);
        }
    }
}