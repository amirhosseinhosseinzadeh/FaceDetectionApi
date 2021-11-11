using System;
using System.Threading.Tasks;
using MassTransit;
using Messaging.InterfacesConstants.Commands;
using OrdersApi.Models;
using OrdersApi.Persistence;

namespace OrdersApi.Messages.Consumers
{
    public class RegisterOrderCommandConsumer : IConsumer<IRegisterOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public RegisterOrderCommandConsumer(IOrderRepository orderRepository)
        {
            this._orderRepository = orderRepository;
        }

        public Task Consume(ConsumeContext<IRegisterOrderCommand> context)
        {
            var result = context.Message;
            if (result.OrderId != Guid.Empty && !string.IsNullOrWhiteSpace(result.PictureUrl) &&
                !string.IsNullOrWhiteSpace(result.UserEmail) && result.ImageData != null)
                SaveOrder(result);

            return Task.FromResult(true);
        }

        private void SaveOrder(IRegisterOrderCommand command)
        {
            Order order = new()
            {
                ImageData = command.ImageData,
                OrderId = command.OrderId,
                PictureUrl = command.PictureUrl,
                Status = Status.Registered,
                UserEmail = command.UserEmail
            };
            _orderRepository.RegisterOrder(order);
            _orderRepository.SaveChanges(); 
        }
    }
}