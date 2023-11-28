using System;
using System.Collections.Generic;
using System.Net;

namespace FluentResults.Extensions.Microservice;

public interface IResultFactory
{
    public Result Success(HttpStatusCode statusCode);
    
    public Result Success(HttpStatusCode statusCode, string message);
    
    public Result Success(HttpStatusCode statusCode, ISuccess success);
    
    public Result Success(HttpStatusCode statusCode, IEnumerable<ISuccess> successes);
    
    public Result Fail(HttpStatusCode statusCode);
    
    public Result Fail(Exception exception);

    public Result Fail(HttpStatusCode statusCode, IError error);
    
    public Result Fail(HttpStatusCode statusCode, string message);

    public Result Fail(HttpStatusCode statusCode, IEnumerable<string> messages);
    
    public Result Fail(HttpStatusCode statusCode, IEnumerable<IError> errors);
    
    public Result<TValue> Success<TValue>(HttpStatusCode statusCode, TValue value);
    
    public Result<TValue> Success<TValue>(HttpStatusCode statusCode, string message, TValue value);
    
    public Result<TValue> Success<TValue>(HttpStatusCode statusCode, ISuccess success, TValue value);
    
    public Result<TValue> Success<TValue>(HttpStatusCode statusCode, IEnumerable<ISuccess> successes, TValue value);
    
    public Result<TValue> Fail<TValue>(HttpStatusCode statusCode, TValue value);
    
    public Result<TValue> Fail<TValue>(Exception exception, TValue value);

    public Result<TValue> Fail<TValue>(HttpStatusCode statusCode, IError error, TValue value);
    
    public Result<TValue> Fail<TValue>(HttpStatusCode statusCode, string message, TValue value);

    public Result<TValue> Fail<TValue>(HttpStatusCode statusCode, IEnumerable<string> messages, TValue value);
    
    public Result<TValue> Fail<TValue>(HttpStatusCode statusCode, IEnumerable<IError> errors, TValue value);
}
