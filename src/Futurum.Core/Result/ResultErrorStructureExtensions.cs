namespace Futurum.Core.Result;

/// <summary>
/// Extension methods for the creation of a <see cref="ResultErrorStructure"/>
/// </summary>
public static class ResultErrorStructureExtensions
{
    /// <summary>
    /// Transforms an <see cref="IResultError"/> into a <see cref="ResultErrorStructure"/>. Sensitive information (e.g. StackTraces) are not included
    /// </summary>
    public static ResultErrorStructure ToErrorStructureSafe(this IResultError resultError) =>
        resultError switch
        {
            null => CreateEmptyResultErrorStructure(),
            _    => resultError.GetErrorStructureSafe()
        };

    /// <summary>
    /// Transforms an <see cref="IResultError"/> into a <see cref="ResultErrorStructure"/>
    /// </summary>
    public static ResultErrorStructure ToErrorStructure(this IResultError resultError) =>
        resultError switch
        {
            null => CreateEmptyResultErrorStructure(),
            _    => resultError.GetErrorStructure()
        };

    /// <summary>
    /// Create an empty <see cref="ResultErrorStructure"/>
    /// </summary>
    public static ResultErrorStructure CreateEmptyResultErrorStructure() =>
        new(string.Empty, Enumerable.Empty<ResultErrorStructure>());

    /// <summary>
    /// Transforms an <see cref="string"/> into a <see cref="ResultErrorStructure"/>
    /// </summary>
    public static ResultErrorStructure ToResultErrorStructure(this string message) =>
        new(message, Enumerable.Empty<ResultErrorStructure>());

    /// <summary>
    /// Creates a <see cref="ResultErrorStructure"/> without a <see cref="ResultErrorStructure.Message"/> and with <paramref name="resultErrorStructures"/> as the children
    /// </summary>
    public static ResultErrorStructure ToResultErrorStructure(this IEnumerable<ResultErrorStructure> resultErrorStructures) =>
        new(string.Empty, resultErrorStructures);

    /// <summary>
    /// Creates a <see cref="ResultErrorStructure"/> with <paramref name="message"/> and <paramref name="resultErrorStructures"/> as the children
    /// </summary>
    public static ResultErrorStructure ToResultErrorStructure(string message, IEnumerable<ResultErrorStructure> resultErrorStructures) =>
        new(message, resultErrorStructures);
}