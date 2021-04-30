using Newtonsoft.Json;

namespace Crosscuting
{
    public static class JsonFunc
    {
        public static string SerializeObject<TRequest>(TRequest request) => JsonConvert.SerializeObject(request);
        public static TRequest DeserializeObject<TRequest>(string request) => JsonConvert.DeserializeObject<TRequest>(request);
    }
}
