namespace Futurum.Core.Functional;

public static class Function
{
    /// <summary>
    /// A function that does nothing
    /// </summary>
    public static readonly Action DoNothing = () => { };

    /// <summary>
    /// A function that does nothing
    /// </summary>
    public static readonly Func<Task> DoNothingAsync = () => Task.CompletedTask;
}

public static class Function<T>
{
    /// <summary>
    /// A function that does nothing
    /// </summary>
    public static readonly Action<T> DoNothing = _ => { };

    /// <summary>
    /// A function that does nothing
    /// </summary>
    public static readonly Func<T, Task> DoNothingAsync = _ => Task.CompletedTask;
}