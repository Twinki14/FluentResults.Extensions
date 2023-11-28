using System.Net;

namespace FluentResults.Extensions.Microservice;

public class Result<TValue> : FluentResults.Result<TValue>, IResultBase
{
    public HttpStatusCode StatusCode { get; private set; } = HttpStatusCode.OK;

    public Result<TValue> WithStatusCode(HttpStatusCode statusCode)
    {
        StatusCode = statusCode;
        
        return this;
    }

    /// <summary>
    /// Converts a Result with a value to a Result without a value
    /// </summary>
    public new Result ToResult()
    {
        return base
            .ToResult()
            .WithStatusCode(StatusCode);
    }
}