namespace Futurum.Core.Result;

/// <summary>
/// Extension methods for the creation of a <see cref="ResultErrorException"/>
/// </summary>
public static class ResultErrorExceptionExtensions
{
    /// <summary>
    /// Transforms an <see cref="Exception"/> into either a <see cref="ResultErrorException"/> or a <see cref="ResultErrorEmpty"/> 
    /// </summary>
    public static IResultErrorNonComposite ToResultError(this Exception exception) =>
        exception switch
        {
            null => ResultErrorEmpty.Value,
            _    => new ResultErrorException(exception)
        };

    /// <summary>
    /// Transforms an <see cref="Exception"/> and a <paramref name="errorMessage"/> into either a <see cref="ResultErrorException"/>
    /// or a <see cref="ResultErrorCompositeExtensions"/> with the <paramref name="errorMessage"/> as the parent and a <see cref="ResultErrorException"/> as the child
    /// </summary>
    public static IResultError ToResultError(this Exception exception, string errorMessage) =>
        string.IsNullOrEmpty(errorMessage)
            ? exception.ToResultError()
            : ResultErrorCompositeExtensions.ToResultError(errorMessage.ToResultError(), exception.ToResultError());
}