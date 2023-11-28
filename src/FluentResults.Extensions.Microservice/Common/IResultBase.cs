using System.Net;

namespace FluentResults.Extensions.Microservice;

public interface IResultBase : FluentResults.IResultBase
{
    public HttpStatusCode StatusCode { get; }
}