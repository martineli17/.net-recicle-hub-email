using HubEmail.Core.Objetos;
using System.Threading.Tasks;

namespace HubEmail.Core.Observables.Observers
{
    public interface IEmailEnviadosColetaObserver
    {
        Task OnNext(Email email);
    }
}
