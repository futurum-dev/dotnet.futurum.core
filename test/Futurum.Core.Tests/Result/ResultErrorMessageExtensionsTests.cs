using System;

using FluentAssertions;

using Futurum.Core.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultErrorMessageExtensionsTests
{
    public class ToResultError
    {
        [Fact]
        public void HasValue()
        {
            var errorMessage = Guid.NewGuid().ToString();

            var resultError = errorMessage.ToResultError();

            resultError.Should().BeOfType<ResultErrorMessage>();

            var resultErrorMessage = resultError as ResultErrorMessage;

            resultErrorMessage.Message.Should().Be(errorMessage);
        }

        [Fact]
        public void Null()
        {
            string errorMessage = null;

            var resultError = errorMessage.ToResultError();

            resultError.Should().BeOfType<ResultErrorEmpty>();
        }

        [Fact]
        public void StringEmpty()
        {
            var errorMessage = string.Empty;

            var resultError = errorMessage.ToResultError();

            resultError.Should().BeOfType<ResultErrorEmpty>();
        }
    }
}