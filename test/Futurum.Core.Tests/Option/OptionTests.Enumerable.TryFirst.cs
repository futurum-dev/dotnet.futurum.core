using System.Linq;

using Futurum.Core.Option;
using Futurum.Core.Tests.Helper.Option;

using Xunit;

namespace Futurum.Core.Tests.Option;

public class OptionEnumerableExtensionsTryFirstTests
{
    public class WithoutPredicate
    {
        [Fact]
        public void HasNoValue()
        {
            var values = Enumerable.Empty<int>()
                                   .ToList();

            var option = values.TryFirst();

            option.ShouldBeHasNoValue();
        }

        [Fact]
        public void HasValue()
        {
            var values = Enumerable.Range(0, 10)
                                   .ToList();

            var option = values.TryFirst();

            option.ShouldBeHasValueWithValue(0);
        }
    }

    public class WithPredicate
    {
        [Fact]
        public void HasNoValue()
        {
            var values = Enumerable.Empty<int>()
                                   .ToList();

            var option = values.TryFirst(x => x % 2 == 0);

            option.ShouldBeHasNoValue();
        }

        [Fact]
        public void HasValue()
        {
            var values = Enumerable.Range(1, 10)
                                   .ToList();

            var option = values.TryFirst(x => x % 2 == 0);

            option.ShouldBeHasValueWithValue(2);
        }
    }
}