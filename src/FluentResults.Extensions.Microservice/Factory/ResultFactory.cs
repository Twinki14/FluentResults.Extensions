using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Extensions.Logging;

namespace FluentResults.Extensions.Microservice;

public class ResultFactory<TSource> : IResultFactory<TSource>
{
    private readonly ILogger<TSource> _logger;

    public ResultFactory(ILogger<TSource> logger)
    {
        _logger = logger;
    }
    
    public Result Success(HttpStatusCode statusCode)
    {
        RequireSuccessStatusCode(statusCode);

        var result = new Result()
            .WithStatusCode(statusCode);
        
        return LogResult(result);
    }

    public Result Success(HttpStatusCode statusCode, string message)
    {
        RequireSuccessStatusCode(statusCode);
        
        var result = new Result()
            .WithSuccess(message)
            .WithStatusCode(statusCode);
        
        return LogResult(result);
    }

    public Result Success(HttpStatusCode statusCode, ISuccess success)
    {
        RequireSuccessStatusCode(statusCode);
        
        var result = new Result()
            .WithSuccess(success)
            .WithStatusCode(statusCode);

        return LogResult(result);
    }

    public Result Success(HttpStatusCode statusCode, IEnumerable<ISuccess> successes)
    {
        RequireSuccessStatusCode(statusCode);
        
        var result = new Result()
            .WithSuccesses(successes)
            .WithStatusCode(statusCode);
        
        return LogResult(result);
    }

    public Result Fail(HttpStatusCode statusCode)
    {
        RequireErrorStatusCode(statusCode);
        
        var result = new Result()
            .WithError(statusCode.ToString())
            .WithStatusCode(statusCode);
        
        return LogResult(result);
    }

    public Result Fail(Exception exception)
    {
        var result = new Result()
            .WithError(new ExceptionalError(exception))
            .WithStatusCode(HttpStatusCode.InternalServerError);
        
        return LogResult(result);
    }

    public Result Fail(HttpStatusCode statusCode, IError error)
    {
        RequireErrorStatusCode(statusCode);
        
        var result = new Result()
            .WithError(error)
            .WithStatusCode(statusCode);
        
        return LogResult(result);
    }

    public Result Fail(HttpStatusCode statusCode, string message)
    {
        RequireErrorStatusCode(statusCode);
        
        var result = new Result()
            .WithError(message)
            .WithStatusCode(statusCode);
        
        return LogResult(result);
    }

    public Result Fail(HttpStatusCode statusCode, IEnumerable<string> messages)
    {
        RequireErrorStatusCode(statusCode);
        
        var result = new Result()
            .WithErrors(messages)
            .WithStatusCode(statusCode);
        
        return LogResult(result);
    }

    public Result Fail(HttpStatusCode statusCode, IEnumerable<IError> errors)
    {
        RequireErrorStatusCode(statusCode);
        
        var result = new Result()
            .WithErrors(errors)
            .WithStatusCode(statusCode);
        
        return LogResult(result);
    }

    public Result<TValue> Success<TValue>(HttpStatusCode statusCode, TValue value)
    {
        RequireSuccessStatusCode(statusCode);
        
        var result = new Result<TValue>()
            .WithValue(value)
            .WithStatusCode(statusCode);

        return LogResult(result);
    }

    public Result<TValue> Success<TValue>(HttpStatusCode statusCode, string message, TValue value)
    {
        RequireSuccessStatusCode(statusCode);
        
        var result = new Result<TValue>()
            .WithValue(value)
            .WithSuccess(message)
            .WithStatusCode(statusCode);

        return LogResult(result);
    }

    public Result<TValue> Success<TValue>(HttpStatusCode statusCode, ISuccess success, TValue value)
    {
        RequireSuccessStatusCode(statusCode);
        
        var result = new Result<TValue>()
            .WithValue(value)
            .WithSuccess(success)
            .WithStatusCode(statusCode);

        return LogResult(result);
    }

