namespace Futurum.Core.Result;

/// <summary>
/// Extension methods for the creation of a <see cref="ResultErrorMessage"/>
/// </summary>
public static class ResultErrorMessageExtensions
{
    /// <summary>
    /// Transforms an <see cref="string"/> into either a <see cref="ResultErrorMessage"/> or a <see cref="ResultErrorEmpty"/> 
    /// </summary>
    public static IResultErrorNonComposite ToResultError(this string errorMessage) =>
        string.IsNullOrEmpty(errorMessage)
            ? ResultErrorEmpty.Value
            : new ResultErrorMessage(errorMessage);
}