using System;

using FluentAssertions;

using Futurum.Core.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultErrorKeyNotFoundTests
{
    [Fact]
    public void ToErrorString()
    {
        var key = Guid.NewGuid().ToString();
        var sourceDescription = Guid.NewGuid().ToString();

        var resultError = ResultErrorKeyNotFound.Create(key, sourceDescription);

        var errorString = resultError.ToErrorString(",");

        errorString.Should().Be($"Unable to find key : '{key}' in source : '{sourceDescription}'");
    }

    [Fact]
    public void ToErrorStructure()
    {
        var key = Guid.NewGuid().ToString();
        var sourceDescription = Guid.NewGuid().ToString();

        var resultError = ResultErrorKeyNotFound.Create(key, sourceDescription);

        var errorStructure = resultError.ToErrorStructure();

        errorStructure.Message.Should().Be($"Unable to find key : '{key}' in source : '{sourceDescription}'");
        errorStructure.Children.Should().BeEmpty();
    }
}