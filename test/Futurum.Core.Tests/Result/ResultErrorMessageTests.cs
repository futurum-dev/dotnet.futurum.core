using System;

using FluentAssertions;

using Futurum.Core.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultErrorMessageTests
{
    [Fact]
    public void ToErrorString()
    {
        var errorMessage = Guid.NewGuid().ToString();

        var resultError = errorMessage.ToResultError();

        var errorString = resultError.ToErrorString(",");

        errorString.Should().Be(errorMessage);
    }

    [Fact]
    public void ToErrorStructure()
    {
        var errorMessage = Guid.NewGuid().ToString();

        var resultError = errorMessage.ToResultError();

        var errorStructure = resultError.ToErrorStructure();

        errorStructure.Message.Should().Be(errorMessage);
        errorStructure.Children.Should().BeEmpty();
    }
}