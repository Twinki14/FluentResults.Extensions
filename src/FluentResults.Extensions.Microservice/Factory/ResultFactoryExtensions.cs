using System;
using System.Net;

namespace FluentResults.Extensions.Microservice;

public static class ResultFactoryExtensions
{
    public static Result Success(this IResultFactory factory)
    {
        return factory.Success(HttpStatusCode.OK);
    }
    
    public static Result<TValue> Success<TValue>(this IResultFactory factory, TValue value)
    {
        return factory.Success(HttpStatusCode.OK, value);
    }
    
    public static Result Exception(this IResultFactory factory, Exception exception)
    {
        return factory.Fail(exception);
    }
    
    public static Result<TValue> Exception<TValue>(this IResultFactory factory, Exception exception, TValue value)
    {
        return factory.Fail(exception, value);
    }
    
    public static Result Error(this IResultFactory factory, IError error)
    {
        return factory.Fail(HttpStatusCode.InternalServerError, error);
    }
    
    public static Result Error(this IResultFactory factory, string message)
    {
        return factory.Fail(HttpStatusCode.InternalServerError, message);
    }
    
    public static Result NotFound(this IResultFactory factory)
    {
        return factory.Fail(HttpStatusCode.NotFound);
    }
    
    public static Result NotFound(this IResultFactory factory, string message)
    {
        return factory.Fail(HttpStatusCode.NotFound, message);
    }
}
