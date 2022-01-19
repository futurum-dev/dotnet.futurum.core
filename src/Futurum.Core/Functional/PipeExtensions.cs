namespace Futurum.Core.Functional;

public static class PipeExtensions
{
    public static TResult Pipe<T, TResult>(this T value, Func<T, TResult> func) =>
        func(value);

    public static async Task<TResult> PipeAsync<T, TResult>(this Task<T> task, Func<T, TResult> func)
    {
        var value = await task;

        return func(value);
    }

    public static async Task<TResult> PipeAsync<T, TInput1, TResult>(this Task<T> task, Func<T, TInput1, TResult> func, TInput1 input1)
    {
        var value = await task;

        return func(value, input1);
    }

    public static async Task<TResult> PipeAsync<T, TInput1, TInput2, TResult>(this Task<T> task, Func<T, TInput1, TInput2, TResult> func, TInput1 input1, TInput2 input2)
    {
        var value = await task;

        return func(value, input1, input2);
    }

    public static async Task<TResult> PipeAsync<T, TInput1, TInput2, TInput3, TResult>(this Task<T> task, Func<T, TInput1, TInput2, TInput3, TResult> func, TInput1 input1, TInput2 input2,
                                                                                       TInput3 input3)
    {
        var value = await task;

        return func(value, input1, input2, input3);
    }

    public static async Task<TResult> PipeAsync<T, TResult>(this Task<T> task, Func<T, Task<TResult>> func)
    {
        var value = await task;

        return await func(value);
    }

    public static async Task<TResult> PipeAsync<T, TResult>(this IEnumerable<Task<T>> tasks, Func<IEnumerable<T>, TResult> func)
    {
        var value = await Task.WhenAll(tasks);

        return func(value);
    }
}