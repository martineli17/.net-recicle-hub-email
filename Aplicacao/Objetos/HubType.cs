using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace Aplicacao.Objetos
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum HubType
    {
        EmailEnviadosColeta,
    }
}
