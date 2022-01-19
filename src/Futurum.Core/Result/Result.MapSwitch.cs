namespace Futurum.Core.Result;

public readonly partial struct Result<T>
{
    /// <summary>
    /// Transforms <see cref="Result{T}"/> to <see cref="Result"/> <see cref="TR"/>
    /// <list type="bullet">
    ///     <item>
    ///         <description>If <see cref="Result{T}"/> <see cref="Result{T}.IsSuccess"/> is true, then call and return <paramref name="successFunc"/></description>
    ///     </item>
    ///     <item>
    ///         <description>If <see cref="Result{T}"/> <see cref="Result{T}.IsFailure"/> is true, then call and return <paramref name="failureFunc"/></description>
    ///     </item>
    /// </list>
    /// </summary>
    public Result<TR> MapSwitch<TR>(Func<T, TR> successFunc, Func<TR> failureFunc) =>
        IsFailure ? failureFunc().ToResultOk() : successFunc(Value.Value).ToResultOk();
}