using System.Threading.Tasks;

using Moq.Language.Flow;

namespace Futurum.Core.Tests.Helper.Result;

/// <summary>
/// Extension methods for <see cref="Moq"/>, so that it supports <see cref="Futurum.Core.Result.Result"/> and <see cref="Futurum.Core.Result.Result{T}"/>
/// </summary>
public static class MoqResultExtensions
{
    /// <summary>
    /// Specifies a <see cref="Futurum.Core.Result.Result"/> <see cref="Futurum.Core.Result.Result.Ok"/> as the return from the method.
    /// </summary>
    public static IReturnsResult<TMock> ReturnsResultOk<TMock>(this ISetup<TMock, Core.Result.Result> setup)
        where TMock : class =>
        setup.Returns(Core.Result.Result.Ok);
    
    /// <summary>
    /// Specifies a <see cref="Futurum.Core.Result.Result{T}"/> <see cref="Futurum.Core.Result.Result.Ok{T}"/> with the <paramref name="value"/> as the return from the method.
    /// </summary>
    public static IReturnsResult<TMock> ReturnsResultOk<TMock, T>(this ISetup<TMock, Core.Result.Result<T>> setup, T value)
        where TMock : class =>
        setup.Returns(Core.Result.Result.Ok(value));
    
    /// <summary>
    /// Specifies a <see cref="Futurum.Core.Result.Result"/> <see cref="Futurum.Core.Result.Result.Fail(string)"/> with the <paramref name="errorMessage"/> as the return from the method.
    /// </summary>
    public static IReturnsResult<TMock> ReturnsResultFailure<TMock>(this ISetup<TMock, Core.Result.Result> setup, string errorMessage)
        where TMock : class =>
        setup.Returns(Core.Result.Result.Fail(errorMessage));
    
    /// <summary>
    /// Specifies a <see cref="Futurum.Core.Result.Result{T}"/> <see cref="Futurum.Core.Result.Result.Fail{T}(string)"/> with the <paramref name="errorMessage"/> as the return from the method.
    /// </summary>
    public static IReturnsResult<TMock> ReturnsResultFailure<TMock, T>(this ISetup<TMock, Core.Result.Result<T>> setup, string errorMessage)
        where TMock : class =>
        setup.Returns(Core.Result.Result.Fail<T>(errorMessage));
    
    /// <summary>
    /// Specifies a <see cref="Futurum.Core.Result.Result"/> <see cref="Futurum.Core.Result.Result.OkAsync"/> as the return from the method.
    /// </summary>
    public static IReturnsResult<TMock> ReturnsResultOkAsync<TMock>(this ISetup<TMock, Task<Core.Result.Result>> setup)
        where TMock : class =>
        setup.Returns(Core.Result.Result.OkAsync);
    
    /// <summary>
    /// Specifies a <see cref="Futurum.Core.Result.Result{T}"/> <see cref="Futurum.Core.Result.Result.OkAsync{T}"/> with the <paramref name="value"/> as the return from the method.
    /// </summary>
    public static IReturnsResult<TMock> ReturnsResultOkAsync<TMock, T>(this ISetup<TMock, Task<Core.Result.Result<T>>> setup, T value)
        where TMock : class =>
        setup.Returns(Core.Result.Result.OkAsync(value));
    
    /// <summary>
    /// Specifies a <see cref="Futurum.Core.Result.Result"/> <see cref="Futurum.Core.Result.Result.FailAsync(string)"/> with the <paramref name="errorMessage"/> as the return from the method.
    /// </summary>
    public static IReturnsResult<TMock> ReturnsResultFailureAsync<TMock>(this ISetup<TMock, Task<Core.Result.Result>> setup, string errorMessage)
        where TMock : class =>
        setup.Returns(Core.Result.Result.FailAsync(errorMessage));
    
    /// <summary>
    /// Specifies a <see cref="Futurum.Core.Result.Result{T}"/> <see cref="Futurum.Core.Result.Result.FailAsync{T}(string)"/> with the <paramref name="errorMessage"/> as the return from the method.
    /// </summary>
    public static IReturnsResult<TMock> ReturnsResultFailureAsync<TMock, T>(this ISetup<TMock, Task<Core.Result.Result<T>>> setup, string errorMessage)
        where TMock : class =>
        setup.Returns(Core.Result.Result.FailAsync<T>(errorMessage));
}