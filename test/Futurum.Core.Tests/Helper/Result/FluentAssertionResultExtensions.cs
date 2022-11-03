using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentAssertions;

using Futurum.Core.Result;
using Futurum.Core.Tests.Helper.Option;

namespace Futurum.Core.Tests.Helper.Result;

/// <summary>
/// Extension methods for <see cref="FluentAssertions"/>, so that it supports <see cref="Futurum.Core.Result.Result"/> and <see cref="Futurum.Core.Result.Result{T}"/>
/// </summary>
public static class FluentAssertionResultExtensions
{
    /// <summary>
    /// Specifies that the <see cref="Futurum.Core.Result.Result"/> should be <see cref="Futurum.Core.Result.Result.IsSuccess"/> true.
    /// </summary>
    public static void ShouldBeSuccess(this Core.Result.Result result)
    {
        var errorMessage = result.IsSuccess ? string.Empty : $"Error : '{result.Error.Value.ToErrorString()}'";

        result.IsSuccess.Should().BeTrue(errorMessage);
    }

    /// <summary>
    /// Specifies that the <see cref="Futurum.Core.Result.Result{T}"/> should be <see cref="Futurum.Core.Result.Result{T}.IsSuccess"/> true.
    /// </summary>
    public static void ShouldBeSuccess<T>(this Result<T> result)
    {
        var errorMessage = result.IsSuccess ? string.Empty : $"Error : '{result.Error.Value.ToErrorString()}'";

        result.IsSuccess.Should().BeTrue(errorMessage);
    }

    /// <summary>
    /// Specifies that the <see cref="Futurum.Core.Result.Result{T}"/> should be <see cref="Futurum.Core.Result.Result{T}.IsSuccess"/> true with <paramref name="value"/>.
    /// </summary>
    public static void ShouldBeSuccessWithValue<T>(this Result<T> result, T value)
    {
        result.ShouldBeSuccess();

        result.Value.Value.Should().Be(value);
    }

    /// <summary>
    /// Specifies that the <see cref="Futurum.Core.Result.Result{T}"/> should be <see cref="Futurum.Core.Result.Result{T}.IsSuccess"/> true with <paramref name="optionValue"/>.
    /// </summary>
    public static void ShouldBeSuccessWithValue<T>(this Result<T> result, Futurum.Core.Option.Option<T> optionValue)
    {
        result.ShouldBeSuccess();

        optionValue.ShouldBeHasValue();

        result.Value.Value.Should().Be(optionValue.Value);
    }

    /// <summary>
    /// Specifies that the <see cref="Futurum.Core.Result.Result{T}"/> should be <see cref="Futurum.Core.Result.Result{T}.IsSuccess"/> true with <paramref name="value"/>.
    /// </summary>
    public static void ShouldBeSuccessWithValue<T, TR>(this Result<T> result, Func<T, TR> selectorFunc, TR value)
    {
        result.ShouldBeSuccess();

        selectorFunc(result.Value.Value).Should().Be(value);
    }

    /// <summary>
    /// Specifies that the <see cref="Futurum.Core.Result.Result{T}"/> should be <see cref="Futurum.Core.Result.Result{T}.IsSuccess"/> true with equivalent to  <paramref name="value"/>.
    /// </summary>
    public static void ShouldBeSuccessWithValueEquivalentTo<T>(this Result<T> result, T value)
    {
        result.ShouldBeSuccess();

        result.Value.Value.Should().BeEquivalentTo(value);
    }

    /// <summary>
    /// Specifies that the <see cref="Futurum.Core.Result.Result{T}"/> should be <see cref="Futurum.Core.Result.Result{T}.IsSuccess"/> true with equivalent to  <paramref name="value"/>.
    /// </summary>
    public static void ShouldBeSuccessWithValueEquivalentTo<T, TR>(this Result<T> result, Func<T, TR> selectorFunc, TR value)
    {
        result.ShouldBeSuccess();

        selectorFunc(result.Value.Value).Should().BeEquivalentTo(value);
    }

    /// <summary>
    /// Specifies that the <see cref="Futurum.Core.Result.Result{T}"/> should be <see cref="Futurum.Core.Result.Result{T}.IsSuccess"/> true with equivalent to <paramref name="value"/>.
    /// </summary>
    public static Task ShouldBeSuccessWithValueEquivalentToAsync<TData>(this Result<IAsyncEnumerable<TData>> result, IEnumerable<TData> value) =>
        result.ShouldBeSuccessWithValueEquivalentToAsync(x => x, value);

    /// <summary>
    /// Specifies that the <see cref="Futurum.Core.Result.Result{T}"/> should be <see cref="Futurum.Core.Result.Result{T}.IsSuccess"/> true with equivalent to <paramref name="value"/>.
    /// </summary>
    public static async Task ShouldBeSuccessWithValueEquivalentToAsync<T, TData>(this Result<T> result, Func<T, IAsyncEnumerable<TData>> selectorFunc, IEnumerable<TData> value)
    {
        result.ShouldBeSuccess();

        var asyncEnumerable = selectorFunc(result.Value.Value);

        var left = await ConvertIAsyncEnumerableToIEnumerable(asyncEnumerable);

        var right = value;

        left.Should().BeEquivalentTo(right);

        static async Task<IEnumerable<TData>> ConvertIAsyncEnumerableToIEnumerable(IAsyncEnumerable<TData> value)
        {
            var list = new List<TData>();

            await foreach (var x in value)
            {
                list.Add(x);
            }

            return list;
        }
    }

