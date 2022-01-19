namespace Futurum.Core.Result;

/// <summary>
/// Extension methods for transforming an <see cref="IResultError"/> into a <see cref="string"/>
/// </summary>
public static class ResultErrorStringExtensions
{
    /// <summary>
    /// Transforms an <see cref="IResultError"/> into a <see cref="string"/>
    /// </summary>
    public static string ToErrorString(this IResultError error) =>
        ToErrorString(error, ";");

    /// <summary>
    /// Transforms an <see cref="IResultError"/> into a <see cref="string"/>
    /// </summary>
    public static string ToErrorString(this IResultError resultError, string seperator) =>
        resultError switch
        {
            null                                             => string.Empty,
            IResultErrorNonComposite resultErrorNonComposite => resultErrorNonComposite.GetErrorString(),
            IResultErrorComposite resultErrorComposite       => resultErrorComposite.GetErrorString(seperator),
            _                                                => $"Unknown ResultError of type {resultError.GetType().FullName}. '{resultError.ToString()}'"
        };
}