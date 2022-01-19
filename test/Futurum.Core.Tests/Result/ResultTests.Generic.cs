using System;

using FluentAssertions;

using Futurum.Core.Result;
using Futurum.Core.Tests.Helper.Option;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultGenericTests
{
    private const string ErrorMessage = "ERROR_MESSAGE1";

    public class Behaviour
    {
        public class when_creating_Success
        {
            [Fact]
            public void with_null_Value_then_throws_exception()
            {
                Action action = () => Core.Result.Result.Ok((StubClass) null);

                action.Should().Throw<ArgumentNullException>();
            }
        }

        public class when_creating_Fail
        {
            [Fact]
            public void with_empty_error_message_then_throws_exception()
            {
                var errorMessage = string.Empty;

                Action action = () => Core.Result.Result.Fail<StubClass>(errorMessage);

                action.Should().Throw<ArgumentNullException>();
            }

            [Fact]
            public void with_null_error_message_then_throws_exception()
            {
                string errorMessage = null;

                Action action = () => Core.Result.Result.Fail<StubClass>(errorMessage);

                action.Should().Throw<ArgumentNullException>();
            }

            [Fact]
            public void with_null_error_then_throws_exception()
            {
                Action action = () => Core.Result.Result.Fail<StubClass>((IResultError) null);

                action.Should().Throw<ArgumentNullException>();
            }

            [Fact]
            public void with_ResultErrorEmpty_error_then_throws_exception()
            {
                Action action = () => Core.Result.Result.Fail<StubClass>(ResultErrorEmpty.Value);

                action.Should().Throw<ArgumentNullException>();
            }
        }

        public class when_Success
        {
            [Fact]
            public void then_Error_HasNoValue_is_true()
            {
                var result = Core.Result.Result.Ok(new StubClass());

                result.Error.ShouldBeHasNoValue();
            }

            [Fact]
            public void then_IsFailure_is_false()
            {
                var payload = new StubClass();

                var result = Core.Result.Result.Ok(payload);

                result.IsFailure.Should().BeFalse();
            }

            [Fact]
            public void then_IsSuccess_is_true()
            {
                var payload = new StubClass();

                var result = Core.Result.Result.Ok(payload);

                result.ShouldBeSuccess();
            }

            [Fact]
            public void then_Value_returns_the_passed_in_payload()
            {
                var payload = new StubClass();

                var result = Core.Result.Result.Ok(payload);

                result.ShouldBeSuccessWithValue(payload);
            }
        }

        public class when_Fail
        {
            [Fact]
            public void then_accessing_HasNoValue_should_be_true()
            {
                var result = Core.Result.Result.Fail<StubClass>(ErrorMessage);

                result.Value.ShouldBeHasNoValue();
            }

            [Fact]
            public void then_Error_returns_the_passed_in_error()
            {
                var result = Core.Result.Result.Fail<StubClass>(ErrorMessage.ToResultError());

                result.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public void then_Error_returns_the_passed_in_error_message()
            {
                var result = Core.Result.Result.Fail<StubClass>(ErrorMessage);

                result.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public void then_IsFailure_is_true()
            {
                var result = Core.Result.Result.Fail<StubClass>(ErrorMessage);

                result.ShouldBeFailure();
            }

            [Fact]
            public void then_IsSuccess_is_false()
            {
                var result = Core.Result.Result.Fail<StubClass>(ErrorMessage);

                result.IsSuccess.Should().BeFalse();
            }
        }
    }

    public class ImplicitConversion
    {
        [Fact]
        public void Success()
        {
            var resultInput = Core.Result.Result.Ok(Guid.NewGuid().ToString());

            Core.Result.Result returnedResult = resultInput;

            returnedResult.ShouldBeSuccess();
        }
    }

    private class StubClass
    {
    }

    [Fact]
    public void DefaultInitializer()
    {
        var result = default(Result<int>);

        result.ShouldBeFailure();
    }
}