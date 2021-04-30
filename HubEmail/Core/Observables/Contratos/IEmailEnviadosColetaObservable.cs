using HubEmail.Core.Objetos;
using HubEmail.Core.Observers;
using System;
using System.Threading.Tasks;

namespace HubEmail.Core.Observables.Contratos
{
    public interface IEmailEnviadosColetaObservable
    {
        Task Send(Email dados);
        void Subscribe(IEmailEnviadosColetaObserver observer);
    }
}
