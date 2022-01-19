namespace Futurum.Core.Result;

public static partial class ResultExtensions
{
    private static IResultError GetError(Result result) => result.Error.Value;

    private static IResultError GetError<T>(Result<T> result) => result.Error.Value;

    internal static T GetValue<T>(Result<T> result) => result.Value.Value;
}