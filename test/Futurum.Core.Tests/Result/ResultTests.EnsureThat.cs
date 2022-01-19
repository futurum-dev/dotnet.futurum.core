using System;
using System.Threading.Tasks;

using Futurum.Core.Result;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class DummyContainerClass<T>
{
    public DummyContainerClass(T value)
    {
        Value = value;
    }

    public T Value { get; }
}

public static class DummyContainerClassHelper
{
    public static DummyContainerClass<T> CreateContainer<T>(this T value) =>
        new(value);
}

public class ResultEnsureThatTests
{
    private const string ErrorMessage1 = "ERROR_MESSAGE_1";
    private const string ErrorMessage2 = "ERROR_MESSAGE_2";

    private static Core.Result.Result SuccessPredicate(Guid value) =>
        value.Equals(value)
            ? Core.Result.Result.Ok()
            : Core.Result.Result.Fail(ErrorMessage1);

    private static Core.Result.Result FailurePredicate(Guid value) =>
        !value.Equals(value)
            ? Core.Result.Result.Ok()
            : Core.Result.Result.Fail(ErrorMessage1);

    public class Sync
    {
        public class Value
        {
            public class Predicate
            {
                [Fact]
                public void Failure()
                {
                    var input = Guid.NewGuid();

                    var result = input.EnsureThat(FailurePredicate, () => ErrorMessage2);

                    result.ShouldBeFailureWithError(ErrorMessage2, ErrorMessage1);
                }

                [Fact]
                public void Success()
                {
                    var input = Guid.NewGuid();

                    var result = input.EnsureThat(SuccessPredicate, () => ErrorMessage2);

                    result.ShouldBeSuccessWithValue(input);
                }
            }

            public class Ensure
            {
                public class WithoutSelector
                {
                    [Fact]
                    public void Failure()
                    {
                        var value = Guid.NewGuid();

                        var result =
                            value.EnsureThat(nameof(value), (_, __) => Core.Result.Result.Fail(ErrorMessage1));

                        result.ShouldBeFailureWithError(ErrorMessage1);
                    }

                    [Fact]
                    public void Success()
                    {
                        var value = Guid.NewGuid();

                        var result = value.EnsureThat(nameof(value), (_, __) => Core.Result.Result.Ok());

                        result.ShouldBeSuccessWithValue(value);
                    }
                }

                public class WithSelector
                {
                    [Fact]
                    public void Failure()
                    {
                        var value = Guid.NewGuid().CreateContainer();

                        var result = value.EnsureThat(x => x.Value, nameof(value), (_, __) => Core.Result.Result.Fail(ErrorMessage1));

                        result.ShouldBeFailureWithError(ErrorMessage1);
                    }

                    [Fact]
                    public void Success()
                    {
                        var value = Guid.NewGuid().CreateContainer();

                        var result = value.EnsureThat(x => x.Value, nameof(value), (_, __) => Core.Result.Result.Ok());

                        result.ShouldBeSuccessWithValue(value);
                    }
                }
            }
        }

        public class Return
        {
            public class BoolPredicate
            {
                [Fact]
                public void Failure()
                {
                    var input = Guid.NewGuid();

                    Func<Guid, bool> predicate = value => !value.Equals(value);
                    var result = input.ToResultOk()
                                      .EnsureThat(predicate, () => ErrorMessage1);

                    result.ShouldBeFailureWithError(ErrorMessage1);
                }

                [Fact]
                public void Success()
                {
                    var input = Guid.NewGuid();

                    Func<Guid, bool> predicate = value => value.Equals(value);
                    var result = input.ToResultOk()
                                      .EnsureThat(predicate, () => ErrorMessage1);

                    result.ShouldBeSuccessWithValue(input);
                }
            }

            public class Predicate
            {
                public class WithoutSelector
                {
                    [Fact]
                    public void Failure()
                    {
                        var input = Guid.NewGuid();

                        var result = input.ToResultOk()
                                          .EnsureThat(FailurePredicate, () => ErrorMessage2);

                        result.ShouldBeFailureWithError(ErrorMessage2, ErrorMessage1);
                    }

                    [Fact]
                    public void Success()
                    {
                        var input = Guid.NewGuid();

                        var result = input.ToResultOk()
                                          .EnsureThat(SuccessPredicate, () => ErrorMessage2);

                        result.ShouldBeSuccessWithValue(input);
                    }
                }

                public class WithSelector
                {
                    [Fact]
                    public void Failure()
                    {
                        var input = Guid.NewGuid().CreateContainer();

                        var result = input.ToResultOk()
                                          .EnsureThat(x => x.Value, FailurePredicate, () => ErrorMessage2);

                        result.ShouldBeFailureWithError(ErrorMessage2, ErrorMessage1);
                    }

                    [Fact]
                    public void Success()
                    {
                        var input = Guid.NewGuid().CreateContainer();

                        var result = input.ToResultOk()
                                          .EnsureThat(x => x.Value, SuccessPredicate, () => ErrorMessage1);

                        result.ShouldBeSuccessWithValue(input);
                    }
                }
            }

            public class Ensure
            {
                public class WithoutSelector
                {
                    [Fact]
                    public void Failure()
                    {
                        var value = Guid.NewGuid();

                        Func<Guid, string, Core.Result.Result> ensure = (_, __) =>
                            Core.Result.Result.Fail(ErrorMessage1);
                        var result = value.ToResultOk()
                                          .EnsureThat(nameof(value), ensure);

                        result.ShouldBeFailureWithError(ErrorMessage1);
                    }

