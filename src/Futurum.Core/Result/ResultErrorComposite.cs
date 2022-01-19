using System.Collections.Immutable;

using Futurum.Core.Linq;
using Futurum.Core.Option;

namespace Futurum.Core.Result;

/// <summary>
/// A <see cref="IResultError"/> with an optional parent <see cref="IResultError"/> and children <see cref="IResultError"/>
/// </summary>
public class ResultErrorComposite : IResultErrorComposite
{
    internal ResultErrorComposite(IEnumerable<IResultError> errors)
    {
        Parent = Option<IResultErrorNonComposite>.None;
        Children = errors.ToImmutableList();
    }

    internal ResultErrorComposite(IResultErrorNonComposite parent, IEnumerable<IResultError> errors)
    {
        Parent = parent.ToOption();
        Children = errors.ToImmutableList();
    }

    /// <inheritdoc />
    public Option<IResultErrorNonComposite> Parent { get; }

    /// <inheritdoc />
    public IEnumerable<IResultError> Children { get; }

    /// <inheritdoc />
    public string GetErrorString(string seperator)
    {
        string Transform(IResultError resultError) =>
            resultError.ToErrorString(seperator);

        string GetChildrenErrorString() =>
            Children.Select(Transform)
                    .StringJoin(seperator);

        string GetParentErrorString() =>
            Parent.Value.GetErrorString();

        return Parent.HasValue
            ? $"{GetParentErrorString()}{seperator}{GetChildrenErrorString()}"
            : GetChildrenErrorString();
    }

    /// <inheritdoc />
    public ResultErrorStructure GetErrorStructure() =>
        Parent.HasValue
            ? ResultErrorStructureExtensions.ToResultErrorStructure(Parent.Value.GetErrorString(), Children.Select(ResultErrorStructureExtensions.ToErrorStructure))
            : ResultErrorStructureExtensions.ToResultErrorStructure(Children.Select(ResultErrorStructureExtensions.ToErrorStructure));
}