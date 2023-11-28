using System.Net;

namespace FluentResults.Extensions.Microservice;

public static class FluentResultExtensions
{
    public static Result WithStatusCode(this FluentResults.Result fluentResult, HttpStatusCode statusCode)
    {
        var converted = fluentResult as Result;

        converted?.WithStatusCode(statusCode);
        
        return converted!;
    }
    
    public static Result<TValue> WithStatusCode<TValue>(this FluentResults.Result<TValue> fluentResult, HttpStatusCode statusCode)
    {
        var converted = fluentResult as Result<TValue>;

        converted?.WithStatusCode(statusCode);
        
        return converted!;
    }
}
