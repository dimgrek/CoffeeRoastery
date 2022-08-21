using System;

namespace CoffeeRoastery.BLL.Interface.Common;

public class Result
{
    public bool IsSucceed { get; }

    public string ErrorMessage { get; }

    protected Result(bool isSucceed, string errorMessage)
    {
        IsSucceed = isSucceed;
        ErrorMessage = errorMessage;
    }

    public static Result Ok()
    {
        return new Result(true, null);
    }

    public static Result<T> Ok<T>(T value)
    {
        return new Result<T>(value, true, null);
    }
    
    public static Result Fail(string message, params object[] args)
    {
        return new Result(false, string.Format(message, args));
    }

    public static Result<T> Fail<T>(string message)
    {
        return new Result<T>(default, false, message);
    }

    public static Result<T> Fail<T>(string message, params object[] args)
    {
        return new Result<T>(default, false, string.Format(message, args));
    }
}

public class Result<T> : Result
{
    public T Value { get; }

    protected internal Result(T value, bool success, string error)
        : base(success, error)
    {
        Value = value;
    }

    public Result<K> ToResult<K>(Func<T, K> converter)
    {
        return new Result<K>(IsSucceed ? converter(Value) : default, IsSucceed, ErrorMessage);
    }
}