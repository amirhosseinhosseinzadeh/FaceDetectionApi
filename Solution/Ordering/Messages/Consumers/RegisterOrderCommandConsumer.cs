using System.Threading.Tasks;
using MassTransit;
using Messaging.InterfacesConstants.Commands;

namespace OrdersApi.Messages.Consumers
{
    public class RegisterOrderCommandConsumer : IConsumer<IRegisterOrderCommand>
    {
        public Task Consume(ConsumeContext<IRegisterOrderCommand> context)
        {
            throw new System.NotImplementedException();
        }
    }
}