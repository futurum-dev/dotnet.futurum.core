using System;
using System.Linq;

using FluentAssertions;

using Futurum.Core.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultErrorExceptionExtensionsTests
{
    public class ToResultError
    {
        public class ExceptionAndMessage
        {
            [Fact]
            public void ExceptionWithErrorMessage()
            {
                var exceptionMessage = Guid.NewGuid().ToString();
                var errorMessage = Guid.NewGuid().ToString();

                var exception = new Exception(exceptionMessage);

                var exceptionResultError = exception.ToResultError(errorMessage);

                exceptionResultError.Should().BeOfType<ResultErrorComposite>();

                var resultError = exceptionResultError as ResultErrorComposite;

                var resultErrorMessage = (ResultErrorMessage) resultError.Parent.Value;
                resultErrorMessage.Message.Should().Be(errorMessage);

                var resultErrorException = (ResultErrorException) resultError.Children.First();
                resultErrorException.Exception.Should().Be(exception);
            }

            [Fact]
            public void ExceptionWithNullErrorMessage()
            {
                var exceptionMessage = Guid.NewGuid().ToString();
                string errorMessage = null;

                var exception = new Exception(exceptionMessage);

                var resultError = exception.ToResultError(errorMessage);

                resultError.Should().BeOfType<ResultErrorException>();
                ((ResultErrorException) resultError).Exception.Should().Be(exception);
            }

            [Fact]
            public void ExceptionWithStringEmptyErrorMessage()
            {
                var exceptionMessage = Guid.NewGuid().ToString();
                var errorMessage = string.Empty;

                var exception = new Exception(exceptionMessage);

                var resultError = exception.ToResultError(errorMessage);

                resultError.Should().BeOfType<ResultErrorException>();
                ((ResultErrorException) resultError).Exception.Should().Be(exception);
            }

            [Fact]
            public void Null()
            {
                var errorMessage = Guid.NewGuid().ToString();

                Exception exception = null;

                var exceptionResultError = exception.ToResultError(errorMessage);

                exceptionResultError.Should().BeOfType<ResultErrorComposite>();

                var resultError = exceptionResultError as ResultErrorComposite;

                var resultErrorMessage = (ResultErrorMessage) resultError.Parent.Value;
                resultErrorMessage.Message.Should().Be(errorMessage);

                resultError.Children.First().Should().BeOfType<ResultErrorEmpty>();
            }
        }

        public class ExceptionOnly
        {
            [Fact]
            public void NotNull()
            {
                var exceptionMessage = Guid.NewGuid().ToString();

                var exception = new Exception(exceptionMessage);

                var resultError = exception.ToResultError();

                resultError.Should().BeOfType<ResultErrorException>();
                ((ResultErrorException) resultError).Exception.Should().Be(exception);
            }
                
            [Fact]
            public void Null()
            {
                Exception exception = null;

                var resultError = exception.ToResultError();

                resultError.Should().BeOfType<ResultErrorEmpty>();
            }
        }
    }
}