namespace Futurum.Core.Result;

public class ResultErrorEmpty : IResultErrorNonComposite
{
    public static readonly ResultErrorEmpty Value = new();

    private ResultErrorEmpty()
    {
    }

    /// <inheritdoc />
    public string GetErrorString() =>
        string.Empty;

    /// <inheritdoc />
    public ResultErrorStructure GetErrorStructure() =>
        ResultErrorStructureExtensions.CreateEmptyResultErrorStructure();
}