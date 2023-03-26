using System;
using System.Linq;

using FluentAssertions;

using Futurum.Core.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultErrorCompositeTests
{
    public class with_Parent
    {
        [Fact]
        public void ToErrorStringSafe()
        {
            var exceptionErrorMessage = Guid.NewGuid().ToString();

            var resultErrorException = ThrowException(exceptionErrorMessage).ToResultError();

            var errorMessage = Guid.NewGuid().ToString();

            var resultErrorMessage = errorMessage.ToResultError();

            var resultError = ResultErrorCompositeExtensions.ToResultError(resultErrorMessage, resultErrorException);

            var errorString = resultError.ToErrorStringSafe(",");

            errorString.Should().Be($"{errorMessage},{exceptionErrorMessage}");
        }

        [Fact]
        public void ToErrorString()
        {
            var exceptionErrorMessage = Guid.NewGuid().ToString();

            var resultErrorException = ThrowException(exceptionErrorMessage).ToResultError();

            var errorMessage = Guid.NewGuid().ToString();

            var resultErrorMessage = errorMessage.ToResultError();

            var resultError = ResultErrorCompositeExtensions.ToResultError(resultErrorMessage, resultErrorException);

            var errorString = resultError.ToErrorString(",");

            var expectedExceptionErrorMessage = @$"System.Exception: {exceptionErrorMessage}
   at Futurum.Core.Tests.Result.ResultErrorCompositeTests.ThrowException(String errorMessage) in";
            errorString.Should().StartWith($"{errorMessage},{expectedExceptionErrorMessage}");
            errorString.Should().EndWith(@$"test/Futurum.Core.Tests/Result/ResultErrorCompositeTests.cs:line 192");
        }

        [Fact]
        public void ToErrorStructureSafe()
        {
            var exceptionErrorMessage = Guid.NewGuid().ToString();

            var resultErrorException = ThrowException(exceptionErrorMessage).ToResultError();

            var errorMessage = Guid.NewGuid().ToString();

            var resultErrorMessage = errorMessage.ToResultError();

            var resultError = ResultErrorCompositeExtensions.ToResultError(resultErrorMessage, resultErrorException);

            var errorStructure = resultError.ToErrorStructureSafe();

            var parentErrorMessage = errorStructure.Message;
            parentErrorMessage.Should().Be(errorMessage);

            var childErrors = errorStructure.Children.ToList();
            childErrors.Count.Should().Be(1);

            childErrors[0].Message.Should().Be(exceptionErrorMessage);
            childErrors[0].Children.Should().BeEmpty();
        }

        [Fact]
        public void ToErrorStructure()
        {
            var exceptionErrorMessage = Guid.NewGuid().ToString();

            var resultErrorException = ThrowException(exceptionErrorMessage).ToResultError();

            var errorMessage = Guid.NewGuid().ToString();

            var resultErrorMessage = errorMessage.ToResultError();

            var resultError = ResultErrorCompositeExtensions.ToResultError(resultErrorMessage, resultErrorException);

            var errorStructure = resultError.ToErrorStructure();

            var parentErrorMessage = errorStructure.Message;
            parentErrorMessage.Should().Be(errorMessage);

            var childErrors = errorStructure.Children.ToList();
            childErrors.Count.Should().Be(1);

            var expectedExceptionErrorMessage = @$"System.Exception: {exceptionErrorMessage}
   at Futurum.Core.Tests.Result.ResultErrorCompositeTests.ThrowException(String errorMessage) in";
            childErrors[0].Message.Should().StartWith(expectedExceptionErrorMessage);
            childErrors[0].Message.Should().EndWith($@"test/Futurum.Core.Tests/Result/ResultErrorCompositeTests.cs:line 192");
            childErrors[0].Children.Should().BeEmpty();
        }
    }

    public class without_Parent
    {
        [Fact]
        public void ToErrorStringSafe()
        {
            var exceptionErrorMessage = Guid.NewGuid().ToString();

            var resultErrorException = ThrowException(exceptionErrorMessage).ToResultError();

            var resultError = ResultErrorCompositeExtensions.ToResultError(new []{resultErrorException});

            var errorString = resultError.ToErrorStringSafe(",");

            errorString.Should().Be(exceptionErrorMessage);
        }

        [Fact]
        public void ToErrorString()
        {
            var exceptionErrorMessage = Guid.NewGuid().ToString();

            var resultErrorException = ThrowException(exceptionErrorMessage).ToResultError();

            var resultError = ResultErrorCompositeExtensions.ToResultError(new []{resultErrorException});

            var errorString = resultError.ToErrorString(",");

            var expectedExceptionErrorMessage = @$"System.Exception: {exceptionErrorMessage}
   at Futurum.Core.Tests.Result.ResultErrorCompositeTests.ThrowException(String errorMessage) in";
            errorString.Should().StartWith(expectedExceptionErrorMessage);
            errorString.Should().EndWith(@$"test/Futurum.Core.Tests/Result/ResultErrorCompositeTests.cs:line 192");
        }

        [Fact]
        public void ToErrorStructureSafe()
        {
            var exceptionErrorMessage = Guid.NewGuid().ToString();

            var resultErrorException = ThrowException(exceptionErrorMessage).ToResultError();

            var resultError = ResultErrorCompositeExtensions.ToResultError(new []{resultErrorException});

            var errorStructure = resultError.ToErrorStructureSafe();

            var parentErrorMessage = errorStructure.Message;
            parentErrorMessage.Should().BeEmpty();

            var childErrors = errorStructure.Children.ToList();
            childErrors.Count.Should().Be(1);

            childErrors[0].Message.Should().Be(exceptionErrorMessage);
            childErrors[0].Children.Should().BeEmpty();
        }

        [Fact]
        public void ToErrorStructure()
        {
            var exceptionErrorMessage = Guid.NewGuid().ToString();

            var resultErrorException = ThrowException(exceptionErrorMessage).ToResultError();

            var resultError = ResultErrorCompositeExtensions.ToResultError(new []{resultErrorException});

            var errorStructure = resultError.ToErrorStructure();

            var parentErrorMessage = errorStructure.Message;
            parentErrorMessage.Should().BeEmpty();

            var childErrors = errorStructure.Children.ToList();
            childErrors.Count.Should().Be(1);

            var expectedExceptionErrorMessage = @$"System.Exception: {exceptionErrorMessage}
   at Futurum.Core.Tests.Result.ResultErrorCompositeTests.ThrowException(String errorMessage) in";
            childErrors[0].Message.Should().StartWith(expectedExceptionErrorMessage);
            childErrors[0].Message.Should().EndWith($@"test/Futurum.Core.Tests/Result/ResultErrorCompositeTests.cs:line 192");
            childErrors[0].Children.Should().BeEmpty();
        }
    }

    private static Exception ThrowException(string? errorMessage)
    {
        try
        {
            throw new Exception(errorMessage);
        }
        catch (Exception exception)
        {
            return exception;
        }
    }
}