namespace Futurum.Core.Result;

/// <summary>
/// A <see cref="IResultError"/> that wraps an Exception
/// </summary>
public class ResultErrorException : IResultErrorNonComposite
{
    internal ResultErrorException(Exception exception)
    {
        Exception = exception;
    }

    public Exception Exception { get; }

    /// <inheritdoc />
    public string GetErrorString() =>
        Exception.Message;

    /// <inheritdoc />
    public ResultErrorStructure GetErrorStructure() =>
        Exception.Message.ToResultErrorStructure();
}