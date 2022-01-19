using System;

using Futurum.Core.Option;
using Futurum.Core.Tests.Helper.Option;

using Xunit;

namespace Futurum.Core.Tests.Option;

public class OptionOrElseTests
{
    public class ReturnNonOption
    {
        public class Value
        {
            [Fact]
            public void HasNoValue()
            {
                string inputValue = null;
                var orElseValue = Guid.NewGuid().ToString();

                var orElseOption = orElseValue;

                var inputOption = inputValue.ToOption();

                var returnedOption = inputOption.OrElse(orElseOption);

                returnedOption.ShouldBeHasValueWithValue(orElseValue);
            }

            [Fact]
            public void HasValue()
            {
                var inputValue = Guid.NewGuid().ToString();
                var orElseValue = Guid.NewGuid().ToString();

                var orElseOption = orElseValue;

                var inputOption = inputValue.ToOption();

                var returnedOption = inputOption.OrElse(orElseOption);

                returnedOption.ShouldBeHasValueWithValue(inputValue);
            }
        }

        public class Func
        {
            [Fact]
            public void HasNoValue()
            {
                string inputValue = null;
                var orElseValue = Guid.NewGuid().ToString();

                var orElseOption = orElseValue;

                var inputOption = inputValue.ToOption();

                var returnedOption = inputOption.OrElse(() => orElseOption);

                returnedOption.ShouldBeHasValueWithValue(orElseValue);
            }

            [Fact]
            public void HasValue()
            {
                var inputValue = Guid.NewGuid().ToString();
                var orElseValue = Guid.NewGuid().ToString();

                var orElseOption = orElseValue;

                var inputOption = inputValue.ToOption();

                var returnedOption = inputOption.OrElse(() => orElseOption);

                returnedOption.ShouldBeHasValueWithValue(inputValue);
            }
        }
    }

    public class ReturnOption
    {
        public class Value
        {
            [Fact]
            public void HasNoValue()
            {
                string inputValue = null;
                var orElseValue = Guid.NewGuid().ToString();

                var orElseOption = orElseValue.ToOption();

                var inputOption = inputValue.ToOption();

                var returnedOption = inputOption.OrElse(orElseOption);

                returnedOption.ShouldBeHasValueWithValue(orElseValue);
            }

            [Fact]
            public void HasValue()
            {
                var inputValue = Guid.NewGuid().ToString();
                var orElseValue = Guid.NewGuid().ToString();

                var orElseOption = orElseValue.ToOption();

                var inputOption = inputValue.ToOption();

                var returnedOption = inputOption.OrElse(orElseOption);

                returnedOption.ShouldBeHasValueWithValue(inputValue);
            }
        }

        public class Func
        {
            [Fact]
            public void HasNoValue()
            {
                string inputValue = null;
                var orElseValue = Guid.NewGuid().ToString();

                var orElseOption = orElseValue.ToOption();

                var inputOption = inputValue.ToOption();

                var returnedOption = inputOption.OrElse(() => orElseOption);

                returnedOption.ShouldBeHasValueWithValue(orElseValue);
            }

            [Fact]
            public void HasValue()
            {
                var inputValue = Guid.NewGuid().ToString();
                var orElseValue = Guid.NewGuid().ToString();

                var orElseOption = orElseValue.ToOption();

                var inputOption = inputValue.ToOption();

                var returnedOption = inputOption.OrElse(() => orElseOption);

                returnedOption.ShouldBeHasValueWithValue(inputValue);
            }
        }
    }
}