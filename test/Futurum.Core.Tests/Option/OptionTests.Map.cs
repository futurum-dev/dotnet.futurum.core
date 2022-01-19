using System;

using Futurum.Core.Option;
using Futurum.Core.Tests.Helper.Option;

using Xunit;

namespace Futurum.Core.Tests.Option;

public class OptionMapTests
{
    public class FuncReturnsNonOption
    {
        [Fact]
        public void HasNoValue()
        {
            string value = null;
            var option = value.ToOption();

            var returnOption = option.Map(x => x.ToString());

            returnOption.ShouldBeHasNoValue();
        }

        [Fact]
        public void HasValue()
        {
            var value = Guid.NewGuid();
            var option = value.ToOption();

            var returnOption = option.Map(x => x.ToString());

            returnOption.ShouldBeHasValueWithValue(value.ToString());
        }
    }

    public class FuncReturnsOption
    {
        [Fact]
        public void HasNoValue()
        {
            string value = null;
            var option = value.ToOption();

            var returnOption = option.Map(x => x.ToString().ToOption());

            returnOption.ShouldBeHasNoValue();
        }

        [Fact]
        public void HasValue()
        {
            var value = Guid.NewGuid();
            var option = value.ToOption();

            var returnOption = option.Map(x => x.ToString().ToOption());

            returnOption.ShouldBeHasValueWithValue(value.ToString());
        }
    }
}