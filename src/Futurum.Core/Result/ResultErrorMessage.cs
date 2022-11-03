namespace Futurum.Core.Result;

/// <summary>
/// A <see cref="IResultError"/> that wraps a string error message
/// </summary>
public class ResultErrorMessage : IResultErrorNonComposite
{
    internal ResultErrorMessage(string message)
    {
        Message = message;
    }

    public string Message { get; }

    /// <inheritdoc />
    public string GetErrorStringSafe() =>
        Message;

    /// <inheritdoc />
    public string GetErrorString() =>
        GetErrorStringSafe();

    /// <inheritdoc />
    public ResultErrorStructure GetErrorStructureSafe() =>
        Message.ToResultErrorStructure();

    /// <inheritdoc />
    public ResultErrorStructure GetErrorStructure() =>
        GetErrorStructureSafe();
}