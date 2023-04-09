using System.Linq;

using FluentAssertions;

using Futurum.Core.Result;
using Futurum.Core.Tests.Helper.Option;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultErrorCompositeExtensionsTests
{
    public class with_Parent
    {
        private const string ErrorMessage = "ERROR_MESSAGE";

        [Fact]
        public void IEnumerable()
        {
            var parentResultError = ErrorMessage.ToResultError();
            
            var resultErrors = Enumerable.Range(0, 10)
                                         .Select(x => x.ToString())
                                         .Select(x => x.ToResultError())
                                         .ToList();

            var resultErrorComposite = ResultErrorCompositeExtensions.ToResultError(parentResultError, resultErrors) as ResultErrorComposite;

            resultErrorComposite.Parent.ShouldBeHasValueWithValue(parentResultError);
            
            resultErrorComposite.Children.Count().Should().Be(resultErrors.Count());

            resultErrorComposite.Children.Should().BeEquivalentTo(resultErrors);
        }

        [Fact]
        public void Array()
        {
            var parentResultError = ErrorMessage.ToResultError();
            
            var resultErrors = Enumerable.Range(0, 10)
                                         .Select(x => x.ToString())
                                         .Select(x => x.ToResultError())
                                         .ToArray();

            var resultErrorComposite = ResultErrorCompositeExtensions.ToResultError(parentResultError, resultErrors) as ResultErrorComposite;

            resultErrorComposite.Parent.ShouldBeHasValueWithValue(parentResultError);
            
            resultErrorComposite.Children.Count().Should().Be(resultErrors.Count());

            resultErrorComposite.Children.Should().BeEquivalentTo(resultErrors);
        }
    }

    public class without_Parent
    {
        [Fact]
        public void IEnumerble()
        {
            var resultErrors = Enumerable.Range(0, 10)
                                         .Select(x => x.ToString())
                                         .Select(x => x.ToResultError())
                                         .ToList()
                                         .AsEnumerable();

            var resultErrorComposite = resultErrors.ToResultError() as ResultErrorComposite;

            resultErrorComposite.Parent.ShouldBeHasNoValue();
            
            resultErrorComposite.Children.Count().Should().Be(resultErrors.Count());

            resultErrorComposite.Children.Should().BeEquivalentTo(resultErrors);
        }
    }
}