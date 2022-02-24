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
    public static Result<T2> CombineAll<T0, T1, T2>(Result<T0> result0, Result<T1> result1, Func<T0, T1, T2> selectorFunc)
    {
        if (result0.IsSuccess && result1.IsSuccess)
        {
            return selectorFunc(result0.Value.Value, result1.Value.Value).ToResultOk();
        }

        return Result.Fail<T2>(Combine(result0.ToNonGeneric(), result1.ToNonGeneric()).Error.Value);
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
    public static Result<T3> CombineAll<T0, T1, T2, T3>(Result<T0> result0, Result<T1> result1, Result<T2> result2, Func<T0, T1, T2, T3> selectorFunc)
    {
        if (result0.IsSuccess && result1.IsSuccess && result2.IsSuccess)
        {
            return selectorFunc(result0.Value.Value, result1.Value.Value, result2.Value.Value).ToResultOk();
        }

        return Result.Fail<T3>(Combine(result0.ToNonGeneric(), result1.ToNonGeneric(), result2.ToNonGeneric()).Error.Value);
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
    public static Result<T4> CombineAll<T0, T1, T2, T3, T4>(Result<T0> result0, Result<T1> result1, Result<T2> result2, Result<T3> result3, Func<T0, T1, T2, T3, T4> selectorFunc)
    {
        if (result0.IsSuccess && result1.IsSuccess && result2.IsSuccess && result3.IsSuccess)
        {
            return selectorFunc(result0.Value.Value, result1.Value.Value, result2.Value.Value, result3.Value.Value).ToResultOk();
        }

        return Result.Fail<T4>(Combine(result0.ToNonGeneric(), result1.ToNonGeneric(), result2.ToNonGeneric(), result3.ToNonGeneric()).Error.Value);
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
    public static async Task<Result<T2>> CombineAllAsync<T0, T1, T2>(Task<Result<T0>> resultTask0, Task<Result<T1>> resultTask1, Func<T0, T1, T2> selectorFunc)
    {
        await Task.WhenAll(resultTask0, resultTask1);

        if (resultTask0.Result.IsSuccess && resultTask1.Result.IsSuccess)
        {
            return selectorFunc(resultTask0.Result.Value.Value, resultTask1.Result.Value.Value).ToResultOk();
        }

        return Result.Fail<T2>(Combine(resultTask0.Result.ToNonGeneric(), resultTask1.Result.ToNonGeneric()).Error.Value);
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
    public static async Task<Result<T3>> CombineAllAsync<T0, T1, T2, T3>(Task<Result<T0>> resultTask0, Task<Result<T1>> resultTask1, Task<Result<T2>> resultTask2, Func<T0, T1, T2, T3> selectorFunc)
    {
        await Task.WhenAll(resultTask0, resultTask1, resultTask2);

        if (resultTask0.Result.IsSuccess && resultTask1.Result.IsSuccess && resultTask2.Result.IsSuccess)
        {
            return selectorFunc(resultTask0.Result.Value.Value, resultTask1.Result.Value.Value, resultTask2.Result.Value.Value).ToResultOk();
        }

        return Result.Fail<T3>(Combine(resultTask0.Result.ToNonGeneric(), resultTask1.Result.ToNonGeneric(), resultTask2.Result.ToNonGeneric()).Error.Value);
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
    public static async Task<Result<T4>> CombineAllAsync<T0, T1, T2, T3, T4>(Task<Result<T0>> resultTask0, Task<Result<T1>> resultTask1, Task<Result<T2>> resultTask2, Task<Result<T3>> resultTask3, Func<T0, T1, T2, T3, T4> selectorFunc)
    {
        await Task.WhenAll(resultTask0, resultTask1, resultTask2, resultTask3);

        if (resultTask0.Result.IsSuccess && resultTask1.Result.IsSuccess && resultTask2.Result.IsSuccess && resultTask3.Result.IsSuccess)
        {
            return selectorFunc(resultTask0.Result.Value.Value, resultTask1.Result.Value.Value, resultTask2.Result.Value.Value, resultTask3.Result.Value.Value).ToResultOk();
        }

        return Result.Fail<T4>(Combine(resultTask0.Result.ToNonGeneric(), resultTask1.Result.ToNonGeneric(), resultTask2.Result.ToNonGeneric(), resultTask3.Result.ToNonGeneric()).Error.Value);
    }
}