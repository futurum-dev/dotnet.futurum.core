namespace Futurum.Core.Result;

/// <summary>
/// A <see cref="IResultError"/> that represents a error where a key is not found
/// </summary>
public class ResultErrorKeyNotFound : IResultErrorNonComposite
{
    private readonly string _key;
    private readonly string _sourceDescription;

    private ResultErrorKeyNotFound(string key, string sourceDescription)
    {
        _key = key;
        _sourceDescription = sourceDescription;
    }

    /// <inheritdoc />
    public string GetErrorStringSafe() =>
        $"Unable to find key : '{_key}' in source : '{_sourceDescription}'";

    /// <inheritdoc />
    public string GetErrorString() =>
        GetErrorStringSafe();

    /// <inheritdoc />
    public ResultErrorStructure GetErrorStructureSafe() =>
        new(GetErrorStringSafe(), Enumerable.Empty<ResultErrorStructure>());

    /// <inheritdoc />
    public ResultErrorStructure GetErrorStructure() =>
        GetErrorStructureSafe();

    /// <summary>
    /// Create a <see cref="ResultErrorKeyNotFound"/> 
    /// </summary>
    public static IResultErrorNonComposite Create(string key, string sourceDescription) =>
        new ResultErrorKeyNotFound(key, sourceDescription);
}