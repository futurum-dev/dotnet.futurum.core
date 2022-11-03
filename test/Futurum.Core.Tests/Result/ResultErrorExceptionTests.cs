using System;

using FluentAssertions;

using Futurum.Core.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultErrorExceptionTests
{
    [Fact]
    public void ToErrorStringSafe()
    {
        var errorMessage = Guid.NewGuid().ToString();

        var exception = ThrowException(errorMessage);

        var resultError = exception.ToResultError();

        var errorString = resultError.ToErrorStringSafe(",");

        errorString.Should().Be(errorMessage);
    }

    [Fact]
    public void ToErrorString()
    {
        var errorMessage = Guid.NewGuid().ToString();

        var exception = ThrowException(errorMessage);

        var resultError = exception.ToResultError();

        var errorString = resultError.ToErrorString(",");

        errorString.Should().StartWith(@$"System.Exception: {errorMessage}
   at Futurum.Core.Tests.Result.ResultErrorExceptionTests.ThrowException(String errorMessage) in");
        errorString.Should().EndWith(@$"test/Futurum.Core.Tests/Result/ResultErrorExceptionTests.cs:line 79");
    }

    [Fact]
    public void ToErrorStructureSafe()
    {
        var errorMessage = Guid.NewGuid().ToString();

        var exception = ThrowException(errorMessage);

        var resultError = exception.ToResultError();

        var errorStructure = resultError.ToErrorStructureSafe();

        errorStructure.Message.Should().Be(errorMessage);
        errorStructure.Children.Should().BeEmpty();
    }

    [Fact]
    public void ToErrorStructure()
    {
        var errorMessage = Guid.NewGuid().ToString();

        var exception = ThrowException(errorMessage);

        var resultError = exception.ToResultError();

        var errorStructure = resultError.ToErrorStructure();
       
        errorStructure.Message.Should().StartWith(@$"System.Exception: {errorMessage}
   at Futurum.Core.Tests.Result.ResultErrorExceptionTests.ThrowException(String errorMessage) in");
        errorStructure.Message.Should().EndWith(@$"test/Futurum.Core.Tests/Result/ResultErrorExceptionTests.cs:line 79");
        errorStructure.Children.Should().BeEmpty();
    }

    private Exception ThrowException(string? errorMessage)
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