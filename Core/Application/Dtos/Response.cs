using System.Text.Json.Serialization;

namespace Application.Dtos
{
    public class Response<T>
    {
        [JsonIgnore]
        public int StatusCode { get; protected set; }
    }
}
