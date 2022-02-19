using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentAssertions;

using Futurum.Core.Result;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultEnumerableExtensionsCastTests
{
    private const string ErrorMessage = "ERROR_MESSAGE_1";

    public class Sync
    {
        [Fact]
        public void Failure()
        {
            var resultInput = Core.Result.Result.Fail<IEnumerable<INumberType>>(ErrorMessage);

            var returnedResult = resultInput.Cast<INumberType, Type1>();

            returnedResult.ShouldBeFailureWithError(ErrorMessage);
        }

        [Fact]
        public void Success()
        {
            var values = Enumerable.Range(0, 1)
                                   .Select(i => new Type1(i) as INumberType);

            var resultInput = Core.Result.Result.Ok(values);

            var returnedResultType = resultInput.Cast<INumberType, Type1>();

            returnedResultType.ShouldBeSuccess();
            
            returnedResultType.ShouldBeSuccessWithValueEquivalentTo(values.Cast<Type1>());
        }
    }

    public class Async
    {
        [Fact]
        public async Task Failure()
        {
            var resultInput = Core.Result.Result.FailAsync<IEnumerable<INumberType>>(ErrorMessage);

            var returnedResult = await resultInput.CastAsync<INumberType, Type1>();

            returnedResult.ShouldBeFailureWithError(ErrorMessage);
        }

        [Fact]
        public async Task Success()
        {
            var values = Enumerable.Range(0, 1)
                                   .Select(i => new Type1(i) as INumberType);

            var resultInput = Core.Result.Result.OkAsync(values);

            var returnedResultType = await resultInput.CastAsync<INumberType, Type1>();

            returnedResultType.ShouldBeSuccess();
            
            returnedResultType.ShouldBeSuccessWithValueEquivalentTo(values.Cast<Type1>());
        }
    }
    
    public interface INumberType
    {
        int Number { get; }
    }

    private record Type1(int Number) : INumberType;
}