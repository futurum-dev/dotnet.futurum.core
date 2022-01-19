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

        public string GetErrorString() =>
            _message;

        public ResultErrorStructure GetErrorStructure() =>
            new(_message, Enumerable.Empty<ResultErrorStructure>());
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
    public void ToErrorStructure()
    {
        var errorMessage = Guid.NewGuid().ToString();

        var resultError = new TestResultError(errorMessage);

        var errorStructure = resultError.ToErrorStructure();

        errorStructure.Message.Should().Be(errorMessage);
        errorStructure.Children.Should().BeEmpty();
    }
}