using System;
using System.Threading.Tasks;

using FluentAssertions;

using Futurum.Core.Option;
using Futurum.Core.Result;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Option;

public class ResultOptionExtensionsMapSwitchTests
{
    private const string ErrorMessage1 = "ERROR_MESSAGE_1";

    public class Sync
    {
        public class InputSuccess
        {
            [Fact]
            public void OptionHasNoValue()
            {
                var trueValue = Guid.NewGuid();
                var falseValue = Guid.NewGuid();

                var resultOption = Option<Guid>.None.ToResultOk();

                var returnValue = ResultOptionExtensions.MapSwitch(resultOption,
                                                                   value => trueValue,
                                                                   () => falseValue);

                returnValue.ShouldBeSuccessWithValue(falseValue);
            }

            [Fact]
            public void OptionHasValue()
            {
                var inputValue = Guid.NewGuid();

                var trueValue = Guid.NewGuid();
                var falseValue = Guid.NewGuid();

                var passedValue = Guid.Empty;

                var resultOption = inputValue.ToOption().ToResultOk();

                var returnValue = resultOption.MapSwitch(value =>
                                                         {
                                                             passedValue = value;

                                                             return trueValue;
                                                         },
                                                         () => falseValue);

                returnValue.ShouldBeSuccessWithValue(trueValue);
                passedValue.Should().Be(inputValue);
            }
        }

        [Fact]
        public void InputFailure()
        {
            var trueWasCalled = false;
            var falseWasCalled = false;

            var resultOption = Core.Result.Result.Fail<Option<Guid>>(ErrorMessage1);

            var returnValue = ResultOptionExtensions.MapSwitch(resultOption,
                                                               value =>
                                                               {
                                                                   trueWasCalled = true;

                                                                   return 0;
                                                               },
                                                               () =>
                                                               {
                                                                   falseWasCalled = true;

                                                                   return 0;
                                                               });

            returnValue.ShouldBeFailureWithError(ErrorMessage1);
            trueWasCalled.Should().BeFalse();
            falseWasCalled.Should().BeFalse();
        }
    }

    public class Async
    {
        public class InputSuccess
        {
            [Fact]
            public async Task OptionHasNoValue()
            {
                var trueValue = Guid.NewGuid();
                var falseValue = Guid.NewGuid();

                var resultOption = Option<Guid>.None.ToResultOkAsync();

                var returnValue = await resultOption.MapMatchAsync(value => trueValue,
                                                                   () => falseValue);

                returnValue.ShouldBeSuccessWithValue(falseValue);
            }

            [Fact]
            public async Task OptionHasValue()
            {
                var inputValue = Guid.NewGuid();

                var trueValue = Guid.NewGuid();
                var falseValue = Guid.NewGuid();

                var passedValue = Guid.Empty;

                var resultOption = inputValue.ToOption().ToResultOkAsync();

                var returnValue = await resultOption.MapMatchAsync(value =>
                                                                   {
                                                                       passedValue = value;

                                                                       return trueValue;
                                                                   },
                                                                   () => falseValue);

                returnValue.ShouldBeSuccessWithValue(trueValue);
                passedValue.Should().Be(inputValue);
            }
        }

        [Fact]
        public async Task InputFailure()
        {
            var trueWasCalled = false;
            var falseWasCalled = false;

            var resultOption = Core.Result.Result.FailAsync<Option<Guid>>(ErrorMessage1);

            var returnValue = await resultOption.MapMatchAsync(value =>
                                                               {
                                                                   trueWasCalled = true;

                                                                   return 0;
                                                               },
                                                               () =>
                                                               {
                                                                   falseWasCalled = true;

                                                                   return 0;
                                                               });

            returnValue.ShouldBeFailureWithError(ErrorMessage1);
            trueWasCalled.Should().BeFalse();
            falseWasCalled.Should().BeFalse();
        }
    }
}