    public Result<TValue> Success<TValue>(HttpStatusCode statusCode, IEnumerable<ISuccess> successes, TValue value)
    {
        RequireSuccessStatusCode(statusCode);
        
        var result = new Result<TValue>()
            .WithValue(value)
            .WithSuccesses(successes)
            .WithStatusCode(statusCode);

        return LogResult(result);
    }

    public Result<TValue> Fail<TValue>(HttpStatusCode statusCode, TValue value)
    {
        RequireErrorStatusCode(statusCode);
        
        var result = new Result<TValue>()
            .WithValue(value)
            .WithError(statusCode.ToString())
            .WithStatusCode(statusCode);

        return LogResult(result);
    }
    
    public Result<TValue> Fail<TValue>(Exception exception, TValue value)
    {
        var result = new Result<TValue>()
            .WithValue(value)
            .WithError(new ExceptionalError(exception))
            .WithStatusCode(HttpStatusCode.InternalServerError);

        return LogResult(result);
    }

    public Result<TValue> Fail<TValue>(HttpStatusCode statusCode, IError error, TValue value)
    {
        RequireErrorStatusCode(statusCode);
        
        var result = new Result<TValue>()
            .WithValue(value)
            .WithError(error)
            .WithStatusCode(statusCode);

        return LogResult(result);
    }

    public Result<TValue> Fail<TValue>(HttpStatusCode statusCode, string message, TValue value)
    {
        RequireErrorStatusCode(statusCode);
        
        var result = new Result<TValue>()
            .WithValue(value)
            .WithError(message)
            .WithStatusCode(statusCode);

        return LogResult(result);
    }

    public Result<TValue> Fail<TValue>(HttpStatusCode statusCode, IEnumerable<string> messages, TValue value)
    {
        RequireErrorStatusCode(statusCode);
        
        var result = new Result<TValue>()
            .WithValue(value)
            .WithErrors(messages)
            .WithStatusCode(statusCode);

        return LogResult(result);
    }

    public Result<TValue> Fail<TValue>(HttpStatusCode statusCode, IEnumerable<IError> errors, TValue value)
    {
        RequireErrorStatusCode(statusCode);
        
        var result = new Result<TValue>()
            .WithValue(value)
            .WithErrors(errors)
            .WithStatusCode(statusCode);

        return LogResult(result);
    }

    private static void RequireSuccessStatusCode(HttpStatusCode statusCode)
    {
        if ((uint) statusCode >= 400)
        {
            throw new ArgumentException("Status code must be that of a generic Success, so less than 400");
        }
    }
    
    private static void RequireErrorStatusCode(HttpStatusCode statusCode)
    {
        if ((uint) statusCode < 400)
        {
            throw new ArgumentException("Status code must be that of a generic Error, so greater than or equal to 400");
        }
    }

    private Result<TValue> LogResult<TValue>(Result<TValue> result)
    {
        LogResultInternal(result);

        return result;
    }
    
    private Result LogResult(Result result)
    {
        LogResultInternal(result);
        
        return result;
    }

    private void LogResultInternal(IResultBase result)
    {
        if (result.IsFailed)
        {
            var logLevel = result.StatusCode >= HttpStatusCode.InternalServerError ? LogLevel.Error : LogLevel.Debug;
            
            foreach (var error in result.Errors)
            {
                if (error is ExceptionalError exceptionalError)
                {
                    _logger.Log(logLevel, 0, exceptionalError.Exception, "Exception result {Message}", exceptionalError.Message);
                    
                    continue;
                }
                
                _logger.Log(logLevel, 0, "Failure result {Message} {Reasons}", error.Message, error.Reasons);
            }
        }
        else
        {
            foreach (var resultSuccess in result.Successes)
            {
                _logger.LogTrace("Successful result {Message}", resultSuccess.Message);
            }
        }
    }
}
