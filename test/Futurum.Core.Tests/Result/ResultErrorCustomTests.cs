using System;
using System.Linq;

using FluentAssertions;

using Futurum.Core.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultErrorCustomTests
{
    private class TestResultError : IResultErrorNonComposite
    {
        private readonly string _message;

        public TestResultError(string message)
        {
            _message = message;
        }

        public string GetErrorStringSafe() =>
            _message;

        public string GetErrorString() =>
            GetErrorStringSafe();

        public ResultErrorStructure GetErrorStructureSafe() =>
            new(_message, Enumerable.Empty<ResultErrorStructure>());

        public ResultErrorStructure GetErrorStructure() =>
            GetErrorStructureSafe();
    }

    [Fact]
    public void ToErrorStringSafe()
    {
        var errorMessage = Guid.NewGuid().ToString();

        var resultError = new TestResultError(errorMessage);

        var errorString = resultError.ToErrorStringSafe(",");

        errorString.Should().Be(errorMessage);
    }

    [Fact]
    public void ToErrorString()
    {
        var errorMessage = Guid.NewGuid().ToString();

        var resultError = new TestResultError(errorMessage);

        var errorString = resultError.ToErrorString(",");

        errorString.Should().Be(errorMessage);
    }

    [Fact]
    public void ToErrorStructureSafe()
    {
        var errorMessage = Guid.NewGuid().ToString();

        var resultError = new TestResultError(errorMessage);

        var errorStructure = resultError.ToErrorStructureSafe();

        errorStructure.Message.Should().Be(errorMessage);
        errorStructure.Children.Should().BeEmpty();
    }

    [Fact]
    public void ToErrorStructure()
    {
        var errorMessage = Guid.NewGuid().ToString();

        var resultError = new TestResultError(errorMessage);

        var errorStructure = resultError.ToErrorStructure();

        errorStructure.Message.Should().Be(errorMessage);
        errorStructure.Children.Should().BeEmpty();
    }
}