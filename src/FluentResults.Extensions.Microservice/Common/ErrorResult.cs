using System;
using System.Text.Json;
using System.Threading.Tasks;
using FluentResults.Extensions.Microservice.Internal;
using Microsoft.AspNetCore.Mvc;

namespace FluentResults.Extensions.Microservice;

public class ErrorResult : ActionResult
{
    public ErrorDto ErrorDto { get; }

    public ErrorResult(ErrorDto errorDto)
    {
        ArgumentNullException.ThrowIfNull(errorDto);

        ErrorDto = errorDto;
    }

    public ErrorResult(IResultBase result)
    {
        ErrorDto = new ErrorDto(result);
    }
    
    public override async Task ExecuteResultAsync(ActionContext context)
    {
        var response = context.HttpContext.Response;
        var responseStream = response.Body; 

        response.StatusCode = (int) ErrorDto.ErrorCode;
        response.ContentType = "application/json; charset=utf-8";
        
        await JsonSerializer.SerializeAsync(
            responseStream, 
            ErrorDto, 
            ErrorSerializerContext.Default.ErrorDto, 
            context.HttpContext.RequestAborted);

        await responseStream.FlushAsync(context.HttpContext.RequestAborted);
    }
}
