using System.Threading.Tasks;
using System.Collections.Generic;
using Faces.WebMvc.Models;
using System.Net.Http;
using System;
using Microsoft.Extensions.Configuration;
using Refit;
using System.Net;

namespace Faces.WebMvc.RestClients
{
    public class OrderManagementApi : IORderManagementApi
    {
        private readonly IORderManagementApi _restClient;

        public OrderManagementApi(IConfiguration configuration, HttpClient client)
        {
            string addressAndPort = configuration.GetSection("ApiServiceLocation")
                .GetValue<string>("OrdersApiLocation");
            client.BaseAddress = new Uri($"Http://{addressAndPort}/api");
            _restClient= RestService.For<IORderManagementApi>(client);
        }

        public async Task<OrderViewModel> GetOrderByIdAsync(string orderId)
        {
            try
            {
                return await _restClient.GetOrderByIdAsync(orderId);
            }
            catch(ApiException ex)
            {
                if(ex.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }

                throw;
            }
        }

        public async Task<List<OrderViewModel>> GetOrdersAsync()
            => await _restClient.GetOrdersAsync();
    }
}