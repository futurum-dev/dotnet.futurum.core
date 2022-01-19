using Moq.Language.Flow;

namespace Futurum.Core.Tests.Helper.Option;

/// <summary>
/// Extension methods for <see cref="Moq"/>, so that it supports <see cref="Futurum.Core.Option.Option{T}"/>
/// </summary>
public static class MoqOptionExtensions
{
    /// <summary>
    /// Specifies a <see cref="Futurum.Core.Option.Option{T}"/> as <see cref="Futurum.Core.Option.Option{T}.HasValue"/> true with the <paramref name="value"/> as the return from the method.
    /// </summary>
    public static IReturnsResult<TMock> ReturnsOptionHasValue<TMock, T>(this ISetup<TMock, Core.Option.Option<T>> setup, T value)
        where TMock : class =>
        setup.Returns(Core.Option.Option.From(value));

    /// <summary>
    /// Specifies a <see cref="Futurum.Core.Option.Option{T}"/> as <see cref="Futurum.Core.Option.Option{T}.HasNoValue"/> true as the return from the method.
    /// </summary>
    public static IReturnsResult<TMock> ReturnsOptionNone<TMock, T>(this ISetup<TMock, Core.Option.Option<T>> setup)
        where TMock : class =>
        setup.Returns(Core.Option.Option.None<T>());
}