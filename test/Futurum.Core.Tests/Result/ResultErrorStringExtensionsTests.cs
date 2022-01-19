using System;
using System.Collections.Generic;

using FluentAssertions;

using Futurum.Core.Option;
using Futurum.Core.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultErrorStringExtensionsTests
{
    public class ToErrorString
    {
        private class TestNonCompositeResultError : IResultErrorNonComposite
        {
            private readonly string _message;

            public TestNonCompositeResultError(string message)
            {
                _message = message;
            }

            public string GetErrorString() =>
                _message;

            public ResultErrorStructure GetErrorStructure() =>
                throw new InvalidOperationException($"'{nameof(GetErrorStructure)}' method should not be called here.");
        }

        private class TestCompositeResultError : IResultErrorComposite
        {
            private readonly string _message;

            public TestCompositeResultError(string message)
            {
                _message = message;
            }

            public string GetErrorString(string seperator) =>
                _message;

            public ResultErrorStructure GetErrorStructure() =>
                throw new InvalidOperationException($"'{nameof(GetErrorStructure)}' method should not be called here.");

            public Option<IResultErrorNonComposite> Parent { get; }
            public IEnumerable<IResultError> Children { get; }
        }

        private class TestUnknownResultError : IResultError
        {
            public ResultErrorStructure GetErrorStructure() =>
                throw new InvalidOperationException($"'{nameof(GetErrorStructure)}' method should not be called here.");
        }

        [Fact]
        public void when_IResultErrorNonComposite_return_ResultError_GetErrorString()
        {
            var errorMessage = Guid.NewGuid().ToString();

            IResultError resultError = new TestNonCompositeResultError(errorMessage);

            var formattedErrorMessage = resultError.ToErrorString(",");

            formattedErrorMessage.Should().Be(errorMessage);
        }

        [Fact]
        public void when_IResultErrorComposite_return_ResultError_GetErrorString()
        {
            var errorMessage = Guid.NewGuid().ToString();

            IResultError resultError = new TestCompositeResultError(errorMessage);

            var formattedErrorMessage = resultError.ToErrorString(",");

            formattedErrorMessage.Should().Be(errorMessage);
        }

        [Fact]
        public void when_Unknown_IResultError_return_UnknownResultErrorOfType_GetErrorString()
        {
            IResultError resultError = new TestUnknownResultError();

            var formattedErrorMessage = resultError.ToErrorString(",");

            formattedErrorMessage.Should().StartWith($"Unknown ResultError of type {typeof(TestUnknownResultError).FullName}");
        }

        [Fact]
        public void when_Null_return_empty_string()
        {
            IResultError resultError = null;

            var formattedErrorMessage = resultError.ToErrorString(",");

            formattedErrorMessage.Should().Be(string.Empty);
        }
    }
}