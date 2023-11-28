using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using FluentResults.Extensions.Microservice.Internal;

namespace FluentResults.Extensions.Microservice;

public class ErrorDto
{
    /// <summary>
    /// Status code of the error.
    /// </summary>
    public HttpStatusCode ErrorCode { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<string> Messages { get; set; }

    public ErrorDto(IResultBase result)
    {
        ErrorCode = result.StatusCode;
        Messages = result.Errors.Select(e => e.Message);
    }
    
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, ErrorSerializerContext.Default.ErrorDto);
    }
}
