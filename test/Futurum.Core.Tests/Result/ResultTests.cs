using System;

using FluentAssertions;

using Futurum.Core.Result;
using Futurum.Core.Tests.Helper.Option;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultTests
{
    private const string ErrorMessage = "ERROR_MESSAGE1";

    public class Behaviour
    {
        public class when_creating_Fail
        {
            [Fact]
            public void with_empty_error_message_then_throws_exception()
            {
                var errorMessage = string.Empty;

                Action action = () => Core.Result.Result.Fail(errorMessage);

                action.Should().Throw<ArgumentNullException>();
            }

            [Fact]
            public void with_null_error_message_then_throws_exception()
            {
                string errorMessage = null;

                Action action = () => Core.Result.Result.Fail(errorMessage);

                action.Should().Throw<ArgumentNullException>();
            }

            [Fact]
            public void with_null_error_then_throws_exception()
            {
                Action action = () => Core.Result.Result.Fail((IResultError) null);

                action.Should().Throw<ArgumentNullException>();
            }

            [Fact]
            public void with_ResultErrorEmpty_error_then_throws_exception()
            {
                Action action = () => Core.Result.Result.Fail(ResultErrorEmpty.Value);

                action.Should().Throw<ArgumentNullException>();
            }
        }

        public class when_Success
        {
            [Fact]
            public void then_Error_HasNoValue_is_true()
            {
                var result = Core.Result.Result.Ok();

                result.Error.ShouldBeHasNoValue();
            }

            [Fact]
            public void then_IsFailure_is_false()
            {
                var result = Core.Result.Result.Ok();

                result.IsFailure.Should().BeFalse();
            }

            [Fact]
            public void then_IsSuccess_is_true()
            {
                var result = Core.Result.Result.Ok();

                result.ShouldBeSuccess();
            }
        }

        public class when_Fail
        {
            [Fact]
            public void then_Error_returns_the_passed_in_error()
            {
                var result = Core.Result.Result.Fail(ErrorMessage.ToResultError());

                result.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public void then_Error_returns_the_passed_in_error_message()
            {
                var result = Core.Result.Result.Fail(ErrorMessage);

                result.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public void then_IsFailure_is_true()
            {
                var result = Core.Result.Result.Fail(ErrorMessage);

                result.ShouldBeFailure();
            }

            [Fact]
            public void then_IsSuccess_is_false()
            {
                var result = Core.Result.Result.Fail(ErrorMessage);

                result.IsSuccess.Should().BeFalse();
            }
        }
    }

    [Fact]
    public void DefaultInitializer()
    {
        var result = default(Core.Result.Result);

        result.ShouldBeFailure();
    }
}