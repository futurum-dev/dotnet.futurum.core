using System.Linq;

using Futurum.Core.Option;
using Futurum.Core.Tests.Helper.Option;

using Xunit;

namespace Futurum.Core.Tests.Option;

public class OptionEnumerableExtensionsTryElementAtTests
{
    [Fact]
    public void HasNoValue()
    {
        var values = Enumerable.Range(0, 10)
                               .ToList();

        var option = values.TryElementAt(100);

        option.ShouldBeHasNoValue();
    }

    [Fact]
    public void HasValue()
    {
        var values = Enumerable.Range(0, 10)
                               .ToList();

        var option = values.TryElementAt(2);

        option.ShouldBeHasValueWithValue(2);
    }
}