using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Futurum.Core.Result;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultEnsureThatAllTests
{
    private const string ErrorMessage = "ERROR_MESSAGE_1";

    public static Core.Result.Result SuccessEnsureForProperty<T>(T value, string propertyName) =>
        Core.Result.Result.Ok();

    public static Core.Result.Result FailureEnsureForProperty<T>(T value, string propertyName) =>
        Core.Result.Result.Fail(ErrorMessage);

    public static Core.Result.Result SuccessEnsure(Guid value) =>
        Core.Result.Result.Ok();

    public static Core.Result.Result FailureEnsure(Guid value) =>
        Core.Result.Result.Fail(ErrorMessage);

    public static IEnumerable<Core.Result.Result> SuccessEnsures(Guid value)
    {
        yield return Core.Result.Result.Ok();
        yield return Core.Result.Result.Ok();
    }

    public static IEnumerable<Core.Result.Result> FailureEnsures(Guid value)
    {
        yield return Core.Result.Result.Fail(ErrorMessage);
        yield return Core.Result.Result.Fail(ErrorMessage);
    }

    public class Sync
    {
        public class ForProperty
        {
            public class WithoutSelector
            {
                [Fact]
                public void AllFailure()
                {
                    var value = Guid.NewGuid();

                    var result = value.ToResultOk()
                                      .EnsureThatAll(nameof(value), FailureEnsureForProperty, FailureEnsureForProperty);

                    result.ShouldBeFailureWithError($"{ErrorMessage};{ErrorMessage}");
                }

                [Fact]
                public void AllSuccess()
                {
                    var value = Guid.NewGuid();

                    var result = value.ToResultOk()
                                      .EnsureThatAll(nameof(value), SuccessEnsureForProperty, SuccessEnsureForProperty);

                    result.ShouldBeSuccessWithValue(value);
                }

                [Fact]
                public void Mixed()
                {
                    var value = Guid.NewGuid();

                    var result = value.ToResultOk()
                                      .EnsureThatAll(nameof(value), SuccessEnsureForProperty, FailureEnsureForProperty);

                    result.ShouldBeFailureWithError(ErrorMessage);
                }
            }

            public class WithSelector
            {
                [Fact]
                public void AllFailure()
                {
                    var value = Guid.NewGuid().CreateContainer();

                    var result = value.ToResultOk()
                                      .EnsureThatAll(x => x.Value, nameof(value), FailureEnsureForProperty, FailureEnsureForProperty);

                    result.ShouldBeFailureWithError($"{ErrorMessage};{ErrorMessage}");
                }

                [Fact]
                public void AllSuccess()
                {
                    var value = Guid.NewGuid().CreateContainer();

                    var result = value.ToResultOk()
                                      .EnsureThatAll(x => x.Value, nameof(value), SuccessEnsureForProperty, SuccessEnsureForProperty);

                    result.ShouldBeSuccessWithValue(value);
                }

                [Fact]
                public void Mixed()
                {
                    var value = Guid.NewGuid().CreateContainer();

                    var result = value.ToResultOk()
                                      .EnsureThatAll(x => x.Value, nameof(value), SuccessEnsureForProperty, FailureEnsureForProperty);

                    result.ShouldBeFailureWithError(ErrorMessage);
                }
            }
        }

        public class ForObject
        {
            public class NonResult
            {
                public class EnsureNOTCollection
                {
                    [Fact]
                    public void AllFailure()
                    {
                        var value = Guid.NewGuid();

                        var result = value.EnsureThatAll(FailureEnsure, FailureEnsure);

                        result.ShouldBeFailureWithError($"{ErrorMessage};{ErrorMessage}");
                    }

                    [Fact]
                    public void AllSuccess()
                    {
                        var value = Guid.NewGuid();

                        var result = value.EnsureThatAll(SuccessEnsure, SuccessEnsure);

                        result.ShouldBeSuccessWithValue(value);
                    }

                    [Fact]
                    public void Mixed()
                    {
                        var value = Guid.NewGuid();

                        var result = value.EnsureThatAll(SuccessEnsure, FailureEnsure);

                        result.ShouldBeFailureWithError(ErrorMessage);
                    }
                }

                public class EnsureCollection
                {
                    [Fact]
                    public void AllFailure()
                    {
                        var value = Guid.NewGuid();

                        var result = value.EnsureThatAll(FailureEnsures, FailureEnsures);

                        result.ShouldBeFailureWithError($"{ErrorMessage};{ErrorMessage};{ErrorMessage};{ErrorMessage}");
                    }

                    [Fact]
                    public void AllSuccess()
                    {
                        var value = Guid.NewGuid();

                        var result = value.EnsureThatAll(SuccessEnsures, SuccessEnsures);

                        result.ShouldBeSuccessWithValue(value);
                    }

                    [Fact]
                    public void Mixed()
                    {
                        var value = Guid.NewGuid();

                        var result = value.EnsureThatAll(SuccessEnsures, FailureEnsures);

                        result.ShouldBeFailureWithError($"{ErrorMessage};{ErrorMessage}");
                    }
                }
            }

            public class Result
            {
                public class EnsureNOTCollection
                {
                    [Fact]
                    public void AllFailure()
                    {
                        var value = Guid.NewGuid();

                        var result = value.ToResultOk()
                                          .EnsureThatAll(FailureEnsure, FailureEnsure);

                        result.ShouldBeFailureWithError($"{ErrorMessage};{ErrorMessage}");
                    }

                    [Fact]
                    public void AllSuccess()
                    {
                        var value = Guid.NewGuid();

                        var result = value.ToResultOk()
                                          .EnsureThatAll(SuccessEnsure, SuccessEnsure);

                        result.ShouldBeSuccessWithValue(value);
                    }

                    [Fact]
                    public void Mixed()
                    {
                        var value = Guid.NewGuid();

                        var result = value.ToResultOk()
                                          .EnsureThatAll(SuccessEnsure, FailureEnsure);

                        result.ShouldBeFailureWithError(ErrorMessage);
                    }
                }

                public class EnsureCollection
                {
                    [Fact]
                    public void AllFailure()
                    {
                        var value = Guid.NewGuid();

                        var result = value.ToResultOk()
                                          .EnsureThatAll(FailureEnsures, FailureEnsures);

                        result.ShouldBeFailureWithError($"{ErrorMessage};{ErrorMessage};{ErrorMessage};{ErrorMessage}");
                    }

                    [Fact]
                    public void AllSuccess()
                    {
                        var value = Guid.NewGuid();

                        var result = value.ToResultOk()
                                          .EnsureThatAll(SuccessEnsures, SuccessEnsures);

                        result.ShouldBeSuccessWithValue(value);
                    }

                    [Fact]
                    public void Mixed()
                    {
                        var value = Guid.NewGuid();

                        var result = value.ToResultOk()
                                          .EnsureThatAll(SuccessEnsures, FailureEnsures);

                        result.ShouldBeFailureWithError($"{ErrorMessage};{ErrorMessage}");
                    }
                }
            }
        }
    }

    public class Async
    {
        public class ForProperty
        {
            public class WithoutSelector
            {
                [Fact]
                public async Task AllFailure()
                {
                    var value = Guid.NewGuid();

                    var result = await value.ToResultOkAsync()
                                            .EnsureThatAllAsync(nameof(value), FailureEnsureForProperty, FailureEnsureForProperty);

                    result.ShouldBeFailureWithError($"{ErrorMessage};{ErrorMessage}");
                }

                [Fact]
                public async Task AllSuccess()
                {
                    var value = Guid.NewGuid();

                    var result = await value.ToResultOkAsync()
                                            .EnsureThatAllAsync(nameof(value), SuccessEnsureForProperty, SuccessEnsureForProperty);

                    result.ShouldBeSuccessWithValue(value);
                }

                [Fact]
                public async Task Mixed()
                {
                    var value = Guid.NewGuid();

                    var result = await value.ToResultOkAsync()
                                            .EnsureThatAllAsync(nameof(value), SuccessEnsureForProperty, FailureEnsureForProperty);

                    result.ShouldBeFailureWithError(ErrorMessage);
                }
            }

            public class WithSelector
            {
                [Fact]
                public async Task AllFailure()
                {
                    var value = Guid.NewGuid().CreateContainer();

                    var result = await value.ToResultOkAsync()
                                            .EnsureThatAllAsync(x => x.Value, nameof(value), FailureEnsureForProperty,
                                                                FailureEnsureForProperty);

                    result.ShouldBeFailureWithError($"{ErrorMessage};{ErrorMessage}");
                }

                [Fact]
                public async Task AllSuccess()
                {
                    var value = Guid.NewGuid().CreateContainer();

                    var result = await value.ToResultOkAsync()
                                            .EnsureThatAllAsync(x => x.Value, nameof(value), SuccessEnsureForProperty,
                                                                SuccessEnsureForProperty);

                    result.ShouldBeSuccessWithValue(value);
                }

                [Fact]
                public async Task Mixed()
                {
                    var value = Guid.NewGuid().CreateContainer();

                    var result = await value.ToResultOkAsync()
                                            .EnsureThatAllAsync(x => x.Value, nameof(value), SuccessEnsureForProperty,
                                                                FailureEnsureForProperty);

                    result.ShouldBeFailureWithError(ErrorMessage);
                }
            }
        }

        public class ForObject
        {
            public class NonResult
            {
                [Fact]
                public async Task AllFailure()
                {
                    var value = Guid.NewGuid();

                    var result = await Task.FromResult(value).EnsureThatAllAsync(FailureEnsure, FailureEnsure);

                    result.ShouldBeFailureWithError($"{ErrorMessage};{ErrorMessage}");
                }

                [Fact]
                public async Task AllSuccess()
                {
                    var value = Guid.NewGuid();

                    var result = await Task.FromResult(value).EnsureThatAllAsync(SuccessEnsure, SuccessEnsure);

                    result.ShouldBeSuccessWithValue(value);
                }

                [Fact]
                public async Task Mixed()
                {
                    var value = Guid.NewGuid();

                    var result = await Task.FromResult(value).EnsureThatAllAsync(SuccessEnsure, FailureEnsure);

                    result.ShouldBeFailureWithError(ErrorMessage);
                }
            }

            public class Result
            {
                [Fact]
                public async Task AllFailure()
                {
                    var value = Guid.NewGuid();

                    var result = await value.ToResultOkAsync()
                                            .EnsureThatAllAsync(FailureEnsure, FailureEnsure);

                    result.ShouldBeFailureWithError($"{ErrorMessage};{ErrorMessage}");
                }

                [Fact]
                public async Task AllSuccess()
                {
                    var value = Guid.NewGuid();

                    var result = await value.ToResultOkAsync()
                                            .EnsureThatAllAsync(SuccessEnsure, SuccessEnsure);

                    result.ShouldBeSuccessWithValue(value);
                }

                [Fact]
                public async Task Mixed()
                {
                    var value = Guid.NewGuid();

                    var result = await value.ToResultOkAsync()
                                            .EnsureThatAllAsync(SuccessEnsure, FailureEnsure);

                    result.ShouldBeFailureWithError(ErrorMessage);
                }
            }
        }
    }
}