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

            public string GetErrorStringSafe() =>
                _message;

            public string GetErrorString() =>
                GetErrorStringSafe();

            public ResultErrorStructure GetErrorStructureSafe() =>
                throw new InvalidOperationException($"'{nameof(GetErrorStructureSafe)}' method should not be called here.");

            public ResultErrorStructure GetErrorStructure() =>
                GetErrorStructureSafe();
        }

        private class TestCompositeResultError : IResultErrorComposite
        {
            private readonly string _message;

            public TestCompositeResultError(string message)
            {
                _message = message;
            }

            public string GetErrorStringSafe(string seperator) =>
                _message;

            public string GetErrorString(string seperator) =>
                GetErrorStringSafe(seperator);

            public ResultErrorStructure GetErrorStructureSafe() =>
                throw new InvalidOperationException($"'{nameof(GetErrorStructureSafe)}' method should not be called here.");

            public ResultErrorStructure GetErrorStructure() =>
                GetErrorStructureSafe();

            public Option<IResultErrorNonComposite> Parent { get; }
            public IEnumerable<IResultError> Children { get; }
        }

        private class TestUnknownResultError : IResultError
        {
            public ResultErrorStructure GetErrorStructureSafe() =>
                throw new InvalidOperationException($"'{nameof(GetErrorStructureSafe)}' method should not be called here.");

            public ResultErrorStructure GetErrorStructure() =>
                GetErrorStructureSafe();
        }

        [Fact]
        public void when_IResultErrorNonComposite_return_ResultError_ToErrorStringSafe_Without_Seperator()
        {
            var errorMessage = Guid.NewGuid().ToString();

            IResultError resultError = new TestNonCompositeResultError(errorMessage);

            var formattedErrorMessage = resultError.ToErrorStringSafe();

            formattedErrorMessage.Should().Be(errorMessage);
        }
        
        [Fact]
        public void when_IResultErrorNonComposite_return_ResultError_ToErrorStringSafe()
        {
            var errorMessage = Guid.NewGuid().ToString();

            IResultError resultError = new TestNonCompositeResultError(errorMessage);

            var formattedErrorMessage = resultError.ToErrorStringSafe(",");

            formattedErrorMessage.Should().Be(errorMessage);
        }

        [Fact]
        public void when_IResultErrorNonComposite_return_ResultError_ToErrorString_Without_Seperator()
        {
            var errorMessage = Guid.NewGuid().ToString();

            IResultError resultError = new TestNonCompositeResultError(errorMessage);

            var formattedErrorMessage = resultError.ToErrorString();

            formattedErrorMessage.Should().Be(errorMessage);
        }

        [Fact]
        public void when_IResultErrorNonComposite_return_ResultError_ToErrorString()
        {
            var errorMessage = Guid.NewGuid().ToString();

            IResultError resultError = new TestNonCompositeResultError(errorMessage);

            var formattedErrorMessage = resultError.ToErrorString(",");

            formattedErrorMessage.Should().Be(errorMessage);
        }

        [Fact]
        public void when_IResultErrorComposite_return_ResultError_ToErrorStringSafe_Without_Seperator()
        {
            var errorMessage = Guid.NewGuid().ToString();

            IResultError resultError = new TestCompositeResultError(errorMessage);

            var formattedErrorMessage = resultError.ToErrorStringSafe();

            formattedErrorMessage.Should().Be(errorMessage);
        }

        [Fact]
        public void when_IResultErrorComposite_return_ResultError_ToErrorStringSafe()
        {
            var errorMessage = Guid.NewGuid().ToString();

            IResultError resultError = new TestCompositeResultError(errorMessage);

            var formattedErrorMessage = resultError.ToErrorStringSafe(",");

            formattedErrorMessage.Should().Be(errorMessage);
        }

        [Fact]
        public void when_IResultErrorComposite_return_ResultError_ToErrorString_Without_Seperator()
        {
            var errorMessage = Guid.NewGuid().ToString();

            IResultError resultError = new TestCompositeResultError(errorMessage);

            var formattedErrorMessage = resultError.ToErrorString();

            formattedErrorMessage.Should().Be(errorMessage);
        }

        [Fact]
        public void when_IResultErrorComposite_return_ResultError_ToErrorString()
        {
            var errorMessage = Guid.NewGuid().ToString();

            IResultError resultError = new TestCompositeResultError(errorMessage);

            var formattedErrorMessage = resultError.ToErrorString(",");

            formattedErrorMessage.Should().Be(errorMessage);
        }

        [Fact]
        public void when_Unknown_IResultError_return_UnknownResultErrorOfType_ToErrorStringSafe_Without_Seperator()
        {
            IResultError resultError = new TestUnknownResultError();

            var formattedErrorMessage = resultError.ToErrorStringSafe();

            formattedErrorMessage.Should().StartWith($"Unknown ResultError of type {typeof(TestUnknownResultError).FullName}");
        }

        [Fact]
        public void when_Unknown_IResultError_return_UnknownResultErrorOfType_ToErrorStringSafe()
        {
            IResultError resultError = new TestUnknownResultError();

            var formattedErrorMessage = resultError.ToErrorStringSafe(",");

            formattedErrorMessage.Should().StartWith($"Unknown ResultError of type {typeof(TestUnknownResultError).FullName}");
        }

        [Fact]
        public void when_Unknown_IResultError_return_UnknownResultErrorOfType_ToErrorString_Without_Seperator()
        {
            IResultError resultError = new TestUnknownResultError();

            var formattedErrorMessage = resultError.ToErrorString();

            formattedErrorMessage.Should().StartWith($"Unknown ResultError of type {typeof(TestUnknownResultError).FullName}");
        }

        [Fact]
        public void when_Unknown_IResultError_return_UnknownResultErrorOfType_ToErrorString()
        {
            IResultError resultError = new TestUnknownResultError();

            var formattedErrorMessage = resultError.ToErrorString(",");

            formattedErrorMessage.Should().StartWith($"Unknown ResultError of type {typeof(TestUnknownResultError).FullName}");
        }

        [Fact]
        public void when_Null_return_empty_string_ToErrorStringSafe_Without_Seperator()
        {
            IResultError resultError = null;

            var formattedErrorMessage = resultError.ToErrorStringSafe();

            formattedErrorMessage.Should().Be(string.Empty);
        }

        [Fact]
        public void when_Null_return_empty_string_ToErrorStringSafe()
        {
            IResultError resultError = null;

            var formattedErrorMessage = resultError.ToErrorStringSafe(",");

            formattedErrorMessage.Should().Be(string.Empty);
        }

        [Fact]
        public void when_Null_return_empty_string_ToErrorString_Without_Seperator()
        {
            IResultError resultError = null;

            var formattedErrorMessage = resultError.ToErrorString();

            formattedErrorMessage.Should().Be(string.Empty);
        }

        [Fact]
        public void when_Null_return_empty_string_ToErrorString()
        {
            IResultError resultError = null;

            var formattedErrorMessage = resultError.ToErrorString(",");

            formattedErrorMessage.Should().Be(string.Empty);
        }
    }
}