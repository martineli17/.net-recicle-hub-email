using HubEmail.Core.Objetos;
using HubEmail.Core.Observables.Contratos;
using HubEmail.Core.Observables.Observers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HubEmail.Core.Observables.Handlers
{
    public class EmailEnviadosColetaObservable : IEmailEnviadosColetaObservable
    {
        private List<IEmailEnviadosColetaObserver> _observers;
        public EmailEnviadosColetaObservable()
        {
            _observers = new List<IEmailEnviadosColetaObserver>();
        }

        public Task Send(Email dados) => Task.Run(() => _observers.ForEach(obs => obs.OnNext(dados)));

        public void Subscribe(IEmailEnviadosColetaObserver observer)
        {
            if (!_observers.Contains(observer)) 
                _observers.Add(observer);
        }
    }
}
