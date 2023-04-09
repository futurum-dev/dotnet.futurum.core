using System;
using System.Threading.Tasks;

using FluentAssertions;

using Futurum.Core.Result;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultEnumerableFlatMapSequentialUntilFailureTests
{
    private const string ErrorMessage1 = "ERROR_MESSAGE_1";

    public class Sync
    {
        public interface IWorker
        {
            Core.Result.Result Execute();
        }

        public class SuccessWorker : IWorker
        {
            private readonly Action _action;

            public SuccessWorker(Action action)
            {
                _action = action;
            }

            public Core.Result.Result Execute()
            {
                _action();

                return Core.Result.Result.Ok();
            }
        }

        public class FailureWorker : IWorker
        {
            private readonly Action _action;

            public FailureWorker(Action action)
            {
                _action = action;
            }

            public Core.Result.Result Execute()
            {
                _action();

                return Core.Result.Result.Fail(ErrorMessage1);
            }
        }

        [Fact]
        public void AllFailure()
        {
            var worker1WasCalled = false;
            var worker2WasCalled = false;
            var worker3WasCalled = false;

            var worker1 = new FailureWorker(() =>
            {
                worker1WasCalled.Should().BeFalse();
                worker2WasCalled.Should().BeFalse();
                worker3WasCalled.Should().BeFalse();

                worker1WasCalled = true;
            });
            var worker2 = new FailureWorker(() =>
            {
                worker1WasCalled.Should().BeTrue();
                worker2WasCalled.Should().BeFalse();
                worker3WasCalled.Should().BeFalse();

                worker2WasCalled = true;
            });
            var worker3 = new FailureWorker(() =>
            {
                worker1WasCalled.Should().BeTrue();
                worker2WasCalled.Should().BeTrue();
                worker3WasCalled.Should().BeFalse();

                worker3WasCalled = true;
            });

            var input = new[] {worker1, worker2, worker3};

            var outputResult = input.FlatMapSequentialUntilFailure(x => x.Execute());

            outputResult.ShouldBeFailureWithError(ErrorMessage1);

            worker1WasCalled.Should().BeTrue();
            worker2WasCalled.Should().BeFalse();
            worker3WasCalled.Should().BeFalse();
        }

        [Fact]
        public void AllSuccess()
        {
            var worker1WasCalled = false;
            var worker2WasCalled = false;
            var worker3WasCalled = false;

            var worker1 = new SuccessWorker(() =>
            {
                worker1WasCalled.Should().BeFalse();
                worker2WasCalled.Should().BeFalse();
                worker3WasCalled.Should().BeFalse();

                worker1WasCalled = true;
            });
            var worker2 = new SuccessWorker(() =>
            {
                worker1WasCalled.Should().BeTrue();
                worker2WasCalled.Should().BeFalse();
                worker3WasCalled.Should().BeFalse();

                worker2WasCalled = true;
            });
            var worker3 = new SuccessWorker(() =>
            {
                worker1WasCalled.Should().BeTrue();
                worker2WasCalled.Should().BeTrue();
                worker3WasCalled.Should().BeFalse();

                worker3WasCalled = true;
            });

            var input = new[] {worker1, worker2, worker3};

            var outputResult = input.FlatMapSequentialUntilFailure(x => x.Execute());

            outputResult.ShouldBeSuccess();

            worker1WasCalled.Should().BeTrue();
            worker2WasCalled.Should().BeTrue();
            worker3WasCalled.Should().BeTrue();
        }

        [Fact]
        public void Mixed()
        {
            var worker1WasCalled = false;
            var worker2WasCalled = false;
            var worker3WasCalled = false;

            var worker1 = new SuccessWorker(() =>
            {
                worker1WasCalled.Should().BeFalse();
                worker2WasCalled.Should().BeFalse();
                worker3WasCalled.Should().BeFalse();

                worker1WasCalled = true;
            });
            var worker2 = new FailureWorker(() =>
            {
                worker1WasCalled.Should().BeTrue();
                worker2WasCalled.Should().BeFalse();
                worker3WasCalled.Should().BeFalse();

                worker2WasCalled = true;
            });
            var worker3 = new SuccessWorker(() =>
            {
                worker1WasCalled.Should().BeTrue();
                worker2WasCalled.Should().BeTrue();
                worker3WasCalled.Should().BeFalse();

                worker3WasCalled = true;
            });

            var input = new IWorker[] {worker1, worker2, worker3};

            var outputResult = input.FlatMapSequentialUntilFailure(x => x.Execute());

            outputResult.ShouldBeFailureWithError(ErrorMessage1);

            worker1WasCalled.Should().BeTrue();
            worker2WasCalled.Should().BeTrue();
            worker3WasCalled.Should().BeFalse();
        }
    }

    public class Async
    {
        public interface IWorker
        {
            Task<Core.Result.Result> ExecuteAsync();
        }

        public class SuccessWorker : IWorker
        {
            private readonly Action _action;

            public SuccessWorker(Action action)
            {
                _action = action;
            }

            public Task<Core.Result.Result> ExecuteAsync()
            {
                _action();

                return Core.Result.Result.OkAsync();
            }
        }

        public class FailureWorker : IWorker
        {
            private readonly Action _action;

            public FailureWorker(Action action)
            {
                _action = action;
            }

            public Task<Core.Result.Result> ExecuteAsync()
            {
                _action();

                return Core.Result.Result.FailAsync(ErrorMessage1);
            }
        }

        [Fact]
        public async Task AllFailure()
        {
            var worker1WasCalled = false;
            var worker2WasCalled = false;
            var worker3WasCalled = false;

            var worker1 = new FailureWorker(() =>
            {
                worker1WasCalled.Should().BeFalse();
                worker2WasCalled.Should().BeFalse();
                worker3WasCalled.Should().BeFalse();

                worker1WasCalled = true;
            });
            var worker2 = new FailureWorker(() =>
            {
                worker1WasCalled.Should().BeTrue();
                worker2WasCalled.Should().BeFalse();
                worker3WasCalled.Should().BeFalse();

                worker2WasCalled = true;
            });
            var worker3 = new FailureWorker(() =>
            {
                worker1WasCalled.Should().BeTrue();
                worker2WasCalled.Should().BeTrue();
                worker3WasCalled.Should().BeFalse();

                worker3WasCalled = true;
            });

            var input = new[] {worker1, worker2, worker3};

            var outputResult = await input.FlatMapSequentialUntilFailureAsync(x => x.ExecuteAsync());

            outputResult.ShouldBeFailureWithError(ErrorMessage1);

            worker1WasCalled.Should().BeTrue();
            worker2WasCalled.Should().BeFalse();
            worker3WasCalled.Should().BeFalse();
        }

        [Fact]
        public async Task AllSuccess()
        {
            var worker1WasCalled = false;
            var worker2WasCalled = false;
            var worker3WasCalled = false;

            var worker1 = new SuccessWorker(() =>
            {
                worker1WasCalled.Should().BeFalse();
                worker2WasCalled.Should().BeFalse();
                worker3WasCalled.Should().BeFalse();

                worker1WasCalled = true;
            });
            var worker2 = new SuccessWorker(() =>
            {
                worker1WasCalled.Should().BeTrue();
                worker2WasCalled.Should().BeFalse();
                worker3WasCalled.Should().BeFalse();

                worker2WasCalled = true;
            });
            var worker3 = new SuccessWorker(() =>
            {
                worker1WasCalled.Should().BeTrue();
                worker2WasCalled.Should().BeTrue();
                worker3WasCalled.Should().BeFalse();

                worker3WasCalled = true;
            });

            var input = new[] {worker1, worker2, worker3};

            var outputResult = await input.FlatMapSequentialUntilFailureAsync(x => x.ExecuteAsync());

            outputResult.ShouldBeSuccess();

            worker1WasCalled.Should().BeTrue();
            worker2WasCalled.Should().BeTrue();
            worker3WasCalled.Should().BeTrue();
        }

        [Fact]
        public async Task Mixed()
        {
            var worker1WasCalled = false;
            var worker2WasCalled = false;
            var worker3WasCalled = false;

            var worker1 = new SuccessWorker(() =>
            {
                worker1WasCalled.Should().BeFalse();
                worker2WasCalled.Should().BeFalse();
                worker3WasCalled.Should().BeFalse();

                worker1WasCalled = true;
            });
            var worker2 = new FailureWorker(() =>
            {
                worker1WasCalled.Should().BeTrue();
                worker2WasCalled.Should().BeFalse();
                worker3WasCalled.Should().BeFalse();

                worker2WasCalled = true;
            });
            var worker3 = new SuccessWorker(() =>
            {
                worker1WasCalled.Should().BeTrue();
                worker2WasCalled.Should().BeTrue();
                worker3WasCalled.Should().BeFalse();

                worker3WasCalled = true;
            });

            var input = new IWorker[] {worker1, worker2, worker3};

            var outputResult = await input.FlatMapSequentialUntilFailureAsync(x => x.ExecuteAsync());

            outputResult.ShouldBeFailureWithError(ErrorMessage1);

            worker1WasCalled.Should().BeTrue();
            worker2WasCalled.Should().BeTrue();
            worker3WasCalled.Should().BeFalse();
        }
    }
}