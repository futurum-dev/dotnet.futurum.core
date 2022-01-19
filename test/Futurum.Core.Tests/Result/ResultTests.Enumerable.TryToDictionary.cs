using System.Collections.Generic;
using System.Linq;

using Futurum.Core.Linq;
using Futurum.Core.Result;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultEnumerableExtensionsTryToDictionaryTests
{
    public class IEnumerableToDictionary
    {
        [Fact]
        public void success()
        {
            var numbers = Enumerable.Range(0, 10)
                                    .ToList();

            var result = numbers.TryToDictionary(x => x, x => x);

            result.ShouldBeSuccess();
            result.ShouldBeSuccessWithValueEquivalentTo(x => x.Keys.Select(n => n), numbers);
            result.ShouldBeSuccessWithValueEquivalentTo(x => x.Values.Select(n => n), numbers);
        }

        [Fact]
        public void failure()
        {
            var numbers = Enumerable.Range(0, 10)
                                    .Concat(Enumerable.Range(0, 10))
                                    .ToList();

            var result = numbers.TryToDictionary(x => x, x => x);

            result.ShouldBeFailureWithError(
                $"{nameof(ResultEnumerableExtensions.TryToDictionary)} failed as there are duplicate keys. Duplicate keys are : '{Enumerable.Range(0, 10).Select(x => $"'{x}'").StringJoin(",")}'");
        }
    }

    public class ResultIEnumerableToDictionary
    {
        [Fact]
        public void success()
        {
            var numbers = Enumerable.Range(0, 10)
                                    .ToList();

            var result = numbers.AsEnumerable()
                                .ToResultOk()
                                .TryToDictionary(x => x, x => x);

            result.ShouldBeSuccess();
            result.ShouldBeSuccessWithValueEquivalentTo(x => x.Keys.Select(n => n), numbers);
            result.ShouldBeSuccessWithValueEquivalentTo(x => x.Values.Select(n => n), numbers);
        }

        [Fact]
        public void failure()
        {
            var numbers = Enumerable.Range(0, 10)
                                    .Concat(Enumerable.Range(0, 10))
                                    .ToList();

            var result = numbers.AsEnumerable()
                                .ToResultOk()
                                .TryToDictionary(x => x, x => x);

            result.ShouldBeFailureWithError(
                $"{nameof(ResultEnumerableExtensions.TryToDictionary)} failed as there are duplicate keys. Duplicate keys are : '{Enumerable.Range(0, 10).Select(x => $"'{x}'").StringJoin(",")}'");
        }
    }

    public class ResultIEnumerableDictionaryToDictionary
    {
        [Fact]
        public void success()
        {
            var numbers = Enumerable.Range(1, 10)
                                    .Select(i => new Dictionary<int, int> { { i, i } })
                                    .ToList();

            var result = numbers.AsEnumerable()
                                .ToResultOk()
                                .TryToDictionary(x => x.Key, x => x.Value);

            result.ShouldBeSuccess();
            result.ShouldBeSuccessWithValueEquivalentTo(x => x.Keys.Select(n => n), numbers.SelectMany(x => x.Keys));
            result.ShouldBeSuccessWithValueEquivalentTo(x => x.Values.Select(n => n), numbers.SelectMany(x => x.Values));
        }

        [Fact]
        public void failure()
        {
            var numbers = Enumerable.Range(1, 10)
                                    .Select(i => new Dictionary<int, int> { { 1, 1 } })
                                    .ToList();

            var result = numbers.AsEnumerable()
                                .ToResultOk()
                                .TryToDictionary(x => x.Key, x => x.Value);

            result.ShouldBeFailureWithError(
                $"{nameof(ResultEnumerableExtensions.TryToDictionary)} failed as there are duplicate keys. Duplicate keys are : ''1''");
        }
    }

    public class ResultIEnumerableReadOnlyDictionaryToDictionary
    {
        [Fact]
        public void success()
        {
            var numbers = Enumerable.Range(1, 10)
                                    .Select(i => new Dictionary<int, int> { { i, i } })
                                    .Select(x => x as IReadOnlyDictionary<int, int>)
                                    .ToList();

            var result = numbers.AsEnumerable()
                                .ToResultOk()
                                .TryToDictionary(x => x.Key, x => x.Value);

            result.ShouldBeSuccess();
            result.ShouldBeSuccessWithValueEquivalentTo(x => x.Keys.Select(n => n), numbers.SelectMany(x => x.Keys));
            result.ShouldBeSuccessWithValueEquivalentTo(x => x.Values.Select(n => n), numbers.SelectMany(x => x.Values));
        }

        [Fact]
        public void failure()
        {
            var numbers = Enumerable.Range(1, 10)
                                    .Select(i => new Dictionary<int, int> { { 1, 1 } })
                                    .Select(x => x as IReadOnlyDictionary<int, int>)
                                    .ToList();

            var result = numbers.AsEnumerable()
                                .ToResultOk()
                                .TryToDictionary(x => x.Key, x => x.Value);

            result.ShouldBeFailureWithError($"{nameof(ResultEnumerableExtensions.TryToDictionary)} failed as there are duplicate keys. Duplicate keys are : ''1''");
        }
    }
}