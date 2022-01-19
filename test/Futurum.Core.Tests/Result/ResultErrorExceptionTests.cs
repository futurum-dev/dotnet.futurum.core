using System;

using FluentAssertions;

using Futurum.Core.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultErrorExceptionTests
{
    [Fact]
    public void ToErrorString()
    {
        var errorMessage = Guid.NewGuid().ToString();

        var exception = new Exception(errorMessage);

        var resultError = exception.ToResultError();

        var errorString = resultError.ToErrorString(",");

        errorString.Should().Be(errorMessage);
    }

    [Fact]
    public void ToErrorStructure()
    {
        var errorMessage = Guid.NewGuid().ToString();

        var exception = new Exception(errorMessage);

        var resultError = exception.ToResultError();

        var errorStructure = resultError.ToErrorStructure();

        errorStructure.Message.Should().Be(errorMessage);
        errorStructure.Children.Should().BeEmpty();
    }
}