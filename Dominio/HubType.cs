using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace Dominio
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum HubType
    {
        EmailEnviadosColeta,
    }
}
