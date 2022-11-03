using FluentAssertions;

using Futurum.Core.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultErrorEmptyTests
{
    [Fact]
    public void ToErrorStringSafe()
    {
        var errorMessage = string.Empty;

        var resultError = errorMessage.ToResultError();

        var errorString = resultError.ToErrorStringSafe(",");

        errorString.Should().Be(string.Empty);
    }

    [Fact]
    public void ToErrorString()
    {
        var errorMessage = string.Empty;

        var resultError = errorMessage.ToResultError();

        var errorString = resultError.ToErrorString(",");

        errorString.Should().Be(string.Empty);
    }

    [Fact]
    public void ToErrorStructureSafe()
    {
        var errorMessage = string.Empty;

        var resultError = errorMessage.ToResultError();

        var errorStructure = resultError.ToErrorStructureSafe();

        errorStructure.Message.Should().BeEmpty();
        errorStructure.Children.Should().BeEmpty();
    }

    [Fact]
    public void ToErrorStructure()
    {
        var errorMessage = string.Empty;

        var resultError = errorMessage.ToResultError();

        var errorStructure = resultError.ToErrorStructure();

        errorStructure.Message.Should().BeEmpty();
        errorStructure.Children.Should().BeEmpty();
    }
}