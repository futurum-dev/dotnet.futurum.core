using System;
using System.Threading.Tasks;

using FluentAssertions;

using Futurum.Core.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultUnwrapTests
{
    private const string ErrorMessage = "ERROR_MESSAGE_1";

    public class Sync
    {
        public class NonGenericInput
        {
            [Fact]
            public void Failure()
            {
                var resultInput = Core.Result.Result.Fail(ErrorMessage);

                Action func = () => resultInput.Unwrap();

                func.Should().Throw<Exception>().WithMessage(ErrorMessage);
            }
                
            [Fact]
            public void Success()
            {
                var resultInput = Core.Result.Result.Ok();

                Action func = () => resultInput.Unwrap();

                func.Should().NotThrow<Exception>();
            }
        }
            
        public class GenericInput
        {
            [Fact]
            public void Failure()
            {
                var resultInput = Core.Result.Result.Fail<Guid>(ErrorMessage);

                Action func = () => resultInput.Unwrap();

                func.Should().Throw<Exception>().WithMessage(ErrorMessage);
            }
                
            [Fact]
            public void Success()
            {
                var outputValue = Guid.NewGuid();

                var resultInput = Core.Result.Result.Ok(outputValue);

                Func<Guid> func = () => resultInput.Unwrap();

                func.Should().NotThrow<Exception>();
                func().Should().Be(outputValue);
            }
        }
    }
        
    public class Async
    {
        public class NonGenericInput
        {
            [Fact]
            public async Task Failure()
            {
                var resultInput = Core.Result.Result.FailAsync(ErrorMessage);

                Func<Task> func = () => resultInput.UnwrapAsync();

                await func.Should().ThrowAsync<Exception>().WithMessage(ErrorMessage);
            }
                
            [Fact]
            public async Task Success()
            {
                var resultInput = Core.Result.Result.OkAsync();

                Func<Task> func = () => resultInput.UnwrapAsync();

                await func.Should().NotThrowAsync();
            }
        }
            
        public class GenericInput
        {
            [Fact]
            public async Task Failure()
            {
                var resultInput = Core.Result.Result.FailAsync<Guid>(ErrorMessage);

                Func<Task<Guid>> func = () => resultInput.UnwrapAsync();

                await func.Should().ThrowAsync<Exception>().WithMessage(ErrorMessage);
            }
                
            [Fact]
            public async Task Success()
            {
                var outputValue = Guid.NewGuid();

                var resultInput = Core.Result.Result.OkAsync(outputValue);

                Func<Task<Guid>> func = () => resultInput.UnwrapAsync();

                await func.Should().NotThrowAsync();
                (await func()).Should().Be(outputValue);
            }
        }
    }
}