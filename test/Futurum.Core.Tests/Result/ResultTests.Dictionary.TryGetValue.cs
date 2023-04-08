using System.Collections.ObjectModel;
using System.Linq;

using Futurum.Core.Result;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultDictionaryExtensionsTryGetValueTests
{
    private const string ErrorMessage = "ERROR_MESSAGE";

    public class Dictionary
    {
        public class AsResult
        {
            [Fact]
            public void HasNoValue()
            {
                var values = Enumerable.Range(0, 10)
                                       .ToDictionary(x => x, x => x);

                var result = values.TryGetValue(100, ErrorMessage);

                result.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public void HasValue()
            {
                var values = Enumerable.Range(0, 10)
                                       .ToDictionary(x => x, x => x);

                var result = values.TryGetValue(2, ErrorMessage);

                result.ShouldBeSuccessWithValue(2);
            }
        }
        
        public class FuncAsResult
        {
            [Fact]
            public void HasNoValue()
            {
                var values = Enumerable.Range(0, 10)
                                       .ToDictionary(x => x, x => x);

                var result = values.TryGetValue(100, () => ErrorMessage);

                result.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public void HasValue()
            {
                var values = Enumerable.Range(0, 10)
                                       .ToDictionary(x => x, x => x);

                var result = values.TryGetValue(2, () => ErrorMessage);

                result.ShouldBeSuccessWithValue(2);
            }
        }
    }

    public class IReadOnlyDictionary
    {
        public class AsResult
        {
            [Fact]
            public void HasNoValue()
            {
                var values = new ReadOnlyDictionary<int, int>(Enumerable.Range(0, 10)
                                                                        .ToDictionary(x => x, x => x));

                var result = values.TryGetValue(100, ErrorMessage);

                result.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public void HasValue()
            {
                var values = new ReadOnlyDictionary<int, int>(Enumerable.Range(0, 10)
                                                                        .ToDictionary(x => x, x => x));

                var result = values.TryGetValue(2, ErrorMessage);

                result.ShouldBeSuccessWithValue(2);
            }
        }

        public class FuncAsResult
        {
            [Fact]
            public void HasNoValue()
            {
                var values = new ReadOnlyDictionary<int, int>(Enumerable.Range(0, 10)
                                                                        .ToDictionary(x => x, x => x));

                var result = values.TryGetValue(100, () => ErrorMessage);

                result.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public void HasValue()
            {
                var values = new ReadOnlyDictionary<int, int>(Enumerable.Range(0, 10)
                                                                        .ToDictionary(x => x, x => x));

                var result = values.TryGetValue(2, () => ErrorMessage);

                result.ShouldBeSuccessWithValue(2);
            }
        }
    }
}