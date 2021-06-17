using Dominio;
using Dominio.Contratos;
using HubEmail.Core.Contratos;
using HubEmail.Core.Objetos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Threading.Tasks;

namespace HubEmail.Hubs
{
    [Authorize]
    public class EmailEnviadosColetaHub : Hub, IEmailEnviadosColetaHub
    {
        private static IHubCallerClients _clients;
        private readonly IHubRepositorio _repositorio;
        public EmailEnviadosColetaHub(IHubRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public override async Task OnConnectedAsync()
        {
            var idUsuario = Context.GetHttpContext().Request.Query["idUsuario"];
            await _repositorio.AddAsync(new HubEntidade(idUsuario, Context.ConnectionId, HubType.EmailEnviadosColeta));
            _clients = Clients;
            await base.OnConnectedAsync();
        }

        public async Task OnNext(Email value)
        {
            if (value is null) return;
            if (Clients is null) Clients = _clients;
            var userHub = (await _repositorio.GetAsync(x => x.Tipo == 0 &&
                                                       x.IdUsuario == value.IdentificadorEmail))
                                             .FirstOrDefault()?.IdConexao;
            if (userHub is null) return;
            await Clients.Client(userHub).SendAsync("EmailRecebidosColeta", value);
        }
    }
}
