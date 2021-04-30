using Crosscuting;
using HubEmail.Core.Contratos;
using HubEmail.Core.Objetos;
using HubEmail.Core.Observables.Contratos;
using Mensageria.Contratos;
using Mensageria.Objetos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HubEmail.Core.BackgroundServices
{
    public class EmailEnviadosColetaBackgroundService : BackgroundService
    {
        private readonly IRabbit _rabbit;
        private readonly IEmailEnviadosColetaObservable _observaleEmail;
        public EmailEnviadosColetaBackgroundService(IServiceProvider serviceProvider)
        {
            _rabbit = (IRabbit)serviceProvider.GetService(typeof(IRabbit));
            _observaleEmail = (IEmailEnviadosColetaObservable)serviceProvider.GetService(typeof(IEmailEnviadosColetaObservable));
            var _hub = (IEmailEnviadosColetaHub)serviceProvider.GetService(typeof(IEmailEnviadosColetaHub));
            _observaleEmail.Subscribe(_hub);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (_rabbit != null)
                await _rabbit.Consumer(GetMensageriaEmail, FilasRabbit.EMAIL_ENVIADOS_COLETA);
        }

        private void GetMensageriaEmail(ResponseRabbitMQ dados)
        {
            if (dados is null || dados.Body.IsEmpty) return;
            var email = JsonFunc.DeserializeObject<Email>(Encoding.UTF8.GetString(dados.Body.ToArray()));
            if (email != null) _observaleEmail.Send(email);
        }
    }
}
