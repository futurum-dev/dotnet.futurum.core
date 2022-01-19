using Futurum.Core.Functional;
using Futurum.Core.Linq;

namespace Futurum.Core.Result;

public static partial class ResultExtensions
{
    public static Result<T> EnsureThatAll<T>(this Result<T> result, string propertyName, params Func<T, string, Result>[] ensures) =>
        result.ApplyEnsures(propertyName, ensures)
              .Then(() => result);

    public static Result<T> EnsureThatAll<T, TValue>(this Result<T> result, Func<T, TValue> selector, string propertyName, params Func<TValue, string, Result>[] ensures) =>
        result.Map(selector)
              .ApplyEnsures(propertyName, ensures)
              .Then(() => result);

    public static Result<T> EnsureThatAll<T>(this Result<T> result, params Func<T, Result>[] ensures) =>
        result.ApplyEnsures(ensures)
              .Then(() => result);

    public static Result<T> EnsureThatAll<T>(this Result<T> result, params Func<T, IEnumerable<Result>>[] ensures) =>
        result.ApplyEnsures(ensures)
              .Then(() => result);

    public static Result<T> EnsureThatAll<T>(this T value, params Func<T, Result>[] ensures) =>
        value.ApplyEnsures(ensures)
             .Map(() => value);

    public static Result<T> EnsureThatAll<T>(this T value, params Func<T, IEnumerable<Result>>[] ensures) =>
        value.ApplyEnsures(ensures)
             .Map(() => value);

    public static Task<Result<T>> EnsureThatAllAsync<T>(this Task<Result<T>> resultTask, string propertyName, params Func<T, string, Result>[] ensures) =>
        resultTask.PipeAsync(EnsureThatAll, propertyName, ensures);

    public static Task<Result<T>> EnsureThatAllAsync<T, TValue>(this Task<Result<T>> resultTask, Func<T, TValue> selector, string propertyName, params Func<TValue, string, Result>[] ensures) =>
        resultTask.PipeAsync(EnsureThatAll, selector, propertyName, ensures);

    public static Task<Result<T>> EnsureThatAllAsync<T>(this Task<Result<T>> resultTask, params Func<T, Result>[] ensures) =>
        resultTask.PipeAsync(EnsureThatAll, ensures);

    public static Task<Result<T>> EnsureThatAllAsync<T>(this Task<T> value, params Func<T, Result>[] ensures) =>
        value.PipeAsync(EnsureThatAll, ensures);

    private static Result ApplyEnsures<T>(this Result<T> result, string propertyName, IEnumerable<Func<T, string, Result>> ensures) =>
        result.Then(value => ensures.Select(ensure => ensure(value, propertyName))
                                    .Combine());

    private static Result ApplyEnsures<T>(this Result<T> result, IEnumerable<Func<T, Result>> ensures) =>
        result.Then(value => ensures.Select(ensure => ensure(value))
                                    .Combine());

    private static Result ApplyEnsures<T>(this Result<T> result, IEnumerable<Func<T, IEnumerable<Result>>> ensures) =>
        result.Then(value => ensures.Select(ensure => ensure(value))
                                    .SelectMany()
                                    .Combine());

    private static Result ApplyEnsures<T>(this T value, IEnumerable<Func<T, Result>> ensures) =>
        ensures.Select(ensure => ensure(value))
               .Combine();

    private static Result ApplyEnsures<T>(this T value, IEnumerable<Func<T, IEnumerable<Result>>> ensures) =>
        ensures.Select(ensure => ensure(value))
               .SelectMany()
               .Combine();
}