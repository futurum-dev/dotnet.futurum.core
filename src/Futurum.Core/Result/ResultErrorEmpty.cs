namespace Futurum.Core.Result;

public class ResultErrorEmpty : IResultErrorNonComposite
{
    public static readonly ResultErrorEmpty Value = new();

    private ResultErrorEmpty()
    {
    }

    /// <inheritdoc />
    public string GetErrorStringSafe() =>
        string.Empty;

    public string GetErrorString() =>
        GetErrorStringSafe();

    /// <inheritdoc />
    public ResultErrorStructure GetErrorStructureSafe() =>
        ResultErrorStructureExtensions.CreateEmptyResultErrorStructure();

    public ResultErrorStructure GetErrorStructure() =>
        GetErrorStructureSafe();
}