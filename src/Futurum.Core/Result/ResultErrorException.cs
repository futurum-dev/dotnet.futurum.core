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
    public string GetErrorStringSafe() =>
        Exception.Message;

    /// <inheritdoc />
    public string GetErrorString() =>
        Exception.ToString();

    /// <inheritdoc />
    public ResultErrorStructure GetErrorStructureSafe() =>
        Exception.Message.ToResultErrorStructure();

    /// <inheritdoc />
    public ResultErrorStructure GetErrorStructure() =>
        Exception.ToString().ToResultErrorStructure();
}