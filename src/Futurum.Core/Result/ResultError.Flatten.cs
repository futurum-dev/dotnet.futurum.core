using Futurum.Core.Linq;

namespace Futurum.Core.Result;

public static partial class ResultEnumerableExtensions
{
    /// <summary>
    /// Flattens an <see cref="IResultError"/> into a sequence of its child <see cref="IResultError"/>
    /// </summary>
    public static IEnumerable<IResultError> Flatten(this IResultError resultError)
    {
        IEnumerable<IResultError> Composite(IResultErrorComposite resultErrorComposite)
        {
            if (resultErrorComposite.Parent.HasValue)
            {
                yield return resultErrorComposite.Parent.Value;
            }

            foreach (var children in resultErrorComposite.Children.SelectMany(Flatten))
            {
                yield return children;
            }
        }

        return resultError switch
        {
            IResultErrorComposite resultErrorComposite => Composite(resultErrorComposite),
            _                                          => EnumerableExtensions.Return(resultError)
        };
    }
}