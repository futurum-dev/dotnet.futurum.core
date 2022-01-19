using System;
using System.Collections.Generic;
using System.Linq;

using FluentAssertions;

using Futurum.Core.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultErrorExtensionsFlattenTests
{
    [Fact]
    public void single_non_composite()
    {
        var errorMessage = Guid.NewGuid().ToString();

        var resultErrorMessage = errorMessage.ToResultError();

        var flattenedResultError = resultErrorMessage.Flatten()
                                                     .Cast<ResultErrorMessage>();

        flattenedResultError.Count().Should().Be(1);

        flattenedResultError.Single().Should().Be(resultErrorMessage);
    }

    public class WithoutParent
    {
        [Fact]
        public void composite_one_level_deep()
        {
            var resultErrorMessages = Enumerable.Range(0, 10)
                                                .Select(x => x.ToString().ToResultError())
                                                .Cast<ResultErrorMessage>();

            var resultErrorComposite = resultErrorMessages.ToResultError();

            var flattenedResultError = resultErrorComposite.Flatten()
                                                           .Cast<ResultErrorMessage>();

            flattenedResultError.Should().BeEquivalentTo(resultErrorMessages);
        }

        [Fact]
        public void composite_two_level_deep()
        {
            static IEnumerable<ResultErrorMessage> CreateResultErrorMessages(int start) =>
                Enumerable.Range(start * 10, 10)
                          .Select(x => x.ToString().ToResultError() as ResultErrorMessage);

            var resultErrorCompositeMessages = Enumerable.Range(0, 10)
                                                         .Select(CreateResultErrorMessages);
            var resultErrorComposites = resultErrorCompositeMessages
                                        .Select(ResultErrorCompositeExtensions.ToResultError)
                                        .Cast<ResultErrorComposite>();

            var resultErrorComposite = resultErrorComposites.ToResultError();

            var flattenedResultError = resultErrorComposite.Flatten()
                                                           .Cast<ResultErrorMessage>();

            var resultErrorChildren = resultErrorCompositeMessages.SelectMany(x => x);

            flattenedResultError.Should().BeEquivalentTo(resultErrorChildren);
        }

        [Fact]
        public void composite_three_level_deep()
        {
            static IEnumerable<ResultErrorMessage> CreateResultErrorMessages(int start) =>
                Enumerable.Range(start * 10, 10)
                          .Select(x => x.ToString().ToResultError() as ResultErrorMessage);

            var resultErrorCompositeMessages = Enumerable.Range(0, 10)
                                                         .Select(CreateResultErrorMessages);
            var resultErrorComposites = resultErrorCompositeMessages
                                        .Select(ResultErrorCompositeExtensions.ToResultError)
                                        .Cast<ResultErrorComposite>();

            var resultErrorComposite = resultErrorComposites.ToResultError();

            var flattenedResultError = resultErrorComposite.Flatten()
                                                           .Cast<ResultErrorMessage>();

            var resultErrorChildren = resultErrorCompositeMessages.SelectMany(x => x);

            flattenedResultError.Should().BeEquivalentTo(resultErrorChildren);
        }

        [Fact]
        public void mix()
        {
            static IEnumerable<ResultErrorMessage> CreateResultErrorMessages(int start) =>
                Enumerable.Range(start * 10, 10)
                          .Select(x => x.ToString().ToResultError() as ResultErrorMessage);

            var resultErrorMessages = CreateResultErrorMessages(0);

            var resultErrorCompositeMessages = Enumerable.Range(100, 10)
                                                         .Select(CreateResultErrorMessages);
            var resultErrorComposites = resultErrorCompositeMessages.Select(ResultErrorCompositeExtensions.ToResultError);

            var resultErrors = new List<IResultError>();
            resultErrors.AddRange(resultErrorMessages);
            resultErrors.Add(resultErrorComposites.ToResultError());

            var resultErrorComposite = resultErrors.ToResultError();

            var flattenedResultError = resultErrorComposite.Flatten()
                                                           .Cast<ResultErrorMessage>();

            var resultErrorChildren = resultErrorCompositeMessages.SelectMany(x => x)
                                                                  .Concat(resultErrorMessages);

            flattenedResultError.Should().BeEquivalentTo(resultErrorChildren);
        }
    }

    public class WithParent
    {
        [Fact]
        public void composite_one_level_deep()
        {
            var parentErrorMessage = Guid.NewGuid().ToString().ToResultError() as ResultErrorMessage;
            
            var resultErrorMessages = Enumerable.Range(0, 10)
                                                .Select(x => x.ToString().ToResultError())
                                                .Cast<ResultErrorMessage>()
                                                .ToList();

            var resultErrorComposite = ResultErrorCompositeExtensions.ToResultError(parentErrorMessage, resultErrorMessages);

            var flattenedResultError = resultErrorComposite.Flatten()
                                                           .Cast<ResultErrorMessage>();

            flattenedResultError.Should().BeEquivalentTo(resultErrorMessages.Prepend(parentErrorMessage));
        }

        [Fact]
        public void composite_two_level_deep()
        {
            var parentErrorMessage = Guid.NewGuid().ToString().ToResultError() as ResultErrorMessage;

            static IEnumerable<ResultErrorMessage> CreateResultErrorMessages(int start) =>
                Enumerable.Range(start * 10, 10)
                          .Select(x => x.ToString().ToResultError() as ResultErrorMessage);

            var resultErrorCompositeMessages = Enumerable.Range(0, 10)
                                                         .Select(CreateResultErrorMessages);
            var resultErrorComposites = resultErrorCompositeMessages
                                        .Select(ResultErrorCompositeExtensions.ToResultError)
                                        .Cast<ResultErrorComposite>();

            var resultErrorComposite = ResultErrorCompositeExtensions.ToResultError(parentErrorMessage, resultErrorComposites);

            var flattenedResultError = resultErrorComposite.Flatten()
                                                           .Cast<ResultErrorMessage>();

            var resultErrorChildren = resultErrorCompositeMessages.SelectMany(x => x);

            flattenedResultError.Should().BeEquivalentTo(resultErrorChildren.Prepend(parentErrorMessage));
        }

        [Fact]
        public void composite_three_level_deep()
        {
            var parentErrorMessage = Guid.NewGuid().ToString().ToResultError() as ResultErrorMessage;

            static IEnumerable<ResultErrorMessage> CreateResultErrorMessages(int start) =>
                Enumerable.Range(start * 10, 10)
                          .Select(x => x.ToString().ToResultError() as ResultErrorMessage);

            var resultErrorCompositeMessages = Enumerable.Range(0, 10)
                                                         .Select(CreateResultErrorMessages);
            var resultErrorComposites = resultErrorCompositeMessages
                                        .Select(ResultErrorCompositeExtensions.ToResultError)
                                        .Cast<ResultErrorComposite>();

            var resultErrorComposite = ResultErrorCompositeExtensions.ToResultError(parentErrorMessage, resultErrorComposites);

            var flattenedResultError = resultErrorComposite.Flatten()
                                                           .Cast<ResultErrorMessage>();

            var resultErrorChildren = resultErrorCompositeMessages.SelectMany(x => x);

            flattenedResultError.Should().BeEquivalentTo(resultErrorChildren.Prepend(parentErrorMessage));
        }

        [Fact]
        public void mix()
        {
            var parentErrorMessage = Guid.NewGuid().ToString().ToResultError() as ResultErrorMessage;

            static IEnumerable<ResultErrorMessage> CreateResultErrorMessages(int start) =>
                Enumerable.Range(start * 10, 10)
                          .Select(x => x.ToString().ToResultError() as ResultErrorMessage);

            var resultErrorMessages = CreateResultErrorMessages(0);

            var resultErrorCompositeMessages = Enumerable.Range(100, 10)
                                                         .Select(CreateResultErrorMessages);
            var resultErrorComposites = resultErrorCompositeMessages.Select(ResultErrorCompositeExtensions.ToResultError);

            var resultErrors = new List<IResultError>();
            resultErrors.AddRange(resultErrorMessages);
            resultErrors.Add(resultErrorComposites.ToResultError());

            var resultErrorComposite = ResultErrorCompositeExtensions.ToResultError(parentErrorMessage, resultErrors);

            var flattenedResultError = resultErrorComposite.Flatten()
                                                           .Cast<ResultErrorMessage>();

            var resultErrorChildren = resultErrorCompositeMessages.SelectMany(x => x)
                                                                  .Concat(resultErrorMessages);

            flattenedResultError.Should().BeEquivalentTo(resultErrorChildren.Prepend(parentErrorMessage));
        }
    }
}