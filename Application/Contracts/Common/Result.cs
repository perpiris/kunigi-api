namespace Application.Contracts.Common;

public class Result
{
    public bool IsSuccess { get; }
    public string[] Errors { get; }
    
    protected Result(bool isSuccess, string[] errors)
    {
        IsSuccess = isSuccess;
        Errors = errors;
    }

    public static Result Success() => new(true, Array.Empty<string>());
    
    public static Result Failure(params string[] errors) => new(false, errors);
}

public class Result<T> : Result
{
    public T? Value { get; }

    private Result(T? value, bool isSuccess, string[] errors) 
        : base(isSuccess, errors)
    {
        Value = value;
    }

    public static Result<T> Success(T value) => new(value, true, Array.Empty<string>());
    
    public new static Result<T> Failure(params string[] errors) => new(default, false, errors);
}