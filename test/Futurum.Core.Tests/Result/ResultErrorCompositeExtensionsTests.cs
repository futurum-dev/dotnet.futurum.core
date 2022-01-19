using System.Linq;

using FluentAssertions;

using Futurum.Core.Result;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultErrorCompositeExtensionsTests
{
    public class ToResultError
    {
        [Fact]
        public void Array()
        {
            var resultErrors = Enumerable.Range(0, 10)
                                         .Select(x => x.ToString())
                                         .Select(x => x.ToResultError())
                                         .ToArray();

            var resultErrorComposite = resultErrors.ToResultError() as ResultErrorComposite;

            resultErrorComposite.Children.Count().Should().Be(resultErrors.Count());

            resultErrorComposite.Children.Should().BeEquivalentTo(resultErrors);
        }

        [Fact]
        public void IEnumerble()
        {
            var resultErrors = Enumerable.Range(0, 10)
                                         .Select(x => x.ToString())
                                         .Select(x => x.ToResultError())
                                         .ToList()
                                         .AsEnumerable();

            var resultErrorComposite = resultErrors.ToResultError() as ResultErrorComposite;

            resultErrorComposite.Children.Count().Should().Be(resultErrors.Count());

            resultErrorComposite.Children.Should().BeEquivalentTo(resultErrors);
        }
    }

    public class EnhanceWithError
    {
        private const string ErrorMessage1 = "ERROR_MESSAGE_1";
        private const string ErrorMessage2 = "ERROR_MESSAGE_2";

        [Fact]
        public void ErrorMessage()
        {
            var error = ErrorMessage1.ToResultError();

            var resultError = error.EnhanceWithError(ErrorMessage2);

            resultError.ShouldBeError(ErrorMessage2, ErrorMessage1);
        }

        [Fact]
        public void IResultError()
        {
            var error = ErrorMessage1.ToResultError();

            var resultError = error.EnhanceWithError(ErrorMessage2.ToResultError());

            resultError.ShouldBeError(ErrorMessage2, ErrorMessage1);
        }
    }
}