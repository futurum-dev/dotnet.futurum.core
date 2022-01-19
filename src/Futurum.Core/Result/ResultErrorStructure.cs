namespace Futurum.Core.Result;

/// <summary>
/// Serializable structure for representing an <see cref="IResultError"/>
/// </summary>
public record ResultErrorStructure(string Message, IEnumerable<ResultErrorStructure> Children);