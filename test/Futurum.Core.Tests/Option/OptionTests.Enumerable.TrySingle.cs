using System.Linq;

using Futurum.Core.Option;
using Futurum.Core.Tests.Helper.Option;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Option;

public class OptionEnumerableExtensionsTrySingleTests
{
    public class WithoutPredicate
    {
        [Fact]
        public void HasNoValue()
        {
            var values = Enumerable.Empty<int>()
                                   .ToList();

            var resultOption = values.TrySingle();

            resultOption.ShouldBeSuccess();
            resultOption.Value.Value.ShouldBeHasNoValue();
        }

        [Fact]
        public void HasValue()
        {
            var values = Enumerable.Range(0, 1)
                                   .ToList();

            var resultOption = values.TrySingle();

            resultOption.ShouldBeSuccess();
            resultOption.Value.ShouldBeHasValueWithValue(0);
        }

        [Fact]
        public void DuplicateValues()
        {
            var values = Enumerable.Repeat(0, 2)
                                   .ToList();

            var resultOption = values.TrySingle();

            resultOption.ShouldBeFailureWithErrorContaining($"Failed {nameof(Enumerable.SingleOrDefault)} for type : '{typeof(int).FullName}'");
        }
    }

    public class WithPredicate
    {
        [Fact]
        public void HasNoValue()
        {
            var values = Enumerable.Empty<int>()
                                   .ToList();

            var resultOption = values.TrySingle(x => x == 1);

            resultOption.ShouldBeSuccess();
            resultOption.Value.Value.ShouldBeHasNoValue();
        }

        [Fact]
        public void HasValue()
        {
            var values = Enumerable.Range(1, 10)
                                   .ToList();

            var resultOption = values.TrySingle(x => x == 1);

            resultOption.ShouldBeSuccess();
            resultOption.Value.ShouldBeHasValueWithValue(1);
        }

        [Fact]
        public void DuplicateValues()
        {
            var values = Enumerable.Repeat(0, 2)
                                   .ToList();

            var resultOption = values.TrySingle(x => x == 0);

            resultOption.ShouldBeFailureWithErrorContaining($"Failed {nameof(Enumerable.SingleOrDefault)} for type : '{typeof(int).FullName}'");
        }
    }
}