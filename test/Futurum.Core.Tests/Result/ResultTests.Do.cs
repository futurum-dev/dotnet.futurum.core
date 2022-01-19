using System;
using System.Threading.Tasks;

using FluentAssertions;

using Futurum.Core.Result;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultDoTests
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

                var wasCalled = false;

                var returnedResult = resultInput.Do(() => wasCalled = true);

                wasCalled.Should().BeFalse();
                returnedResult.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public void Success()
            {
                var resultInput = Core.Result.Result.Ok();

                var wasCalled = false;

                var returnedResult = resultInput.Do(() => wasCalled = true);

                wasCalled.Should().BeTrue();
                returnedResult.ShouldBeSuccess();
            }
        }

        public class GenericInput
        {
            [Fact]
            public void Failure()
            {
                var resultInput = Core.Result.Result.Fail<Guid>(ErrorMessage);

                var wasCalled = false;

                var resultOutput = resultInput.Do(_ => wasCalled = true);

                wasCalled.Should().BeFalse();
                resultOutput.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public void Success()
            {
                var inputValue = Guid.NewGuid();

                var resultInput = Core.Result.Result.Ok(inputValue);

                var wasCalled = false;

                var passedValue = Guid.Empty;

                var resultOutput = resultInput.Do(value =>
                {
                    passedValue = value;

                    wasCalled = true;
                });

                wasCalled.Should().BeTrue();
                passedValue.Should().Be(inputValue);
                resultOutput.ShouldBeSuccess();
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

                var wasCalled = false;

                var returnedResult = await resultInput.DoAsync(() =>
                {
                    wasCalled = true;

                    return Task.CompletedTask;
                });

                wasCalled.Should().BeFalse();
                returnedResult.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public async Task Success()
            {
                var resultInput = Core.Result.Result.OkAsync();

                var wasCalled = false;

                var returnedResult = await resultInput.DoAsync(() =>
                {
                    wasCalled = true;

                    return Task.CompletedTask;
                });

                wasCalled.Should().BeTrue();
                returnedResult.ShouldBeSuccess();
            }
        }

        public class GenericInput
        {
            [Fact]
            public async Task Failure()
            {
                var resultInput = Core.Result.Result.FailAsync<Guid>(ErrorMessage);

                var wasCalled = false;

                var resultOutput = await resultInput.DoAsync(_ =>
                {
                    wasCalled = true;

                    return Task.CompletedTask;
                });

                wasCalled.Should().BeFalse();
                resultOutput.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public async Task Success()
            {
                var inputValue = Guid.NewGuid();

                var resultInput = Core.Result.Result.OkAsync(inputValue);

                var wasCalled = false;

                var passedValue = Guid.Empty;

                var resultOutput = await resultInput.DoAsync(value =>
                {
                    passedValue = value;

                    wasCalled = true;

                    return Task.CompletedTask;
                });

                wasCalled.Should().BeTrue();
                passedValue.Should().Be(inputValue);
                resultOutput.ShouldBeSuccess();
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

                var wasCalled = false;

                var returnedResult = await resultInput.DoAsync(() => { wasCalled = true; });

                wasCalled.Should().BeFalse();
                returnedResult.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public async Task Success()
            {
                var resultInput = Core.Result.Result.OkAsync();

                var wasCalled = false;

                var returnedResult = await resultInput.DoAsync(() => { wasCalled = true; });

                wasCalled.Should().BeTrue();
                returnedResult.ShouldBeSuccess();
            }
        }

        public class GenericInput
        {
            [Fact]
            public async Task Failure()
            {
                var resultInput = Core.Result.Result.FailAsync<Guid>(ErrorMessage);

                var wasCalled = false;

                var resultOutput = await resultInput.DoAsync(_ => { wasCalled = true; });

                wasCalled.Should().BeFalse();
                resultOutput.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public async Task Success()
            {
                var inputValue = Guid.NewGuid();

                var resultInput = Core.Result.Result.OkAsync(inputValue);

                var wasCalled = false;

                var passedValue = Guid.Empty;

                var resultOutput = await resultInput.DoAsync(value =>
                {
                    passedValue = value;

                    wasCalled = true;
                });

                wasCalled.Should().BeTrue();
                passedValue.Should().Be(inputValue);
                resultOutput.ShouldBeSuccess();
            }
        }
    }

    public class AsyncFuncActionOnly
    {
        public class NonGenericInput
        {
            [Fact]
            public async Task Failure()
            {
                var resultInput = Core.Result.Result.Fail(ErrorMessage);

                var wasCalled = false;

                var returnedResult = await resultInput.DoAsync(() =>
                {
                    wasCalled = true;

                    return Task.CompletedTask;
                });

                wasCalled.Should().BeFalse();
                returnedResult.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public async Task Success()
            {
                var resultInput = Core.Result.Result.Ok();

                var wasCalled = false;

                var returnedResult = await resultInput.DoAsync(() =>
                {
                    wasCalled = true;

                    return Task.CompletedTask;
                });

                wasCalled.Should().BeTrue();
                returnedResult.ShouldBeSuccess();
            }
        }

        public class GenericInput
        {
            [Fact]
            public async Task Failure()
            {
                var resultInput = Core.Result.Result.Fail<Guid>(ErrorMessage);

                var wasCalled = false;

                var resultOutput = await resultInput.DoAsync(_ =>
                {
                    wasCalled = true;

                    return Task.CompletedTask;
                });

                wasCalled.Should().BeFalse();
                resultOutput.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public async Task Success()
            {
                var inputValue = Guid.NewGuid();

                var resultInput = Core.Result.Result.Ok(inputValue);

                var wasCalled = false;

                var passedValue = Guid.Empty;

                var resultOutput = await resultInput.DoAsync(value =>
                {
                    passedValue = value;

                    wasCalled = true;

                    return Task.CompletedTask;
                });

                wasCalled.Should().BeTrue();
                passedValue.Should().Be(inputValue);
                resultOutput.ShouldBeSuccess();
            }
        }
    }
}