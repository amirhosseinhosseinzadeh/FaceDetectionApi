using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using MassTransit;

namespace Faces.WebMvc.Services
{
    public class BusService : IHostedService
    {
        private readonly IBusControl _busControl;

        public BusService(IBusControl busControl
            )
        {
            this._busControl = busControl;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
            => await _busControl.StartAsync(cancellationToken);

        public async Task StopAsync(CancellationToken cancellationToken)
            => await _busControl.StopAsync(cancellationToken);
    }
}