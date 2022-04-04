using Futurum.Core.Result;

namespace Futurum.Core.Option;

public static partial class OptionExtensions
{
    /// <summary>
    /// Tries to cast <paramref name="value"/> as <typeparamref name="TResult"/>
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If the cast is successful (is not null), the cast result is returned as a <see cref="Option{TResult}"/>.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If the cast is not successful (is null), then an <see cref="Option{TResult}"/>.None is returned.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Option<TResult> TryCast<TResult>(this object? value)
        where TResult : class =>
        (value as TResult).ToOption();

    /// <summary>
    /// Tries to cast <paramref name="value"/> as <typeparamref name="TResult"/>
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If the cast is successful (is not null), the cast result is returned as a <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If the cast is not successful (is null), then as a <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Result<TResult> TryCast<TResult>(this object? value, string errorMessage)
        where TResult : class =>
        TryCast<TResult>(value).ToResult(errorMessage);

    /// <summary>
    /// Tries to cast <paramref name="value"/> as <typeparamref name="TResult"/>
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If the cast is successful (is not null), the cast result is returned as a <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If the cast is not successful (is null), then as a <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Result<TResult> TryCast<TResult>(this object? value, Func<string> errorMessage)
        where TResult : class =>
        TryCast<TResult>(value).ToResult(errorMessage);
}