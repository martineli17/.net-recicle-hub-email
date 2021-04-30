using HubEmail.Core.Objetos;
using System.Threading.Tasks;

namespace HubEmail.Core.Observers
{
    public interface IEmailEnviadosColetaObserver
    {
        Task OnNext(Email email);
    }
}
