namespace Messaging.InterfacesConstants.Constants
{
    public class RabbitMqMassTransitConstants
    {
        public const string RabbitMqUri = "rabbitmq://rabbitmq:5672";

        public const string UserName = "guest";

        public const string Passwrod = "guest";

        // the point that application will send commands to rabbitmq
        public const string RegisterOrderCommandQueue = "register.order.command";
    }
}