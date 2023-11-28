using System.Net;

namespace FluentResults.Extensions.Microservice;

public class Result : FluentResults.Result, IResultBase
{
    public HttpStatusCode StatusCode { get; private set; } = HttpStatusCode.OK;

    public Result WithStatusCode(HttpStatusCode statusCode)
    {
        StatusCode = statusCode;
        
        return this;
    }
}