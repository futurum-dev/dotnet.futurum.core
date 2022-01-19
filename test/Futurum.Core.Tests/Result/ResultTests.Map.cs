using System;
using System.Threading.Tasks;

using FluentAssertions;

using Futurum.Core.Result;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultMapTests
{
    private const string ErrorMessage = "ERROR_MESSAGE_1";

    public class Sync
    {
        public class NonGenericInputValueNotFunc
        {
            [Fact]
            public void Failure()
            {
                var resultInput = Core.Result.Result.Fail(ErrorMessage);

                var outputValue = Guid.NewGuid();

                var returnedResult = resultInput.Map(outputValue);

                returnedResult.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public void Success()
            {
                var resultInput = Core.Result.Result.Ok();

                var outputValue = Guid.NewGuid();

                var returnedResult = resultInput.Map(outputValue);

                returnedResult.ShouldBeSuccessWithValue(outputValue);
            }
        }
        
        public class NonGenericInput
        {
            [Fact]
            public void Failure()
            {
                var resultInput = Core.Result.Result.Fail(ErrorMessage);

                var outputValue = Guid.NewGuid();

                var returnedResult = resultInput.Map(() => outputValue);

                returnedResult.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public void Success()
            {
                var resultInput = Core.Result.Result.Ok();

                var outputValue = Guid.NewGuid();

                var returnedResult = resultInput.Map(() => outputValue);

                returnedResult.ShouldBeSuccessWithValue(outputValue);
            }
        }

        public class GenericInput
        {
            [Fact]
            public void Failure()
            {
                var resultInput = Core.Result.Result.Fail<Guid>(ErrorMessage);

                var outputValue = Guid.NewGuid();

                var returnedResult = resultInput.Map(_ => outputValue);

                returnedResult.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public void Success()
            {
                var inputValue = Guid.NewGuid();

                var resultInput = Core.Result.Result.Ok(inputValue);

                var outputValue = Guid.NewGuid();

                var passedValue = Guid.Empty;

                var returnedResult = resultInput.Map(value =>
                {
                    passedValue = value;

                    return outputValue;
                });

                passedValue.Should().Be(inputValue);
                returnedResult.ShouldBeSuccessWithValue(outputValue);
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

                var outputValue = Guid.NewGuid();

                var returnedResult = await resultInput.MapAsync(() => Task.FromResult(outputValue));

                returnedResult.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public async Task Success()
            {
                var resultInput = Core.Result.Result.OkAsync();

                var outputValue = Guid.NewGuid();

                var returnedResult = await resultInput.MapAsync(() => Task.FromResult(outputValue));

                returnedResult.ShouldBeSuccessWithValue(outputValue);
            }
        }

        public class GenericInput
        {
            [Fact]
            public async Task Failure()
            {
                var resultInput = Core.Result.Result.FailAsync<Guid>(ErrorMessage);

                var outputValue = Guid.NewGuid();

                var returnedResult = await resultInput.MapAsync(_ => Task.FromResult(outputValue));

                returnedResult.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public async Task Success()
            {
                var inputValue = Guid.NewGuid();

                var resultInput = Core.Result.Result.OkAsync(inputValue);

                var outputValue = Guid.NewGuid();

                var passedValue = Guid.Empty;

                var returnedResult = await resultInput.MapAsync(value =>
                {
                    passedValue = value;

                    return Task.FromResult(outputValue);
                });

                passedValue.Should().Be(inputValue);
                returnedResult.ShouldBeSuccessWithValue(outputValue);
            }
        }
    }

    public class AsyncInputOnly
    {
        public class NonGenericInput
        {
            [Fact]
            public async Task Failure()
            {
                var resultInput = Core.Result.Result.FailAsync(ErrorMessage);

                var outputValue = Guid.NewGuid();

                var returnedResult = await resultInput.MapAsync(() => outputValue);

                returnedResult.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public async Task Success()
            {
                var resultInput = Core.Result.Result.OkAsync();

                var outputValue = Guid.NewGuid();

                var returnedResult = await resultInput.MapAsync(() => outputValue);

                returnedResult.ShouldBeSuccessWithValue(outputValue);
            }
        }

        public class GenericInput
        {
            [Fact]
            public async Task Failure()
            {
                var resultInput = Core.Result.Result.FailAsync<Guid>(ErrorMessage);

                var outputValue = Guid.NewGuid();

                var returnedResult = await resultInput.MapAsync(_ => outputValue);

                returnedResult.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public async Task Success()
            {
                var inputValue = Guid.NewGuid();

                var resultInput = Core.Result.Result.OkAsync(inputValue);

                var outputValue = Guid.NewGuid();

                var passedValue = Guid.Empty;

                var returnedResult = await resultInput.MapAsync(value =>
                {
                    passedValue = value;

                    return outputValue;
                });

                passedValue.Should().Be(inputValue);
                returnedResult.ShouldBeSuccessWithValue(outputValue);
            }
        }

        public class GenericInputWithSelector
        {
            public class Container
            {
                public Container(Guid value)
                {
                    Value = value;
                }

                public Guid Value { get; }
            }

            [Fact]
            public async Task Failure()
            {
                var resultInput = Core.Result.Result.FailAsync<Container>(ErrorMessage);

                var outputValue = Guid.NewGuid();

                var returnedResult = await resultInput.MapAsync(x => x.Value, _ => outputValue);

                returnedResult.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public async Task Success()
            {
                var inputValue = Guid.NewGuid();

                var resultInput = Core.Result.Result.OkAsync(new Container(inputValue));

                var outputValue = Guid.NewGuid();

                var passedValue = Guid.Empty;

                var returnedResult = await resultInput.MapAsync(x => x.Value, value =>
                {
                    passedValue = value;

                    return outputValue;
                });

                passedValue.Should().Be(inputValue);
                returnedResult.ShouldBeSuccessWithValue(outputValue);
            }
        }
    }

    public class AsyncFuncOnly
    {
        public class NonGenericInput
        {
            [Fact]
            public async Task Failure()
            {
                var resultInput = Core.Result.Result.Fail(ErrorMessage);

                var outputValue = Guid.NewGuid();

                var returnedResult = await resultInput.MapAsync(() => Task.FromResult(outputValue));

                returnedResult.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public async Task Success()
            {
                var resultInput = Core.Result.Result.Ok();

                var outputValue = Guid.NewGuid();

                var returnedResult = await resultInput.MapAsync(() => Task.FromResult(outputValue));

                returnedResult.ShouldBeSuccessWithValue(outputValue);
            }
        }

        public class GenericInput
        {
            [Fact]
            public async Task Failure()
            {
                var resultInput = Core.Result.Result.Fail<Guid>(ErrorMessage);

                var outputValue = Guid.NewGuid();

                var returnedResult = await resultInput.MapAsync(_ => Task.FromResult(outputValue));

                returnedResult.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public async Task Success()
            {
                var inputValue = Guid.NewGuid();

                var resultInput = Core.Result.Result.Ok(inputValue);

                var outputValue = Guid.NewGuid();

                var passedValue = Guid.Empty;

                var returnedResult = await resultInput.MapAsync(value =>
                {
                    passedValue = value;

                    return Task.FromResult(outputValue);
                });

                passedValue.Should().Be(inputValue);
                returnedResult.ShouldBeSuccessWithValue(outputValue);
            }
        }
    }
}