using System.Text.Json.Serialization;

namespace FluentResults.Extensions.Microservice.Internal;

[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault)]
[JsonSerializable(typeof(ErrorDto))]
public partial class ErrorSerializerContext : JsonSerializerContext
{
    
}