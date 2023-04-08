using System.Collections.ObjectModel;
using System.Linq;

using Futurum.Core.Option;
using Futurum.Core.Tests.Helper.Option;

using Xunit;

namespace Futurum.Core.Tests.Option;

public class OptionDictionaryExtensionsTryGetValueTests
{
    private const string ErrorMessage = "ERROR_MESSAGE";

    public class Dictionary
    {
        [Fact]
        public void HasNoValue()
        {
            var values = Enumerable.Range(0, 10)
                                   .ToDictionary(x => x, x => x);

            var option = values.TryGetValue(100);

            option.ShouldBeHasNoValue();
        }

        [Fact]
        public void HasValue()
        {
            var values = Enumerable.Range(0, 10)
                                   .ToDictionary(x => x, x => x);

            var option = values.TryGetValue(2);

            option.ShouldBeHasValueWithValue(2);
        }
    }

    public class IReadOnlyDictionary
    {
        [Fact]
        public void HasNoValue()
        {
            var values = new ReadOnlyDictionary<int, int>(Enumerable.Range(0, 10)
                                                                    .ToDictionary(x => x, x => x));

            var option = values.TryGetValue(100);

            option.ShouldBeHasNoValue();
        }

        [Fact]
        public void HasValue()
        {
            var values = new ReadOnlyDictionary<int, int>(Enumerable.Range(0, 10)
                                                                    .ToDictionary(x => x, x => x));

            var option = values.TryGetValue(2);

            option.ShouldBeHasValueWithValue(2);
        }
    }
}