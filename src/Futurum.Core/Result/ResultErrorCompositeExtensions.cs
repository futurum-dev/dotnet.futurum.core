namespace Futurum.Core.Result;

/// <summary>
/// Extension methods for the creation of a <see cref="ResultErrorComposite"/>
/// </summary>
public static class ResultErrorCompositeExtensions
{
    /// <summary>
    /// Create a <see cref="ResultErrorComposite"/>, with <paramref name="parentError"/> as the <see cref="ResultErrorComposite.Parent"/> and <paramref name="errors"/> as the <see cref="ResultErrorComposite.Children"/> 
    /// </summary>
    public static IResultError ToResultError(IResultErrorNonComposite parentError, IEnumerable<IResultError> errors) =>
        new ResultErrorComposite(parentError, errors);

    /// <summary>
    /// Create a <see cref="ResultErrorComposite"/>, with <paramref name="errors"/> as the <see cref="ResultErrorComposite.Children"/> 
    /// </summary>
    public static IResultError ToResultError(this IEnumerable<IResultError> errors) =>
        new ResultErrorComposite(errors);

    /// <summary>
    /// Create a <see cref="ResultErrorComposite"/>, with <paramref name="parentError"/> as the <see cref="ResultErrorComposite.Parent"/> and <paramref name="errors"/> as the <see cref="ResultErrorComposite.Children"/> 
    /// </summary>
    public static IResultError ToResultError(IResultErrorNonComposite parentError, params IResultError[] errors) =>
        ToResultError(parentError, errors.AsEnumerable());
}