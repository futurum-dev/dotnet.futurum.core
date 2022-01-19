namespace Futurum.Core.Result;

public partial struct Result
{
    /// <summary>
    /// Combines multiple <see cref="Result{T}"/>'s into one.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If all are <see cref="Result{T}.IsSuccess"/> true, the return a <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If any are <see cref="Result{T}.IsFailure"/> true, the return a <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Result<(T0, T1)> CombineAll<T0, T1>(Result<T0> result0, Result<T1> result1)
    {
        if (result0.IsSuccess && result1.IsSuccess)
        {
            return (result0.Value.Value, result1.Value.Value).ToResultOk();
        }

        return Result.Fail<(T0, T1)>(Combine(result0.ToNonGeneric(), result1.ToNonGeneric()).Error.Value);
    }

    /// <summary>
    /// Combines multiple <see cref="Result{T}"/>'s into one.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If all are <see cref="Result{T}.IsSuccess"/> true, the return a <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If any are <see cref="Result{T}.IsFailure"/> true, the return a <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Result<(T0, T1, T2)> CombineAll<T0, T1, T2>(Result<T0> result0, Result<T1> result1, Result<T2> result2)
    {
        if (result0.IsSuccess && result1.IsSuccess && result2.IsSuccess)
        {
            return (result0.Value.Value, result1.Value.Value, result2.Value.Value).ToResultOk();
        }

        return Result.Fail<(T0, T1, T2)>(Combine(result0.ToNonGeneric(), result1.ToNonGeneric(), result2.ToNonGeneric()).Error.Value);
    }

    /// <summary>
    /// Combines multiple <see cref="Result{T}"/>'s into one.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If all are <see cref="Result{T}.IsSuccess"/> true, the return a <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If any are <see cref="Result{T}.IsFailure"/> true, the return a <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Result<(T0, T1, T2, T3)> CombineAll<T0, T1, T2, T3>(Result<T0> result0, Result<T1> result1, Result<T2> result2, Result<T3> result3)
    {
        if (result0.IsSuccess && result1.IsSuccess && result2.IsSuccess && result3.IsSuccess)
        {
            return (result0.Value.Value, result1.Value.Value, result2.Value.Value, result3.Value.Value).ToResultOk();
        }

        return Result.Fail<(T0, T1, T2, T3)>(Combine(result0.ToNonGeneric(), result1.ToNonGeneric(), result2.ToNonGeneric(), result3.ToNonGeneric()).Error.Value);
    }

    /// <summary>
    /// Combines multiple <see cref="Result{T}"/>'s into one.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If all are <see cref="Result{T}.IsSuccess"/> true, the return a <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If any are <see cref="Result{T}.IsFailure"/> true, the return a <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result<(T0, T1)>> CombineAllAsync<T0, T1>(Task<Result<T0>> resultTask0, Task<Result<T1>> resultTask1)
    {
        await Task.WhenAll(resultTask0, resultTask1);

        if (resultTask0.Result.IsSuccess && resultTask1.Result.IsSuccess)
        {
            return (resultTask0.Result.Value.Value, resultTask1.Result.Value.Value).ToResultOk();
        }

        return Result.Fail<(T0, T1)>(Combine(resultTask0.Result.ToNonGeneric(), resultTask1.Result.ToNonGeneric()).Error.Value);
    }

    /// <summary>
    /// Combines multiple <see cref="Result{T}"/>'s into one.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If all are <see cref="Result{T}.IsSuccess"/> true, the return a <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If any are <see cref="Result{T}.IsFailure"/> true, the return a <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result<(T0, T1, T2)>> CombineAllAsync<T0, T1, T2>(Task<Result<T0>> resultTask0, Task<Result<T1>> resultTask1, Task<Result<T2>> resultTask2)
    {
        await Task.WhenAll(resultTask0, resultTask1, resultTask2);

        if (resultTask0.Result.IsSuccess && resultTask1.Result.IsSuccess && resultTask2.Result.IsSuccess)
        {
            return (resultTask0.Result.Value.Value, resultTask1.Result.Value.Value, resultTask2.Result.Value.Value).ToResultOk();
        }

        return Result.Fail<(T0, T1, T2)>(Combine(resultTask0.Result.ToNonGeneric(), resultTask1.Result.ToNonGeneric(), resultTask2.Result.ToNonGeneric()).Error.Value);
    }

    /// <summary>
    /// Combines multiple <see cref="Result{T}"/>'s into one.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If all are <see cref="Result{T}.IsSuccess"/> true, the return a <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If any are <see cref="Result{T}.IsFailure"/> true, the return a <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result<(T0, T1, T2, T3)>> CombineAllAsync<T0, T1, T2, T3>(Task<Result<T0>> resultTask0, Task<Result<T1>> resultTask1, Task<Result<T2>> resultTask2,
                                                                                       Task<Result<T3>> resultTask3)
    {
        await Task.WhenAll(resultTask0, resultTask1, resultTask2, resultTask3);

        if (resultTask0.Result.IsSuccess && resultTask1.Result.IsSuccess && resultTask2.Result.IsSuccess && resultTask3.Result.IsSuccess)
        {
            return (resultTask0.Result.Value.Value, resultTask1.Result.Value.Value, resultTask2.Result.Value.Value, resultTask3.Result.Value.Value).ToResultOk();
        }

        return Result.Fail<(T0, T1, T2, T3)>(Combine(resultTask0.Result.ToNonGeneric(), resultTask1.Result.ToNonGeneric(), resultTask2.Result.ToNonGeneric(), resultTask3.Result.ToNonGeneric()).Error.Value);
    }
}