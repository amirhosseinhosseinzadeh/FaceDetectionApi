using Refit;
using System.Threading.Tasks;
using System.Collections.Generic;
using Faces.WebMvc.Models;
using System.Net.Http;
using System;
namespace Faces.WebMvc.RestClients
{
    public interface IORderManagementApi
    {
        [Get("/orders")]
        Task<List<OrderViewModel>> GetOrdersAsync();

        [Get("/orders/{orderId}")]
        Task<OrderViewModel> GetOrderByIdAsync(string orderId);
    }
}