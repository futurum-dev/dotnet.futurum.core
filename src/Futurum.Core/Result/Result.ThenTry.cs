using Futurum.Core.Functional;

namespace Futurum.Core.Result;

public readonly partial struct Result
{
    /// <summary>
    /// Try to run <paramref name="func"/>.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and it is successful, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and if it fails, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public Result<T> ThenTry<T>(Func<T> func, Func<string> errorMessage)
    {
        Result<T> Execute() => Try(func, errorMessage);

        return Then(Execute);
    }

    /// <summary>
    /// Try to run <paramref name="func"/>.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and it is successful, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and if it fails, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public Task<Result<T>> ThenTryAsync<T>(Func<Task<T>> func, Func<string> errorMessage)
    {
        Task<Result<T>> Execute() => TryAsync(func, errorMessage);

        return ThenAsync(Execute);
    }
}

public readonly partial struct Result<T>
{
    /// <summary>
    /// Try to run <paramref name="func"/>.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <see cref="Result{T}"/> <see cref="Result{T}.IsSuccess"/> is true and it is successful, returns <see cref="Result{TR}"/> with <see cref="Result{TR}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <see cref="Result{T}"/> <see cref="Result{T}.IsSuccess"/> is true and if it fails, returns <see cref="Result{TR}"/> with <see cref="Result{TR}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public Result<TR> ThenTry<TR>(Func<T, TR> func, Func<string> errorMessage)
    {
        Result<TR> Execute(T value)
        {
            TR ExecuteTry() => func(value);

            return Result.Try(ExecuteTry, errorMessage);
        }

        return Then(Execute);
    }

    /// <summary>
    /// Try to run <paramref name="func"/>.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <see cref="Result{T}"/> <see cref="Result{T}.IsSuccess"/> is true and it is successful, returns <see cref="Result{TR}"/> with <see cref="Result{TR}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <see cref="Result{T}"/> <see cref="Result{T}.IsSuccess"/> is true and if it fails, returns <see cref="Result{TR}"/> with <see cref="Result{TR}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public Result<T> ThenTry(Action<T> func, Func<T, string> errorMessage)
    {
        Result<T> Execute(T value)
        {
            void ExecuteTry() => func(value);

            return Result.Try(ExecuteTry, () => errorMessage(value))
                         .Map(() => value);
        }

        return Then(Execute);
    }

    /// <summary>
    /// Try to run <paramref name="func"/>.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <see cref="Result{T}"/> <see cref="Result{T}.IsSuccess"/> is true and it is successful, returns <see cref="Result{TR}"/> with <see cref="Result{TR}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <see cref="Result{T}"/> <see cref="Result{T}.IsSuccess"/> is true and if it fails, returns <see cref="Result{TR}"/> with <see cref="Result{TR}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public Task<Result<TR>> ThenTryAsync<TR>(Func<T, Task<TR>> func, Func<string> errorMessage)
    {
        Task<Result<TR>> Execute(T value)
        {
            Task<TR> ExecuteTry() => func(value);

            return Result.TryAsync(ExecuteTry, errorMessage);
        }

        return ThenAsync(Execute);
    }
}

public static partial class ResultExtensions
{
    /// <summary>
    /// Try to run <paramref name="func"/>.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and it is successful, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and if it fails, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result<T>> ThenTryAsync<T>(this Task<Result> resultTask, Func<Task<T>> func, Func<string> errorMessage)
    {
        Task<Result<T>> Execute(Result result) => result.ThenTryAsync(func, errorMessage);

        return resultTask.PipeAsync(Execute);
    }

    /// <summary>
    /// Try to run <paramref name="func"/>.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <see cref="Result{T}"/> <see cref="Result{T}.IsSuccess"/> is true and it is successful, returns <see cref="Result{TR}"/> with <see cref="Result{TR}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <see cref="Result{T}"/> <see cref="Result{T}.IsSuccess"/> is true and if it fails, returns <see cref="Result{TR}"/> with <see cref="Result{TR}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result<TR>> ThenTryAsync<T, TR>(this Task<Result<T>> resultTask, Func<T, Task<TR>> func, Func<string> errorMessage)
    {
        Task<Result<TR>> Execute(Result<T> result) => result.ThenTryAsync(func, errorMessage);

        return resultTask.PipeAsync(Execute);
    }

    /// <summary>
    /// Try to run <paramref name="func"/>.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and it is successful, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and if it fails, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result<T>> ThenTryAsync<T>(this Task<Result> resultTask, Func<T> func, Func<string> errorMessage)
    {
        Result<T> Execute(Result result) => result.ThenTry(func, errorMessage);

        return resultTask.PipeAsync(Execute);
    }

    /// <summary>
    /// Try to run <paramref name="func"/>.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <see cref="Result{T}"/> <see cref="Result{T}.IsSuccess"/> is true and it is successful, returns <see cref="Result{TR}"/> with <see cref="Result{TR}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <see cref="Result{T}"/> <see cref="Result{T}.IsSuccess"/> is true and if it fails, returns <see cref="Result{TR}"/> with <see cref="Result{TR}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result<TR>> ThenTryAsync<T, TR>(this Task<Result<T>> resultTask, Func<T, TR> func, Func<string> errorMessage)
    {
        Result<TR> Execute(Result<T> result) => result.ThenTry(func, errorMessage);

        return resultTask.PipeAsync(Execute);
    }
}