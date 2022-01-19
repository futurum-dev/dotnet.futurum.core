using System;
using System.Linq;

using FluentAssertions;

using Futurum.Core.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultErrorStructureExtensionsTests
{
    public class ToErrorStructure
    {
        private class TestResultError : IResultErrorNonComposite
        {
            private readonly string _message;

            public TestResultError(string message)
            {
                _message = message;
            }

            public string GetErrorString() =>
                throw new InvalidOperationException($"'{nameof(GetErrorString)}' method should not be called here.");

            public ResultErrorStructure GetErrorStructure() =>
                _message.ToResultErrorStructure();
        }

        [Fact]
        public void when_not_Null_return_ResultError_GetErrorStructure()
        {
            var errorMessage = Guid.NewGuid().ToString();

            IResultError resultError = new TestResultError(errorMessage);

            var errorStructure = resultError.ToErrorStructure();

            errorStructure.Message.Should().Be(errorMessage);
            errorStructure.Children.Should().BeEmpty();
        }

        [Fact]
        public void when_Null_return_empty_string()
        {
            IResultError resultError = null;

            var errorStructure = resultError.ToErrorStructure();

            errorStructure.Message.Should().BeEmpty();
            errorStructure.Children.Should().BeEmpty();
        }
    }

    [Fact]
    public void CreateEmptyResultErrorStructure()
    {
        var resultErrorStructure = ResultErrorStructureExtensions.CreateEmptyResultErrorStructure();

        resultErrorStructure.Message.Should().Be(string.Empty);
        resultErrorStructure.Children.Should().BeEmpty();
    }

    public class ToResultErrorStructure
    {
        [Fact]
        public void only_message()
        {
            var errorMessage = Guid.NewGuid().ToString();
            
            var resultErrorStructure = ResultErrorStructureExtensions.ToResultErrorStructure(errorMessage);

            resultErrorStructure.Message.Should().Be(errorMessage);
            resultErrorStructure.Children.Should().BeEmpty();
        }
        
        [Fact]
        public void only_children()
        {
            var children = Enumerable.Range(0, 10)
                                         .Select(x => x.ToString().ToResultError().ToErrorStructure());
            
            var resultErrorStructure = ResultErrorStructureExtensions.ToResultErrorStructure(children);

            resultErrorStructure.Message.Should().Be(string.Empty);
            resultErrorStructure.Children.Should().BeSameAs(children);
        }
        
        [Fact]
        public void message_and_children()
        {
            var errorMessage = Guid.NewGuid().ToString();

            var children = Enumerable.Range(0, 10)
                                         .Select(x => x.ToString().ToResultError().ToErrorStructure());
            
            var resultErrorStructure = ResultErrorStructureExtensions.ToResultErrorStructure(errorMessage, children);

            resultErrorStructure.Message.Should().Be(errorMessage);
            resultErrorStructure.Children.Should().BeSameAs(children);
        }
    }
}