    /// <summary>
    /// Specifies that the <see cref="Futurum.Core.Result.Result{T}"/> should be <see cref="Futurum.Core.Result.Result{T}.IsSuccess"/> true with equivalent to <paramref name="value"/>.
    /// </summary>
    public static Task ShouldBeSuccessWithValueEquivalentToAsync<TData>(this Result<IAsyncEnumerable<TData>> result, IAsyncEnumerable<TData> value) =>
        result.ShouldBeSuccessWithValueEquivalentToAsync(x => x, value);

    /// <summary>
    /// Specifies that the <see cref="Futurum.Core.Result.Result{T}"/> should be <see cref="Futurum.Core.Result.Result{T}.IsSuccess"/> true with equivalent to <paramref name="value"/>.
    /// </summary>
    public static async Task ShouldBeSuccessWithValueEquivalentToAsync<T, TData>(this Result<T> result, Func<T, IAsyncEnumerable<TData>> selectorFunc, IAsyncEnumerable<TData> value)
    {
        result.ShouldBeSuccess();

        var asyncEnumerable = selectorFunc(result.Value.Value);

        var left = await ConvertIAsyncEnumerableToIEnumerable(asyncEnumerable);

        var right = await ConvertIAsyncEnumerableToIEnumerable(value);

        left.Should().BeEquivalentTo(right);

        static async Task<IEnumerable<TData>> ConvertIAsyncEnumerableToIEnumerable(IAsyncEnumerable<TData> value)
        {
            var list = new List<TData>();

            await foreach (var x in value)
            {
                list.Add(x);
            }

            return list;
        }
    }

    /// <summary>
    /// Specifies that the <see cref="Futurum.Core.Result.Result"/> should be <see cref="Futurum.Core.Result.Result.IsFailure"/> true.
    /// </summary>
    public static void ShouldBeFailure(this Core.Result.Result result)
    {
        result.IsFailure.Should().BeTrue();
    }

    /// <summary>
    /// Specifies that the <see cref="Futurum.Core.Result.Result"/> should be <see cref="Futurum.Core.Result.Result.IsFailure"/> true with <paramref name="errorMessages"/>.
    /// </summary>
    public static void ShouldBeFailureWithErrorSafe(this Core.Result.Result result, params string[] errorMessages)
    {
        result.ShouldBeFailure();

        result.Error.Map(ResultErrorStringExtensions.ToErrorStringSafe).Should().Be(string.Join(";", errorMessages));
    }

    /// <summary>
    /// Specifies that the <see cref="Futurum.Core.Result.Result"/> should be <see cref="Futurum.Core.Result.Result.IsFailure"/> true with <paramref name="errorMessages"/>.
    /// </summary>
    public static void ShouldBeFailureWithError(this Core.Result.Result result, params string[] errorMessages)
    {
        result.ShouldBeFailure();

        result.Error.Map(ResultErrorStringExtensions.ToErrorString).Should().Be(string.Join(";", errorMessages));
    }

    /// <summary>
    /// Specifies that the <see cref="Futurum.Core.Result.Result{T}"/> should be <see cref="Futurum.Core.Result.Result{T}.IsFailure"/> true.
    /// </summary>
    public static void ShouldBeFailure<T>(this Result<T> result)
    {
        result.IsFailure.Should().BeTrue();
    }

    /// <summary>
    /// Specifies that the <see cref="Futurum.Core.Result.Result{T}"/> should be <see cref="Futurum.Core.Result.Result{T}.IsFailure"/> true with <paramref name="errorMessages"/>.
    /// </summary>
    public static void ShouldBeFailureWithErrorSafe<T>(this Result<T> result, params string[] errorMessages)
    {
        result.ShouldBeFailure();

        result.Error.Map(ResultErrorStringExtensions.ToErrorStringSafe).Should().Be(string.Join(";", errorMessages));
    }
    /// <summary>
    /// Specifies that the <see cref="Futurum.Core.Result.Result{T}"/> should be <see cref="Futurum.Core.Result.Result{T}.IsFailure"/> true with <paramref name="errorMessages"/>.
    /// </summary>
    public static void ShouldBeFailureWithError<T>(this Result<T> result, params string[] errorMessages)
    {
        result.ShouldBeFailure();

        result.Error.Map(ResultErrorStringExtensions.ToErrorString).Should().Be(string.Join(";", errorMessages));
    }

    /// <summary>
    /// Specifies that the <see cref="Futurum.Core.Result.Result{T}"/> should be <see cref="Futurum.Core.Result.Result{T}.IsFailure"/> true containing <paramref name="errorMessage"/>.
    /// </summary>
    public static void ShouldBeFailureWithErrorContaining(this Core.Result.Result result, string errorMessage)
    {
        result.ShouldBeFailure();

        result.Error.Value.Flatten().Any(e => e.ToErrorString(",").Contains(errorMessage))
              .Should().BeTrue($"Error does not contain error message : '{errorMessage}' , '{result.Error.Value.ToErrorString(",")}'");
    }

    /// <summary>
    /// Specifies that the <see cref="Futurum.Core.Result.Result{T}"/> should be <see cref="Futurum.Core.Result.Result{T}.IsFailure"/> true containing <paramref name="errorMessage"/>.
    /// </summary>
    public static void ShouldBeFailureWithErrorContaining<T>(this Result<T> result, string errorMessage)
    {
        result.ShouldBeFailure();

        result.Error.Value.Flatten().Any(e => e.ToErrorString(",").Contains(errorMessage))
              .Should().BeTrue($"Error does not contain error message : '{errorMessage}' , '{result.Error.Value.ToErrorString(",")}'");
    }

    /// <summary>
    /// Specifies that the <see cref="Futurum.Core.Result.IResultError"/> should be <paramref name="errorMessages"/>.
    /// </summary>
    public static void ShouldBeError(this IResultError error, params string[] errorMessages)
    {
        error.ToErrorString().Should().Be(string.Join(";", errorMessages));
    }
}