                    [Fact]
                    public void Success()
                    {
                        var value = Guid.NewGuid();

                        Func<Guid, string, Core.Result.Result> ensure = (_, __) => Core.Result.Result.Ok();
                        var result = value.ToResultOk()
                                          .EnsureThat(nameof(value), ensure);

                        result.ShouldBeSuccessWithValue(value);
                    }
                }

                public class WithSelector
                {
                    [Fact]
                    public void Failure()
                    {
                        var value = Guid.NewGuid().CreateContainer();

                        Func<Guid, string, Core.Result.Result> ensure = (_, __) =>
                            Core.Result.Result.Fail(ErrorMessage1);
                        var result = value.ToResultOk()
                                          .EnsureThat(x => x.Value, nameof(value), ensure);

                        result.ShouldBeFailureWithError(ErrorMessage1);
                    }

                    [Fact]
                    public void Success()
                    {
                        var value = Guid.NewGuid().CreateContainer();

                        Func<Guid, string, Core.Result.Result> ensure = (_, __) => Core.Result.Result.Ok();
                        var result = value.ToResultOk()
                                          .EnsureThat(x => x.Value, nameof(value), ensure);

                        result.ShouldBeSuccessWithValue(value);
                    }
                }
            }
        }
    }

    public class Async
    {
        public class Return
        {
            public class BoolPredicate
            {
                [Fact]
                public async Task Failure()
                {
                    var input = Guid.NewGuid();

                    Func<Guid, bool> predicate = value => !value.Equals(value);
                    var result = await input.ToResultOkAsync()
                                            .EnsureThatAsync(predicate, () => ErrorMessage1);

                    result.ShouldBeFailureWithError(ErrorMessage1);
                }

                [Fact]
                public async Task Success()
                {
                    var input = Guid.NewGuid();

                    Func<Guid, bool> predicate = value => value.Equals(value);
                    var result = await input.ToResultOkAsync()
                                            .EnsureThatAsync(predicate, () => ErrorMessage1);

                    result.ShouldBeSuccessWithValue(input);
                }
            }

            public class Predicate
            {
                public class WithoutSelector
                {
                    [Fact]
                    public async Task Failure()
                    {
                        var input = Guid.NewGuid();

                        var result = await input.ToResultOkAsync()
                                                .EnsureThatAsync(FailurePredicate, () => ErrorMessage2);

                        result.ShouldBeFailureWithError(ErrorMessage2, ErrorMessage1);
                    }

                    [Fact]
                    public async Task Success()
                    {
                        var input = Guid.NewGuid();

                        var result = await input.ToResultOkAsync()
                                                .EnsureThatAsync(SuccessPredicate, () => ErrorMessage1);

                        result.ShouldBeSuccessWithValue(input);
                    }
                }

                public class WithSelector
                {
                    [Fact]
                    public async Task Failure()
                    {
                        var input = Guid.NewGuid().CreateContainer();

                        var result = await input.ToResultOkAsync()
                                                .EnsureThatAsync(x => x.Value, FailurePredicate, () => ErrorMessage2);

                        result.ShouldBeFailureWithError(ErrorMessage2, ErrorMessage1);
                    }

                    [Fact]
                    public async Task Success()
                    {
                        var input = Guid.NewGuid().CreateContainer();

                        var result = await input.ToResultOkAsync()
                                                .EnsureThatAsync(x => x.Value, SuccessPredicate, () => ErrorMessage1);

                        result.ShouldBeSuccessWithValue(input);
                    }
                }
            }

            public class Ensure
            {
                public class WithoutSelector
                {
                    [Fact]
                    public async Task Failure()
                    {
                        var value = Guid.NewGuid();

                        Func<Guid, string, Core.Result.Result> ensure = (_, __) => Core.Result.Result.Fail(ErrorMessage1);
                        var result = await value.ToResultOkAsync()
                                                .EnsureThatAsync(nameof(value), ensure);

                        result.ShouldBeFailureWithError(ErrorMessage1);
                    }

                    [Fact]
                    public async Task Success()
                    {
                        var value = Guid.NewGuid();

                        Func<Guid, string, Core.Result.Result> ensure = (_, __) => Core.Result.Result.Ok();
                        var result = await value.ToResultOkAsync()
                                                .EnsureThatAsync(nameof(value), ensure);

                        result.ShouldBeSuccessWithValue(value);
                    }
                }

                public class WithSelector
                {
                    [Fact]
                    public async Task Failure()
                    {
                        var value = Guid.NewGuid().CreateContainer();

                        Func<Guid, string, Core.Result.Result> ensure = (_, __) => Core.Result.Result.Fail(ErrorMessage1);
                        var result = await value.ToResultOkAsync()
                                                .EnsureThatAsync(x => x.Value, nameof(value), ensure);

                        result.ShouldBeFailureWithError(ErrorMessage1);
                    }

                    [Fact]
                    public async Task Success()
                    {
                        var value = Guid.NewGuid().CreateContainer();

                        Func<Guid, string, Core.Result.Result> ensure = (_, __) => Core.Result.Result.Ok();
                        var result = await value.ToResultOkAsync()
                                                .EnsureThatAsync(x => x.Value, nameof(value), ensure);

                        result.ShouldBeSuccessWithValue(value);
                    }
                }
            }
        }
    }
}